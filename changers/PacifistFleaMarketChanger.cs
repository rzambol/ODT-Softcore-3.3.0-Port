using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;
using Softcore.Assets;

namespace Softcore.Changers;

[Injectable]
public class PacifistFleaMarketChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();

    public void Apply(PacifistFleaMarket config)
    {
        if (!config.Enabled) return;

        try
        {
            DoPacifistFleaMarket();
        }
        catch (Exception e)
        {
            log.Warning($"PacifistFleaMarket: PacifistFleaMarket failed gracefully. Send bug report. Continue safely.\n{e}");
        }

        if (config.Whitelist.Enabled)
        {
            try
            {
                AllowOnRagfair("explicit whitelist", FleaMarket.WhitelistMongoIds, config.Whitelist.PriceMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"PacifistFleaMarket: Whitelist failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.QuestKeys.Enabled)
        {
            try
            {
                AllowOnRagfair("quest keys", Keys.QuestKeysMongoIds, config.QuestKeys.PriceMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"PacifistFleaMarket: QuestKeys failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }

        if (config.MarkedKeys.Enabled)
        {
            try
            {
                AllowOnRagfair("marked keys", Keys.MarkedKeysMongoIds, config.MarkedKeys.PriceMultiplier);
            }
            catch (Exception e)
            {
                log.Warning($"PacifistFleaMarket: MarkedKeys failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }
    }

    private void DoPacifistFleaMarket()
    {
        var templates = databaseService.GetTemplates();
        var handbookItems = templates.Handbook.Items;
        var items = templates.Items;
        int banned = 0;

        foreach (var handbookItem in handbookItems)
        {
            var itemId = handbookItem.Id;
            if (!FleaMarket.FleaListingsWhitelistHandbook.Contains(handbookItem.ParentId.ToString()) ||
                (items.TryGetValue(itemId, out var item) && item.Properties?.QuestItem == true))
            {
                _ragfairConfig.Dynamic.Blacklist.Custom.Add(itemId);
                banned++;
            }
        }

        log.DebugLog($"PacifistFleaMarket: items banned from flea: {banned}");
        log.DebugLog($"PacifistFleaMarket: whitelist handbook categories: {FleaMarket.FleaListingsWhitelistHandbook.Count}");
    }

    private void AllowOnRagfair(string label, IEnumerable<MongoId> whitelist, double priceMultiplier)
    {
        var templates = databaseService.GetTemplates();
        int allowed = 0;
        foreach (var itemId in whitelist)
        {
            if (!templates.Items.TryGetValue(itemId, out var item))
            {
                log.Warning($"PacifistFleaMarket: {itemId} not found.");
                continue;
            }
            if (templates.Prices.TryGetValue(itemId, out var currentPrice))
                templates.Prices[itemId] = (int) Math.Round(currentPrice * priceMultiplier);
            if (item.Properties != null) item.Properties.CanSellOnRagfair = true;
            _ragfairConfig.Dynamic.Blacklist.Custom.RemoveWhere(x => x == itemId);
            allowed++;
        }
        log.DebugLog($"PacifistFleaMarket: {label} allowed on flea (x{priceMultiplier} price): {allowed}");
    }
}
