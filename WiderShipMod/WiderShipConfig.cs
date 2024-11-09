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
