using SPTarkov.Server.Core.Models.Eft.Hideout;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Enums.Hideout;

namespace Softcore.Assets;
public static class Recipes
{
    // Container crafting recipes

    public static HideoutProduction ContainerAlpha => new()
    {
        Id = "63da4dbee8fa73e22500001a",
        AreaType = HideoutAreas.Workbench,
        Requirements = new()
        {
            new() { AreaType = (int) HideoutAreas.Workbench, RequiredLevel = 1, Type = "Area" },
            new() { TemplateId = ItemTpl.LOCKABLECONTAINER_PISTOL_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_SIMPLE_WALLET, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_DOGTAG_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.INFO_SECURE_FLASH_DRIVE, Count = 2, IsFunctional = false, Type = "Item" },
        },
        ProductionTime = 5600,
        EndProduct = ItemTpl.SECURE_CONTAINER_ALPHA,
        IsEncoded = false,
        Locked = false,
        NeedFuelForAllProductionTime = true,
        Continuous = false,
        Count = 1,
        ProductionLimitCount = 0,
        IsCodeProduction = false,
    };

    public static HideoutProduction ContainerBeta => new()
    {
        Id = "63da4dbee8fa73e22500001b",
        AreaType = HideoutAreas.Workbench,
        Requirements = new()
        {
            new() { AreaType = (int) HideoutAreas.Workbench, RequiredLevel = 1, Type = "Area" },
            new() { TemplateId = ItemTpl.SECURE_CONTAINER_ALPHA, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_AMMUNITION_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_DOCUMENTS_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.INFO_MILITARY_FLASH_DRIVE, Count = 2, IsFunctional = false, Type = "Item" },
        },
        ProductionTime = 10800,
        EndProduct = ItemTpl.SECURE_CONTAINER_BETA,
        IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = true,
        Continuous = false, Count = 1, ProductionLimitCount = 0, IsCodeProduction = false,
    };

    public static HideoutProduction ContainerEpsilon => new()
    {
        Id = "63da4dbee8fa73e22500001c",
        AreaType = HideoutAreas.Workbench,
        Requirements = new()
        {
            new() { AreaType = (int) HideoutAreas.Workbench, RequiredLevel = 2, Type = "Area" },
            new() { TemplateId = ItemTpl.SECURE_CONTAINER_BETA, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_MAGAZINE_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.KEY_DORM_ROOM_203, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_KEYCARD_HOLDER_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.INFO_SECURE_MAGNETIC_TAPE_CASSETTE, Count = 2, IsFunctional = false, Type = "Item" },
        },
        ProductionTime = 35000,
        EndProduct = ItemTpl.SECURE_CONTAINER_EPSILON,
        IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = true,
        Continuous = false, Count = 1, ProductionLimitCount = 0, IsCodeProduction = false,
    };

    public static HideoutProduction ContainerGamma => new()
    {
        Id = "63da4dbee8fa73e22500001d",
        AreaType = HideoutAreas.Workbench,
        Requirements = new()
        {
            new() { AreaType = (int) HideoutAreas.Workbench, RequiredLevel = 3, Type = "Area" },
            new() { TemplateId = ItemTpl.SECURE_CONTAINER_EPSILON, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_GRENADE_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_MONEY_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_SICC, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.CONTAINER_INJECTOR_CASE, Count = 2, IsFunctional = false, Type = "Item" },
            new() { TemplateId = ItemTpl.BARTER_MICROCONTROLLER_BOARD, Count = 2, IsFunctional = false, Type = "Item" },
        },
        ProductionTime = 61200,
        EndProduct = ItemTpl.SECURE_CONTAINER_GAMMA,
        IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = true,
        Continuous = false, Count = 1, ProductionLimitCount = 0, IsCodeProduction = false,
    };

    public static List<HideoutProduction> ContainerRecipes =>
        new() { ContainerAlpha, ContainerBeta, ContainerEpsilon, ContainerGamma };

    // Additional crafting recipes

