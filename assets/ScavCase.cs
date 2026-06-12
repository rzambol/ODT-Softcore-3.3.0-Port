using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Enums;
namespace Softcore.Assets;

public record ScavRewardRange(int Min, int Max);

public record ScavEndProducts(ScavRewardRange Common, ScavRewardRange Rare, ScavRewardRange Superrare);

public record ScavRecipeRequirement(string TemplateId, int Count, string Type = "Item");

public record ScavRecipe(
    string Id,
    List<ScavRecipeRequirement> Requirements,
    int ProductionTime,
    ScavEndProducts EndProducts
);
public static class ScavCase
{
    // Reward value ranges
    // AVG 7941
    public static readonly ScavRewardRange CommonRange = new(1, 20000);
    // AVG 36415
    public static readonly ScavRewardRange RareRange = new(20001, 60000);
    // AVG 157978
    public static readonly ScavRewardRange SuperrareRange = new(60001, 1200000);

    // Reworked scav case recipes
    public static readonly List<ScavRecipe> ReworkedRecipes = new()
    {
        new ScavRecipe(
            "62710974e71632321e5afd5f",
            new() { new(ItemTpl.DRINK_BOTTLE_OF_PEVKO_LIGHT_BEER, 1) }, // Pevko Light Beer
            2500,
            new(new(3, 3), new(0, 0), new(0, 0))
        ),
        new ScavRecipe(
            "62710a8c403346379e3de9be",
            new() { new(ItemTpl.DRINK_BOTTLE_OF_TARKOVSKAYA_VODKA, 1) }, // Tarkovskaya vodka
            7700,
            new(new(3, 4), new(0, 1), new(0, 0))
        ),
        new ScavRecipe(
            "62710a69adfbd4354d79c58e",
            new() { new(ItemTpl.DRINK_BOTTLE_OF_DAN_JACKIEL_WHISKEY, 1) }, // Dan Jackiel whiskey
            8100,
            new(new(4, 5), new(1, 2), new(0, 0))
        ),
        new ScavRecipe(
            "6271093e621b0a76055cd61e",
            new() { new(ItemTpl.DRINK_BOTTLE_OF_FIERCE_HATCHLING_MOONSHINE, 1) }, // Fierce Hatchling Moonshine
            16800,
            new(new(1, 3), new(0, 3), new(0, 2))
        ),
        new ScavRecipe(
            "62710a0e436dcc0b9c55f4ec",
            new() { new(ItemTpl.INFO_INTELLIGENCE_FOLDER, 1) }, // Intelligence folder
            19200,
            new(new(3, 3), new(3, 5), new(1, 1))
        ),
    };

