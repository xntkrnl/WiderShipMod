using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx.Configuration;
using WiderShipMod.Patches;
using WiderShipMod.Compatibility;
using WiderShipMod.Compatibility.ShipWindows;


namespace WiderShipMod
{
    [BepInDependency("MelanieMelicious.2StoryShip", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("TestAccount666.ShipWindows", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WiderShipPlugin : BaseUnityPlugin
    {
        // Mod Details
        private const string modGUID = "mborsh.WiderShipMod";
        private const string modName = "WiderShipMod";
        private const string modVersion = "1.3.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static ManualLogSource mls;

        public static AssetBundle mainAssetBundle;

        public static Material[] lampMaterials;
        public static Material bulbOnMaterial;
        public static Material bulbOffMaterial;

        private static WiderShipPlugin Instance;

        public static bool is2StoryHere = false;
        public static bool isShipWindowsHere = false;

        void Awake()
        {
            Instance = this;
            var cfg = new ConfigFile(Path.Combine(Paths.ConfigPath, "mborsh.WiderShipMod.cfg"), true);
            WiderShipConfig.Config(cfg);
            mls = BepInEx.Logging.Logger.CreateLogSource("Wider Ship Mod");
            mls = Logger;

            if (!LoadAssetBundle())
            {
                mls.LogError("Failed to load asset bundle! Abort mission!");
                return;
            }
            mls.LogInfo("Wider Ship Mod loaded. Patching.");
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("MelanieMelicious.2StoryShip"))
            {
                is2StoryHere = true;
                mls.LogMessage("Hi Mel, ñan you do all the hard work for me since 2StoryShip here?");
                harmony.PatchAll(typeof(LightPatches));
                return; //melanie only needs lamps and light patch
            }

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("TestAccount666.ShipWindows"))
            {
                mls.LogMessage("Hi TestAccount666.");
                isShipWindowsHere = true;
                harmony.PatchAll(typeof(ShipWindowsPatches));
            }

            harmony.PatchAll(typeof(WiderShipPatches));
            harmony.PatchAll(typeof(LightPatches));

            bool LoadAssetBundle()
            {
                mls.LogInfo("Loading AssetBundle...");
                string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                mainAssetBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "newship"));

                if (mainAssetBundle == null)
                    return false;

                mls.LogInfo($"AssetBundle {mainAssetBundle.name} loaded from {sAssemblyLocation}.");
                return true;
            }
        }
    }
}