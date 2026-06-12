using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;

namespace Softcore.Changers;

[Injectable]
public class HideoutOptionsChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    StashOptionsChanger stashChanger,
    FasterBitcoinFarmingChanger bitcoinChanger,
    FasterCraftingTimeChanger craftingTimeChanger,
    FasterHideoutConstructionChanger constructionChanger,
    FuelConsumptionChanger fuelChanger,
    ScavCaseOptionsChanger scavCaseChanger,
    HideoutContainersChanger containersChanger)
{
    public void Apply(HideoutOptions config)
    {
        if (!config.Enabled) return;

        try
        {
            if (config.StashOptions.Enabled)
                stashChanger.Apply(config.StashOptions);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: StashOptions failed gracefully. Send bug report. Continue safely.\n{e}");
        }
        try
        {
            if (config.HideoutContainers.Enabled)
                containersChanger.Apply(config.HideoutContainers);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: HideoutContainers failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.FasterBitcoinFarming.Enabled)
                bitcoinChanger.Apply(config.FasterBitcoinFarming);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: FasterBitcoinFarming failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.FasterCraftingTime.Enabled)
                craftingTimeChanger.Apply(config.FasterCraftingTime);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: FasterCraftingTime failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.FasterHideoutConstruction.Enabled)
                constructionChanger.Apply(config.FasterHideoutConstruction);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: FasterHideoutConstruction failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.FuelConsumption.Enabled)
                fuelChanger.Apply(config.FuelConsumption);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: FuelConsumption failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        try
        {
            if (config.ScavCaseOptions.Enabled)
                scavCaseChanger.Apply(config.ScavCaseOptions);
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: ScavCaseOptions failed gracefully. Send bug report. Continue safely.\n{e}");
        }


        try
        {
            if (config.AllowGymTrainingWithMusclePain)
            {
                var globals = databaseService.GetGlobals();
                globals.Configuration.Health.Effects.SevereMusclePain.GymEffectivity = 0.75f;
                log.DebugLog("AllowGymWithMusclePain: GymEffectivity set to: 0.75");
            }
        }
        catch (Exception e)
        {
            log.Warning($"HideoutOptions: AllowGymTrainingWithMusclePain failed gracefully. Send bug report. Continue safely.\n{e}");
        }
    }
}

