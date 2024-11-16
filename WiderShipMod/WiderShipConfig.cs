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
        internal static ConfigEntry<bool> enableWarning;

        internal static ConfigEntry<bool> enableForceDontMovePosters;

        internal static ConfigEntry<bool> enableLeftInnerWall;
        internal static ConfigEntry<bool> enableLeftInnerWallSolidMode;

        internal static ConfigEntry<bool> enableRightInnerWall;
        internal static ConfigEntry<bool> enableRightInnerWallSolidMode;

        internal static void Config(ConfigFile cfg)
        {
            extendedSide = cfg.Bind("General", "Extended Side", Side.Both,
                "Left - only left side;\nRight - only right side;\nBoth - both sides.");
            enableWarning = cfg.Bind("General", "Enable Warning", true,
                "Enable if you want to know when to expect more bugs lol.");

            enableForceDontMovePosters = cfg.Bind("Compatibility", "Force Dont move posters to true", true,
                "Force \"Don't move posters\" to true so that the posters don't hang in the middle of the ship.\n" +
                "It probably won't matter in the future if TestAccount666 makes DontMovePosters true by default");

            enableLeftInnerWall = cfg.Bind("Left Part", "Enable Left Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableLeftInnerWallSolidMode = cfg.Bind("Left Part", "Solid Left Inner Wall", false,
                "Enable if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");

            enableRightInnerWall = cfg.Bind("Right Part", "Enable Right Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableRightInnerWallSolidMode = cfg.Bind("Right Part", "Solid Right Inner Wall", false,
                "Enable if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");
        }
    }
}
