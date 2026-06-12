using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Eft.Hideout;
using SPTarkov.Server.Core.Helpers;
using SptScavRecipe = SPTarkov.Server.Core.Models.Eft.Hideout.ScavRecipe;
using AssetScavCase = Softcore.Assets.ScavCase;
using SPTarkov.Server.Core.Models.Enums;

namespace Softcore.Changers;

[Injectable]
public class ScavCaseOptionsChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer,
    HandbookHelper handbookHelper,
    ItemFilterService itemFilterService,
    SeasonalEventService seasonalEventService)
{
    private readonly ScavCaseConfig _scavCaseConfig = configServer.GetConfig<ScavCaseConfig>();

    public void Apply(ScavCaseOptions config)
    {
        if (!config.Enabled) return;

        if (config.BetterRewards)
        {
            try
            {
                DoBetterRewards();
            }
            catch (Exception e)
            {
                log.Warning($"ScavCaseOptions: DoBetterRewards failed. {e.Message}");
            }
        }

        if (config.Rebalance)
        {
            try
            {
                DoRebalance();
            }
            catch (Exception e)
            {
                log.Warning($"ScavCaseOptions: DoRebalance failed. {e.Message}");
            }
        }

        if (config.FasterScavcase.Enabled)
        {
            try
            {
                DoFasterScavcase(config.FasterScavcase.SpeedMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"ScavCaseOptions: DoFasterScavcase failed. {e.Message}");
            }
        }
    }

    private void DoBetterRewards()
    {
        // Assets expose MongoId caches so this stays close to the TS blacklist/whitelist data
        // without rebuilding the same conversions on every Apply.
        _scavCaseConfig.RewardItemParentBlacklist = AssetScavCase.RewardParentBlacklistMongoIds;

        var items = databaseService.GetTemplates().Items;
        var traders = databaseService.GetTraders();
        var buyableItems = new HashSet<MongoId>();

        foreach (var (traderId, trader) in traders)
        {
            if (traderId == Traders.LIGHTHOUSEKEEPER) continue;
            if (trader.Assort?.Items == null) continue;
            foreach (var assortItem in trader.Assort.Items)
            {
                var tpl = assortItem.Template;
                if (items.TryGetValue(tpl, out var tplItem) &&
                    tplItem.Parent != BaseClasses.BUILT_IN_INSERTS)
                    buyableItems.Add(tpl);
            }
        }

        int kept = 0, filtered = 0;
        foreach (var (id, item) in items)
        {
            if (item.Type != "Item") continue;

            var handbookPrice = handbookHelper.GetTemplatePrice(id);

            if (item.Parent == BaseClasses.AMMO_BOX)
            {
                try
                {
                    // TS patches ammo box handbook values to ammo price * contained stack size
                    // before reward filtering, otherwise ammo boxes are priced far too low.
                    var firstSlot = item.Properties?.StackSlots?.FirstOrDefault();
                    var count = (double) (firstSlot?.MaxCount ?? 0);
                    var ammoId = firstSlot?.Properties?.Filters
                        ?.FirstOrDefault()?.Filter?.FirstOrDefault();
                    if (ammoId.HasValue)
                    {
                        var ammoPrice = handbookHelper.GetTemplatePrice(ammoId.Value);
                        handbookPrice = ammoPrice * count;
                        var hbEntry = databaseService.GetTemplates().Handbook.Items
                            .FirstOrDefault(e => e.Id == id);
                        if (hbEntry != null) hbEntry.Price = handbookPrice;
                    }
                }
                catch { /* ignore ammo box patch failures */ }
            }

            if (ScavCaseItemFilter(id, item) &&
                (!buyableItems.Contains(id) || handbookPrice >= 10000 ||
                 AssetScavCase.WhitelistMongoIds.Contains(item.Parent)))
            {
                kept++;
            }
            else
            {
                _scavCaseConfig.RewardItemBlacklist.Add(id);
                filtered++;
            }
        }

        log.DebugLog($"ScavCaseBetterRewards: items kept in pool: {kept}");
        log.DebugLog($"ScavCaseBetterRewards: items filtered out (blacklisted): {filtered}");
    }

    private bool ScavCaseItemFilter(
        MongoId id,
        SPTarkov.Server.Core.Models.Eft.Common.Tables.TemplateItem item)
    {
        if (item.Parent == default) return false;
        if (item.Type == "Node") return false;
        if (item.Properties?.QuestItem == true) return false;
        // This mirrors the TS item-level blacklist, but uses cached MongoIds instead of
        // comparing string ids on every check.
        if (AssetScavCase.ItemBlacklistMongoIds.Contains(id)) return false;
        if (handbookHelper.GetTemplatePrice(id) < 2) return false;
        if (itemFilterService.IsItemBlacklisted(id)) return false;
        if (itemFilterService.IsBossItem(id)) return false;
        if (itemFilterService.IsItemRewardBlacklisted(id)) return false;
        if (seasonalEventService.ItemIsSeasonalRelated(id)) return false;
        return true;
    }

    private void DoFasterScavcase(double multiplier)
    {
        var hideout = databaseService.GetHideout();
        var recipes = hideout.Production.ScavRecipes;
        foreach (var recipe in recipes)
            recipe.ProductionTime = Math.Round((recipe.ProductionTime ?? 0) / multiplier);
        log.DebugLog($"FasterScavcase: recipes sped up (x{multiplier}): {recipes.Count}");
    }

    private void DoRebalance()
    {
        _scavCaseConfig.RewardItemValueRangeRub = new()
        {
            ["common"] = new() { Min = AssetScavCase.CommonRange.Min, Max = AssetScavCase.CommonRange.Max },
            ["rare"] = new() { Min = AssetScavCase.RareRange.Min, Max = AssetScavCase.RareRange.Max },
            ["superrare"] = new() { Min = AssetScavCase.SuperrareRange.Min, Max = AssetScavCase.SuperrareRange.Max },
        };

        var hideout = databaseService.GetHideout();
        var reworked = AssetScavCase.ReworkedRecipes.Select(r => new SptScavRecipe
        {
            Id = r.Id,
            Requirements = r.Requirements
                .Select(req => new Requirement
                {
                    TemplateId = req.TemplateId,
                    Count = req.Count,
                    Type = req.Type,
                }).ToList(),
            ProductionTime = r.ProductionTime,
            EndProducts = new()
            {
                Common = new() { Min = r.EndProducts.Common.Min, Max = r.EndProducts.Common.Max },
                Rare = new() { Min = r.EndProducts.Rare.Min, Max = r.EndProducts.Rare.Max },
                Superrare = new() { Min = r.EndProducts.Superrare.Min, Max = r.EndProducts.Superrare.Max },
            }
        }).ToList();
        hideout.Production.ScavRecipes = reworked;
    }
}
