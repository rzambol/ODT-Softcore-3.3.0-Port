using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Common;

namespace Softcore.Assets;
public static class Keys
{
    // Quest keys
    public static readonly List<string> QuestKeys = new()
    {
        // Whitelist for quest keys updated for 3.10 taken from the Wiki some are commented out for balancing
        //Factory
        ItemTpl.KEY_FACTORY_EMERGENCY_EXIT.ToString(),
        ItemTpl.KEYCARD_TERRAGROUP_STORAGE_ROOM.ToString(),
        //Customs
        ItemTpl.KEY_DORM_OVERSEER.ToString(),
        ItemTpl.KEY_DORM_ROOM_114.ToString(),
        // ItemTpl.KEY_DORM_ROOM_203.ToString(),
        // ItemTpl.KEY_DORM_ROOM_206.ToString(),
        ItemTpl.KEY_DORM_ROOM_214.ToString(),
        ItemTpl.KEY_DORM_ROOM_220.ToString(),
        ItemTpl.KEY_DORM_ROOM_303.ToString(),
        // ItemTpl.KEY_DORM_ROOM_314_MARKED.ToString(),
        // ItemTpl.KEY_MACHINERY.ToString(),
        ItemTpl.KEY_PORTABLE_BUNKHOUSE.ToString(),
        // ItemTpl.KEY_TARCONE_DIRECTORS_OFFICE.ToString(),
        // ItemTpl.KEY_TRAILER_PARK_PORTABLE_CABIN.ToString(),
        // ItemTpl.KEY_UNKNOWN.ToString(),
        //Woods
        // ItemTpl.KEY_SHTURMANS_STASH.ToString(),
        ItemTpl.KEY_ZB014.ToString(),
        //Shoreline
        ItemTpl.KEY_COTTAGE_BACK_DOOR.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_EAST_WING_ROOM_306.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_EAST_WING_ROOM_308.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_EAST_WING_ROOM_328.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_OFFICE_KEY_WITH_A_BLUE_TAPE.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_WEST_WING_OFFICE_ROOM_112.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_WEST_WING_ROOM_216.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_WEST_WING_ROOM_219.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_WEST_WING_ROOM_220.ToString(),
        ItemTpl.KEY_HEALTH_RESORT_WEST_WING_ROOM_306.ToString(),
        //Interchange
        ItemTpl.KEY_EMERCOM_MEDICAL_UNIT.ToString(),
        ItemTpl.KEY_GOSHAN_CASH_REGISTER.ToString(),
        ItemTpl.KEY_KIBA_ARMS_INNER_GRATE_DOOR.ToString(),
        ItemTpl.KEY_KIBA_ARMS_OUTER_DOOR.ToString(),
        ItemTpl.KEYCARD_OBJECT_11SR.ToString(),
        ItemTpl.KEYCARD_OBJECT_21WS.ToString(),
        ItemTpl.KEY_OLI_LOGISTICS_DEPARTMENT_OFFICE.ToString(),
        //TheLab
        ItemTpl.KEYCARD_WITH_A_BLUE_MARKING.ToString(),
        // ItemTpl.KEYCARD_TERRAGROUP_LABS_ACCESS.ToString(),
        ItemTpl.KEYCARD_TERRAGROUP_LABS_KEYCARD_BLACK.ToString(),
        ItemTpl.KEY_TERRAGROUP_LABS_MANAGERS_OFFICE_ROOM.ToString(),
        ItemTpl.KEY_TERRAGROUP_LABS_WEAPON_TESTING_AREA.ToString(),
        //Reserve
        ItemTpl.KEY_RBKSM.ToString(),
        ItemTpl.KEY_RBOB.ToString(),
        ItemTpl.KEY_RBORB1.ToString(),
        ItemTpl.KEY_RBORB2.ToString(),
        ItemTpl.KEY_RBORB3.ToString(),
        ItemTpl.KEY_RBSMP.ToString(),
        ItemTpl.KEY_RBST.ToString(),
        //Lighthouse
        ItemTpl.KEY_OPERATING_ROOM.ToString(),
        ItemTpl.KEY_RADAR_STATION_COMMANDANT_ROOM.ToString(),
        ItemTpl.KEY_ROGUE_USEC_BARRACK.ToString(),
        ItemTpl.KEY_WATER_TREATMENT_PLANT_STORAGE_ROOM.ToString(),
        //Streets
        ItemTpl.KEY_ABANDONED_FACTORY_MARKED.ToString(),
        ItemTpl.KEY_BACKUP_HIDEOUT.ToString(),
        ItemTpl.KEY_BELUGA_RESTAURANT_DIRECTOR.ToString(),
        ItemTpl.KEY_CAR_DEALERSHIP_CLOSED_SECTION.ToString(),
        ItemTpl.KEY_CAR_DEALERSHIP_DIRECTORS_OFFICE_ROOM.ToString(),
        ItemTpl.KEY_CHEKANNAYA_15_APARTMENT.ToString(),
        ItemTpl.KEY_CONCORDIA_SECURITY_ROOM.ToString(),
        ItemTpl.KEY_IRON_GATE.ToString(),
        ItemTpl.KEY_NEGOTIATION_ROOM.ToString(),
        ItemTpl.KEY_PINEWOOD_HOTEL_ROOM_215.ToString(),
        // ItemTpl.KEY_PRIMORSKY_4648_SKYBRIDGE.ToString(),
        ItemTpl.KEY_REAL_ESTATE_AGENCY_OFFICE_ROOM.ToString(),
        ItemTpl.KEY_RELAXATION_ROOM.ToString(),
        // ItemTpl.KEY_RUSTED_BLOODY.ToString(), // haha
        ItemTpl.KEY_TERRAGROUP_MEETING_ROOM.ToString(),
        ItemTpl.KEY_XRAY_ROOM.ToString(),
        //GroundZero
        ItemTpl.KEY_TERRAGROUP_SCIENCE_OFFICE.ToString(),
        //Others
        ItemTpl.KEY_MISSAM_FORKLIFT.ToString(),
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in keys.ts
    public static readonly HashSet<MongoId> QuestKeysMongoIds = QuestKeys
        .Select(static x => new MongoId(x))
        .ToHashSet();

    // Marked keys
    public static readonly List<string> MarkedKeys = new()
    {
        ItemTpl.KEY_DORM_ROOM_314_MARKED.ToString(),
        ItemTpl.KEY_RBBK_MARKED.ToString(),
        ItemTpl.KEY_RBVO_MARKED.ToString(),
        ItemTpl.KEY_SHARED_BEDROOM_MARKED.ToString(),
        ItemTpl.KEY_RBPKPM_MARKED.ToString(),
        ItemTpl.KEY_MYSTERIOUS_ROOM_MARKED.ToString(),
        ItemTpl.KEY_ABANDONED_FACTORY_MARKED.ToString(),
    };

    // C# helper projection for runtime lookup; no 1:1 equivalent in keys.ts
    public static readonly HashSet<MongoId> MarkedKeysMongoIds = MarkedKeys
        .Select(static x => new MongoId(x))
        .ToHashSet();
}