    public static List<HideoutProduction> AdditionalRecipes => new()
    {
        // 3BTG
        new() { Id = "63da4dbee8fa73e225000006", AreaType = HideoutAreas.MedStation, ProductionTime = 31,
            EndProduct = ItemTpl.STIM_3BTG_STIMULANT_INJECTOR, Count = 2,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_ADRENALINE_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_BOTTLE_OF_HYDROGEN_PEROXIDE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.FOOD_ALYONKA_CHOCOLATE_BAR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // Adrenaline
        new() { Id = "63da4dbee8fa73e225000005", AreaType = HideoutAreas.MedStation, ProductionTime = 23,
            EndProduct = ItemTpl.STIM_ADRENALINE_INJECTOR, Count = 1,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.DRINK_CAN_OF_HOT_ROD_ENERGY, Count = 3, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.MEDKIT_AI2, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // L1 (Norepinephrine)
        new() { Id = "63da4dbee8fa73e225000009", AreaType = HideoutAreas.MedStation, ProductionTime = 71,
            EndProduct = ItemTpl.STIM_L1_NOREPINEPHRINE_INJECTOR, Count = 1,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_ADRENALINE_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.STIM_SJ6_TGLABS_COMBAT_STIMULANT_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // AHF1
        new() { Id = "63da4dbee8fa73e225000007", AreaType = HideoutAreas.MedStation, ProductionTime = 47,
            EndProduct = ItemTpl.STIM_AHF1M_STIMULANT_INJECTOR, Count = 1,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.DRUGS_AUGMENTIN_ANTIBIOTIC_PILLS, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRUGS_MORPHINE_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // CALOK-B
        new() { Id = "63da4dbee8fa73e225000004", AreaType = HideoutAreas.MedStation, ProductionTime = 48,
            EndProduct = ItemTpl.MEDICAL_CALOKB_HEMOSTATIC_APPLICATOR, Count = 2,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 2, Type = "Area" },
                new() { TemplateId = ItemTpl.BARTER_PACK_OF_SODIUM_BICARBONATE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRUGS_VASELINE_BALM, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // Ophthalmoscope
        new() { Id = "63da4dbee8fa73e225000001", AreaType = HideoutAreas.MedStation, ProductionTime = 105,
            EndProduct = ItemTpl.BARTER_OPHTHALMOSCOPE, Count = 1,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.BARTER_GREENBAT_LITHIUM_BATTERY, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_MEDICAL_TOOLS, Count = 2, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.FLASHLIGHT_ULTRAFIRE_WF501B, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.SPECITEM_WIFI_CAMERA, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_DUCT_TAPE, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // Zagustin
        new() { Id = "63da4dbee8fa73e225000002", AreaType = HideoutAreas.MedStation, ProductionTime = 105,
            EndProduct = ItemTpl.STIM_ZAGUSTIN_HEMOSTATIC_DRUG_INJECTOR, Count = 3,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_PROPITAL_REGENERATIVE_STIMULANT_INJECTOR, Count = 2, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.MEDICAL_CALOKB_HEMOSTATIC_APPLICATOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.STIM_AHF1M_STIMULANT_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // Obdolbos
        new() { Id = "63da4dbee8fa73e225000003", AreaType = HideoutAreas.MedStation, ProductionTime = 564,
            EndProduct = ItemTpl.STIM_OBDOLBOS_COCKTAIL_INJECTOR, Count = 8,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                // Did you always want to run your own meth lab in Tarkov? Now you can.
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_SJ1_TGLABS_COMBAT_STIMULANT_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_FUEL_CONDITIONER, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_SMOKED_CHIMNEY_DRAIN_CLEANER, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_BOTTLE_OF_PEVKO_LIGHT_BEER, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_BOTTLE_OF_TARKOVSKAYA_VODKA, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_BOTTLE_OF_DAN_JACKIEL_WHISKEY, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_FP100_FILTER_ABSORBER, Type = "Tool" },
            }},
        // OLOLO
        new() { Id = "63da4dbee8fa73e225000008", AreaType = HideoutAreas.Kitchen, ProductionTime = 71,
            EndProduct = ItemTpl.BARTER_BOTTLE_OF_OLOLO_MULTIVITAMINS, Count = 3,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.Kitchen, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.DRINK_PACK_OF_GRAND_JUICE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_PACK_OF_VITA_JUICE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_PACK_OF_APPLE_JUICE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_CAN_OF_ICE_GREEN_TEA, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRINK_PACK_OF_RUSSIAN_ARMY_PINEAPPLE_JUICE, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.DRUGS_ANALGIN_PAINKILLERS, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.BARTER_WATER_FILTER, Type = "Tool" },
                new() { TemplateId = ItemTpl.BARTER_ANTIQUE_TEAPOT, Type = "Tool" },
            }},
        // Perfotran
        new() { Id = "63da4dbee8fa73e225000014", AreaType = HideoutAreas.MedStation, ProductionTime = 45,
            EndProduct = ItemTpl.STIM_PERFOTORAN_BLUE_BLOOD_STIMULANT_INJECTOR, Count = 2,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_ZAGUSTIN_HEMOSTATIC_DRUG_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.STIM_XTG12_ANTIDOTE_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.STIM_PROPITAL_REGENERATIVE_STIMULANT_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // Trimadol
        new() { Id = "63da4dbee8fa73e225000011", AreaType = HideoutAreas.MedStation, ProductionTime = 52,
            EndProduct = ItemTpl.STIM_TRIMADOL_STIMULANT_INJECTOR, Count = 2,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_3BTG_STIMULANT_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.STIM_L1_NOREPINEPHRINE_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
        // Meldonin
        new() { Id = "63da4dbee8fa73e225000012", AreaType = HideoutAreas.MedStation, ProductionTime = 39,
            EndProduct = ItemTpl.STIM_MELDONIN_INJECTOR, Count = 2,
            IsEncoded = false, Locked = false, NeedFuelForAllProductionTime = false, Continuous = false, ProductionLimitCount = 0, IsCodeProduction = false,
            Requirements = new()
            {
                new() { AreaType = (int) HideoutAreas.MedStation, RequiredLevel = 3, Type = "Area" },
                new() { TemplateId = ItemTpl.STIM_L1_NOREPINEPHRINE_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
                new() { TemplateId = ItemTpl.STIM_MULE_STIMULANT_INJECTOR, Count = 1, IsFunctional = false, Type = "Item" },
            }},
    };
}
