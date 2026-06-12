using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Common;

namespace Softcore.Assets;
public static class FleaMarket
{
    // Whitelist
    public static readonly HashSet<string> Whitelist = new()
    {
        // Whitelist for items that can be bartered FOR on flea, outside of fleaWhitelist categories
        // Can be modified by user
        ItemTpl.FACECOVER_GP7_GAS_MASK.ToString(),
        ItemTpl.VEST_SECURITY.ToString(),
        ItemTpl.BACKPACK_LOLKEK_3F_TRANSFER_TOURIST.ToString(),
        ItemTpl.KEY_DORM_ROOM_308.ToString(),
        ItemTpl.VISORS_ANTIFRAGMENTATION_GLASSES.ToString(),
        ItemTpl.FACECOVER_LOWER_HALFMASK.ToString(),
        ItemTpl.HEADWEAR_POMPON_HAT.ToString(),
        ItemTpl.HEADWEAR_USHANKA_EAR_FLAP_HAT.ToString(),
        ItemTpl.FACECOVER_PESTILY_PLAGUE_MASK.ToString(),
        ItemTpl.FACECOVER_NEOPRENE_MASK.ToString(),
        ItemTpl.FACECOVER_GP5_GAS_MASK.ToString(),
        ItemTpl.VISORS_ROUND_FRAME_SUNGLASSES.ToString(),
        ItemTpl.HEADWEAR_PSH97_DJETA_RIOT_HELMET.ToString(),
        ItemTpl.KNIFE_BARS_A2607_95KH18.ToString(),
        ItemTpl.KNIFE_BARS_A2607_DAMASCUS.ToString(),
        ItemTpl.HEADWEAR_LEATHER_CAP.ToString(),
        ItemTpl.BACKPACK_DUFFLE_BAG.ToString(),
        ItemTpl.HEADPHONES_WALKERS_XCEL_500BT_DIGITAL_HEADSET.ToString(),
        // ItemTpl.HEADPHONES_PELTOR_TACTICAL_SPORT_HEADSET.ToString(),
        ItemTpl.HEADPHONES_WALKERS_RAZOR_DIGITAL_HEADSET.ToString(),
        ItemTpl.HEADPHONES_PELTOR_COMTAC_IV_HYBRID_HEADSET_COYOTE_BROWN.ToString(),
        // ItemTpl.HEADWEAR_KINDA_COWBOY_HAT.ToString(),
        ItemTpl.PISTOL_MAKAROV_PM_T_9X18PM.ToString(),
        ItemTpl.KNIFE_SP8_SURVIVAL_MACHETE.ToString(),
        ItemTpl.AMMO_40MMRU_VOG25.ToString(),
        ItemTpl.CONTAINER_SIMPLE_WALLET.ToString(),
        ItemTpl.KNIFE_ER_FULCRUM_BAYONET.ToString(),
        ItemTpl.VISORS_RAYBENCH_HIPSTER_RESERVE_SUNGLASSES.ToString(),
        // ItemTpl.FACECOVER_SHEMAGH_GREEN.ToString(),
        // ItemTpl.FACECOVER_GHOST_BALACLAVA.ToString(),
        // ItemTpl.SILENCER_SUREFIRE_SOCOM556MINI_MONSTER_556X45_SOUND_SUPPRESSOR.ToString(),
        // ItemTpl.FOREGRIP_FORTIS_SHIFT_TACTICAL.ToString(), // Fortis Shift tactical foregrip
        // ItemTpl.PISTOLGRIP_AR15_HK_ERGO_PSG1_STYLE_PISTOL_GRIP.ToString(), // AR-15 HK Ergo PSG-1 style pistol grip
        // ItemTpl.MUZZLECOMBO_AK_CNC_WARRIOR_556X45_MUZZLE_DEVICE_ADAPTER.ToString(), // AK CNC Warrior 5.56x45 muzzle device adapter
        // ItemTpl.FOREGRIP_MAGPUL_AFG_TACTICAL_FOREGRIP_OLIVE_DRAB.ToString(), // Magpul AFG tactical foregrip (Olive Drab)
        ItemTpl.HEADWEAR_TACKEK_FAST_MT_HELMET_REPLICA.ToString(), // Tac-Kek FAST MT helmet (Replica)
        ItemTpl.BARTER_CASE_KEY.ToString(), // Contraband case key
        ItemTpl.BARREL_MK47_409MM.ToString(),
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in fleamarket.ts
    public static readonly HashSet<MongoId> WhitelistMongoIds = Whitelist
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // Base classes used by Barter Economy for barter request blacklist
    // Ported 1:1 from fleamarket.ts actualBaseClasses in SPT 3.11.
    public static readonly HashSet<string> ActualBaseClasses = new()
    {
        "566162e44bdc2d3f298b4573", // CompoundItem
        "54009119af1c881c07000029", // Item
        "5661632d4bdc2d903d8b456b", // StackableItem
        "5447b5f14bdc2d61278b4567", // AssaultRifle
        "543be5cb4bdc2deb348b4568", // AmmoBox
        "5422acb9af1c889c16000029", // Weapon
        "5c99f98d86f7745c314214b3", // KeyMechanical
        "55802f3e4bdc2de7118b4584", // GearMod
        "5447b5cf4bdc2d65278b4567", // Pistol
        "543be6564bdc2df4348b4568", // ThrowWeap
        "566168634bdc2d144c8b456c", // SearchableItem
        "5448bc234bdc2d3c308b4569", // Magazine
        "57bef4c42459772e8d35a53b", // ArmoredEquipment
        "543be6674bdc2df1348b4569", // FoodDrink
        "543be5664bdc2dd4348b4569", // Meds
        "550aa4154bdc2dd8348b456b", // FunctionalMod
        "5448e8d64bdc2dce718b4568", // Drink
        "5448e8d04bdc2ddf718b4569", // Food
        "543be5dd4bdc2deb348b4569", // Money
        "55818b164bdc2ddc698b456c", // TacticalCombo
        "550aa4cd4bdc2dd8348b456c", // Silencer
        "5447e1d04bdc2dff2f8b4567", // Knife
        "5447b6094bdc2dc3278b4567", // Shotgun
        "5448bf274bdc2dfc2f8b456a", // MobContainer
        "550aa4bf4bdc2dd6348b456b", // FlashHider
        "55818add4bdc2d5b648b456f", // AssaultScope
        "55818ae44bdc2dde698b456c", // OpticScope
        "5448e5284bdc2dcb718b4567", // Vest
        "5448e53e4bdc2d60728b4567", // Backpack
        "5448f3ac4bdc2dce718b4569", // Medical
        "5448f3a14bdc2d27728b4569", // Drugs
        "5448f39d4bdc2d0a728b4568", // MedKit
        "66abb0743f4d8b145b1612c1", // Multitools
        "5485a8684bdc2da71d8b4567", // Ammo
        "5448e54d4bdc2dcc718b4568", // Armor
        "5448fe124bdc2da5018b4567", // Mod
        "5448fe394bdc2d0d028b456c", // Muzzle
        "55802f4a4bdc2ddb688b4569", // MasterMod
        "5448e5724bdc2ddf718b4568", // Visors
        "557596e64bdc2dc2118b4571", // Pockets
        "555ef6e44bdc2de9068b457e", // Barrel
        "5447b6254bdc2dc3278b4568", // SniperRifle
        "55818ad54bdc2ddc698b4569", // Collimator
        "550aa4dd4bdc2dc9348b4569", // MuzzleCombo
        "55818a684bdc2ddd698b456d", // PistolGrip
        "55818af64bdc2d5b648b4570", // Foregrip
        "5448fe7a4bdc2d6f028b456b", // Sights
        "55818a304bdc2db5418b457d", // Receiver
        "55818a6f4bdc2db9688b456b", // Charge
        "55818a104bdc2db9688b4569", // Handguard
        "55818b224bdc2dde698b456f", // Mount
        "55818a594bdc2db9688b456a", // Stock
        "55818ac54bdc2d5b648b456e", // IronSight
        "55d720f24bdc2d88028b456d", // Inventory
        "5a74651486f7744e73386dd1", // AuxiliaryMod
        "5a341c4086f77401f2541505", // Headwear
        "543be5f84bdc2dd4348b456a", // Equipment
        "5645bcb74bdc2ded0b8b4578", // Headphones
        "55818b014bdc2ddc698b456b", // Launcher
        "566965d44bdc2d814c8b4571", // LootContainer
        "566abbb64bdc2d144c8b457d", // Stash
        "5671435f4bdc2d96058b4569", // LockableContainer
        "57864ee62459775490116fc1", // Battery
        "57864a66245977548f04a81f", // Electronics
        "57864e4c24597754843f8723", // Lubricant
        "567583764bdc2d98058b456e", // StationaryContainer
        "55818afb4bdc2dde698b456d", // Bipod
        "56ea9461d2720b67698b456f", // Gasblock
        "5a2c3a9486f774688b05e574", // NightVision
        "5a341c4686f77469e155819e", // FaceCover
        "57864a3d24597754843f8721", // Jewelry
        "590c745b86f7743cc433c5f2", // Other
        "57864ada245977548638de91", // BuildingMaterial
        "57864c322459775490116fbf", // HouseholdGoods
        "5447b5fc4bdc2d87278b4567", // AssaultCarbine
        "567849dd4bdc2d150f8b456e", // Map
        "5c164d2286f774194c5e69fa", // Keycard
        "55818acf4bdc2dde698b456b", // CompactCollimator
        "5447b6194bdc2d67278b4567", // MarksmanRifle
        "5795f317245977243854e041", // SimpleContainer
        "5448eb774bdc2d0a728b4567", // BarterItem
        "5447b5e04bdc2d62278b4567", // Smg
        "55818b084bdc2d5b648b4571", // Flashlight
        "57864bb7245977548b3b66c2", // Tool
        "5448ecbe4bdc2d60728b4568", // Info
        "616eb7aea207f41933308f46", // RepairKits
        "5447e0e74bdc2d3c308b4567", // SpecItem
        "57864c8c245977548867e7f1", // MedicalSupplies
        "55818aeb4bdc2ddc698b456a", // SpecialScope
        "5b3f15d486f77432d0509248", // ArmBand
        "5447bed64bdc2d97278b4568", // MachineGun
        "5448f3a64bdc2d60728b456a", // Stimulator
        "5d21f59b6dbe99052b54ef83", // ThermalVision
        "543be5e94bdc2df1348b4568", // Key
        "5d650c3e815116009f6201d2", // Fuel
        "5447bedf4bdc2d87278b4568", // GrenadeLauncher
        "5f4fbaaca5573a5ac31db429", // Compass
        "6050cac987d3f925bf016837", // SortingTable
        "617f1ef5e8b54b0998387733", // Revolver
        "610720f290b75a49ff2e5e25", // CylinderMagazine
        "61605ddea09d851a0a0c1bbc", // PortableRangeFinder
        "627a137bf21bc425b06ab944", // SpringDrivenCylinder
        "62e9103049c018f425059f38", // RadioTransmitter
        "62f109593b54472778797866", // RandomLootContainer
        "63da6da4784a55176c018dba", // HideoutAreaContainer
        "65649eb40bf0ed77b8044453", // BuiltInInserts
        "644120aa86ffbe10ee032b6f", // ArmorPlate
        "64b69b0c8f3be32ed22682f8", // CultistAmulet
        "65ddcc7aef36f6413d0829b9", // MarkOfUnknown
        "6672e40ebb23210ae87d39eb", // PlantingKits
        "6759673c76e93d8eb20b2080", // Flyer
    };

    public static readonly HashSet<MongoId> ActualBaseClassesMongoIds = ActualBaseClasses
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // Base class whitelist
    public static readonly HashSet<string> FleaBarterRequestWhitelist = new()
    {
        // Items that can be REQUESTED for flea offers
        // For user modability:
        // BaseClasses.WEAPON.ToString(),
        // BaseClasses.UBGL.ToString(),
        // BaseClasses.ARMOR.ToString(),
        // BaseClasses.ARMORED_EQUIPMENT.ToString(),
        // BaseClasses.REPAIR_KITS.ToString(),
        // BaseClasses.HEADWEAR.ToString(),
        // BaseClasses.FACECOVER.ToString(),
        // BaseClasses.VEST.ToString(),
        // BaseClasses.BACKPACK.ToString(),
        // BaseClasses.COMPOUND.ToString(),
        // BaseClasses.VISORS.ToString(),
        BaseClasses.FOOD.ToString(),
        // BaseClasses.GAS_BLOCK.ToString(),
        // BaseClasses.RAIL_COVER.ToString(),
        BaseClasses.DRINK.ToString(),
        BaseClasses.BARTER_ITEM.ToString(),
        BaseClasses.INFO.ToString(),
        BaseClasses.MED_KIT.ToString(),
        BaseClasses.DRUGS.ToString(),
        // BaseClasses.STIMULATOR.ToString(),
        BaseClasses.MEDICAL.ToString(),
        BaseClasses.MEDICAL_SUPPLIES.ToString(),
        // BaseClasses.MOD.ToString(),
        // BaseClasses.FUNCTIONAL_MOD.ToString(),
        BaseClasses.FUEL.ToString(),
        // BaseClasses.GEAR_MOD.ToString(),
        // BaseClasses.STOCK.ToString(),
        // BaseClasses.FOREGRIP.ToString(),
        // BaseClasses.MASTER_MOD.ToString(),
        // BaseClasses.MOUNT.ToString(),
        // BaseClasses.MUZZLE.ToString(),
        // BaseClasses.SIGHTS.ToString(),
        BaseClasses.MEDS.ToString(),
        // BaseClasses.MAP.ToString(),
        BaseClasses.MONEY.ToString(),
        // BaseClasses.NIGHTVISION.ToString(),
        // BaseClasses.THERMAL_VISION.ToString(),
        // BaseClasses.KEY.ToString(),
        // BaseClasses.KEY_MECHANICAL.ToString(),
        // BaseClasses.KEYCARD.ToString(),
        // BaseClasses.EQUIPMENT.ToString(),
        // BaseClasses.THROW_WEAPON.ToString(),
        BaseClasses.FOOD_DRINK.ToString(),
        // BaseClasses.PISTOL.ToString(),
        // BaseClasses.REVOLVER.ToString(),
        // BaseClasses.SMG.ToString(),
        // BaseClasses.ASSAULT_RIFLE.ToString(),
        // BaseClasses.ASSAULT_CARBINE.ToString(),
        // BaseClasses.SHOTGUN.ToString(),
        // BaseClasses.MARKSMAN_RIFLE.ToString(),
        // BaseClasses.SNIPER_RIFLE.ToString(),
        // BaseClasses.MACHINE_GUN.ToString(),
        // BaseClasses.GRENADE_LAUNCHER.ToString(),
        // BaseClasses.SPECIAL_WEAPON.ToString(),
        // BaseClasses.SPEC_ITEM.ToString(),
        // BaseClasses.SPRING_DRIVEN_CYLINDER.ToString(),
        // BaseClasses.KNIFE.ToString(),
        // BaseClasses.AMMO.ToString(),
        // BaseClasses.AMMO_BOX.ToString(),
        // BaseClasses.LOOT_CONTAINER.ToString(),
        // BaseClasses.MOB_CONTAINER.ToString(),
        // BaseClasses.SEARCHABLE_ITEM.ToString(),
        // BaseClasses.STASH.ToString(),
        // BaseClasses.SORTING_TABLE.ToString(),
        // BaseClasses.LOCKABLE_CONTAINER.ToString(),
        // BaseClasses.SIMPLE_CONTAINER.ToString(),
        // BaseClasses.INVENTORY.ToString(),
        // BaseClasses.STATIONARY_CONTAINER.ToString(),
        // BaseClasses.POCKETS.ToString(),
        // BaseClasses.ARMBAND.ToString(),
        BaseClasses.JEWELRY.ToString(),
        BaseClasses.ELECTRONICS.ToString(),
        BaseClasses.BUILDING_MATERIAL.ToString(),
        BaseClasses.TOOL.ToString(),
        BaseClasses.HOUSEHOLD_GOODS.ToString(),
        BaseClasses.LUBRICANT.ToString(),
        BaseClasses.BATTERY.ToString(),
        // BaseClasses.ASSAULT_SCOPE.ToString(),
        // BaseClasses.TACTICAL_COMBO.ToString(),
        // BaseClasses.FLASHLIGHT.ToString(),
        // BaseClasses.MAGAZINE.ToString(),
        // BaseClasses.LIGHT_LASER_DESIGNATOR.ToString(),
        // BaseClasses.FLASH_HIDER.ToString(),
        // BaseClasses.COLLIMATOR.ToString(),
        // BaseClasses.IRON_SIGHT.ToString(),
        // BaseClasses.COMPACT_COLLIMATOR.ToString(),
        // BaseClasses.COMPENSATOR.ToString(),
        // BaseClasses.OPTIC_SCOPE.ToString(),
        // BaseClasses.SPECIAL_SCOPE.ToString(),
        BaseClasses.OTHER.ToString(),
        // BaseClasses.SILENCER.ToString(),
        // BaseClasses.PORTABLE_RANGE_FINDER.ToString(),
        BaseClasses.ITEM.ToString(),
        // BaseClasses.CYLINDER_MAGAZINE.ToString(),
        // BaseClasses.AUXILIARY_MOD.ToString(),
        // BaseClasses.BIPOD.ToString(),
        // BaseClasses.HEADPHONES.ToString(),
        // BaseClasses.RANDOM_LOOT_CONTAINER.ToString(),
        BaseClasses.STACKABLE_ITEM.ToString(),
        // BaseClasses.BUILT_IN_INSERTS.ToString(),
        // BaseClasses.ARMOR_PLATE.ToString(),
        // BaseClasses.CULTIST_AMULET.ToString(),
        // BaseClasses.RADIO_TRANSMITTER.ToString(),
        // BaseClasses.HANDGUARD.ToString(),
        // BaseClasses.PISTOL_GRIP.ToString(),
        // BaseClasses.RECEIVER.ToString(),
        // BaseClasses.BARREL.ToString(),
        // BaseClasses.CHARGING_HANDLE.ToString(),
        // BaseClasses.COMB_MUZZLE_DEVICE.ToString(),
        // BaseClasses.HIDEOUT_AREA_CONTAINER.ToString(),
        // BaseClasses.FLYER.ToString(), // Flyer
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in fleamarket.ts
    public static readonly HashSet<MongoId> FleaBarterRequestWhitelistMongoIds = FleaBarterRequestWhitelist
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // Request whitelist
    public static readonly Dictionary<string, int> RequestWhitelist = new()
    {
        // Whitelist for specific items that can be REQUESTED for flea offers and flea price patch. Those items cannot be bought, but can be requested.
        { ItemTpl.BARTER_MICROCONTROLLER_BOARD.ToString(), 1480000 }, // Microcontroller board
        { ItemTpl.BARTER_FARFORWARD_GPS_SIGNAL_AMPLIFIER_UNIT.ToString(), 2126000 }, // Far-forward GPS Signal Amplifier Unit
        { ItemTpl.BARTER_ADVANCED_CURRENT_CONVERTER.ToString(), 4924000 }, // Advanced current converter
        { ItemTpl.BARTER_PHYSICAL_BITCOIN.ToString(), 100000 }, // Physical Bitcoin
        { ItemTpl.INFO_SILICON_OPTOELECTRONIC_INTEGRATED_CIRCUITS_TEXTBOOK.ToString(), 500000 }, // Silicon Optoelectronic Integrated Circuits textbook
        { ItemTpl.INFO_ADVANCED_ELECTRONIC_MATERIALS_TEXTBOOK.ToString(), 490000 }, // Advanced Electronic Materials textbook
        { ItemTpl.BARTER_LEGA_MEDAL.ToString(), 900000 }, // Lega Medal
        { ItemTpl.INFO_SECURE_FLASH_DRIVE_V2.ToString(), 0 }, // Secure Flash drive V2 <- blacklist
        { ItemTpl.BARTER_CASE_KEY.ToString(), 0 }, // Case key <- blacklist?, 32524
        { ItemTpl.MEDKIT_SANITARS_FIRST_AID_KIT.ToString(), 0 }, // Sanitar's first aid kit <- blacklist
        { ItemTpl.MEDICAL_SANITAR_KIT.ToString(), 0 }, // Sanitar kit <- blacklist
        { ItemTpl.BARTER_DOGTAGT.ToString(), 0 }, // dogtagt <- blacklist
        { ItemTpl.INFO_ENCRYPTED_FLASH_DRIVE.ToString(), 0 }, // encrypted flash drive
        { ItemTpl.BARTER_SHYSHKA_CHRISTMAS_TREE_LIFE_EXTENDER.ToString(), 0 }, // SHYSHKA
        { ItemTpl.BARTER_JAR_OF_PICKLES.ToString(), 0 }, // pickles
        { ItemTpl.BARTER_OLIVIER_SALAD_BOX.ToString(), 0 }, // olivie
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in fleamarket.ts
    public static readonly Dictionary<MongoId, int> RequestWhitelistMongoIds = RequestWhitelist
        .ToDictionary(static x => new MongoId(x.Key), static x => x.Value);

    // Handbook whitelist
    public static readonly HashSet<string> FleaListingsWhitelistHandbook = new()
    {
        // Whitelist for ENABLING BUYING on flea, used in Pacifist_FleaMarket
        // Handbook Categories IDs, updated for 3.10.5
        // Can be modified by user
        "5b47574386f77428ca22b2ed", // Energy elements
        "5b47574386f77428ca22b2ee", // Building materials
        "5b47574386f77428ca22b2ef", // Electronics
        "5b47574386f77428ca22b2f0", // Household materials
        "5b47574386f77428ca22b2f1", // Valuables
        "5b47574386f77428ca22b2f2", // Flammable materials
        "5b47574386f77428ca22b2f3", // Medical supplies
        "5b47574386f77428ca22b2f4", // Others
        "5b47574386f77428ca22b2f6", // Tools
        // "5b47574386f77428ca22b32f", // Facecovers
        // "5b47574386f77428ca22b330", // Headgear
        // "5b47574386f77428ca22b331", // Eyewear
        "5b47574386f77428ca22b335", // Drinks
        "5b47574386f77428ca22b336", // Food
        "5b47574386f77428ca22b337", // Pills
        "5b47574386f77428ca22b338", // Medkits
        "5b47574386f77428ca22b339", // Injury treatment
        "5b47574386f77428ca22b33a", // Injectors
        // "5b47574386f77428ca22b33b", // Rounds
        // "5b47574386f77428ca22b33c", // Ammo packs
        "5b47574386f77428ca22b33e", // Barter items
        // "5b47574386f77428ca22b33f", // Gear
        "5b47574386f77428ca22b340", // Provisions
        "5b47574386f77428ca22b341", // Info items
        // "5b47574386f77428ca22b342", // Keys
        "5b47574386f77428ca22b343", // Maps
        "5b47574386f77428ca22b344", // Medication
        // "5b47574386f77428ca22b345", // Special equipment
        // "5b47574386f77428ca22b346", // Ammo
        // "5b5f6f3c86f774094242ef87", // Headsets
        // "5b5f6f6c86f774093f2ecf0b", // Backpacks
        // "5b5f6f8786f77447ed563642", // Tactical rigs
        // "5b5f6fa186f77409407a7eb7", // Storage containers
        // "5b5f6fd286f774093f2ecf0d", // Secure containers
        // "5b5f701386f774093f2ecf0f", // Body armor
        // "5b5f704686f77447ec5d76d7", // Gear components
        // "5b5f71a686f77447ed5636ab", // Weapon parts & mods
        // "5b5f71b386f774093f2ecf11", // Functional mods
        // "5b5f71c186f77409407a7ec0", // Bipods
        // "5b5f71de86f774093f2ecf13", // Foregrips
        // "5b5f724186f77447ed5636ad", // Muzzle devices
        // "5b5f724c86f774093f2ecf15", // Flashhiders & brakes
        // "5b5f72f786f77447ec5d7702", // Muzzle adapters
        // "5b5f731a86f774093e6cb4f9", // Suppressors
        // "5b5f736886f774094242f193", // Light & laser devices
        // "5b5f737886f774093e6cb4fb", // Tactical combo devices
        // "5b5f73ab86f774094242f195", // Flashlights
        // "5b5f73c486f77447ec5d7704", // Laser target pointers
        // "5b5f73ec86f774093e6cb4fd", // Sights
        // "5b5f740a86f77447ec5d7706", // Assault scopes
        // "5b5f742686f774093e6cb4ff", // Collimators
        // "5b5f744786f774094242f197", // Compact collimators
        // "5b5f746686f77447ec5d7708", // Iron sights
        // "5b5f748386f774093e6cb501", // Optics
        // "5b5f749986f774094242f199", // Special purpose sights
        // "5b5f74cc86f77447ec5d770a", // Auxiliary parts
        // "5b5f750686f774093e6cb503", // Gear mods
        // "5b5f751486f77447ec5d770c", // Charging handles
        // "5b5f752e86f774093e6cb505", // Launchers
        // "5b5f754a86f774094242f19b", // Magazines
        // "5b5f755f86f77447ec5d770e", // Mounts
        // "5b5f757486f774093e6cb507", // Stocks & chassis
        // "5b5f759686f774094242f19d", // Magwells
        // "5b5f75b986f77447ec5d7710", // Vital parts
        // "5b5f75c686f774094242f19f", // Barrels
        // "5b5f75e486f77447ec5d7712", // Handguards
        // "5b5f760586f774093e6cb509", // Gas blocks
        // "5b5f761f86f774094242f1a1", // Pistol grips
        // "5b5f764186f77447ec5d7714", // Receivers & slides
        // "5b5f78b786f77447ed5636af", // Money
        // "5b5f78dc86f77409407a7f8e", // Weapons
        // "5b5f78e986f77447ed5636b1", // Assault carbines
        // "5b5f78fc86f77409407a7f90", // Assault rifles
        // "5b5f791486f774093f2ed3be", // Marksman rifles
        // "5b5f792486f77447ed5636b3", // Pistols
        // "5b5f794b86f77409407a7f92", // Shotguns
        // "5b5f796a86f774093f2ed3c0", // SMGs
        // "5b5f798886f77447ed5636b5", // Bolt-action rifles
        // "5b5f79a486f77409407a7f94", // Machine guns
        // "5b5f79d186f774093f2ed3c2", // Grenade launchers
        // "5b5f79eb86f77447ed5636b7", // Special weapons
        // "5b5f7a0886f77409407a7f96", // Melee weapons
        // "5b5f7a2386f774093f2ed3c4", // Throwables
        // "5b619f1a86f77450a702a6f3", // Quest items
        // "5c518ec986f7743b68682ce2", // Mechanical keys
        // "5c518ed586f774119a772aee", // Electronic keys
        // "6564b96a189fe36f356d177c", // hidden armor incerts
    };

    // Request blacklist
    public static readonly List<string> FleaBarterRequestBlacklistItems = new()
    {
        // unused?
        ItemTpl.DRINK_BOTTLE_OF_TARKOVSKAYA_VODKA_BAD.ToString(),
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_VIOLET.ToString(),
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_SILVER.ToString(),
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_RED.ToString(),
        ItemTpl.BARTER_DOGTAGT.ToString(),
        ItemTpl.BARTER_DOGTAG_BEAR.ToString(),
        ItemTpl.BARTER_DOGTAG_BEAR_EOD.ToString(),
        ItemTpl.BARTER_DOGTAG_BEAR_TUE.ToString(),
        ItemTpl.BARTER_DOGTAG_USEC.ToString(),
        ItemTpl.BARTER_DOGTAG_USEC_EOD.ToString(),
        ItemTpl.BARTER_DOGTAG_USEC_TUE.ToString(),
    };

    // Fence whitelist
    public static readonly List<string> PacifistFenceItemBaseWhitelist = new()
    {
        BaseClasses.DRINK.ToString(),
        BaseClasses.INFO.ToString(),
        BaseClasses.FOOD.ToString(),
        BaseClasses.DRUGS.ToString(),
        BaseClasses.MED_KIT.ToString(),
        BaseClasses.MEDICAL.ToString(), // this breaks Fence
        BaseClasses.BATTERY.ToString(),
        BaseClasses.ELECTRONICS.ToString(),
        BaseClasses.BUILDING_MATERIAL.ToString(),
        BaseClasses.HOUSEHOLD_GOODS.ToString(),
        BaseClasses.JEWELRY.ToString(),
        BaseClasses.LUBRICANT.ToString(),
        BaseClasses.OTHER.ToString(),
        BaseClasses.TOOL.ToString(),
        BaseClasses.MEDICAL_SUPPLIES.ToString(),
        BaseClasses.FUEL.ToString(),
        BaseClasses.STIMULATOR.ToString(),
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in fleamarket.ts
    public static readonly HashSet<MongoId> PacifistFenceItemBaseWhitelistMongoIds = PacifistFenceItemBaseWhitelist
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // BSG blacklist
    public static readonly HashSet<string> BsgBlacklist = new()
    {
        // BSGblacklist from 3.10.2, hardcoded for safety, NEED TO UPDATE ON NEW PATCHES!!!!!!!
        // DON'T TOUCH THIS
        // DON'T TOUCH THIS
        // DON'T TOUCH THIS
        // DON'T TOUCH THIS
        // DON'T TOUCH THIS
        ItemTpl.SECURE_CONTAINER_ALPHA.ToString(), // Secure container Alpha
        ItemTpl.MAGAZINE_556X45_MAG560_60RND.ToString(), // 5.56x45 SureFire MAG5-60 STANAG 60-round magazine
        ItemTpl.AMMO_556X45_M855A1.ToString(), // 5.56x45mm M855A1
        ItemTpl.ARMOR_6B43_ZABRALOSH_BODY_ARMOR_EMR.ToString(), // 6B43 Zabralo-Sh body armor (Digital Flora)
        ItemTpl.AMMO_762X54R_SNB.ToString(), // 7.62x54mm R SNB gzh
        ItemTpl.LAUNCHER_GP34_40MM_UNDERBARREL_GRENADE.ToString(), // GP-34 40mm underbarrel grenade launcher
        ItemTpl.AMMO_545X39_BP.ToString(), // 5.45x39mm BP gs
        ItemTpl.AMMO_545X39_BS.ToString(), // 5.45x39mm BS gs
        ItemTpl.SECURE_WAIST_POUCH.ToString(), // Waist pouch
        ItemTpl.AMMOBOX_545X39_BS_120RND.ToString(), // 5.45x39mm BS gs ammo pack (120 pcs)
        ItemTpl.AMMOBOX_545X39_BS_120RND_DAMAGED.ToString(), // 5.45x39mm BS gs ammo pack (120 pcs)
        ItemTpl.AMMOBOX_545X39_BS_30RND.ToString(), // 5.45x39mm BS gs ammo pack (30 pcs)
        ItemTpl.AMMOBOX_545X39_BT_120RND.ToString(), // 5.45x39mm BT gs ammo pack (120 pcs)
        ItemTpl.AMMOBOX_545X39_BT_120RND_DAMAGED.ToString(), // 5.45x39mm BT gs ammo pack (120 pcs)
        ItemTpl.AMMOBOX_545X39_BT_30RND.ToString(), // 5.45x39mm BT gs ammo pack (30 pcs)
        ItemTpl.MARKSMANRIFLE_VSS_VINTOREZ_9X39_SPECIAL_SNIPER_RIFLE.ToString(), // VSS Vintorez 9x39 special sniper rifle
        ItemTpl.AMMO_9X39_SP6.ToString(), // 9x39mm SP-6 gs
        ItemTpl.SECURE_CONTAINER_BETA.ToString(), // Secure container Beta
        ItemTpl.SECURE_CONTAINER_GAMMA.ToString(), // Secure container Gamma
        ItemTpl.AMMO_762X51_M80.ToString(), // 7.62x51mm M80
        ItemTpl.KEY_MACHINERY.ToString(), // Machinery key
        ItemTpl.KEY_UNKNOWN.ToString(), // Unknown key
        ItemTpl.MAGAZINE_556X45_PMAG_D60_60RND.ToString(), // 5.56x45 Magpul PMAG D-60 STANAG 60-round magazine
        ItemTpl.SECURE_CONTAINER_EPSILON.ToString(), // Secure container Epsilon
        ItemTpl.AMMO_762X39_BP.ToString(), // 7.62x39mm BP gzh
        ItemTpl.AMMO_556X45_M995.ToString(), // 5.56x45mm M995
        ItemTpl.BACKPACK_PILGRIM_TOURIST.ToString(), // Pilgrim tourist backpack
        ItemTpl.AMMO_762X54R_PS.ToString(), // 7.62x54mm R PS gzh
        ItemTpl.BARTER_DOGTAG_BEAR.ToString(), // Dogtag BEAR
        ItemTpl.BARTER_DOGTAG_USEC.ToString(), // Dogtag USEC
        ItemTpl.BARTER_PHYSICAL_BITCOIN.ToString(), // Physical Bitcoin
        ItemTpl.HEADWEAR_OPSCORE_FAST_MT_SUPER_HIGH_CUT_HELMET_BLACK.ToString(), // Ops-Core FAST MT Super High Cut helmet (Black)
        ItemTpl.ARMOREDEQUIPMENT_OPSCORE_FAST_MULTIHIT_BALLISTIC_FACE_SHIELD.ToString(), // Ops-Core FAST multi-hit ballistic face shield
        ItemTpl.SPECIALSCOPE_TRIJICON_REAPIR_THERMAL_SCOPE.ToString(), // Trijicon REAP-IR thermal scope
        ItemTpl.AMMO_762X51_M61.ToString(), // 7.62x51mm M61
        ItemTpl.AMMO_762X51_M62.ToString(), // 7.62x51mm M62 Tracer
        ItemTpl.HEADWEAR_ALTYN_BULLETPROOF_HELMET_OLIVE_DRAB.ToString(), // Altyn bulletproof helmet (Olive Drab)
        ItemTpl.CONTAINER_MEDICINE_CASE.ToString(), // Medicine case
        ItemTpl.BACKPACK_SSO_ATTACK_2_RAID_BACKPACK_KHAKI.ToString(), // SSO Attack 2 raid backpack (Khaki)
        ItemTpl.HEADWEAR_OPSCORE_FAST_MT_SUPER_HIGH_CUT_HELMET_URBAN_TAN.ToString(), // Ops-Core FAST MT Super High Cut helmet (Urban Tan)
        ItemTpl.HEADWEAR_DEVTAC_RONIN_RESPIRATOR.ToString(), // DevTac Ronin Respirator
        ItemTpl.CONTAINER_THICC_WEAPON_CASE.ToString(), // T H I C C Weapon case
        ItemTpl.CONTAINER_LUCKY_SCAV_JUNK_BOX.ToString(), // Lucky Scav Junk box
        ItemTpl.AMMO_46X30_AP_SX.ToString(), // 4.6x30mm AP SX
        ItemTpl.NIGHTVISION_L3HARRIS_GPNVG18_NIGHT_VISION_GOGGLES.ToString(), // L3Harris GPNVG-18 night vision goggles
        ItemTpl.ARMOREDEQUIPMENT_MASKA1SCH_FACE_SHIELD_OLIVE_DRAB.ToString(), // Maska-1SCh face shield (Olive Drab)
        ItemTpl.SECURE_CONTAINER_KAPPA.ToString(), // Secure container Kappa
        ItemTpl.CONTAINER_THICC_ITEM_CASE.ToString(), // T H I C C item case
        ItemTpl.AMMO_545X39_PPBS.ToString(), // 5.45x39mm PPBS gs Igolnik
        ItemTpl.AMMO_9X39_BP.ToString(), // 9x39mm BP gs
        ItemTpl.ARMOREDEQUIPMENT_OPSCORE_SLAAP_ARMOR_HELMET_PLATE_TAN.ToString(), // Ops-Core SLAAP armor helmet plate (Tan)
        ItemTpl.BACKPACK_MYSTERY_RANCH_BLACKJACK_50_BACKPACK_MULTICAM.ToString(), // Mystery Ranch Blackjack 50 backpack (MultiCam)
        ItemTpl.BACKPACK_3V_GEAR_PARATUS_3DAY_OPERATORS_TACTICAL_BACKPACK_FOLIAGE_GREY.ToString(), // 3V Gear Paratus 3-Day Operator's Tactical backpack (Foliage Grey)
        ItemTpl.AMMOBOX_9X39_BP_8RND.ToString(), // 9x39mm BP gs ammo pack (8 pcs)
        ItemTpl.AMMOBOX_545X39_PPBS_30RND.ToString(), // 5.45x39mm PPBS gs Igolnik ammo pack (30 pcs)
        ItemTpl.HEADWEAR_CRYE_PRECISION_AIRFRAME_HELMET_TAN.ToString(), // Crye Precision AirFrame helmet (Tan)
        ItemTpl.MAGAZINE_366TKM_AKA16_73RND.ToString(), // AK 7.62x39 ProMag AK-A-16 73-round drum magazine
        ItemTpl.HEADWEAR_VULKAN5_LSHZ5_BULLETPROOF_HELMET_BLACK.ToString(), // Vulkan-5 LShZ-5 bulletproof helmet (Black)
        ItemTpl.ARMOREDEQUIPMENT_VULKAN5_HELMET_FACE_SHIELD.ToString(), // Vulkan-5 helmet face shield
        ItemTpl.ARMOR_FORT_REDUTT5_BODY_ARMOR_SMOG.ToString(), // FORT Redut-T5 body armor (Smog)
        ItemTpl.AMMO_127X55_PS12B.ToString(), // 12.7x55mm PS12B
        ItemTpl.AMMO_57X28_SB193.ToString(), // 5.7x28mm SB193
        ItemTpl.MAGAZINE_366TKM_X47_762_50RND.ToString(), // AK 7.62x39 X Products X-47 50-round drum magazine
        ItemTpl.SPECIALSCOPE_FLIR_RS32_2259X_35MM_60HZ_THERMAL_RIFLESCOPE.ToString(), // FLIR RS-32 2.25-9x 35mm 60Hz thermal riflescope
        ItemTpl.AMMO_12G_AP20.ToString(), // 12/70 AP-20 armor-piercing slug
        ItemTpl.AMMO_12G_CSP.ToString(), // 12/70 Copper Sabot Premier HP slug
        ItemTpl.ASSAULTRIFLE_DESERT_TECH_MDR_762X51_ASSAULT_RIFLE.ToString(), // Desert Tech MDR 7.62x51 assault rifle
        ItemTpl.BACKPACK_6SH118_RAID_BACKPACK_EMR.ToString(), // 6Sh118 raid backpack (Digital Flora)
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_RED.ToString(), // Christmas tree ornament (Red)
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_SILVER.ToString(), // Christmas tree ornament (Silver)
        ItemTpl.BARTER_CHRISTMAS_TREE_ORNAMENT_VIOLET.ToString(), // Christmas tree ornament (Violet)
        ItemTpl.HEADWEAR_TEAM_WENDY_EXFIL_BALLISTIC_HELMET_BLACK.ToString(), // Team Wendy EXFIL Ballistic Helmet (Black)
        ItemTpl.ARMOREDEQUIPMENT_TEAM_WENDY_EXFIL_BALLISTIC_FACE_SHIELD_BLACK.ToString(), // Team Wendy EXFIL Ballistic face shield (Black)
        ItemTpl.HEADWEAR_TEAM_WENDY_EXFIL_BALLISTIC_HELMET_COYOTE_BROWN.ToString(), // Team Wendy EXFIL Ballistic Helmet (Coyote Brown)
        ItemTpl.ARMOREDEQUIPMENT_TEAM_WENDY_EXFIL_BALLISTIC_FACE_SHIELD_COYOTE_BROWN.ToString(), // Team Wendy EXFIL Ballistic face shield (Coyote Brown)
        ItemTpl.AMMO_762X54R_BT.ToString(), // 7.62x54mm R BT gzh
        ItemTpl.AMMO_762X54R_BS.ToString(), // 7.62x54mm R BS gs
        ItemTpl.GRENADELAUNCHER_FN40GL_01.ToString(), // FN40GL Mk2 40mm grenade launcher
        ItemTpl.SHOTGUN_TOZ_KS23M_23X75MM_PUMPACTION.ToString(), // TOZ KS-23M 23x75mm pump-action shotgun
        ItemTpl.AMMO_23X75_ZVEZDA.ToString(), // 23x75mm Zvezda flashbang round
        ItemTpl.ARMOREDEQUIPMENT_DIAMOND_AGE_BASTION_HELMET_ARMOR_PLATE.ToString(), // Diamond Age Bastion helmet armor plate
        ItemTpl.AMMO_40X46_M441.ToString(), // 40x46mm M441 (HE) grenade
        ItemTpl.AMMO_40X46_M381.ToString(), // 40x46mm M381 (HE) grenade
        ItemTpl.AMMO_762X51_M993.ToString(), // 7.62x51mm M993
        ItemTpl.AMMO_366TKM_APM.ToString(), // .366 TKM AP-M
        ItemTpl.AMMO_40X46_M433.ToString(), // 40x46mm M433 (HEDP) grenade
        ItemTpl.BACKPACK_EBERLESTOCK_F4_TERMINATOR_LOAD_BEARING_BACKPACK_TIGER_STRIPE.ToString(), // Eberlestock F4 Terminator load bearing backpack (Tiger Stripe)
        ItemTpl.HEADWEAR_GALVION_CAIMAN_HYBRID_HELMET_GREY.ToString(), // Galvion Caiman Hybrid helmet (Grey)
        ItemTpl.HEADWEAR_RYST_BULLETPROOF_HELMET_BLACK.ToString(), // Rys-T bulletproof helmet (Black)
        ItemTpl.ARMOREDEQUIPMENT_RYST_FACE_SHIELD.ToString(), // Rys-T face shield
        ItemTpl.MARKSMANRIFLE_SWORD_INTERNATIONAL_MK18_338_LM_MARKSMAN_RIFLE.ToString(), // SWORD International Mk-18 .338 LM marksman rifle
        ItemTpl.AMMO_86X70_FMJ.ToString(), // .338 Lapua Magnum FMJ
        ItemTpl.AMMO_86X70_AP.ToString(), // .338 Lapua Magnum AP
        ItemTpl.AMMO_762X35_AP.ToString(), // .300 Blackout AP
        ItemTpl.AMMO_556X45_SSA_AP.ToString(), // 5.56x45mm SSA AP
        ItemTpl.AMMO_762X39_MAI_AP.ToString(), // 7.62x39mm MAI AP
        ItemTpl.BACKPACK_EBERLESTOCK_G2_GUNSLINGER_II_BACKPACK_DRY_EARTH.ToString(), // Eberlestock G2 Gunslinger II backpack (Dry Earth)
        ItemTpl.ARMOR_NFM_THOR_INTEGRATED_CARRIER_BODY.ToString(), // NFM THOR Integrated Carrier body armor
        ItemTpl.FACECOVER_TAGILLAS_WELDING_MASK_UBEY.ToString(), // Tagilla's welding mask "UBEY"
        ItemTpl.FACECOVER_TAGILLAS_WELDING_MASK_GORILLA.ToString(), // Tagilla's welding mask "Gorilla"
        ItemTpl.DRINK_BOTTLE_OF_TARKOVSKAYA_VODKA_BAD.ToString(), // Bottle of Tarkovskaya vodka
        ItemTpl.ASSAULTRIFLE_FN_SCARH_762X51_ASSAULT_RIFLE_FDE.ToString(), // FN SCAR-H 7.62x51 assault rifle (FDE)
        ItemTpl.GRENADE_RGN_HAND.ToString(), // RGN hand grenade
        ItemTpl.ASSAULTRIFLE_FN_SCARH_762X51_ASSAULT_RIFLE.ToString(), // FN SCAR-H 7.62x51 assault rifle
        ItemTpl.GRENADE_RGO_HAND.ToString(), // RGO hand grenade
        ItemTpl.AMMO_545X39_7N40.ToString(), // 5.45x39mm 7N40
        ItemTpl.AMMO_9X39_PAB9.ToString(), // 9x39mm PAB-9 gs
        ItemTpl.ARMBAND_ALPHA.ToString(), // Armband (Alpha)
        ItemTpl.ARMBAND_DEADSKUL.ToString(), // Armband (DEADSKUL)
        ItemTpl.ARMBAND_TRAIN_HARD.ToString(), // Armband (Train Hard)
        ItemTpl.ARMBAND_KIBA_ARMS.ToString(), // Armband (Kiba Arms)
        ItemTpl.ARMBAND_RFARMY.ToString(), // Armband (RFARMY)
        ItemTpl.ARMBAND_UNTAR.ToString(), // Armband (UNTAR)
        ItemTpl.BACKPACK_SANTAS_BAG.ToString(), // Santa's bag
        ItemTpl.SIGNALPISTOL_ZID_SP81_26X75_SIGNAL_PISTOL.ToString(), // ZiD SP-81 26x75 signal pistol
        ItemTpl.FLARE_RSP30_REACTIVE_SIGNAL_CARTRIDGE_RED.ToString(), // RSP-30 reactive signal cartridge (Red)
        ItemTpl.AMMO_26X75_GREEN.ToString(), // 26x75mm flare cartridge (Green)
        ItemTpl.AMMO_26X75_RED.ToString(), // 26x75mm flare cartridge (Red)
        ItemTpl.REVOLVER_MILKOR_M32A1_MSGL_40MM_GRENADE_LAUNCHER.ToString(), // Milkor M32A1 MSGL 40mm grenade launcher
        ItemTpl.SNIPERRIFLE_ACCURACY_INTERNATIONAL_AXMC_338_LM_BOLTACTION_SNIPER_RIFLE.ToString(), // Accuracy International AXMC .338 LM bolt-action sniper rifle
        ItemTpl.FACECOVER_DEATH_KNIGHT_MASK.ToString(), // Death Knight mask
        ItemTpl.FACECOVER_BIG_PIPES_SMOKING_PIPE.ToString(), // Big Pipe's smoking pipe
        ItemTpl.LAUNCHER_GP25_KOSTYOR_40MM_UNDERBARREL_GRENADE.ToString(), // GP-25 Kostyor 40mm underbarrel grenade launcher
        ItemTpl.RADIOTRANSMITTER_DIGITAL_SECURE_DSP_RADIO_TRANSMITTER.ToString(), // Digital secure DSP radio transmitter
        ItemTpl.AMMO_26X75_AG.ToString(), // 26x75mm flare cartridge (Acid Green)
        ItemTpl.LAUNCHER_M203_40MM_UNDERBARREL_GRENADE.ToString(), // M203 40mm underbarrel grenade launcher
        ItemTpl.BARTER_MICROCONTROLLER_BOARD.ToString(), // Microcontroller board
        ItemTpl.BARTER_FARFORWARD_GPS_SIGNAL_AMPLIFIER_UNIT.ToString(), // Far-forward GPS Signal Amplifier Unit
        ItemTpl.BARTER_ADVANCED_CURRENT_CONVERTER.ToString(), // Advanced current converter
        ItemTpl.INFO_SILICON_OPTOELECTRONIC_INTEGRATED_CIRCUITS_TEXTBOOK.ToString(), // Silicon Optoelectronic Integrated Circuits textbook
        ItemTpl.INFO_ADVANCED_ELECTRONIC_MATERIALS_TEXTBOOK.ToString(), // Advanced Electronic Materials textbook
        ItemTpl.BACKPACK_TASMANIAN_TIGER_TROOPER_35_BACKPACK_KHAKI.ToString(), // Tasmanian Tiger Trooper 35 backpack (Khaki)
        ItemTpl.KEY_BACKUP_HIDEOUT.ToString(), // Backup hideout key
        ItemTpl.SPECITEM_RADIO_REPEATER.ToString(), // Radio repeater
        ItemTpl.KEY_PRIMORSKY_4648_SKYBRIDGE.ToString(), // Primorsky 46-48 skybridge key
        ItemTpl.SPECIALSCOPE_ARMASIGHT_ZEUSPRO_640_28X50_30HZ_THERMAL_SCOPE.ToString(), // Armasight Zeus-Pro 640 2-8x50 30Hz thermal scope
        ItemTpl.ASSAULTCARBINE_TOKAREV_AVT40_762X54R_AUTOMATIC_RIFLE.ToString(), // Tokarev AVT-40 7.62x54R automatic rifle
        ItemTpl.MACHINEGUN_KALASHNIKOV_PKM_762X54R_MACHINE_GUN.ToString(), // Kalashnikov PKM 7.62x54R machine gun
        ItemTpl.SPECIALSCOPE_SIG_SAUER_ECHO1_12X30MM_30HZ_THERMAL_REFLEX_SCOPE.ToString(), // SIG Sauer ECHO1 1-2x30mm 30Hz thermal reflex scope
        ItemTpl.AMMOBOX_127X55_PS12B_10RND.ToString(), // 12.7x55mm PS12B (10 pcs)
        ItemTpl.AMMOBOX_86X70_AP_20RND.ToString(), // .338 Lapua Magnum AP ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X54R_BS_20RND.ToString(), // 7.62x54mm R BS ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X51_M993_20RND.ToString(), // 7.62x51mm M993 ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X39_MAI_AP_20RND.ToString(), // 7.62x39mm MAI AP ammo pack (20 pcs)
        ItemTpl.AMMOBOX_9X39_BP_20RND.ToString(), // 9x39mm BP ammo pack (20 pcs)
        ItemTpl.AMMOBOX_556X45_SSA_AP_50RND.ToString(), // 5.56x45mm SSA AP ammo pack (50 pcs)
        ItemTpl.AMMOBOX_762X35_AP_50RND.ToString(), // .300 Blackout AP ammo pack (50 pcs)
        ItemTpl.AMMOBOX_545X39_7N40_30RND.ToString(), // 5.45x39mm 7N40 ammo pack (30 pcs)
        ItemTpl.AMMOBOX_57X28_SS190_50RND.ToString(), // 5.7x28mm SS190 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_46X30_AP_SX_40RND.ToString(), // 4.6x30mm AP SX ammo pack (40 pcs)
        ItemTpl.AMMOBOX_9X21_BT_30RND.ToString(), // 9x21mm BT ammo pack (30 pcs)
        ItemTpl.AMMOBOX_45ACP_AP_50RND.ToString(), // .45 ACP AP ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X19_PBP_50RND.ToString(), // 9x19mm PBP ammo pack (50 pcs)
        ItemTpl.AMMOBOX_12G_AP20_25RND.ToString(), // 12/70 AP-20 ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X39_BP_20RND.ToString(), // 7.62x39mm BP gzh ammo pack (20 pcs)
        ItemTpl.ARMORPLATE_GRANIT_BR5_BALLISTIC_PLATE.ToString(), // Granit Br5 ballistic plate
        ItemTpl.ARMORPLATE_ESAPI_LEVEL_IV_BALLISTIC_PLATE.ToString(), // ESAPI level IV ballistic plate
        ItemTpl.AMMO_762X35_CBJ.ToString(), // .300 Blackout CBJ
        ItemTpl.MACHINEGUN_KALASHNIKOV_PKP_762X54R_INFANTRY_MACHINE_GUN.ToString(), // Kalashnikov PKP 7.62x54R infantry machine gun
        ItemTpl.CULTISTAMULET_SACRED_AMULET.ToString(), // Sacred Amulet
        ItemTpl.KEY_RUSTED_BLOODY.ToString(), // Rusted bloody key
        ItemTpl.ASSAULTCARBINE_SR3M_9X39_COMPACT_ASSAULT_RIFLE.ToString(), // SR-3M 9x39 compact assault rifle
        ItemTpl.MACHINEGUN_DEGTYAREV_RPDN_762X39_MACHINE_GUN.ToString(), // Degtyarev RPDN 7.62x39 machine gun
        ItemTpl.ASSAULTRIFLE_SIG_MCXSPEAR_68X51_ASSAULT_RIFLE.ToString(), // SIG MCX-SPEAR 6.8x51 assault rifle
        ItemTpl.AMMO_68X51_HYBRID.ToString(), // 6.8x51mm SIG Hybrid
        ItemTpl.ARMORPLATE_GRANIT_4RS_BALLISTIC_PLATES_BACK.ToString(), // Granit 4RS ballistic plates (Back)
        ItemTpl.ARMORPLATE_GRANIT_BR4_BALLISTIC_PLATE.ToString(), // Granit Br4 ballistic plate
        ItemTpl.ARMORPLATE_SAPI_LEVEL_III_BALLISTIC_PLATE.ToString(), // SAPI level III+ ballistic plate
        ItemTpl.ARMORPLATE_GRANIT_4_BALLISTIC_PLATES_BACK.ToString(), // Granit 4 ballistic plates (Back)
        ItemTpl.ARMORPLATE_GRANIT_4_BALLISTIC_PLATE_FRONT.ToString(), // Granit 4 ballistic plate (Front)
        ItemTpl.ARMORPLATE_GRANIT_4RS_BALLISTIC_PLATE_FRONT.ToString(), // Granit 4RS ballistic plate (Front)
        ItemTpl.ARMORPLATE_KORUNDVM_BALLISTIC_PLATES_FRONT.ToString(), // Korund-VM ballistic plates (Front)
        ItemTpl.ARMORPLATE_KORUNDVMK_BALLISTIC_PLATES_FRONT.ToString(), // Korund-VM-K ballistic plates (Front)
        ItemTpl.ARMORPLATE_TALLCOM_GUARDIAN_BALLISTIC_PLATE.ToString(), // TallCom Guardian ballistic plate
        ItemTpl.ARMORPLATE_NESCO_4400SAMC_BALLISTIC_PLATE.ToString(), // NESCO 4400-SA-MC ballistic plate
        ItemTpl.ARMORPLATE_KIBA_ARMS_STEEL_BALLISTIC_PLATE.ToString(), // Kiba Arms Steel ballistic plate
        ItemTpl.ARMORPLATE_CULT_LOCUST_BALLISTIC_PLATE.ToString(), // Cult Locust ballistic plate
        ItemTpl.ARMORPLATE_CULT_TERMITE_BALLISTIC_PLATE.ToString(), // Cult Termite ballistic plate
        ItemTpl.ARMORPLATE_GAC_3S15M_BALLISTIC_PLATE.ToString(), // GAC 3s15m ballistic plate
        ItemTpl.ARMORPLATE_GAC_4SSS2_BALLISTIC_PLATE.ToString(), // GAC 4sss2 ballistic plate
        ItemTpl.ARMORPLATE_KITECO_SCIV_SA_BALLISTIC_PLATE.ToString(), // KITECO SC-IV SA ballistic plate
        ItemTpl.AMMOBOX_762X35_CBJ_50RND.ToString(), // .300 Blackout CBJ ammo pack (50 pcs)
        ItemTpl.AMMOBOX_762X35_M62_50RND.ToString(), // .300 Blackout M62 Tracer ammo pack (50 pcs)
        ItemTpl.AMMOBOX_762X35_VMAX_50RND.ToString(), // .300 Blackout V-Max ammo pack (50 pcs)
        ItemTpl.AMMOBOX_762X35_FMJ_50RND.ToString(), // .300 Blackout BCP FMJ ammo pack (50 pcs)
        ItemTpl.AMMOBOX_762X35_WHISPER_50RND.ToString(), // .300 Whisper ammo pack (50 pcs)
        ItemTpl.AMMOBOX_86X70_FMJ_20RND.ToString(), // .338 Lapua Magnum FMJ ammo pack (20 pcs)
        ItemTpl.AMMOBOX_86X70_TACX_20RND.ToString(), // .338 Lapua Magnum TAC-X ammo pack (20 pcs)
        ItemTpl.AMMOBOX_86X70_UCW_20RND.ToString(), // .338 Lapua Magnum UCW ammo pack (20 pcs)
        ItemTpl.AMMOBOX_9X33R_FMJ_25RND.ToString(), // .357 Magnum FMJ ammo pack (25 pcs)
        ItemTpl.AMMOBOX_9X33R_HP_25RND.ToString(), // .357 Magnum HP ammo pack (25 pcs)
        ItemTpl.AMMOBOX_9X33R_JHP_25RND.ToString(), // .357 Magnum JHP ammo pack (25 pcs)
        ItemTpl.AMMOBOX_9X33R_SP_25RND.ToString(), // .357 Magnum SP ammo pack (25 pcs)
        ItemTpl.AMMOBOX_366TKM_FMJ_20RND.ToString(), // .366 TKM FMJ ammo pack (20 pcs)
        ItemTpl.AMMOBOX_366TKM_APM_20RND.ToString(), // .366 TKM AP-M ammo pack (20 pcs)
        ItemTpl.AMMOBOX_366TKM_GEKSA_20RND.ToString(), // .366 TKM Geksa ammo pack (20 pcs)
        ItemTpl.AMMOBOX_366TKM_EKO_20RND.ToString(), // .366 TKM EKO ammo pack (20 pcs)
        ItemTpl.AMMOBOX_45ACP_HYDRASHOK_50RND.ToString(), // .45 ACP Hydra-Shok ammo pack (50 pcs)
        ItemTpl.AMMOBOX_45ACP_LASERMATCH_50RND.ToString(), // .45 ACP Lasermatch FMJ ammo pack (50 pcs)
        ItemTpl.AMMOBOX_45ACP_FMJ_50RND.ToString(), // .45 ACP Match FMJ ammo pack (50 pcs)
        ItemTpl.AMMOBOX_45ACP_RIP_50RND.ToString(), // .45 ACP RIP ammo pack (50 pcs)
        ItemTpl.AMMOBOX_127X55_PS12_10RND.ToString(), // 12.7x55mm PS12 ammo pack (10 pcs)
        ItemTpl.AMMOBOX_127X55_PS12A_10RND.ToString(), // 12.7x55mm PS12A ammo pack (10 pcs)
        ItemTpl.AMMOBOX_12G_525MM_25RND.ToString(), // 12/70 5.25mm buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_EXPRESS_25RND.ToString(), // 12/70 6.5mm Express buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_7MM_25RND.ToString(), // 12/70 7mm buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_MAGNUM_25RND.ToString(), // 12/70 8.5mm Magnum buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_DUALSABOT_25RND.ToString(), // 12/70 Dual Sabot slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_PIRANHA_25RND.ToString(), // 12/70 Piranha ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_FTX_25RND.ToString(), // 12/70 FTX Custom Lite slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_GRIZZLY_40_25RND.ToString(), // 12/70 Grizzly 40 slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_POLEVA3_25RND.ToString(), // 12/70 Poleva-3 slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_POLEVA6U_25RND.ToString(), // 12/70 Poleva-6u slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_50_BMG_25RND.ToString(), // 12/70 makeshift .50 BMG slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_SLUG_25RND.ToString(), // 12/70 lead slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_FLECHETTE_25RND.ToString(), // 12/70 flechette ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_CSP_25RND.ToString(), // 12/70 Copper Sabot Premier HP slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_12G_SFORMANCE_25RND.ToString(), // 12/70 SuperFormance HP slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_56MM_25RND.ToString(), // 20/70 5.6mm buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_62MM_25RND.ToString(), // 20/70 6.2mm buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_73MM_25RND.ToString(), // 20/70 7.3mm buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_75MM_25RND.ToString(), // 20/70 7.5mm buckshot ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_DEVASTATOR_25RND.ToString(), // 20/70 Devastator slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_STAR_25RND.ToString(), // 20/70 Star slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_POLEVA3_25RND.ToString(), // 20/70 Poleva-3 slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_20G_POLEVA6U_25RND.ToString(), // 20/70 Poleva-6u slug ammo pack (25 pcs)
        ItemTpl.AMMOBOX_23X75_SHRAP10_5RND.ToString(), // 23x75mm Shrapnel-10 buckshot ammo pack (5 pcs)
        ItemTpl.AMMOBOX_23X75_SHRAP25_5RND.ToString(), // 23x75mm Shrapnel-25 buckshot ammo pack (5 pcs)
        ItemTpl.AMMOBOX_23X75_BARRIKADA_5RND.ToString(), // 23x75mm Barrikada slug ammo pack (5 pcs)
        ItemTpl.AMMOBOX_23X75_ZVEZDA_5RND.ToString(), // 23x75mm Zvezda flashbang round ammo pack (5 pcs)
        ItemTpl.AMMOBOX_46X30_ACTION_SX_40RND.ToString(), // 4.6x30mm Action SX ammo pack (40 pcs)
        ItemTpl.AMMOBOX_46X30_FMJ_SX_40RND.ToString(), // 4.6x30mm FMJ SX ammo pack (40 pcs)
        ItemTpl.AMMOBOX_46X30_SUBSONIC_SX_40RND.ToString(), // 4.6x30mm Subsonic SX ammo pack (40 pcs)
        ItemTpl.AMMOBOX_556X45_FMJ_50RND.ToString(), // 5.56x45mm FMJ ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_HP_50RND.ToString(), // 5.56x45mm HP ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_M855A1_50RND.ToString(), // 5.56x45mm M855A1 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_M856_50RND.ToString(), // 5.56x45mm M856 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_M856A1_50RND.ToString(), // 5.56x45mm M856A1 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_M995_50RND.ToString(), // 5.56x45mm M995 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_RRLP_50RND.ToString(), // 5.56x45mm MK 255 Mod 0 (RRLP) ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_SOST_50RND.ToString(), // 5.56x45mm MK 318 Mod 0 (SOST) ammo pack (50 pcs)
        ItemTpl.AMMOBOX_57X28_L191_50RND.ToString(), // 5.7x28mm L191 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_57X28_R37F_50RND.ToString(), // 5.7x28mm R37.F ammo pack (50 pcs)
        ItemTpl.AMMOBOX_57X28_R37X_50RND.ToString(), // 5.7x28mm R37.X ammo pack (50 pcs)
        ItemTpl.AMMOBOX_57X28_SB193_50RND.ToString(), // 5.7x28mm SB193 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_57X28_SS197SR_50RND.ToString(), // 5.7x28mm SS197SR ammo pack (50 pcs)
        ItemTpl.AMMOBOX_57X28_SS198LF_50RND.ToString(), // 5.7x28mm SS198LF ammo pack (50 pcs)
        ItemTpl.AMMOBOX_762X25TT_FMJ43_25RND.ToString(), // 7.62x25mm Đ˘Đ˘ FMJ43 ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X25TT_LRN_25RND.ToString(), // 7.62x25mm Đ˘Đ˘ LRN ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X25TT_LRNPC_25RND.ToString(), // 7.62x25mm Đ˘Đ˘ LRNPC ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X25TT_AKBS_25RND.ToString(), // 7.62x25mm TT AKBS ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X25TT_P_25RND.ToString(), // 7.62x25mm TT P gl ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X25TT_PST_25RND.ToString(), // 7.62x25mm TT Pst gzh ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X25TT_PT_25RND.ToString(), // 7.62x25mm TT PT gzh ammo pack (25 pcs)
        ItemTpl.AMMOBOX_762X51_M61_20RND.ToString(), // 7.62x51mm M61 ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X51_M62_20RND.ToString(), // 7.62x51mm M62 Tracer ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X51_M80_20RND.ToString(), // 7.62x51mm M80 ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X51_ULTRA_NOSLER_20RND.ToString(), // 7.62x51mm Ultra Nosler ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X51_BCP_FMJ_20RND.ToString(), // 7.62x51mm BCP FMJ ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X51_TCW_SP_20RND.ToString(), // 7.62x51mm TCW SP ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X54R_BT_20RND.ToString(), // 7.62x54mm R BT gzh ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X54R_LPS_20RND.ToString(), // 7.62x54mm R LPS gzh ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X54R_PS_20RND.ToString(), // 7.62x54mm R PS gzh ammo pack (20 pcs)
        ItemTpl.AMMOBOX_762X54R_T46M_20RND.ToString(), // 7.62x54mm R T-46M gzh ammo pack (20 pcs)
        ItemTpl.AMMOBOX_9X19_AP_63_50RND.ToString(), // 9x19mm AP 6.3 ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X19_GT_50RND.ToString(), // 9x19mm Green Tracer ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X19_LUGER_CCI_50RND.ToString(), // 9x19mm Luger CCI ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X19_QUAKEMAKER_50RND.ToString(), // 9x19mm QuakeMaker ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X19_PSO_50RND.ToString(), // 9x19mm PSO gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X19_PST_50RND.ToString(), // 9x19mm Pst gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X21_P_30RND.ToString(), // 9x21mm P gzh ammo pack (30 pcs)
        ItemTpl.AMMOBOX_9X21_PS_30RND.ToString(), // 9x21mm PS gzh ammo pack (30 pcs)
        ItemTpl.AMMOBOX_9X21_PE_30RND.ToString(), // 9x21mm PE gzh ammo pack (30 pcs)
        ItemTpl.AMMOBOX_9X39_PAB9_20RND.ToString(), // 9x39mm PAB-9 gs ammo pack (20 pcs)
        ItemTpl.AMMOBOX_9X39_SP5_20RND.ToString(), // 9x39mm SP-5 gs ammo pack (20 pcs)
        ItemTpl.AMMOBOX_9X39_SP6_20RND.ToString(), // 9x39mm SP-6 gs ammo pack (20 pcs)
        ItemTpl.AMMOBOX_9X39_SPP_20RND.ToString(), // 9x39mm SPP gs ammo pack (20 pcs)
        ItemTpl.AMMOBOX_545X39_PPBS_120RND.ToString(), // 5.45x39mm PPBS gs Igolnik ammo pack (120 pcs)
        ItemTpl.AMMOBOX_9X18PM_BZHT_50RND.ToString(), // 9x18mm PM BZhT gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_P_50RND.ToString(), // 9x18mm PM P gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PBM_50RND.ToString(), // 9x18mm PM PBM gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PPT_50RND.ToString(), // 9x18mm PM PPT gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PPE_50RND.ToString(), // 9x18mm PM PPe gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PRS_50RND.ToString(), // 9x18mm PM PRS gs ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PS_PPO_50RND.ToString(), // 9x18mm PM PS gs PPO ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PSV_50RND.ToString(), // 9x18mm PM PSV ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PSO_50RND.ToString(), // 9x18mm PM PSO gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PST_50RND.ToString(), // 9x18mm PM Pst gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_RG028_50RND.ToString(), // 9x18mm PM RG028 gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_SP7_50RND.ToString(), // 9x18mm PM SP7 gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_SP8_50RND.ToString(), // 9x18mm PM SP8 gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_9X18PM_PSTM_50RND.ToString(), // 9x18mm PMM PstM gzh ammo pack (50 pcs)
        ItemTpl.AMMOBOX_556X45_FMJ_100RND.ToString(), // 5.56x45mm FMJ ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_HP_100RND.ToString(), // 5.56x45mm HP ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_M855_100RND.ToString(), // 5.56x45mm M855 ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_M855A1_100RND.ToString(), // 5.56x45mm M855A1 ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_M856_100RND.ToString(), // 5.56x45mm M856 ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_M856A1_100RND.ToString(), // 5.56x45mm M856A1 ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_M995_100RND.ToString(), // 5.56x45mm M995 ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_RRLP_100RND.ToString(), // 5.56x45mm MK 255 Mod 0 (RRLP) ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_SOST_100RND.ToString(), // 5.56x45mm MK 318 Mod 0 (SOST) ammo pack (100 pcs)
        ItemTpl.AMMOBOX_556X45_SSA_AP_100RND.ToString(), // 5.56x45mm SSA AP ammo pack (100 pcs)
        ItemTpl.FACECOVER_ATOMIC_DEFENSE_CQCM_UP_ARMORED_BALLISTIC_MASK_BLACK.ToString(), // Atomic Defense CQCM ballistic mask (Black)
        ItemTpl.AMMOBOX_545X39_7N40_120RND.ToString(), // 5.45x39mm 7N40 ammo pack (120 pcs)
        ItemTpl.HEADWEAR_DIAMOND_AGE_NEOSTEEL_HIGH_CUT_HELMET_BLACK.ToString(), // Diamond Age NeoSteel High Cut helmet (Black)
        ItemTpl.FACECOVER_DEATH_SHADOW_LIGHTWEIGHT_ARMORED_MASK.ToString(), // Death Shadow lightweight armored mask
        ItemTpl.HEADWEAR_NPP_KLASS_TOR2_HELMET_OLIVE_DRAB.ToString(), // NPP KlASS Tor-2 helmet (Olive Drab)
        ItemTpl.ARMOREDEQUIPMENT_NPP_KLASS_TOR2_HELMET_FACE_SHIELD.ToString(), // NPP KlASS Tor-2 helmet face shield
        ItemTpl.AMMOBOX_9X21_7U4_30RND.ToString(), // 9x21mm 7U4 ammo pack (30 pcs)
        ItemTpl.AMMOBOX_9X21_7N42_30RND.ToString(), // 9x21mm 7N42 ammo pack (30 pcs)
        ItemTpl.AMMOBOX_9X39_FMJ_20RND.ToString(), // 9x39mm FMJ ammo pack (20 pcs)
        ItemTpl.ARMORPLATE_KORUNDVM_BALLISTIC_PLATE_BACK.ToString(), // Korund-VM ballistic plate (Back)
        ItemTpl.ARMORPLATE_KORUNDVMK_BALLISTIC_PLATE_BACK.ToString(), // Korund-VM-K ballistic plate (Back)
        ItemTpl.KNIFE_UNITED_CUTLERY_M48_TACTICAL_KUKRI.ToString(), // United Cutlery M48 Tactical Kukri
        ItemTpl.MARKOFUNKNOWN_MARK_OF_THE_UNHEARD.ToString(), // Mark of The Unheard
        ItemTpl.ARMBAND_OF_THE_UNHEARD.ToString(), // Armband of The Unheard
        ItemTpl.INFO_DECRYPTED_FLASH_DRIVE.ToString(), // Decrypted flash drive
        ItemTpl.INFO_DOCUMENTS_WITH_DECRYPTED_DATA.ToString(), // Documents with decrypted data
        ItemTpl.KNIFE_APOK_TACTICAL_WASTELAND_GLADIUS.ToString(), // APOK Tactical Wasteland Gladius
        ItemTpl.ARMBAND_ARENA.ToString(), // Armband (ARENA)
        ItemTpl.SECURE_CONTAINER_THETA.ToString(), // Secure container Theta
        ItemTpl.KEY_SHATUNS_HIDEOUT.ToString(), // Shatun's hideout key
        ItemTpl.KEY_GRUMPYS_HIDEOUT.ToString(), // Grumpy's hideout key
        ItemTpl.KEY_VORONS_HIDEOUT.ToString(), // Voron's hideout key
        ItemTpl.KEY_LEONS_HIDEOUT.ToString(), // Leon's hideout key
        ItemTpl.SPECITEM_THE_EYE_MORTAR_STRIKE_SIGNALING_DEVICE.ToString(), // "The Eye" mortar strike signaling device
        ItemTpl.BARTER_LEGA_MEDAL.ToString(), // Lega Medal
        ItemTpl.BARTER_LOCKED_EQUIPMENT_CRATE_RARE.ToString(), // Locked equipment crate (Rare)
        ItemTpl.BARTER_LOCKED_WEAPON_CRATE_RARE.ToString(), // Locked weapon crate (Rare)
        ItemTpl.BARTER_LOCKED_SUPPLY_CRATE_RARE.ToString(), // Locked supply crate (Rare)
        ItemTpl.BARTER_LOCKED_VALUABLES_CRATE_RARE.ToString(), // Locked valuables crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_GEARCRATE_BLUE_OPEN.ToString(), // Unlocked equipment crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_WEAPONCRATE_BLUE_OPEN.ToString(), // Unlocked weapon crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_JUNKCRATE_BLUE_OPEN.ToString(), // Unlocked supply crate (Rare)
        ItemTpl.RANDOMLOOTCONTAINER_ARENA_JEWELRYCRATE_BLUE_OPEN.ToString(), // Unlocked valuables crate (Rare)
        ItemTpl.SECURE_CONTAINER_GAMMA_TUE.ToString(), // Secure container Gamma
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
        ItemTpl.SHOTGUN_MPS_AUTO_ASSAULT12_GEN_1_12GA_AUTOMATIC.ToString(), // MPS Auto Assault-12 Gen 1 12ga automatic shotgun
        ItemTpl.SHOTGUN_MPS_AUTO_ASSAULT12_GEN_2_12GA_AUTOMATIC.ToString(), // MPS Auto Assault-12 Gen 2 12ga automatic shotgun
        ItemTpl.RANDOMLOOTCONTAINER_EVENT_CONTAINER_CONTRABAND_MAIN.ToString(), // Opened case
        ItemTpl.BARTER_CONTRABAND_BOX.ToString(), // Contraband box
        ItemTpl.BARTER_SEALED_BOX.ToString(), // Sealed box
        ItemTpl.RANDOMLOOTCONTAINER_EVENT_CONTAINER_CONTRABAND_FAKE.ToString(), // Opened box
        ItemTpl.BARTER_LOCKED_CASE.ToString(), // Locked case
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in fleamarket.ts
    public static readonly HashSet<MongoId> BsgBlacklistMongoIds = BsgBlacklist
        .Select(static x => new MongoId(x))
        .ToHashSet();
}

