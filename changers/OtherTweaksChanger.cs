using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;

namespace Softcore.Changers;

[Injectable]
public class OtherTweaksChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly BotConfig _botConfig = configServer.GetConfig<BotConfig>();

    private static readonly MongoId SignalPistolTpl = ItemTpl.SIGNALPISTOL_ZID_SP81_26X75_SIGNAL_PISTOL;
    private static readonly MongoId PocketsTpl1 = ItemTpl.POCKETS_1X4_SPECIAL;
    private static readonly MongoId PocketsTpl2 = ItemTpl.POCKETS_1X4_TUE;
    private static readonly string AmmoBaseClass = BaseClasses.AMMO.ToString();

    private static readonly MongoId Euros = ItemTpl.MONEY_EUROS;
    private static readonly MongoId Dollars = ItemTpl.MONEY_DOLLARS;
    private static readonly MongoId GpCoin = ItemTpl.MONEY_GP_COIN;
    private static readonly MongoId Roubles = ItemTpl.MONEY_ROUBLES;

    private static readonly MongoId[] SmallContainerTpls =
    {
        ItemTpl.CONTAINER_DOGTAG_CASE,
        ItemTpl.CONTAINER_INJECTOR_CASE,
        ItemTpl.CONTAINER_KEY_TOOL,
        ItemTpl.CONTAINER_KEYCARD_HOLDER_CASE,
        ItemTpl.CONTAINER_SIMPLE_WALLET,
        ItemTpl.CONTAINER_WZ_WALLET
    };

    private static readonly HashSet<string> ExamineExclusions = new()
    {
        BaseClasses.BUILT_IN_INSERTS.ToString(),
        BaseClasses.MAGAZINE.ToString(),
        BaseClasses.CYLINDER_MAGAZINE.ToString(),
        BaseClasses.ARMOR_PLATE.ToString(),
        ItemTpl.BARTER_DOGTAG_BEAR_EOD,
        ItemTpl.BARTER_DOGTAG_BEAR_TUE,
        ItemTpl.BARTER_DOGTAG_USEC_EOD,
        ItemTpl.BARTER_DOGTAG_USEC_TUE,
        ItemTpl.BARTER_DOGTAG_USEC,
        ItemTpl.BARTER_DOGTAG_BEAR,
    };

    public void Apply(OtherTweaks config)
    {
        if (!config.Enabled) return;

        if (config.SkillExpBuffs)
        {
            try { DoSkillExpBuffs(); }
            catch (Exception e) { log.Warning($"OtherTweaks: SkillExpBuffs failed. {e.Message}"); }
        }

        if (config.SignalPistolInSpecialSlots)
        {
            try { DoSignalPistolInSpecialSlots(); }
            catch (Exception e) { log.Warning($"OtherTweaks: SignalPistol failed. {e.Message}"); }
        }

        if (config.UnexaminedItemsAreBack)
        {
            try { DoUnexaminedItemsAreBack(); }
            catch (Exception e) { log.Warning($"OtherTweaks: UnexaminedItems failed. {e.Message}"); }
        }

        if (config.FasterExamineTime)
        {
            try { DoFasterExamineTime(); }
            catch (Exception e) { log.Warning($"OtherTweaks: FasterExamine failed. {e.Message}"); }
        }

        if (config.RemoveBackpackRestrictions)
        {
            try { DoRemoveBackpackRestrictions(); }
            catch (Exception e) { log.Warning($"OtherTweaks: BackpackRestrictions failed. {e.Message}"); }
        }

        if (config.RemoveDiscardLimit)
        {
            try { DoRemoveDiscardLimit(); }
            catch (Exception e) { log.Warning($"OtherTweaks: DiscardLimit failed. {e.Message}"); }
        }

        if (config.ReshalaAlwaysHasGoldenTT)
        {
            try { DoReshalaAlwaysHasGoldenTT(); }
            catch (Exception e) { log.Warning($"OtherTweaks: GoldenTT failed. {e.Message}"); }
        }

        if (config.BiggerAmmoStacks.Enabled)
        {
            try { DoBiggerAmmoStacks(config.BiggerAmmoStacks); }
            catch (Exception e) { log.Warning($"OtherTweaks: BiggerAmmoStacks failed. {e.Message}"); }
        }

        if (config.QuestChanges)
        {
            try { DoQuestChanges(); }
            catch (Exception e) { log.Warning($"OtherTweaks: QuestChanges failed. {e.Message}"); }
        }

        if (config.RemoveRaidItemLimits)
        {
            try { DoRemoveRaidItemLimits(); }
            catch (Exception e)
            {
                log.Warning($"OtherTweaks: RaidItemLimits failed. {e.Message}");
            }
        }

        if (config.BiggerCurrencyStacks)
        {
            try { DoCurrencyStack(); }
            catch (Exception e) { log.Warning($"OtherTweaks: CurrencyStacks failed. {e.Message}"); }
        }

        if (config.SmallContainersInSpecialSlots)
        {
            try { DoSmallContainersInSpecialSlots(); }
            catch (Exception e) { log.Warning($"OtherTweaks: SmallContainers failed. {e.Message}"); }
        }
    }

    private void DoSkillExpBuffs()
    {
        var globals = databaseService.GetGlobals();
        var skills = globals.Configuration.SkillsSettings;
        skills.Vitality.DamageTakenAction *= 10;
        skills.Sniper.WeaponShotAction *= 10;
        skills.Surgery.SurgeryAction *= 10;
        // TS iterates MagDrills values without assigning them back, which is a no-op there.
        // We keep the intended gameplay effect by mutating the three concrete fields directly.
        skills.MagDrills.RaidLoadedAmmoAction *= 10;
        skills.MagDrills.RaidUnloadedAmmoAction *= 10;
        skills.MagDrills.MagazineCheckAction *= 10;
        skills.WeaponTreatment.SkillPointsPerRepair *= 100;
        log.DebugLog("SkillExpBuffs: Vitality x10, Sniper x10, Surgery x10, MagDrills x10, WeaponTreatment x100");
    }

    private void DoSignalPistolInSpecialSlots()
    {
        PushToSpecialSlots(SignalPistolTpl);
    }

    private void DoUnexaminedItemsAreBack()
    {
        var items = databaseService.GetTemplates().Items;
        int count = 0;
        foreach (var (_, item) in items)
        {
            var parentStr = item.Parent.ToString();
            var idStr = item.Id.ToString();
            if (ExamineExclusions.Contains(parentStr) || ExamineExclusions.Contains(idStr))
                continue;
            if (item.Properties?.ExaminedByDefault == true)
            { item.Properties.ExaminedByDefault = false; count++; }
        }
        log.DebugLog($"UnexaminedItemsAreBack: items set to unexamined: {count}");
    }

    private void DoFasterExamineTime()
    {
        var items = databaseService.GetTemplates().Items;
        int count = 0;
        foreach (var (_, item) in items)
            if (item.Properties?.ExamineTime > 0)
            { item.Properties.ExamineTime = 0.2; count++; }
        log.DebugLog($"FasterExamineTime: items examine time set to 0.2s: {count}");
    }

    private void DoRemoveBackpackRestrictions()
    {
        var items = databaseService.GetTemplates().Items;
        int count = 0;
        foreach (var (_, item) in items)
        {
            if (item.Type != "Item") continue;
            // TS only touches the first filter of the first grid when it sees an ammo case
            // exclusion. We keep that narrower behavior instead of clearing all grid filters.
            var excluded = item.Properties?.Grids?.FirstOrDefault()
                ?.Properties?.Filters?.FirstOrDefault()?.ExcludedFilter;

            if (excluded == null || !excluded.Contains(ItemTpl.CONTAINER_AMMUNITION_CASE))
                continue;

            excluded.Clear();
            count++;
        }
        log.DebugLog($"RemoveBackpackRestrictions: TS-style ammo case exclusions cleared: {count}");
    }

    private void DoRemoveDiscardLimit()
    {
        var items = databaseService.GetTemplates().Items;
        int count = 0;
        foreach (var (_, item) in items)
            if (item.Type == "Item" && item.Properties != null)
            { item.Properties.DiscardLimit = -1; count++; }
        log.DebugLog($"RemoveDiscardLimit: items discard limit removed: {count}");
    }

    private void DoReshalaAlwaysHasGoldenTT()
    {
        var goldenTT = ItemTpl.PISTOL_TT33_762X25_TT_PISTOL_GOLDEN;
        var bots = databaseService.GetBots();
        if (!bots.Types.TryGetValue("bossbully", out var reshala))
        {
            log.Warning("DoReshalaAlwaysHasGoldenTT: bossbully not found.");
            return;
        }

        if (reshala.BotChances?.EquipmentChances != null)
            reshala.BotChances.EquipmentChances["Holster"] = 100;

        if (reshala.BotInventory?.Equipment != null)
            reshala.BotInventory.Equipment[EquipmentSlots.Holster] =
                new Dictionary<MongoId, double> { [goldenTT] = 1 };

        log.DebugLog("ReshalaAlwaysHasGoldenTT: Holster chance 100%, Golden TT assigned");
    }

    private void DoBiggerAmmoStacks(BiggerAmmoStacks config)
    {
        var items = databaseService.GetTemplates().Items;
        int count = 0;
        foreach (var (_, item) in items)
        {
            if (item.Parent.ToString() != AmmoBaseClass) continue;
            if (item.Properties?.StackMaxSize > 0)
            {
                item.Properties.StackMaxSize *= config.StackMultiplier;
                count++;
            }
        }
        if (config.BotAmmoStackFix)
            _botConfig.SecureContainerAmmoStackCount =
                (int) Math.Round(_botConfig.SecureContainerAmmoStackCount / (double) config.StackMultiplier);
        log.DebugLog($"BiggerAmmoStacks: ammo types stacks multiplied (x{config.StackMultiplier}): {count}");
    }

    private void DoQuestChanges()
    {
        var quests = databaseService.GetTemplates().Quests;
        int modified = 0;

        if (quests.TryGetValue("60e71c48c1bfa3050473b8e5", out var crisis))
        {
            var startCond = crisis.Conditions.AvailableForStart.ElementAtOrDefault(1);
            if (startCond != null) { startCond.Value = 30; modified++; }
        }

        foreach (var quest in quests.Values.Where(q => q.QuestName?.Contains("Drip-Out") == true))
        {
            var handover = quest.Conditions.AvailableForFinish.FirstOrDefault(c => c.ConditionType == "HandoverItem");
            if (handover != null) { handover.Value = 10; modified++; }
            var counter = quest.Conditions.AvailableForFinish.FirstOrDefault(c => c.ConditionType == "CounterCreator");
            if (counter != null) { counter.Value = 20; modified++; }
        }

        if (quests.TryGetValue("6663149f1d3ec95634095e75", out var circulate))
        {
            var cond = circulate.Conditions.AvailableForFinish.FirstOrDefault();
            if (cond != null) { cond.Value = 50; modified++; }
        }

        if (quests.TryGetValue("5edac34d0bb72a50635c2bfa", out var colleagues3))
        {
            var c1 = colleagues3.Conditions.AvailableForFinish.FirstOrDefault(c => c.Id == "5f07025e27cec53d5d24fe25");
            if (c1 != null) { c1.OnlyFoundInRaid = false; modified++; }
            var c2 = colleagues3.Conditions.AvailableForFinish.FirstOrDefault(c => c.Id == "5f04935cde3b9e0ecf03d864");
            if (c2 != null) { c2.OnlyFoundInRaid = false; modified++; }
        }

        log.DebugLog($"QuestChanges: quest conditions modified: {modified}");
    }

    private void DoRemoveRaidItemLimits()
    {
        databaseService.GetGlobals().Configuration.RestrictionsInRaid = [];
        log.DebugLog("RemoveRaidItemLimits: all raid restrictions cleared");
    }

    private void DoCurrencyStack()
    {
        var items = databaseService.GetTemplates().Items;
        SetStack(items, Euros, 100000);
        SetStack(items, Dollars, 100000);
        SetStack(items, GpCoin, 100);
        SetStack(items, Roubles, 1000000);
    }

    private static void SetStack(
        Dictionary<MongoId, SPTarkov.Server.Core.Models.Eft.Common.Tables.TemplateItem> items,
        MongoId tpl, int size)
    {
        if (items.TryGetValue(tpl, out var item) && item.Properties != null)
            item.Properties.StackMaxSize = size;
    }

    private void DoSmallContainersInSpecialSlots()
    {
        foreach (var tpl in SmallContainerTpls)
        {
            try
            {
                PushToSpecialSlots(tpl);
            }
            catch (Exception e)
            {
                log.Warning($"OtherTweaks: PushToSpecialSlots:{tpl} failed. {e.Message}");
            }
        }
    }

    private void PushToSpecialSlots(MongoId itemTpl)
    {
        var items = databaseService.GetTemplates().Items;
        var pockets = new[] { PocketsTpl1, PocketsTpl2 };

        foreach (var pocketTpl in pockets)
        {
            if (!items.TryGetValue(pocketTpl, out var pocket)) continue;
            var slots = pocket.Properties?.Slots;
            if (slots == null) continue;
            foreach (var slot in slots)
            {
                var allowed = slot.Properties?.Filters?.FirstOrDefault()?.Filter;
                if (allowed != null && !allowed.Contains(itemTpl))
                    allowed.Add(itemTpl);
            }
        }
    }
}
