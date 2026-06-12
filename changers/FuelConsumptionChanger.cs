using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using System.Globalization;

namespace Softcore.Changers;

[Injectable]
public class FuelConsumptionChanger(
    PrefixLogger log,
    DatabaseService databaseService)
{
    public void Apply(FuelConsumption config)
    {
        if (!config.Enabled) return;
        try
        {
            var hideout = databaseService.GetHideout();
            var before = hideout.Settings.GeneratorFuelFlowRate;
            hideout.Settings.GeneratorFuelFlowRate *= config.FuelConsumptionMultiplier;
            var beforeText = string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", before);
            var afterText = string.Format(CultureInfo.InvariantCulture, "{0:0.0000}", hideout.Settings.GeneratorFuelFlowRate);
            log.DebugLog(
                "FuelConsumption: GeneratorFuelFlowRate: "
                + beforeText
                + " -> "
                + afterText
                + $" (x{config.FuelConsumptionMultiplier})");
        }
        catch (Exception e) { log.Warning($"FuelConsumption failed gracefully. Send bug report. Continue safely.\n{e}"); }
    }
}
