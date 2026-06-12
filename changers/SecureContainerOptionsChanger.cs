using SPTarkov.Server.Core.Models.Enums;
using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Utils.Json;
using Softcore.Assets;

namespace Softcore.Changers;

[Injectable]
public class SecureContainerOptionsChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly HideoutConfig _hideoutConfig = configServer.GetConfig<HideoutConfig>();

    public void Apply(SecureContainerOptions config)
    {
        if (!config.Enabled) return;

        if (config.ProgressiveContainers.Enabled)
        {
            try
            {
                DoProgressiveContainers();
            }
            catch (Exception e)
            {
                log.Warning($"SecureContainerOptions: DoProgressiveContainers failed gracefully. Send bug report. Continue safely.\n{e}");
            }

            if (config.ProgressiveContainers.CollectorQuestRedone)
            {
                try
                {
                    DoCollectorQuestRedone();
                }
                catch (Exception e)
                {
                    log.Warning($"SecureContainerOptions: DoCollectorQuestRedone failed gracefully. Send bug report. Continue safely.\n{e}");
                }
            }
        }

        if (config.BiggerContainers)
        {
            try
            {
                DoBiggerContainers();
            }
            catch (Exception e)
            {
                log.Warning($"SecureContainerOptions: DoBiggerContainers failed gracefully. Send bug report. Continue safely.\n{e}");
            }
        }
    }

    private void DoProgressiveContainers()
    {
        var profiles = databaseService.GetTemplates().Profiles;
        var waistPouch = ItemTpl.SECURE_WAIST_POUCH;
        int updated = 0;

        foreach (var profile in profiles.Values)
        {
            var bearContainer = profile.Bear?.Character?.Inventory?.Items
                ?.FirstOrDefault(x => x.SlotId == "SecuredContainer");
            if (bearContainer != null) { bearContainer.Template = waistPouch; updated++; }

            var usecContainer = profile.Usec?.Character?.Inventory?.Items
                ?.FirstOrDefault(x => x.SlotId == "SecuredContainer");
            if (usecContainer != null) { usecContainer.Template = waistPouch; updated++; }
        }

        log.DebugLog($"ProgressiveContainers: existing profiles reset to Waist Pouch: {updated}");

        var betaId = ItemTpl.SECURE_CONTAINER_BETA;
        var traders = databaseService.GetTraders();
        if (traders.TryGetValue(Traders.PEACEKEEPER, out var pk) && pk.Assort?.Items != null)
        {
            var betaItem = pk.Assort.Items.FirstOrDefault(i => i.Template == betaId);
            if (betaItem?.Upd != null)
            {
                betaItem.Upd.UnlimitedCount = false;
                betaItem.Upd.StackObjectsCount = 0;
                betaItem.Upd.BuyRestrictionMax = 0;
            }
        }

        var waistPouchId = ItemTpl.SECURE_WAIST_POUCH;
        var kappaId = ItemTpl.SECURE_CONTAINER_KAPPA;
        var reward = _hideoutConfig.CultistCircle?.DirectRewards
            ?.FirstOrDefault(r => r.RequiredItems?.Any(i => i == waistPouchId) == true);
        if (reward != null)
        {
            // Block cultistCircle Kappa reward for SECURE_WAIST_POUCH.
            // Softcore 3.1.0 replaces the matching waist pouch entry with Kappa here.
            var idx = reward.RequiredItems.ToList().FindIndex(i => i == waistPouchId);
            if (idx >= 0)
            {
                var list = reward.RequiredItems.ToList();
                list[idx] = kappaId;
                reward.RequiredItems = list;
            }
        }

        var hideout = databaseService.GetHideout();
        hideout.Production.Recipes.AddRange(Recipes.ContainerRecipes);
        log.DebugLog($"ProgressiveContainers: container recipes added: {Recipes.ContainerRecipes.Count}");
    }

    private void DoCollectorQuestRedone()
    {
        var quests = databaseService.GetTemplates().Quests;
        var collectorId = quests.Keys.FirstOrDefault(k => quests[k].QuestName == "Collector");
        if (collectorId == default)
        {
            log.Warning("DoCollectorQuestRedone: Collector quest not found.");
            return;
        }

        var quest = quests[collectorId];

        quest.Conditions.AvailableForFinish.Add(new()
        {
            ConditionType = "HandoverItem",
            DogtagLevel = 0,
            Id = "639135534b15ca31f76bc319",
            Index = 69,
            MaxDurability = 100,
            MinDurability = 0,
            ParentId = BaseClasses.MOB_CONTAINER.ToString(),
            IsEncoded = false,
            OnlyFoundInRaid = false,
            DynamicLocale = false,
            // SPT4 models this as ListOrT<string>; TS just passes a single-item string array.
            Target = new ListOrT<string>(new List<string> { ItemTpl.SECURE_CONTAINER_GAMMA }, null),
            Value = 2,
            VisibilityConditions = new(),
        });

        quest.Conditions.AvailableForStart = new()
        {
            new()
            {
                Id                   = "51d33b2d4fad9e61441772c0",
                CompareMethod        = ">=",
                ConditionType        = "Level",
                DynamicLocale        = false,
                GlobalQuestCounterId = "",
                Index                = 0,
                ParentId             = "",
                Value                = 10,
                VisibilityConditions = new(),
            }
        };
    }

    private void DoBiggerContainers()
    {
        ModifyContainer(ItemTpl.SECURE_WAIST_POUCH, 2, 4);
        ModifyContainer(ItemTpl.SECURE_CONTAINER_ALPHA, 3, 3);
        ModifyContainer(ItemTpl.SECURE_CONTAINER_BETA, 3, 4);
        ModifyContainer(ItemTpl.SECURE_CONTAINER_EPSILON, 3, 5);
        ModifyContainer(ItemTpl.SECURE_CONTAINER_GAMMA, 4, 5);
        ModifyContainer(ItemTpl.SECURE_CONTAINER_KAPPA, 5, 5);
        log.DebugLog("BiggerContainers: secure container grids updated");
    }

    private void ModifyContainer(MongoId tpl, int cellsV, int cellsH)
    {
        var items = databaseService.GetTemplates().Items;
        if (!items.TryGetValue(tpl, out var item) ||
            item.Properties?.Grids == null || !item.Properties.Grids.Any())
        {
            log.Warning($"ModifyContainer: {tpl} not found or has no grids.");
            return;
        }
        item.Properties.Grids.First().Properties.CellsV = cellsV;
        item.Properties.Grids.First().Properties.CellsH = cellsH;
    }
}
