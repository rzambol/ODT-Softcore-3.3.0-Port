using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;

namespace Softcore.Changers;

[Injectable]
public class FasterHideoutConstructionChanger(
    PrefixLogger log,
    DatabaseService databaseService)
{
    public void Apply(FasterHideoutConstruction config)
    {
        if (!config.Enabled) return;

        try
        {
            if (config.HideoutConstructionTimeMultiplier <= 0)
            {
                log.Warning("FasterHideoutConstruction: invalid multiplier, skipping.");
                return;
            }

            var hideout = databaseService.GetHideout();
            int stages = 0;
            foreach (var area in hideout.Areas)
                foreach (var stage in area.Stages.Values)
                {
                    stage.ConstructionTime = (int) Math.Round((double) (stage.ConstructionTime ?? 0) / config.HideoutConstructionTimeMultiplier);
                    stages++;
                }
            log.DebugLog($"FasterHideoutConstruction: stages speed up (x{config.HideoutConstructionTimeMultiplier}): {stages}");
        }
        catch (Exception e) { log.Warning($"FasterHideoutConstruction failed gracefully. Send bug report. Continue safely.\n{e}"); }
    }
}
