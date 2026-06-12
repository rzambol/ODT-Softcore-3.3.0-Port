using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;

namespace Softcore.Changers;

[Injectable]
public class EconomyOptionsChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    PriceRebalanceChanger priceRebalanceChanger,
    PacifistFleaMarketChanger pacifistFleaChanger,
    BarterEconomyChanger barterChanger,
    OtherFleaMarketChangesChanger otherFleaChanger)
{
    public void Apply(EconomyOptions config)
    {
        if (!config.Enabled) return;

        if (config.DisableFleaMarketCompletely)
        {
            try
            {
                UpdateRagfairMinLevel(99);
            }
            catch (Exception e)
            {
                log.Warning($"EconomyOptions: DisableFleaMarket failed gracefully. Send bug report. Continue safely.\n{e}");
            }
            return;
        }

        try
        {
            if (config.PriceRebalance.Enabled)
                priceRebalanceChanger.Apply(config.PriceRebalance);
        }
        catch (Exception e)
        {
            log.Warning($"EconomyOptions: PriceRebalance failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.PacifistFleaMarket.Enabled)
                pacifistFleaChanger.Apply(config.PacifistFleaMarket);
        }
        catch (Exception e)
        {
            log.Warning($"EconomyOptions: PacifistFleaMarket failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.BarterEconomy.Enabled)
                barterChanger.Apply(config.BarterEconomy);
        }
        catch (Exception e)
        {
            log.Warning($"EconomyOptions: BarterEconomy failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.OtherFleaMarketChanges.Enabled)
            {
                otherFleaChanger.Apply(config.OtherFleaMarketChanges);
                UpdateRagfairMinLevel(config.OtherFleaMarketChanges.FleaMarketOpenAtLevel);
            }
        }
        catch (Exception e)
        {
            log.Warning($"EconomyOptions: OtherFleaMarketChanges failed gracefully. Send bug report. Continue safely.\n{e}");
        }
    }

    private void UpdateRagfairMinLevel(int level)
    {
        var globals = databaseService.GetGlobals();
        globals.Configuration.RagFair.MinUserLevel = level;
        log.DebugLog($"EconomyOptions: RagFair.MinUserLevel set to: {level}");
    }

}
