using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;

namespace Softcore.Changers;

[Injectable]
public class PriceRebalanceChanger(
    PrefixLogger log,
    DatabaseService databaseService)
{
    public void Apply(PriceRebalance config)
    {
        if (!config.Enabled) return;

        if (config.ItemFixes)
        {
            try
            {
                DoItemFixes();
            }
            catch (Exception e)
            {
                log.Warning($"PriceRebalance: DoItemFixes failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        try
        {
            DoPriceRebalance();
        }
        catch (Exception e)
        {
            log.Warning($"PriceRebalance: DoPriceRebalance failed gracefully. Send bug report. Continue safely.\n{e}");
        }
    }

    private void DoItemFixes()
    {
        var itemsToFix = new Dictionary<MongoId, double>
        {
            [ItemTpl.VISORS_ROUND_FRAME_SUNGLASSES] = 3084 * 5,
            [ItemTpl.AMMO_40MMRU_VOG25] = 6750 * 5,
            [ItemTpl.VISORS_ANTIFRAGMENTATION_GLASSES] = 2181 * 2,
            [ItemTpl.BACKPACK_LOLKEK_3F_TRANSFER_TOURIST] = 18000 * 2,
            [ItemTpl.FOOD_EMELYA_RYE_CROUTONS] = 1500,
            [ItemTpl.FOOD_RYE_CROUTONS] = 2000,
            [ItemTpl.INFO_INTELLIGENCE_FOLDER] = 588000,
            [ItemTpl.INFO_MILITARY_FLASH_DRIVE] = 224400,
            [ItemTpl.BARTER_CASE_KEY] = 32524 * 20,
        };

        var handbook = databaseService.GetTemplates().Handbook;
        int fixedCount = 0;
        foreach (var (tpl, price) in itemsToFix)
        {
            var entry = handbook.Items.FirstOrDefault(i => i.Id == tpl);
            if (entry == null)
            {
                log.Warning($"PriceRebalance: {tpl} not in handbook.");
                continue;
            }
            entry.Price = price;
            fixedCount++;
        }
        log.DebugLog($"PriceRebalance: handbook item fixes applied: {fixedCount}");
    }

    private void DoPriceRebalance()
    {
        var templates = databaseService.GetTemplates();
        int count = 0;
        foreach (var item in templates.Handbook.Items)
        {
            templates.Prices[item.Id] = item.Price ?? 0;
            count++;
        }
        log.DebugLog("PriceRebalance: reset all flea prices to handbook values");
        log.DebugLog($"PriceRebalance: prices reset: {count}");
    }
}
