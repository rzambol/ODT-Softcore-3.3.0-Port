using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using System.Globalization;

namespace Softcore.Changers;

[Injectable]
public class FasterBitcoinFarmingChanger(
    PrefixLogger log,
    DatabaseService databaseService)
{
    public void Apply(FasterBitcoinFarming config)
    {
        if (!config.Enabled) return;

        try
        {
            var hideout = databaseService.GetHideout();
            var bitcoinRecipes = hideout.Production.Recipes
                .Where(p => p.EndProduct == ItemTpl.BARTER_PHYSICAL_BITCOIN).ToList();
            var multiplier = config.BaseBitcoinTimeMultiplier;

            if (multiplier <= 0)
            {
                log.Warning("FasterBitcoinFarming: invalid multiplier, skipping.");
                return;
            }

            foreach (var recipe in bitcoinRecipes)
            {
                recipe.ProductionTime = Math.Round((recipe.ProductionTime ?? 0) / multiplier, 0);
            }
            hideout.Settings.GpuBoostRate = config.GpuEfficiency;
            log.DebugLog($"FasterBitcoinFarming: recipes speed up (x{config.BaseBitcoinTimeMultiplier}): {bitcoinRecipes.Count}");
            log.DebugLog("FasterBitcoinFarming: GpuBoostRate set to: "
                + string.Format(CultureInfo.InvariantCulture, "{0:0.###}", config.GpuEfficiency));
        }
        catch (Exception e)
        {
            log.Warning($"FasterBitcoinFarming: DoFasterBitcoinFarming failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        if (config.SetBitcoinPriceTo100k)
        {
            try
            {
                var handbook = databaseService.GetTemplates().Handbook;
                var entry = handbook.Items.FirstOrDefault(i => i.Id == ItemTpl.BARTER_PHYSICAL_BITCOIN);
                if (entry != null) entry.Price = 100000;
            }
            catch (Exception e)
            {
                log.Warning($"FasterBitcoinFarming: SetBitcoinPriceTo100k failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }
    }
}
