using LethalConfig;
using LethalConfig.ConfigItems;

namespace WiderShipMod.Compatibility.LethalConfig
{
    internal class LethalConfigCompat
    {
        internal static void LethalConfigSetup()
        {
            var enumExtendedSide = new EnumDropDownConfigItem<Side>(WiderShipConfig.extendedSide);

            var boolForceDontMovePosters = new BoolCheckBoxConfigItem(WiderShipConfig.enableForceDontMovePosters);

            var boolLeftInnerWall = new BoolCheckBoxConfigItem(WiderShipConfig.enableLeftInnerWall);
            var boolLeftInnerWallSolidMode = new BoolCheckBoxConfigItem(WiderShipConfig.enableLeftInnerWallSolidMode);
            var boolRightInnerWall = new BoolCheckBoxConfigItem(WiderShipConfig.enableRightInnerWall);
            var boolRightInnerWallSolidMode = new BoolCheckBoxConfigItem(WiderShipConfig.enableRightInnerWallSolidMode);

            var boolBuildNewNavmesh = new BoolCheckBoxConfigItem(WiderShipConfig.enableBuildNewNavmesh);
            var boolClientBuildNavmeshToo = new BoolCheckBoxConfigItem(WiderShipConfig.enableClientBuildNavmeshToo);
            var textWhitelist = new TextInputFieldConfigItem(WiderShipConfig.whitelist);
            var textBlacklist = new TextInputFieldConfigItem(WiderShipConfig.blacklist);
            var textExanmple = new TextInputFieldConfigItem(WiderShipConfig.example);

            LethalConfigManager.AddConfigItem(enumExtendedSide);

            LethalConfigManager.AddConfigItem(boolForceDontMovePosters);

            LethalConfigManager.AddConfigItem(boolLeftInnerWall);
            LethalConfigManager.AddConfigItem(boolLeftInnerWallSolidMode);
            LethalConfigManager.AddConfigItem(boolRightInnerWall);
            LethalConfigManager.AddConfigItem(boolRightInnerWallSolidMode);

            LethalConfigManager.AddConfigItem(boolBuildNewNavmesh);
            LethalConfigManager.AddConfigItem(boolClientBuildNavmeshToo);
            LethalConfigManager.AddConfigItem(textWhitelist);
            LethalConfigManager.AddConfigItem(textBlacklist);
            LethalConfigManager.AddConfigItem(textExanmple);
        }
    }
}

