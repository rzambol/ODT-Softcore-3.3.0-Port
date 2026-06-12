using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using Softcore.Assets;
using SPTarkov.Server.Core.Models.Enums.Hideout;

namespace Softcore.Changers;

[Injectable]
public class CraftingChangesChanger(
    PrefixLogger log,
    DatabaseService databaseService)
{
    public void Apply(CraftingChanges config)
    {
        if (!config.Enabled) return;

        try
        {
            if (config.CraftingRebalance)
                DoCraftingRebalance();
        }
        catch (Exception e)
        {
            log.Warning($"CraftingChanges: DoCraftingRebalance failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.AdditionalCraftingRecipes)
                DoAdditionalCraftingRecipes();
        }
        catch (Exception e)
        {
            log.Warning($"CraftingChanges: DoAdditionalCraftingRecipes failed gracefully. Send bug report. Continue safely.\n{e}");
        }
    }

    private void DoCraftingRebalance()
    {
        var hideout = databaseService.GetHideout();
        int adjusted = 0, missing = 0;

        foreach (var adjustment in ProductionAdjustments.CraftingAdjustments)
        {
            var craft = hideout.Production.Recipes
                .FirstOrDefault(r =>
                    r.EndProduct == adjustment.Id &&
                    r.AreaType != HideoutAreas.ChristmasIllumination &&
                    (adjustment.Match == null || adjustment.Match(r)));

            if (craft == null)
            {
                log.Warning($"CraftingRebalance: recipe for {adjustment.Id} not found, skipping.");
                missing++;
                continue;
            }

            try { adjustment.Adjust(craft); adjusted++; }
            catch (Exception e)
            {
                log.Warning($"CraftingRebalance: adjust for {adjustment.Id} failed. {e.Message}");
            }
        }

        log.DebugLog($"CraftingRebalance: recipes adjusted: {adjusted}");

        if (missing > 0)
            log.DebugLog($"CraftingRebalance: recipes not found (skipped): {missing}");
    }

    private void DoAdditionalCraftingRecipes()
    {
        var hideout = databaseService.GetHideout();
        var added = Recipes.AdditionalRecipes;
        hideout.Production.Recipes.AddRange(added);
        log.DebugLog($"AdditionalCraftingRecipes: recipes added: {added.Count}");
    }

}