    // Base class whitelist
    public static readonly HashSet<string> Whitelist = new()
    {
        BaseClasses.ASSAULT_RIFLE.ToString(), // AssaultRifle
        BaseClasses.AMMO_BOX.ToString(), // AmmoBox
        BaseClasses.KEY_MECHANICAL.ToString(), // KeyMechanical
        BaseClasses.PISTOL.ToString(), // Pistol
        BaseClasses.THROW_WEAP.ToString(), // ThrowWeap
        // BaseClasses.MAGAZINE.ToString(), // Magazine
        BaseClasses.DRINK.ToString(), // Drink
        BaseClasses.FOOD.ToString(), // Food
        BaseClasses.MONEY.ToString(), // Money
        // BaseClasses.TACTICAL_COMBO.ToString(), // TacticalCombo
        // BaseClasses.SILENCER.ToString(), // Silencer
        BaseClasses.KNIFE.ToString(), // Knife
        BaseClasses.SHOTGUN.ToString(), // Shotgun
        // BaseClasses.MOB_CONTAINER.ToString(), // MobContainer
        // BaseClasses.FLASH_HIDER.ToString(), // FlashHider
        // BaseClasses.ASSAULT_SCOPE.ToString(), // AssaultScope
        // BaseClasses.OPTIC_SCOPE.ToString(), // OpticScope
        BaseClasses.VEST.ToString(), // Vest
        BaseClasses.BACKPACK.ToString(), // Backpack
        BaseClasses.MEDICAL.ToString(), // Medical
        BaseClasses.DRUGS.ToString(), // Drugs
        BaseClasses.MED_KIT.ToString(), // MedKit
        BaseClasses.MULTITOOLS.ToString(), // Multitools
        BaseClasses.AMMO.ToString(), // Ammo
        BaseClasses.ARMOR.ToString(), // Armor
        // BaseClasses.VISORS.ToString(), // Visors
        // BaseClasses.POCKETS.ToString(), // Pockets
        // BaseClasses.BARREL.ToString(), // Barrel
        BaseClasses.SNIPER_RIFLE.ToString(), // SniperRifle
        // BaseClasses.COLLIMATOR.ToString(), // Collimator
        // BaseClasses.MUZZLE_COMBO.ToString(), // MuzzleCombo
        // BaseClasses.PISTOL_GRIP.ToString(), // PistolGrip
        // BaseClasses.FOREGRIP.ToString(), // Foregrip
        // BaseClasses.RECEIVER.ToString(), // Receiver
        // BaseClasses.CHARGE.ToString(), // Charge
        // BaseClasses.HANDGUARD.ToString(), // Handguard
        // BaseClasses.MOUNT.ToString(), // Mount
        // BaseClasses.STOCK.ToString(), // Stock
        // BaseClasses.IRON_SIGHT.ToString(), // IronSight
        // BaseClasses.INVENTORY.ToString(), // Inventory
        // BaseClasses.AUXILIARY_MOD.ToString(), // AuxiliaryMod
        // BaseClasses.HEADWEAR.ToString(), // Headwear
        BaseClasses.HEADPHONES.ToString(), // Headphones
        BaseClasses.LAUNCHER.ToString(), // Launcher
        // BaseClasses.STASH.ToString(), // Stash
        // BaseClasses.LOCKABLE_CONTAINER.ToString(), // LockableContainer
        BaseClasses.BATTERY.ToString(), // Battery
        BaseClasses.ELECTRONICS.ToString(), // Electronics
        BaseClasses.LUBRICANT.ToString(), // Lubricant
        // BaseClasses.BIPOD.ToString(), // Bipod
        // BaseClasses.GASBLOCK.ToString(), // Gasblock
        BaseClasses.NIGHT_VISION.ToString(), // NightVision
        // BaseClasses.FACE_COVER.ToString(), // FaceCover
        BaseClasses.JEWELRY.ToString(), // Jewelry
        // BaseClasses.OTHER.ToString(), // Other
        BaseClasses.BUILDING_MATERIAL.ToString(), // BuildingMaterial
        BaseClasses.HOUSEHOLD_GOODS.ToString(), // HouseholdGoods
        BaseClasses.ASSAULT_CARBINE.ToString(), // AssaultCarbine
        // BaseClasses.MAP.ToString(), // Map
        // BaseClasses.COMPACT_COLLIMATOR.ToString(), // CompactCollimator
        BaseClasses.MARKSMAN_RIFLE.ToString(), // MarksmanRifle
        BaseClasses.SIMPLE_CONTAINER.ToString(), // SimpleContainer
        // BaseClasses.LOOT_CONTAINER.ToString(), // LootContainer
        BaseClasses.SMG.ToString(), // Smg
        // BaseClasses.FLASHLIGHT.ToString(), // Flashlight
        BaseClasses.TOOL.ToString(), // Tool
        BaseClasses.INFO.ToString(), // Info
        BaseClasses.REPAIR_KITS.ToString(), // RepairKits
        // BaseClasses.SPEC_ITEM.ToString(), // SpecItem
        BaseClasses.MEDICAL_SUPPLIES.ToString(), // MedicalSupplies
        BaseClasses.ARMORED_EQUIPMENT.ToString(), // ArmoredEquipment
        BaseClasses.SPECIAL_SCOPE.ToString(), // SpecialScope
        // BaseClasses.ARM_BAND.ToString(), // ArmBand
        BaseClasses.MACHINE_GUN.ToString(), // MachineGun
        BaseClasses.STIMULATOR.ToString(), // Stimulator
        BaseClasses.THERMAL_VISION.ToString(), // ThermalVision
        BaseClasses.KEYCARD.ToString(), // Keycard
        BaseClasses.FUEL.ToString(), // Fuel
        BaseClasses.GRENADE_LAUNCHER.ToString(), // GrenadeLauncher
        // BaseClasses.COMPASS.ToString(), // Compass
        // BaseClasses.SORTING_TABLE.ToString(), // SortingTable
        BaseClasses.REVOLVER.ToString(), // Revolver
        // BaseClasses.CYLINDER_MAGAZINE.ToString(), // CylinderMagazine
        // BaseClasses.PORTABLE_RANGE_FINDER.ToString(), // PortableRangeFinder
        // BaseClasses.SPRING_DRIVEN_CYLINDER.ToString(), // SpringDrivenCylinder
        // BaseClasses.RADIO_TRANSMITTER.ToString(), // RadioTransmitter
        // BaseClasses.RANDOM_LOOT_CONTAINER.ToString(), // RandomLootContainer
        // BaseClasses.HIDEOUT_AREA_CONTAINER.ToString(), // HideoutAreaContainer
        // BaseClasses.BUILT_IN_INSERTS.ToString(), // BuiltInInserts
        BaseClasses.ARMOR_PLATE.ToString(), // ArmorPlate
        // BaseClasses.CULTIST_AMULET.ToString(), // CultistAmulet
        // BaseClasses.MARK_OF_UNKNOWN.ToString(), // MarkOfUnknown
        // BaseClasses.PLANTING_KITS.ToString(), // PlantingKits
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in scavcase.ts
    public static readonly HashSet<MongoId> WhitelistMongoIds = Whitelist
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // Item blacklist
    public static readonly HashSet<string> ItemBlacklist = new()
    {
        // Blacklist (itemIDs) to always exclude items from reward list
        ItemTpl.INFO_ENCRYPTED_FLASH_DRIVE.ToString(), // Encrypted flash drive: 10000
        ItemTpl.BARTER_MICROCONTROLLER_BOARD.ToString(), // Microcontroller board
        ItemTpl.BARTER_FARFORWARD_GPS_SIGNAL_AMPLIFIER_UNIT.ToString(), // Far-forward GPS Signal Amplifier Unit
        ItemTpl.BARTER_ADVANCED_CURRENT_CONVERTER.ToString(), // Advanced current converter
        ItemTpl.INFO_SILICON_OPTOELECTRONIC_INTEGRATED_CIRCUITS_TEXTBOOK.ToString(), // Silicon Optoelectronic Integrated Circuits textbook
        ItemTpl.INFO_ADVANCED_ELECTRONIC_MATERIALS_TEXTBOOK.ToString(), // Advanced Electronic Materials textbook
        ItemTpl.KEY_BACKUP_HIDEOUT.ToString(), // Backup hideout key
        ItemTpl.SPECITEM_RADIO_REPEATER.ToString(), // Radio repeater
        ItemTpl.KEY_PRIMORSKY_4648_SKYBRIDGE.ToString(), // Primorsky 46-48 skybridge key
        ItemTpl.CULTISTAMULET_SACRED_AMULET.ToString(), // Sacred Amulet
        ItemTpl.KEY_RUSTED_BLOODY.ToString(), // Rusted bloody key
        ItemTpl.MARKOFUNKNOWN_MARK_OF_THE_UNHEARD.ToString(), // Mark of The Unheard
        ItemTpl.ARMBAND_OF_THE_UNHEARD.ToString(), // Armband of The Unheard
        ItemTpl.INFO_DECRYPTED_FLASH_DRIVE.ToString(), // Decrypted flash drive
        ItemTpl.INFO_DOCUMENTS_WITH_DECRYPTED_DATA.ToString(), // Documents with decrypted data
        ItemTpl.ARMBAND_ARENA.ToString(), // Armband (ARENA)
        ItemTpl.ARMBAND_ALPHA.ToString(), // Armband (Alpha)
        ItemTpl.ARMBAND_DEADSKUL.ToString(), // Armband (DEADSKUL)
        ItemTpl.ARMBAND_TRAIN_HARD.ToString(), // Armband (Train Hard)
        ItemTpl.ARMBAND_KIBA_ARMS.ToString(), // Armband (Kiba Arms)
        ItemTpl.ARMBAND_RFARMY.ToString(), // Armband (RFARMY)
        ItemTpl.ARMBAND_UNTAR.ToString(), // Armband (UNTAR)
        ItemTpl.KEY_SHATUNS_HIDEOUT.ToString(), // Shatun's hideout key
        ItemTpl.KEY_GRUMPYS_HIDEOUT.ToString(), // Grumpy's hideout key
        ItemTpl.KEY_VORONS_HIDEOUT.ToString(), // Voron's hideout key
        ItemTpl.KEY_LEONS_HIDEOUT.ToString(), // Leon's hideout key
        ItemTpl.SPECITEM_THE_EYE_MORTAR_STRIKE_SIGNALING_DEVICE.ToString(), // "The Eye" mortar strike signaling device
        ItemTpl.BARTER_LOCKED_EQUIPMENT_CRATE_RARE.ToString(), // Locked equipment crate (Rare)
        ItemTpl.BARTER_LOCKED_WEAPON_CRATE_RARE.ToString(), // Locked weapon crate (Rare)
        ItemTpl.BARTER_LOCKED_SUPPLY_CRATE_RARE.ToString(), // Locked supply crate (Rare)
        ItemTpl.BARTER_LOCKED_VALUABLES_CRATE_RARE.ToString(), // Locked valuables crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_GEARCRATE_BLUE_OPEN.ToString(), // Unlocked equipment crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_WEAPONCRATE_BLUE_OPEN.ToString(), // Unlocked weapon crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_JUNKCRATE_BLUE_OPEN.ToString(), // Unlocked supply crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_JEWELRYCRATE_BLUE_OPEN.ToString(), // Unlocked valuables crate (Rare)
        ItemTpl.BARTER_DOGTAG_BEAR_EOD.ToString(), // Dogtag BEAR
        ItemTpl.BARTER_DOGTAG_BEAR_TUE.ToString(), // Dogtag BEAR
        ItemTpl.BARTER_DOGTAG_USEC_EOD.ToString(), // Dogtag USEC
        ItemTpl.BARTER_DOGTAG_USEC_TUE.ToString(), // Dogtag USEC
        ItemTpl.PLANTINGKITS_TRIPWIRE_INSTALLATION_KIT.ToString(), // Tripwire installation kit
        ItemTpl.CONTAINER_STREAMER_ITEM_CASE.ToString(), // Streamer item case
        ItemTpl.FLARE_RSP30_REACTIVE_SIGNAL_CARTRIDGE_SPECIAL_YELLOW.ToString(), // RSP-30 reactive signal cartridge (Special Yellow)
        ItemTpl.BARTER_RADAR_STATION_SPARE_PARTS.ToString(), // Radar station spare parts
        ItemTpl.BARTER_KOSA_UAV_ELECTRONIC_JAMMING_DEVICE.ToString(), // KOSA UAV electronic jamming device
        ItemTpl.BARTER_GARY_ZONT_PORTABLE_ELECTRONIC_WARFARE_DEVICE.ToString(), // GARY ZONT portable electronic warfare device
        ItemTpl.RANDOMLOOTCONTAINER_EVENT_CONTAINER_CONTRABAND_MAIN.ToString(), // Opened case
        ItemTpl.BARTER_CONTRABAND_BOX.ToString(), // Contraband box
        ItemTpl.BARTER_SEALED_BOX.ToString(), // Sealed box
        ItemTpl.RANDOMLOOTCONTAINER_EVENT_CONTAINER_CONTRABAND_FAKE.ToString(), // Opened box
        ItemTpl.BARTER_LOCKED_CASE.ToString(), // Locked case
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_RED.ToString(), // Christmas tree ornament (Red): 7000
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_SILVER.ToString(), // Christmas tree ornament (Silver): 10000
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_VIOLET.ToString(), // Christmas tree ornament (Violet): 20000
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in scavcase.ts
    public static readonly HashSet<MongoId> ItemBlacklistMongoIds = ItemBlacklist
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // Reward parent blacklist
    public static readonly List<string> RewardParentBlacklist = new()
    {
        BaseClasses.AMMO.ToString(), // Ammo
        BaseClasses.MONEY.ToString(), // Money
        BaseClasses.MOB_CONTAINER.ToString(), // MobContainer (portable container)
        ItemTpl.MACHINEGUN_AGS30_30X29MM_AUTOMATIC_GRENADE_LAUNCHER, // AGS-30 launcher
        BaseClasses.RANDOM_LOOT_CONTAINER.ToString(), // RandomLootContainer
        BaseClasses.BUILT_IN_INSERTS.ToString(), // BuiltInInserts
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in scavcase.ts
    public static readonly HashSet<MongoId> RewardParentBlacklistMongoIds = RewardParentBlacklist
        .Select(static x => new MongoId(x))
        .ToHashSet();
}
