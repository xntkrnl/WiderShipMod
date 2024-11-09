using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx.Configuration;


namespace WiderShipMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("ainavt.lc.lethalconfig")]
    public class WiderShipPlugin : BaseUnityPlugin
    {
        // Mod Details
        private const string modGUID = "mborsh.WiderShipMod";
        private const string modName = "WiderShipMod";
        private const string modVersion = "1.2.5";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static ManualLogSource mls;

        public static AssetBundle mainAssetBundle;

        public static Material[] lampMaterials;
        public static Material bulbOnMaterial;
        public static Material bulbOffMaterial;

        private static WiderShipPlugin Instance;

        void Awake()
        {
            Instance = this;
            var cfg = new ConfigFile(Path.Combine(Paths.ConfigPath, "mborsh.WiderShipMod.cfg"), true);
            WiderShipConfig.Config(cfg);
            mls = BepInEx.Logging.Logger.CreateLogSource("Wider Ship Mod");
            mls = Logger;

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("MelanieMelicious.2StoryShip"))
            {
                mls.LogMessage("Hi Mel, �an you do all the hard work for me since 2StoryShip here?");
                return; //nothing is patched
            }

            if (!LoadAssetBundle())
            {
                mls.LogError("Failed to load asset bundle! Abort mission!");
                return;
            }

            mls.LogInfo("Wider Ship Mod loaded. Patching.");
            harmony.PatchAll(typeof(WiderShipPatches));

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