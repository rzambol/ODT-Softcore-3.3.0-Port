using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Softcore.Changers;

[Injectable]
public class FasterCraftingTimeChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly HideoutConfig _hideoutConfig = configServer.GetConfig<HideoutConfig>();

    private static readonly Dictionary<string, string> ProductNames = new()
    {
        [ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE] = "Fierce Hatchling Moonshine",
        [ItemTpl.DRINK_CANISTER_WITH_PURIFIED_WATER] = "Purified Water",
    };

    private static readonly HashSet<string> ExcludedFromGlobal = new()
    {
        ItemTpl.BARTER_PHYSICAL_BITCOIN,
        ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE,
        ItemTpl.DRINK_CANISTER_WITH_PURIFIED_WATER
    };

    public void Apply(FasterCraftingTime config)
    {
        if (!config.Enabled) return;

        try
        {
            DoFasterProductionForAll(config.BaseCraftingTimeMultiplier);
        }
        catch (Exception e)
        {
            log.Warning($"FasterCraftingTime: DoFasterProductionForAll failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        if (config.HideoutSkillExpFix.Enabled)
        {
            try
            {
                // TS uses a generic number here. SPT4 exposes an int, so we round once at
                // assignment time instead of keeping a fractional value in config.
                _hideoutConfig.HoursForSkillCrafting =
                    (int) Math.Round(_hideoutConfig.HoursForSkillCrafting / config.HideoutSkillExpFix.HideoutSkillExpMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"FasterCraftingTime: DoHideoutSkillExpFix failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.FasterMoonshineProduction.Enabled)
        {
            try
            {
                DoFasterProductionFor(ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE, config.FasterMoonshineProduction.BaseCraftingTimeMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"FasterCraftingTime: FasterMoonshine failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.FasterPurifiedWaterProduction.Enabled)
        {
            try
            {
                DoFasterProductionFor(ItemTpl.DRINK_CANISTER_WITH_PURIFIED_WATER, config.FasterPurifiedWaterProduction.BaseCraftingTimeMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"FasterCraftingTime: FasterPurifiedWater failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.FasterCultistCircle.Enabled)
        {
            try
            {
                DoFasterCultistCircle(config.FasterCultistCircle.BaseCraftingTimeMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"FasterCraftingTime: FasterCultistCircle failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }
    }

    private void DoFasterProductionForAll(double multiplier)
    {
        var hideout = databaseService.GetHideout();
        var recipes = hideout.Production.Recipes
            // Bitcoin, moonshine and purified water are handled by their dedicated options,
            // just like in TS, so the global speedup skips them here.
            .Where(r => !ExcludedFromGlobal.Contains(r.EndProduct))
            .ToList();

        foreach (var recipe in recipes)
        {
            double prodTime = recipe.ProductionTime ?? 0;
            recipe.ProductionTime = (int) Math.Ceiling(prodTime / multiplier);
        }

        log.DebugLog($"FasterCraftingTime: recipes speed up (x{multiplier}): {recipes.Count}");
    }

    private void DoFasterProductionFor(string tpl, double multiplier)
    {
        var hideout = databaseService.GetHideout();
        var items = databaseService.GetTemplates().Items;
        var recipes = hideout.Production.Recipes.Where(r => r.EndProduct == tpl).ToList();
        foreach (var recipe in recipes)
        {
            double prodTime = recipe.ProductionTime ?? 0;
            recipe.ProductionTime = (int) Math.Ceiling(prodTime / multiplier);
        }

        var productName = ProductNames.TryGetValue(tpl, out var explicitName)
            ? explicitName
            : items.TryGetValue(tpl, out var item)
                ? item.Name.ToString()
                : tpl;

        log.DebugLog($"FasterCraftingTime: productions for {productName} speed up (x{multiplier}): {recipes.Count}");
    }

    private void DoFasterCultistCircle(double multiplier)
    {
        _hideoutConfig.CultistCircle.HideoutTaskRewardTimeSeconds =
            (int) Math.Ceiling(_hideoutConfig.CultistCircle.HideoutTaskRewardTimeSeconds / multiplier);

        foreach (var craft in _hideoutConfig.CultistCircle.CraftTimeThresholds)
        {
            craft.CraftTimeSeconds = (int) Math.Ceiling(craft.CraftTimeSeconds / multiplier);
        }

        foreach (var reward in _hideoutConfig.CultistCircle.DirectRewards)
        {
            reward.CraftTimeSeconds = (int) Math.Ceiling(reward.CraftTimeSeconds / multiplier);
        }

        log.DebugLog($"FasterCraftingTime: cultist circle speed up (x{multiplier})");
    }
}
