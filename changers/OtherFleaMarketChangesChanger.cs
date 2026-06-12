using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;
using System.Globalization;

namespace Softcore.Changers;

[Injectable]
public class OtherFleaMarketChangesChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();

    public void Apply(OtherFleaMarketChanges config)
    {
        if (!config.Enabled) return;


        if (config.SellingOnFlea)
        {
            try
            {
                _ragfairConfig.Sell.Chance.Base = 0;
                _ragfairConfig.Sell.Chance.MaxSellChancePercent = 0;
                log.DebugLog("OtherFleaMarket: selling on flea disabled (sell chances zeroed)");
            }
            catch (Exception e)
            {
                log.Warning($"OtherFleaMarketChanges: DoSellingOnFlea failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.OnlyFoundInRaidItemsAllowedForBarters)
        {
            try
            {
                databaseService.GetGlobals().Configuration.RagFair.IsOnlyFoundInRaidAllowed = true;
                log.DebugLog("OtherFleaMarket: only FIR items allowed for barters");
            }
            catch (Exception e)
            {
                log.Warning($"OtherFleaMarketChanges: OnlyFIR failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.FleaPristineItems)
        {
            try
            {
                int conditions = 0;
                foreach (var condition in _ragfairConfig.Dynamic.Condition.Values)
                { condition.ConditionChance = 0; conditions++; }
                log.DebugLog($"OtherFleaMarket: item condition slots zeroed (pristine only): {conditions}");
            }
            catch (Exception e)
            {
                log.Warning($"OtherFleaMarketChanges: PristineItems failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        try
        {
            _ragfairConfig.Dynamic.PriceRanges.Default.Max *= config.FleaPricesIncreased;
            _ragfairConfig.Dynamic.PriceRanges.Default.Min *= config.FleaPricesIncreased;
            log.DebugLog("OtherFleaMarket: flea price range multiplied: x"
                + string.Format(CultureInfo.InvariantCulture, "{0:0.###}", config.FleaPricesIncreased));
        }
        catch (Exception e)
        {
            log.Warning($"OtherFleaMarketChanges: IncreaseFleaPrices failed gracefully. Send bug report. Continue safely.\n{e}");
        }
    }
}

