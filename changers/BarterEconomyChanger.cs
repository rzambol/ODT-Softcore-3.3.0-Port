using System;
using System.Linq;
using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Helpers;
using SptConfigServer = SPTarkov.Server.Core.Servers.ConfigServer;
using Assets = Softcore.Assets;
using SPTarkov.Server.Core.Models.Common;

namespace Softcore.Changers;

[Injectable]
public class BarterEconomyChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    SptConfigServer configServer,
    ItemHelper itemHelper)
{
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();

    public void Apply(BarterEconomy config)
    {
        if (!config.Enabled) return;

        try
        {
            DoBarterEconomy();
        }
        catch (Exception e)
        {
            log.Warning($"BarterEconomy: DoBarterEconomy failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            AdjustCashOffers(config.CashOffersPercentage);
        }
        catch (Exception e)
        {
            log.Warning($"BarterEconomy: AdjustCashOffers failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            AdjustBarterPriceVariance(config.BarterPriceVariance);
        }
        catch (Exception e)
        {
            log.Warning($"BarterEconomy: AdjustBarterPriceVariance failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            AdjustItemCountMax(config.ItemCountMax);
        }
        catch (Exception e)
        {
            log.Warning($"BarterEconomy: AdjustItemCountMax failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            AdjustOfferItemCount(config.OfferItemCount);
        }
        catch (Exception e)
        {
            log.Warning($"BarterEconomy: AdjustOfferItemCount failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            AdjustNonStackableAmount(config.NonStackableCount);
        }
        catch (Exception e)
        {
            log.Warning($"BarterEconomy: AdjustNonStackableAmount failed gracefully. Send bug report. Continue safely.\n{e}");
        }
    }

    private void DoBarterEconomy()
    {
        var barterBlacklist = Assets.FleaMarket.ActualBaseClassesMongoIds
            .Where(bc => !Assets.FleaMarket.FleaBarterRequestWhitelistMongoIds.Contains(bc))
            .ToHashSet();

        _ragfairConfig.Dynamic.Barter.ItemTypeBlacklist = barterBlacklist;
        _ragfairConfig.Dynamic.Barter.MinRoubleCostToBecomeBarter = 100;
        log.DebugLog($"BarterEconomy: barter blacklist base classes: {barterBlacklist.Count}");

        var templates = databaseService.GetTemplates();
        var items = templates.Items;
        var prices = templates.Prices;
        var moneyBaseClass = BaseClasses.MONEY;
        int zeroed = 0, whitelisted = 0;

        foreach (var (id, item) in items)
        {
            if (item.Type != "Item") continue;
            if (itemHelper.IsOfBaseclasses(item.Id, barterBlacklist)) continue;
            if (item.Parent == moneyBaseClass) continue;

            if (item.Properties?.QuestItem == true)
            {
                prices[id] = 0; zeroed++;
            }
            else if (item.Properties?.CanSellOnRagfair == false)
            {
                prices[id] = 0; zeroed++;
            }
            else
            {
                if (Assets.FleaMarket.BsgBlacklistMongoIds.Contains(id) && item.Properties?.CanSellOnRagfair == true)
                    log.Warning($"BarterEconomy: Item {id} can be bought on flea, don't use BSG blacklist unlockers with Barter Economy enabled!");
            }
        }

        foreach (var (itemId, price) in Assets.FleaMarket.RequestWhitelistMongoIds)
        {
            prices[itemId] = price;
            whitelisted++;
        }

        log.DebugLog($"BarterEconomy: item prices zeroed (quest/non-flea): {zeroed}");
        log.DebugLog($"BarterEconomy: request whitelist price overrides applied: {whitelisted}");
    }

    private void AdjustCashOffers(int cashOffersPercentage)
    {
        _ragfairConfig.Dynamic.Barter.ChancePercent = 100 - cashOffersPercentage;
    }

    private void AdjustBarterPriceVariance(int barterPriceVariance)
    {
        _ragfairConfig.Dynamic.Barter.PriceRangeVariancePercent = barterPriceVariance;
    }

    private void AdjustItemCountMax(int itemCountMax)
    {
        _ragfairConfig.Dynamic.Barter.ItemCountMax = itemCountMax;
    }

    private void AdjustOfferItemCount(Softcore.MinMaxInt minMax)
    {
        // TS assigns a plain object here; in SPT4 C# the closest equivalent is replacing
        // the whole dictionary with a single "default" range entry.
        _ragfairConfig.Dynamic.OfferItemCount = new Dictionary<string, MinMax<int>>
        {
            ["default"] = new MinMax<int>
            {
                Min = minMax.Min,
                Max = minMax.Max
            }
        };
    }

    private void AdjustNonStackableAmount(Softcore.MinMaxInt minMax)
    {
        _ragfairConfig.Dynamic.NonStackableCount = new MinMax<int>
        {
            Min = minMax.Min,
            Max = minMax.Max
        };
    }

}

