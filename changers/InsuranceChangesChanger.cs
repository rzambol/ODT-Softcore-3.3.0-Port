using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;

namespace Softcore.Changers;

[Injectable]
public class InsuranceChangesChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly InsuranceConfig _insuranceConfig = configServer.GetConfig<InsuranceConfig>();

    private static readonly string Prapor = Traders.PRAPOR;
    private static readonly string Therapist = Traders.THERAPIST;

    public void Apply(InsuranceChanges config)
    {
        if (!config.Enabled) return;

        if (config.PraporInsuranceChanges.Enabled)
        {
            try
            {
                DoTraderInsuranceChanges(Prapor, config.PraporInsuranceChanges);
            }
            catch (Exception e)
            {
                log.Warning($"InsuranceChanges: Prapor failed. {e.Message}");
            }
        }

        if (config.TherapistInsuranceChanges.Enabled)
        {
            try
            {
                DoTraderInsuranceChanges(Therapist, config.TherapistInsuranceChanges);
            }
            catch (Exception e)
            {
                log.Warning($"InsuranceChanges: Therapist failed. {e.Message}");
            }
        }

        try
        {
            _insuranceConfig.RunIntervalSeconds = 10;
            _insuranceConfig.StorageTimeOverrideSeconds = 2592000;
            log.DebugLog("Insurance: global config set - interval 10s, storage 30d");
        }
        catch (Exception e)
        {
            log.Warning($"InsuranceChanges: GlobalInsuranceConfig failed. {e.Message}");
        }
    }

    private void DoTraderInsuranceChanges(string traderId, TraderInsuranceChanges changes)
    {
        var traders = databaseService.GetTraders();
        if (!traders.TryGetValue(traderId, out var trader))
        {
            log.Warning($"InsuranceChanges: trader {traderId} not found.");
            return;
        }

        trader.Base.Insurance.MinReturnHour = changes.ReturnTime.Min;
        trader.Base.Insurance.MaxReturnHour = changes.ReturnTime.Max;
        _insuranceConfig.ReturnChancePercent[traderId] = changes.ReturnChance;
        // In TS this value is applied inside the trader-specific branch, not as a global
        // side effect of enabling the parent module.
        _insuranceConfig.ChanceNoAttachmentsTakenPercent = 50;

        foreach (var loyaltyLevel in trader.Base.LoyaltyLevels)
            loyaltyLevel.InsurancePriceCoefficient = changes.InsuranceCostPercentage;

        log.DebugLog($"Insurance [{trader.Base.Nickname}] ReturnChance: {changes.ReturnChance}%");
        log.DebugLog($"Insurance [{trader.Base.Nickname}] ReturnTime: {changes.ReturnTime.Min}-{changes.ReturnTime.Max}h");
        log.DebugLog($"Insurance [{trader.Base.Nickname}] CostPercentage: {changes.InsuranceCostPercentage}%");
        log.DebugLog($"Insurance [{trader.Base.Nickname}] loyalty levels updated: {trader.Base.LoyaltyLevels.Count}");
    }
}