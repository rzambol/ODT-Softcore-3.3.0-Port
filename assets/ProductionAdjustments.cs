using SPTarkov.Server.Core.Models.Enums.Hideout;
using SPTarkov.Server.Core.Models.Eft.Hideout;
using SPTarkov.Server.Core.Models.Enums;

namespace Softcore.Assets;
public static class ProductionAdjustments
{
    public record ProductionAdjustment(
        string Id,
        Action<HideoutProduction> Adjust,
        Func<HideoutProduction, bool>? Match = null);

    private static ProductionAdjustment Entry(
        string id,
        Action<HideoutProduction> adjust,
        Func<HideoutProduction, bool>? match = null)
        => new(id, adjust, match);

    public static readonly List<ProductionAdjustment> CraftingAdjustments = new()
    {
        Entry(ItemTpl.BARTER_TOILET_PAPER, craft => { craft.Count = 1; }), // Toilet paper
        Entry(ItemTpl.BARTER_CLIN_WINDOW_CLEANER, craft => { craft.Count = 4; }), // CLIN window cleaner
        Entry(ItemTpl.BARTER_PARACORD, craft => { craft.Count = 2; }), // Paracord
        Entry(ItemTpl.BARTER_CORRUGATED_HOSE, craft => // Corrugated hose - all req counts to 1, count 1
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
            craft.Count = 1;
        }),
        Entry(ItemTpl.BARTER_WATER_FILTER, craft => // Water filter - gas mask air filter count = 2
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_GAS_MASK_AIR_FILTER);
            if (req != null) req.Count = 2;
        }),
        Entry(ItemTpl.DRINK_EMERGENCY_WATER_RATION,
            craft => { craft.Count = 3; },
            craft => craft.AreaType == HideoutAreas.Kitchen
        ), // Emergency water ration - kitchen recipe only
        Entry(ItemTpl.BARTER_CAN_OF_MAJAICA_COFFEE_BEANS, craft => { craft.Count = 3; }), // Can of Majaica coffee
        Entry(ItemTpl.DRINK_BOTTLE_OF_WATER_06L,
            craft => { craft.Count = 16; },
            craft => craft.AreaType == HideoutAreas.Kitchen
        ), // Bottle of water 0.6L - kitchen recipe only
        Entry(ItemTpl.STIM_MULE_STIMULANT_INJECTOR, craft => { craft.Count = 2; }), // MULE stimulant injector
        Entry(ItemTpl.STIM_ETGCHANGE_REGENERATIVE_STIMULANT_INJECTOR, craft => // eTG-change - count 2, CALOK-B count 2
        {
            craft.Count = 2;
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.MEDICAL_CALOKB_HEMOSTATIC_APPLICATOR);
            if (req != null) req.Count = 2;
        }),
        Entry(ItemTpl.MEDKIT_AFAK_TACTICAL_INDIVIDUAL_FIRST_AID_KIT, craft => // AFAK - IFAK count 1, replace army bandage with CALOK-B
        {
            var ifak = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.MEDKIT_IFAK_INDIVIDUAL_FIRST_AID_KIT);
            if (ifak != null) ifak.Count = 1;
            var bandage = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.MEDICAL_ARMY_BANDAGE);
            if (bandage != null) bandage.TemplateId = ItemTpl.MEDICAL_CALOKB_HEMOSTATIC_APPLICATOR; // CALOK-B
        }),
        Entry(ItemTpl.MEDICAL_SURV12_FIELD_SURGICAL_KIT, craft => // SURV12 - replace self-ref req with CMS x2
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.MEDICAL_SURV12_FIELD_SURGICAL_KIT);
            if (req != null) { req.Count = 2; req.TemplateId = ItemTpl.MEDICAL_CMS_SURGICAL_KIT; } // CMS
        }),
        Entry(ItemTpl.BARTER_PORTABLE_DEFIBRILLATOR, craft => // Portable defibrillator - powerbank count 4
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_PORTABLE_POWERBANK);
            if (req != null) req.Count = 4;
        }),
        Entry(ItemTpl.BARTER_LEDX_SKIN_TRANSILLUMINATOR, craft => // LEDX - all requirement counts to 1
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.MEDICAL_CMS_SURGICAL_KIT, craft => // CMS surgical kit - medical tools count 2
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_MEDICAL_TOOLS);
            if (req != null) req.Count = 2;
        }),
        Entry(ItemTpl.MEDKIT_GRIZZLY_MEDICAL_KIT, craft => { craft.Count = 1; }), // Grizzly medical kit - count 1
        Entry(ItemTpl.STIM_SJ6_TGLABS_COMBAT_STIMULANT_INJECTOR, craft => { craft.Count = 3; }), // SJ6 TGLabs stimulant
        Entry(ItemTpl.INFO_TOPOGRAPHIC_SURVEY_MAPS, craft => { craft.Count = 2; }), // Topographic survey maps
        Entry(ItemTpl.INFO_MILITARY_FLASH_DRIVE, craft => // Military flash drive - full rework
        {
            craft.Count = 1;
            var secure = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.INFO_SECURE_FLASH_DRIVE);
            if (secure != null) secure.TemplateId = ItemTpl.BARTER_VPX_FLASH_STORAGE_MODULE; // replace secure flash drive >> VPX
            var area = craft.Requirements.FirstOrDefault(r => r.Type == "Area");
            if (area != null) area.RequiredLevel = 2;
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.INFO_INTELLIGENCE_FOLDER, craft => // Intelligence folder - military flash drive count 1
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.INFO_MILITARY_FLASH_DRIVE);
            if (req != null) req.Count = 1;
        }),
        Entry(ItemTpl.BARTER_VPX_FLASH_STORAGE_MODULE, craft => // VPX flash storage module - all counts to 2
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 2;
        }),
        Entry(ItemTpl.BARTER_VIRTEX_PROGRAMMABLE_PROCESSOR, craft => // Virtex programmable processor - military circuit board count 1
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_MILITARY_CIRCUIT_BOARD);
            if (req != null) req.Count = 1;
        }),
        Entry(ItemTpl.BARTER_GRAPHICS_CARD, craft => // Graphics card - replace VPX>>Virtex, CPU count 1, PCB count 1
        {
            var vpx = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_VPX_FLASH_STORAGE_MODULE);
            if (vpx != null) { vpx.Count = 1; vpx.TemplateId = ItemTpl.BARTER_VIRTEX_PROGRAMMABLE_PROCESSOR; } // VPX>>Virtex
            var cpu = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_PC_CPU);
            if (cpu != null) cpu.Count = 1;
            var pcb = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_PRINTED_CIRCUIT_BOARD);
            if (pcb != null) pcb.Count = 1;
        }),
        Entry(ItemTpl.BARTER_MILITARY_CIRCUIT_BOARD, craft => { craft.Count = 2; }), // Military circuit board
        Entry(ItemTpl.SPECIALSCOPE_FLIR_RS32_2259X_35MM_60HZ_THERMAL_RIFLESCOPE, craft => // FLIR RS-32 thermal scope - all counts to 1, replace SAS drive >> Armasight Vulcan
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
            var sas = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.INFO_SAS_DRIVE);
            if (sas != null) sas.TemplateId = ItemTpl.SPECIALSCOPE_ARMASIGHT_VULCAN_MG_35X_BRAVO_NIGHT_VISION_SCOPE; // Armasight Vulcan MG night vision scope
        }),
        Entry(ItemTpl.BARTER_UHF_RFID_READER, craft => // UHF RFID reader - full requirements rework
        {
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.IntelligenceCenter, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.BARTER_BROKEN_GPHONE_X_SMARTPHONE, Count = 1, IsFunctional = false, Type = "Item" }, // Broken GPhone
                new() { TemplateId = ItemTpl.SPECITEM_SIGNAL_JAMMER, Count = 1, IsFunctional = false, Type = "Item" }, // Signal jammer
                new() { TemplateId = ItemTpl.BARTER_FLAT_SCREWDRIVER_LONG, Type = "Tool" }, // Flat screwdriver (long)
                new() { TemplateId = ItemTpl.BARTER_FLAT_SCREWDRIVER , Type = "Tool" }, // Flat screwdriver
                new() { Type = "QuestComplete", QuestId = "63966fccac6f8f3c677b9d89" },
            };
        }),
        Entry(ItemTpl.BARTER_GAS_ANALYZER, craft => // Gas analyzer - all counts to 1
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.BARTER_GUNPOWDER_HAWK, craft => // Gunpowder "Hawk" - replace classic matches >> thermite, workbench lv2
            {
                var matches = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_CLASSIC_MATCHES);
                if (matches != null) matches.TemplateId = ItemTpl.BARTER_CAN_OF_THERMITE; // Can of thermite
                var area = craft.Requirements.FirstOrDefault(r => r.Type == "Area");
                if (area != null) area.RequiredLevel = 2;
            },
            craft => craft.Requirements.Any(r => r.TemplateId == ItemTpl.BARTER_CLASSIC_MATCHES)),
        Entry(ItemTpl.BARTER_SPARK_PLUG, craft => { craft.Count = 4; }), // Spark plug
        Entry(ItemTpl.BARTER_PRINTED_CIRCUIT_BOARD, craft => // Printed circuit board - count 3, replace gas analyzer req >> Geiger-Muller counter
            {
                craft.Count = 3;
                var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_GAS_ANALYZER);
                if (req != null) req.TemplateId = ItemTpl.BARTER_GEIGERMULLER_COUNTER; // Gas analyzer >> Geiger-Muller counter
            },
            craft => craft.Requirements.Any(r => r.TemplateId == ItemTpl.BARTER_GAS_ANALYZER)),
        Entry(ItemTpl.BARTER_GEIGERMULLER_COUNTER, craft => // Geiger-Muller counter - full requirements rework
        {
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 1, Type = "Area" },
                new() { TemplateId = ItemTpl.BARTER_GAS_ANALYZER, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Gas analyzer
                new() { TemplateId = ItemTpl.BARTER_TOOLSET, Type = "Tool" }, // Toolset
            };
        }),
        Entry(ItemTpl.BARTER_GREENBAT_LITHIUM_BATTERY, craft => // GreenBat lithium battery - count 2, full requirements rework
        {
            craft.Count = 2;
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.BARTER_PORTABLE_POWERBANK, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Portable powerbank
                new() { TemplateId = ItemTpl.BARTER_ROUND_PLIERS, Type = "Tool" }, // Round pliers
            };
        }),
        Entry(ItemTpl.GRENADE_VOG25_KHATTABKA_IMPROVISED_HAND, craft => // VOG-25 Khattabka improvised hand grenade - all counts to 2
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 2;
        }),
        Entry(ItemTpl.BARTER_BROKEN_LCD, craft => // Broken LCD - count 1, all req counts to 1
        {
            craft.Count = 1;
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.AMMO_23X75_ZVEZDA, craft => // 23x75mm Zvezda flashbang round - count 20, full rework
        {
            craft.Count = 20;
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.BARTER_GUNPOWDER_EAGLE, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Gunpowder Eagle
                new() { TemplateId = ItemTpl.AMMO_23X75_SHRAP10, Count = 20, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 23x75 Shrapnel-10
                new() { TemplateId = ItemTpl.GRENADE_ZARYA_STUN, Count = 2, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Zarya stun grenade
                new() { TemplateId = ItemTpl.BARTER_TOOLSET, Type = "Tool" }, // Toolset
                new() { TemplateId = ItemTpl.MULTITOOLS_LEATHERMAN_MULTITOOL , Type = "Tool" }, // Leatherman multitool
            };
        }),
        Entry(ItemTpl.BARTER_RECHARGEABLE_BATTERY, craft => // Rechargeable battery - replace powerbank >> electric drill
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.BARTER_PORTABLE_POWERBANK );
            if (req != null) req.TemplateId = ItemTpl.BARTER_ELECTRIC_DRILL; // Portable powerbank >> electric drill
        }),
        Entry(ItemTpl.BARTER_CAN_OF_THERMITE, craft => // Can of thermite - replace dorm 308 key >> Damascus knife
        {
            var req = craft.Requirements.FirstOrDefault(r => r.TemplateId == ItemTpl.KEY_DORM_ROOM_308);
            if (req != null) req.TemplateId = ItemTpl.KNIFE_BARS_A2607_DAMASCUS; // Dorm 308 key >> Bars A-2607 Damascus knife
        }),
        Entry(ItemTpl.AMMO_45ACP_AP, craft => // .45 ACP AP - count 120, full rework
        {
            craft.Count = 120;
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.AMMO_45ACP_LASERMATCH, Count = 120, IsFunctional = false, IsEncoded = false, Type = "Item" }, // .45 ACP Lasermatch FMJ
                new() { TemplateId = ItemTpl.MULTITOOLS_LEATHERMAN_MULTITOOL, Type = "Tool" }, // Leatherman multitool
                new() { TemplateId = ItemTpl.BARTER_GUNPOWDER_EAGLE, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Gunpowder Eagle
                new() { TemplateId = ItemTpl.BARTER_PACK_OF_NAILS, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Pack of nails
                new() { TemplateId = ItemTpl.BARTER_SET_OF_FILES_MASTER, Type = "Tool" }, // Set of files (Master)
            };
        }),
        // C# reference only — kept commented out as in productionAdjustments.ts
        // Entry(ItemTpl.AMMO_57X28_SS190, craft => // 5.7x28mm SS190 - full rework
        // {
        //     craft.Requirements = new()
        //     {
        //         new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
        //         new() { TemplateId = ItemTpl.BARTER_HAND_DRILL, Type = "Tool" }, // Hand drill
        //         new() { TemplateId = ItemTpl.BARTER_PLIERS_ELITE, Type = "Tool" }, // Pliers (Elite)
        //         new() { TemplateId = ItemTpl.AMMO_57X28_SS197SR, Count = 180, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 5.7x28mm SS197SR
        //         new() { TemplateId = ItemTpl.BARTER_GUNPOWDER_HAWK, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Gunpowder "Hawk"
        //         new() { TemplateId = ItemTpl.BARTER_PACK_OF_NAILS, Count = 2, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Pack of nails
        //     };
        // }),
        Entry(ItemTpl.AMMO_556X45_SOST, craft => // 5.56x45mm MK 318 Mod 0 (SOST) - full rework
        {
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.AMMO_556X45_HP, Count = 150, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 5.56x45 HP
                new() { TemplateId = ItemTpl.BARTER_GUNPOWDER_EAGLE, Count = 1, IsFunctional = false, IsEncoded = false, Type = "Item" }, // Gunpowder Eagle
                new() { TemplateId = ItemTpl.BARTER_PLIERS_ELITE, Type = "Tool" }, // Pliers (Elite)
            };
        }),
        Entry(ItemTpl.AMMO_9X18PM_PSTM, craft => // 9x18mm PMM PstM - push additional base ammo requirement
        {
            craft.Requirements.Add(new()
            {
                TemplateId = ItemTpl.AMMO_9X18PM_PST, Count = 140, // 9x18mm PM PST
                IsFunctional = false, IsEncoded = false, Type = "Item",
            });
        }),
        Entry(ItemTpl.AMMO_12G_AP20, craft => // 12/70 AP-20 armor-piercing slug - full rework
        {
            craft.Requirements = new()
            {
                new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.AMMO_12G_MAGNUM, Count = 80, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 12/70 8.5mm Magnum buckshot
                new() { TemplateId = ItemTpl.AMMO_9X19_AP_63, Count = 80, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 9x19mm AP 6.3
                new() { TemplateId = ItemTpl.BARTER_NIPPERS, Type = "Tool" }, // Nippers
                new() { TemplateId = ItemTpl.BARTER_FLAT_SCREWDRIVER_LONG, Type = "Tool" }, // Flat screwdriver (long)
                new() { Type = "QuestComplete", QuestId = "6179ad0a6e9dd54ac275e3f2" },
            };
        }),
        // C# reference only — kept commented out as in productionAdjustments.ts
        // Entry(ItemTpl.AMMO_366TKM_APM, craft => // .366 TKM AP-M - full rework
        // {
        //     craft.Requirements = new()
        //     {
        //         new() { AreaType = (int?)HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
        //         new() { TemplateId = ItemTpl.AMMO_9X39_SPP, Count = 100, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 9x39mm SPP gs
        //         new() { TemplateId = ItemTpl.AMMO_762X39_HP, Count = 100, IsFunctional = false, IsEncoded = false, Type = "Item" }, // 7.62x39mm HP
        //         new() { TemplateId = ItemTpl.BARTER_PLIERS, Type = "Tool" }, // Pliers
        //         new() { Type = "QuestComplete", QuestId = "5bc47dbf86f7741ee74e93b9" },
        //     };
        // }),
        Entry(ItemTpl.BARTER_OFZ_30X165MM_SHELL, craft => // OFZ 30x165mm shell - all counts to 1
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.GRENADE_RGD5_HAND, craft => // RGD-5 hand grenade - all counts to 1
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.GRENADE_ZARYA_STUN, craft => // Zarya stun grenade - all counts to 1
        {
            foreach (var req in craft.Requirements.Where(r => r.Count.HasValue))
                req.Count = 1;
        }),
        Entry(ItemTpl.AMMO_12G_PIRANHA, craft => { craft.Count = 150; }), // 12/70 Piranha - count 150
        Entry(ItemTpl.AMMO_545X39_BP, craft => { craft.Count = 180; }), // 5.45x39mm BP - count 180
        Entry(ItemTpl.AMMO_556X45_M855A1, craft => { craft.Count = 180; }), // 5.56x45mm M855A1 - count 180
    };
}
