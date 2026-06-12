using Softcore.Util;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Enums;
using Softcore.Assets;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;

namespace Softcore.Changers;

[Injectable]
public class TraderChangesChanger(
    PrefixLogger log,
    DatabaseService databaseService,
    ConfigServer configServer)
{
    private readonly TraderConfig _traderConfig = configServer.GetConfig<TraderConfig>();

    private static readonly HashSet<string> StaticTraders = new()
    {
        Traders.PRAPOR,
        Traders.THERAPIST,
        Traders.FENCE,
        Traders.SKIER,
        Traders.PEACEKEEPER,
        Traders.MECHANIC,
        Traders.RAGMAN,
        Traders.JAEGER,
        Traders.REF,
    };

    private static readonly string Prapor = Traders.PRAPOR;
    private static readonly string Therapist = Traders.THERAPIST;
    private static readonly string Fence = Traders.FENCE;
    private static readonly string Skier = Traders.SKIER;
    private static readonly string Peacekeeper = Traders.PEACEKEEPER;
    private static readonly string Mechanic = Traders.MECHANIC;
    private static readonly string Ragman = Traders.RAGMAN;
    private static readonly string Jaeger = Traders.JAEGER;

    private static readonly MongoId Roubles = ItemTpl.MONEY_ROUBLES;
    private static readonly MongoId Euros = ItemTpl.MONEY_EUROS;

    private static readonly MongoId MedicalSupplies = BaseClasses.MEDICAL_SUPPLIES;
    private static readonly MongoId HouseholdGoods = BaseClasses.HOUSEHOLD_GOODS;
    private static readonly MongoId BarterItem = BaseClasses.BARTER_ITEM;
    private static readonly MongoId Jewelry = BaseClasses.JEWELRY;
    private static readonly MongoId Info = BaseClasses.INFO;

    private static readonly MongoId ThiccItemCase = ItemTpl.CONTAINER_THICC_ITEM_CASE;
    private static readonly MongoId WeaponCase = ItemTpl.CONTAINER_THICC_WEAPON_CASE;
    private static readonly MongoId ItemCase = ItemTpl.CONTAINER_ITEM_CASE;
    private static readonly MongoId LuckyScavJunkBox = ItemTpl.CONTAINER_LUCKY_SCAV_JUNK_BOX;
    private static readonly MongoId MedicineCase = ItemTpl.CONTAINER_MEDICINE_CASE;
    private static readonly MongoId LedxTpl = ItemTpl.BARTER_LEDX_SKIN_TRANSILLUMINATOR;
    private static readonly MongoId BlueFoldersTpl = ItemTpl.INFO_TERRAGROUP_BLUE_FOLDERS_MATERIALS;
    private static readonly MongoId MoonshineTpl = ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE;
    private static readonly MongoId OphthalmoscopeTpl = ItemTpl.BARTER_OPHTHALMOSCOPE;
    private static readonly MongoId DogtagUsecTpl = ItemTpl.BARTER_DOGTAG_USEC;
    private static readonly MongoId EncryptedFlashDrive = ItemTpl.INFO_SECURE_FLASH_DRIVE_V2;

    public void Apply(TraderChanges config)
    {
        if (!config.Enabled) return;

        if (config.BetterSalesToTraders)
        {
            try { DoBetterSalesToTraders(); }
            catch (Exception e) { log.Warning($"TraderChanges: DoBetterSalesToTraders failed gracefully. Send bug report. Continue safely.\n{e}"); }
        }

        if (config.AlternativeCategories)
        {
            try { DoAlternativeCategories(); }
            catch (Exception e) { log.Warning($"TraderChanges: DoAlternativeCategories failed gracefully. Send bug report. Continue safely.\n{e}"); }
        }

        if (config.PacifistFence.Enabled)
        {
            try { DoPacifistFence(config.PacifistFence.NumberOfFenceOffers); }
            catch (Exception e) { log.Warning($"TraderChanges: DoPacifistFence failed gracefully. Send bug report. Continue safely.\n{e}"); }
        }

        if (config.ReasonablyPricedCases)
        {
            try { DoReasonablyPricedCases(); }
            catch (Exception e) { log.Warning($"TraderChanges: DoReasonablyPricedCases failed gracefully. Send bug report. Continue safely.\n{e}"); }
        }

        if (config.SkierUsesEuros)
        {
            try { DoSkierUsesEuros(); }
            catch (Exception e) { log.Warning($"TraderChanges: DoSkierUsesEuros failed gracefully. Send bug report. Continue safely.\n{e}"); }
        }

        if (config.BiggerLimits.Enabled)
        {
            try { DoBiggerLimits(config.BiggerLimits.Multiplier); }
            catch (Exception e) { log.Warning($"TraderChanges: DoBiggerLimits failed gracefully. Send bug report. Continue safely.\n{e}"); }
        }
    }

    private void DoBetterSalesToTraders()
    {
        var buyPriceAdjustment = new Dictionary<string, int>
        {
            [Peacekeeper] = 7,
            [Skier] = 6,
            [Prapor] = 5,
            [Mechanic] = 4,
            [Jaeger] = 3,
            [Ragman] = 2,
            [Therapist] = 1,
        };

        var traders = databaseService.GetTraders();
        int tradersUpdated = 0;
        foreach (var traderId in StaticTraders)
        {
            if (!buyPriceAdjustment.TryGetValue(traderId, out var adjustment)) continue;
            if (!traders.TryGetValue(traderId, out var trader)) continue;

            int coef = 35;
            foreach (var loyaltyLevel in trader.Base.LoyaltyLevels)
            {
                loyaltyLevel.BuyPriceCoefficient = coef + adjustment;
                coef -= 5;
            }
            tradersUpdated++;
        }
        log.DebugLog($"BetterSalesToTraders: traders updated: {tradersUpdated}");
    }

    private void DoAlternativeCategories()
    {
        var traders = databaseService.GetTraders();

        if (traders.TryGetValue(Therapist, out var therapist) && therapist.Base.ItemsBuy?.Category != null)
        {
            var cats = therapist.Base.ItemsBuy.Category;
            if (!cats.Contains(MedicalSupplies)) cats.Add(MedicalSupplies);
            if (!cats.Contains(HouseholdGoods)) cats.Add(HouseholdGoods);
            cats.RemoveWhere(c => c == BarterItem);

            log.DebugLog("AlternativeCategories: Therapist - +MedicalSupplies, +HouseholdGoods, -BarterItems");
        }

        if (traders.TryGetValue(Ragman, out var ragman) && ragman.Base.ItemsBuy?.Category != null)
        {
            var cats = ragman.Base.ItemsBuy.Category;
            if (!cats.Contains(Jewelry)) cats.Add(Jewelry);
            log.DebugLog("AlternativeCategories: Ragman - +Jewelry");
        }

        if (traders.TryGetValue(Skier, out var skier) && skier.Base.ItemsBuy?.Category != null)
        {
            var cats = skier.Base.ItemsBuy.Category;
            if (!cats.Contains(Info)) cats.Add(Info);
            log.DebugLog("AlternativeCategories: Skier - +Info");
        }
    }

    private void DoPacifistFence(int numberOfOffers)
    {
        // The asset still mirrors TS data, but the changers consume cached MongoIds so Fence
        // config can be updated without rebuilding ids on every loop.
        var fenceWhitelist = FleaMarket.PacifistFenceItemBaseWhitelistMongoIds;

        var fenceBlacklist = ItemBaseClasses.AllMongoIds
            .Where(bc => !fenceWhitelist.Contains(bc))
            .ToList();

        foreach (var bc in ItemBaseClasses.AllMongoIds)
        {
            _traderConfig.Fence.ItemTypeLimits[bc] = numberOfOffers;
        }

        _traderConfig.Fence.ItemTypeLimits.Remove(MedicalSupplies);

        var items = databaseService.GetTemplates()?.Items;
        if (items == null) return;

        var questItemIds = items.Values
            .Where(i => i.Properties?.QuestItem == true)
            .Select(i => i.Id)
            .ToList();

        var combined = new HashSet<MongoId>(_traderConfig.Fence.Blacklist);
        combined.UnionWith(questItemIds);
        combined.UnionWith(FleaMarket.BsgBlacklistMongoIds);
        combined.UnionWith(fenceBlacklist);
        combined.Add(EncryptedFlashDrive);

        _traderConfig.Fence.Blacklist = combined;

        _traderConfig.Fence.PreventDuplicateOffersOfCategory =
            fenceWhitelist.Where(x => x != BaseClasses.MEDICAL_SUPPLIES)
                          .ToHashSet();

        _traderConfig.Fence.AssortSize = numberOfOffers;

        log.DebugLog($"PacifistFence applied: offers={numberOfOffers}, blacklist={combined.Count}");

        _traderConfig.Fence.EquipmentPresetMinMax.Min = 0;
        _traderConfig.Fence.EquipmentPresetMinMax.Max = 0;
        _traderConfig.Fence.WeaponPresetMinMax.Min = 0;
        _traderConfig.Fence.WeaponPresetMinMax.Max = 0;
        _traderConfig.Fence.ItemPriceMult = 1;

        _traderConfig.Fence.DiscountOptions.AssortSize = numberOfOffers * 2;
        _traderConfig.Fence.DiscountOptions.ItemPriceMult = 0.82;
        _traderConfig.Fence.DiscountOptions.WeaponPresetMinMax.Min = 0;
        _traderConfig.Fence.DiscountOptions.WeaponPresetMinMax.Max = 0;
        _traderConfig.Fence.DiscountOptions.EquipmentPresetMinMax.Min = 0;
        _traderConfig.Fence.DiscountOptions.EquipmentPresetMinMax.Max = 0;
    }

    
    private void DoReasonablyPricedCases()
    {
        AdjustTherapistBarters();
        AdjustPeacekeeperBarters();
        AdjustSkierBarters();
    }

    private void AdjustPeacekeeperBarters()
    {
        int changes = 0;
        ModifyTraderBarters(Peacekeeper, ThiccItemCase, new()
        {
            [BlueFoldersTpl] = req =>
            {
                req.Count = (int) Math.Round((req.Count ?? 0) / 5.0, MidpointRounding.AwayFromZero) + 1;
                changes++;
            },
        });
        log.DebugLog($"AdjustPeacekeeperBarters: requirements changed: {changes}");
    }

    private void AdjustSkierBarters()
    {
        int changes = 0;
        var traders = databaseService.GetTraders();
        bool weaponCaseBarterFound = false;
        bool moonshineRequirementFound = false;

        if (traders.TryGetValue(Skier, out var skier) && skier.Assort?.Items != null)
        {
            var barterIds = skier.Assort.Items
                .Where(i => i.Template == WeaponCase)
                .Select(i => i.Id)
                .ToHashSet();

            weaponCaseBarterFound = barterIds.Count > 0;

            if (weaponCaseBarterFound && skier.Assort.BarterScheme != null)
            {
                foreach (var barterId in barterIds)
                {
                    if (!skier.Assort.BarterScheme.TryGetValue(barterId, out var requirementSets) ||
                        requirementSets == null)
                        continue;

                    if (requirementSets
                        .Where(set => set != null)
                        .SelectMany(set => set)
                        .Any(req => req.Template == MoonshineTpl))
                    {
                        moonshineRequirementFound = true;
                        break;
                    }
                }
            }
        }

        ModifyTraderBarters(Skier, WeaponCase, new()
        {
            [MoonshineTpl] = req =>
            {
                req.Count = 4;
                changes++;
            },
        });

        if (changes > 0)
        {
            log.DebugLog($"AdjustSkierBarters: matching moonshine requirements changed: {changes}");
            return;
        }

        if (!weaponCaseBarterFound)
        {
            log.DebugLog("AdjustSkierBarters: Weapon Case barter not found in Skier assort");
            return;
        }

        if (!moonshineRequirementFound)
        {
            log.DebugLog("AdjustSkierBarters: Weapon Case barter found, but no moonshine requirements matched");
            return;
        }

        log.DebugLog("AdjustSkierBarters: Weapon Case barter matched, but no requirement values needed changing");
    }

    private void AdjustTherapistBarters()
    {
        int changes = 0;
        ModifyTraderBarters(Therapist, ItemCase, new()
        {
            [Euros] = req => { req.Count = 7256; changes++; },
            [OphthalmoscopeTpl] = req => { req.Count = 8; changes++; },
            [DogtagUsecTpl] = req => { req.Count = 20; changes++; },
        });
        ModifyTraderBarters(Therapist, LuckyScavJunkBox, new()
        {
            [Roubles] = req => { req.Count = 961138; changes++; },
            [DogtagUsecTpl] = req => { req.Count = 15; changes++; },
        });
        ModifyTraderBarters(Therapist, MedicineCase, new()
        {
            [Roubles] = req => { req.Count = 290610; changes++; },
        });
        ModifyTraderBarters(Therapist, LedxTpl, new()
        {
            [DogtagUsecTpl] = req =>
            {
                req.Count = (int) Math.Round((req.Count ?? 0) / 10.0, MidpointRounding.AwayFromZero);
                changes++;
            },
        });
        ModifyTraderBarters(Therapist, ThiccItemCase, new()
        {
            [LedxTpl] = req => { req.Count = 5; changes++; },
            [MoonshineTpl] = req => { req.Count = 10; changes++; },
        });
        log.DebugLog($"AdjustTherapistBarters: requirements changed: {changes}");
    }

    private void ModifyTraderBarters(
    string traderId,
    MongoId targetItemTpl,
    Dictionary<MongoId, Action<SPTarkov.Server.Core.Models.Eft.Common.Tables.BarterScheme>> adjustments)
    {
        var traders = databaseService.GetTraders();
        if (!traders.TryGetValue(traderId, out var trader))
        {
            log.Warning($"ModifyTraderBarters: trader {traderId} not found.");
            return;
        }

        var assort = trader.Assort;
        if (assort?.Items == null)
        {
            log.Warning($"ModifyTraderBarters: assort/items null for trader {traderId}");
            return;
        }

        if (assort.BarterScheme == null)
        {
            log.Warning($"ModifyTraderBarters: BarterScheme null for trader {traderId}");
            return;
        }

        var barterIds = assort.Items
            .Where(i => i.Template == targetItemTpl)
            .Select(i => i.Id)
            .ToHashSet();

        foreach (var (adjustTpl, applyAdjustment) in adjustments)
        {
            foreach (var barterId in barterIds)
            {
                if (!assort.BarterScheme.TryGetValue(barterId, out var requirementSets))
                    continue;

                if (requirementSets == null || requirementSets.Count == 0)
                    continue;

                foreach (var set in requirementSets)
                {
                    // TS mutates the whole requirement set once it finds a matching tpl.
                    // Here we only touch the matching requirements, which keeps the intended
                    // barter edits without broad side effects.
                    foreach (var req in set.Where(r => r.Template == adjustTpl))
                    {
                        applyAdjustment(req);
                    }
                }
            }
        }
    }

    private void DoSkierUsesEuros()
    {
        var traders = databaseService.GetTraders();
        if (!traders.TryGetValue(Skier, out var skier))
        {
            log.Warning("DoSkierUsesEuros: Skier not found.");
            return;
        }

        var handbook = databaseService.GetTemplates().Handbook;
        var euroEntry = handbook.Items.FirstOrDefault(i => i.Id == Euros);
        if (euroEntry == null)
        {
            log.Warning("DoSkierUsesEuros: Euro price not found.");
            return;
        }

        var euroPrice = euroEntry.Price ?? 1;

        skier.Base.Currency = CurrencyType.EUR;
        skier.Base.BalanceEuro = 700000;

        foreach (var ll in skier.Base.LoyaltyLevels)
        {
            // TS divides a generic number; in C# we round back into the int field used by SPT4.
            ll.MinSalesSum = (int) Math.Round((ll.MinSalesSum ?? 0) / euroPrice, MidpointRounding.AwayFromZero);

        }

        var eurAssortItem = skier.Assort?.Items?.FirstOrDefault(i => i.Template == Euros);

        if (skier.Assort?.BarterScheme == null)
            return;

        foreach (var (offerId, barterSets) in skier.Assort.BarterScheme)
        {
            if (eurAssortItem != null && offerId == eurAssortItem.Id)
                continue;

            if (barterSets == null || barterSets.Count == 0 || barterSets[0].Count == 0)
                continue;

            var firstReq = barterSets[0][0];

            if (firstReq.Template == Roubles)
            {
                // SPT4 barter counts are doubles, so we can keep the TS behavior of preserving
                // sub-1 EUR store prices instead of forcing those offers up to 1 EUR.
                firstReq.Count = Math.Round(
                    ((firstReq.Count ?? 0) / euroPrice) * 100,
                    MidpointRounding.AwayFromZero
                ) / 100;

                firstReq.Template = Euros;

            }
        }

        var skierId = new MongoId(Skier);
        var quests = databaseService.GetTemplates().Quests;

        foreach (var quest in quests.Values.Where(q => q.TraderId == skierId))
        {
            if (quest.Rewards == null) continue;
            if (!quest.Rewards.TryGetValue("Success", out var rewards)) continue;

            foreach (var reward in rewards)
            {
                if (reward.Items == null) continue;

                foreach (var item in reward.Items.Where(i => i.Template == Roubles))
                {
                    item.Template = Euros;

                    if (item.Upd?.StackObjectsCount == null)
                        continue;

                    item.Upd.StackObjectsCount = (int) Math.Ceiling((item.Upd.StackObjectsCount ?? 0) / euroPrice);

                    if (reward.Value != null)
                    {
                        reward.Value = (int) Math.Ceiling(
                            Convert.ToDouble(reward.Value) / euroPrice
                        );
                    }
                }
            }
        }

        log.DebugLog("DoSkierUsesEuros: skier currency, assorts and quest rewards converted to EUR");
    }

    private void DoBiggerLimits(double multiplier)
    {
        var traders = databaseService.GetTraders();
        int itemsUpdated = 0;
        foreach (var traderId in StaticTraders)
        {
            if (!traders.TryGetValue(traderId, out var trader)) continue;
            var items = trader.Assort?.Items;
            if (items == null) continue;

            foreach (var item in items)
            {
                if (item.Upd?.BuyRestrictionMax.HasValue == true)
                {
                    item.Upd.BuyRestrictionMax = (int) Math.Round(item.Upd.BuyRestrictionMax.Value * multiplier, MidpointRounding.AwayFromZero); itemsUpdated++;
                }
            }
        }
        log.DebugLog($"BiggerLimits: assort items with limits multiplied (x{multiplier}): {itemsUpdated}");
    }
}
