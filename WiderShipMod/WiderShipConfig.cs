using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace WiderShipMod
{
    public class WiderShipConfig
    {
        public static ConfigEntry<bool> enableLeftInnerWall;
        public static ConfigEntry<bool> enableLeftInnerWallSolidMode;
        //public static ConfigEntry<bool> enableLeftUpgradeBuyable;
        //public static ConfigEntry<bool> enableRightWall;

        public static void Config(ConfigFile cfg)
        {
            enableLeftInnerWall = cfg.Bind("General", "Enable Left Inner Wall", true,
                "Enable this if you want to have a wall between the new ship part and the vanilla one.");
            enableLeftInnerWallSolidMode = cfg.Bind("General", "Solid Left Inner Wall", false,
                "Enable if you want your wall to be solid. Doesn't work without Left Inner Wall enabled.");
            //enableLeftUpgradeBuyable = cfg.Bind("General", "Enable Upgrade Mode", false,
            //    "WIP - DOES NOTHING.\nEnable this if you want to buy a new ship part.");

            var boolLeftInnerWall = new BoolCheckBoxConfigItem(enableLeftInnerWall);
            var boolLeftInnerWallSolidMode = new BoolCheckBoxConfigItem(enableLeftInnerWallSolidMode);
            //var boolLeftUpgradeBuyable = new BoolCheckBoxConfigItem(enableLeftUpgradeBuyable);

            LethalConfigManager.AddConfigItem(boolLeftInnerWall);
            LethalConfigManager.AddConfigItem(boolLeftInnerWallSolidMode);
            //LethalConfigManager.AddConfigItem(boolLeftUpgradeBuyable);
        }
    }
}
