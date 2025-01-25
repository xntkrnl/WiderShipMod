using BepInEx.Configuration;

namespace WiderShipMod
{
    internal enum Side
    {
        Left,
        Right,
        Both
    }

    internal class WiderShipConfig
    {
        internal static ConfigEntry<Side> extendedSide;
        //internal static ConfigEntry<bool> enableWarning;

        internal static ConfigEntry<bool> enableForceDontMovePosters;

        internal static ConfigEntry<bool> enableLeftInnerWall;
        internal static ConfigEntry<bool> enableLeftInnerWallSolidMode;
        internal static ConfigEntry<bool> enableRightInnerWall;
        internal static ConfigEntry<bool> enableRightInnerWallSolidMode;

        internal static ConfigEntry<bool> enableBuildNewNavmesh;
        internal static ConfigEntry<bool> enableClientBuildNavmeshToo;
        internal static ConfigEntry<string> whitelist;
        internal static ConfigEntry<string> blacklist;
        internal static ConfigEntry<string> example;

        internal static void Config(ConfigFile cfg)
        {
            extendedSide = cfg.Bind("General", "Extended Side", Side.Both,
                "Left - only left side.\nRight - only right side.\nBoth - both sides.");
            //enableWarning = cfg.Bind("General", "Enable Warning", true,
            //    "Enable if you want to know when to expect more bugs lol.");

            enableForceDontMovePosters = cfg.Bind("Compatibility", "Force Dont move posters to true", true,
                "Force \"Don't move posters\" to true so that the posters don't hang in the middle of the ship.\n" +
                "It probably won't matter in the future if TestAccount666 makes DontMovePosters true by default.");

            enableLeftInnerWall = cfg.Bind("Walls", "Enable Left Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableLeftInnerWallSolidMode = cfg.Bind("Walls", "Solid Left Inner Wall", false,
                "Enable this if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");
            enableRightInnerWall = cfg.Bind("Walls", "Enable Right Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableRightInnerWallSolidMode = cfg.Bind("Walls", "Solid Right Inner Wall", false,
                "Enable this if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");
            
            enableBuildNewNavmesh = cfg.Bind("Navmesh", "Enable New Navmesh", true,
                "Enable this to generate new navmesh (YOU DON't WANT THIS TO BE FALSE!!!)\n" +
                "Only for testing purposes.");
            enableClientBuildNavmeshToo = cfg.Bind("Navmesh", "Enable Client Build Navmesh Too", false,
                "Enable this if you (as a client) want to generate navmesh.\n" +
                "It won't speed up map loading, but will most likely slow it down for everyone.\n" +
                "Made for testing and debugging in case of anything.");
            blacklist = cfg.Bind("Navmesh", "Moon Blacklist", "42 Cosmocos, 67 Utril",
                "On these moons,Wider Ship will try to PLACE its own navmesh instead of the vanilla one.\n" +
                "(Fast, buggy)");
            whitelist = cfg.Bind("Navmesh", "Moon Whitelist", "220 Assurance,5 Embrion",
                "On these moons,Wider Ship will try to CREATE its own navmesh instead of the vanilla one.\n" +
                "(Slower, less buggy, old method of creating navmesh)");
            example = cfg.Bind("Navmesh", "Example moon list", "220 Assurance,5 Embrion,44 Atlantica,46 Infernis,134 Oldred,154 Etern",
                "Example list for whitelist/blacklist.\n" +
                "Will fill up again every time if you accidentally erase it.");
        }
    }
}
