using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace WiderShipMod
{
    public class WiderShipConfig
    {
        public static ConfigEntry<bool> enableLeftInnerWall;
        public static ConfigEntry<bool> enableLeftInnerWallSolidMode;
        public static ConfigEntry<bool> enableRightInnerWall;
        public static ConfigEntry<bool> enableRightInnerWallSolidMode;

        public static void Config(ConfigFile cfg)
        {
            enableLeftInnerWall = cfg.Bind("Left Part", "Enable Left Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableLeftInnerWallSolidMode = cfg.Bind("Left Part", "Solid Left Inner Wall", false,
                "Enable if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");

            enableRightInnerWall = cfg.Bind("Right Part", "Enable Right Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableRightInnerWallSolidMode = cfg.Bind("Right Part", "Solid Right Inner Wall", false,
                "Enable if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");

            var boolLeftInnerWall = new BoolCheckBoxConfigItem(enableLeftInnerWall);
            var boolLeftInnerWallSolidMode = new BoolCheckBoxConfigItem(enableLeftInnerWallSolidMode);

            var boolRightInnerWall = new BoolCheckBoxConfigItem(enableRightInnerWall);
            var boolRightInnerWallSolidMode = new BoolCheckBoxConfigItem(enableRightInnerWallSolidMode);

            LethalConfigManager.AddConfigItem(boolLeftInnerWall);
            LethalConfigManager.AddConfigItem(boolLeftInnerWallSolidMode);

            LethalConfigManager.AddConfigItem(boolRightInnerWall);
            LethalConfigManager.AddConfigItem(boolRightInnerWallSolidMode);
        }
    }
}
