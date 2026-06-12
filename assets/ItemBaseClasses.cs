using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Common;

namespace Softcore.Assets;
public static class ItemBaseClasses
{
    public static readonly List<string> All = new()
    {
        // Manually generates baseclasses list that excludes Nodes, Nodes break Fence and possibly other instances
        BaseClasses.ASSAULT_RIFLE.ToString(), // AssaultRifle
        BaseClasses.AMMO_BOX.ToString(), // AmmoBox
        BaseClasses.KEY_MECHANICAL.ToString(), // KeyMechanical
        BaseClasses.PISTOL.ToString(), // Pistol
        BaseClasses.THROW_WEAP.ToString(), // ThrowWeap
        BaseClasses.MAGAZINE.ToString(), // Magazine
        BaseClasses.DRINK.ToString(), // Drink
        BaseClasses.FOOD.ToString(), // Food
        BaseClasses.MONEY.ToString(), // Money
        BaseClasses.TACTICAL_COMBO.ToString(), // TacticalCombo
        BaseClasses.SILENCER.ToString(), // Silencer
        BaseClasses.KNIFE.ToString(), // Knife
        BaseClasses.SHOTGUN.ToString(), // Shotgun
        BaseClasses.MOB_CONTAINER.ToString(), // MobContainer
        BaseClasses.FLASH_HIDER.ToString(), // FlashHider
        BaseClasses.ASSAULT_SCOPE.ToString(), // AssaultScope
        BaseClasses.OPTIC_SCOPE.ToString(), // OpticScope
        BaseClasses.VEST.ToString(), // Vest
        BaseClasses.BACKPACK.ToString(), // Backpack
        BaseClasses.MEDICAL.ToString(), // Medical
        BaseClasses.DRUGS.ToString(), // Drugs
        BaseClasses.MED_KIT.ToString(), // MedKit
        BaseClasses.MULTITOOLS.ToString(), // Multitools
        BaseClasses.AMMO.ToString(), // Ammo
        BaseClasses.ARMOR.ToString(), // Armor
        BaseClasses.VISORS.ToString(), // Visors
        BaseClasses.POCKETS.ToString(), // Pockets
        BaseClasses.BARREL.ToString(), // Barrel
        BaseClasses.SNIPER_RIFLE.ToString(), // SniperRifle
        BaseClasses.COLLIMATOR.ToString(), // Collimator
        BaseClasses.MUZZLE_COMBO.ToString(), // MuzzleCombo
        BaseClasses.PISTOL_GRIP.ToString(), // PistolGrip
        BaseClasses.FOREGRIP.ToString(), // Foregrip
        BaseClasses.SIGHTS.ToString(), // Sights
        BaseClasses.RECEIVER.ToString(), // Receiver
        BaseClasses.CHARGE.ToString(), // Charge
        BaseClasses.HANDGUARD.ToString(), // Handguard
        BaseClasses.MOUNT.ToString(), // Mount
        BaseClasses.STOCK.ToString(), // Stock
        BaseClasses.IRON_SIGHT.ToString(), // IronSight
        BaseClasses.INVENTORY.ToString(), // Inventory
        BaseClasses.AUXILIARY_MOD.ToString(), // AuxiliaryMod
        BaseClasses.HEADWEAR.ToString(), // Headwear
        BaseClasses.HEADPHONES.ToString(), // Headphones
        BaseClasses.LAUNCHER.ToString(), // Launcher
        BaseClasses.STASH.ToString(), // Stash
        BaseClasses.LOCKABLE_CONTAINER.ToString(), // LockableContainer
        BaseClasses.BATTERY.ToString(), // Battery
        BaseClasses.ELECTRONICS.ToString(), // Electronics
        BaseClasses.LUBRICANT.ToString(), // Lubricant
        BaseClasses.BIPOD.ToString(), // Bipod
        BaseClasses.GASBLOCK.ToString(), // Gasblock
        BaseClasses.NIGHT_VISION.ToString(), // NightVision
        BaseClasses.FACE_COVER.ToString(), // FaceCover
        BaseClasses.JEWELRY.ToString(), // Jewelry
        BaseClasses.OTHER.ToString(), // Other
        BaseClasses.BUILDING_MATERIAL.ToString(), // BuildingMaterial
        BaseClasses.HOUSEHOLD_GOODS.ToString(), // HouseholdGoods
        BaseClasses.ASSAULT_CARBINE.ToString(), // AssaultCarbine
        BaseClasses.MAP.ToString(), // Map
        BaseClasses.KEYCARD.ToString(), // Keycard
        BaseClasses.COMPACT_COLLIMATOR.ToString(), // CompactCollimator
        BaseClasses.MARKSMAN_RIFLE.ToString(), // MarksmanRifle
        BaseClasses.SIMPLE_CONTAINER.ToString(), // SimpleContainer
        BaseClasses.LOOT_CONTAINER.ToString(), // LootContainer
        BaseClasses.SMG.ToString(), // Smg
        BaseClasses.FLASHLIGHT.ToString(), // Flashlight
        BaseClasses.TOOL.ToString(), // Tool
        BaseClasses.INFO.ToString(), // Info
        BaseClasses.REPAIR_KITS.ToString(), // RepairKits
        BaseClasses.SPEC_ITEM.ToString(), // SpecItem
        BaseClasses.MEDICAL_SUPPLIES.ToString(), // MedicalSupplies
        BaseClasses.ARMORED_EQUIPMENT.ToString(), // ArmoredEquipment
        BaseClasses.SPECIAL_SCOPE.ToString(), // SpecialScope
        BaseClasses.ARM_BAND.ToString(), // ArmBand
        BaseClasses.MACHINE_GUN.ToString(), // MachineGun
        BaseClasses.STIMULATOR.ToString(), // Stimulator
        BaseClasses.THERMAL_VISION.ToString(), // ThermalVision
        BaseClasses.FUEL.ToString(), // Fuel
        BaseClasses.GRENADE_LAUNCHER.ToString(), // GrenadeLauncher
        BaseClasses.COMPASS.ToString(), // Compass
        BaseClasses.SORTING_TABLE.ToString(), // SortingTable
        BaseClasses.REVOLVER.ToString(), // Revolver
        BaseClasses.CYLINDER_MAGAZINE.ToString(), // CylinderMagazine
        BaseClasses.PORTABLE_RANGE_FINDER.ToString(), // PortableRangeFinder
        BaseClasses.SPRING_DRIVEN_CYLINDER.ToString(), // SpringDrivenCylinder
        BaseClasses.RADIO_TRANSMITTER.ToString(), // RadioTransmitter
        BaseClasses.RANDOM_LOOT_CONTAINER.ToString(), // RandomLootContainer
        BaseClasses.HIDEOUT_AREA_CONTAINER.ToString(), // HideoutAreaContainer
        BaseClasses.BUILT_IN_INSERTS.ToString(), // BuiltInInserts
        BaseClasses.ARMOR_PLATE.ToString(), // ArmorPlate
        BaseClasses.CULTIST_AMULET.ToString(), // CultistAmulet
        BaseClasses.MARK_OF_UNKNOWN.ToString(), // MarkOfUnknown
        BaseClasses.PLANTING_KITS.ToString(), // PlantingKits
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in itemBaseClasses.ts
    public static readonly HashSet<MongoId> AllMongoIds = All
        .Select(static x => new MongoId(x))
        .ToHashSet();
}
