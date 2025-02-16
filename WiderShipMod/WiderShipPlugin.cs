using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx.Configuration;
using WiderShipMod.Patches;
using WiderShipMod.Compatibility.TwoStoryShip;
using WiderShipMod.Compatibility.LethalConfig;
using WiderShipMod.Compatibility.CelestialTint;
using BepInEx.Bootstrap;
using System;
using WiderShipMod.Compatibility;
using WiderShipMod.Compatibility.ShipWindowsBeta;


namespace WiderShipMod
{
    [BepInDependency("ainavt.lc.lethalconfig", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("MelanieMelicious.2StoryShip", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("CelestialTint", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("TestAccount666.ShipWindows", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("TestAccount666.ShipWindowsBeta", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WiderShipPlugin : BaseUnityPlugin
    {
        // Mod Details
        private const string modGUID = "mborsh.WiderShipMod";
        private const string modName = "WiderShipMod";
        private const string modVersion = "1.3.13";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static ManualLogSource mls;

        public static AssetBundle mainAssetBundle;

        public static Material[] lampMaterials;
        public static Material bulbOnMaterial;
        public static Material bulbOffMaterial;

        private static WiderShipPlugin Instance;

        public static bool is2StoryHere = false;

        internal static GameObject[] windowGOs = new GameObject[3];
        internal static GameObject[] vanilaGOs = new GameObject[3];

        void Awake()
        {
            Instance = this;
            var cfg = new ConfigFile(Path.Combine(Paths.ConfigPath, "mborsh.WiderShipMod.cfg"), true);
            WiderShipConfig.Config(cfg);
            mls = BepInEx.Logging.Logger.CreateLogSource("Wider Ship Mod");
            mls = Logger;

            WiderShipConfig.example.Value = "220 Assurance,5 Embrion,44 Atlantica,46 Infernis,134 Oldred,154 Etern";

            if (!LoadAssetBundle())
            {
                mls.LogError("Failed to load asset bundle! Abort mission!");
                return;
            }

            mls.LogInfo("Wider Ship Mod loaded. Patching.");
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("ainavt.lc.lethalconfig"))
            {
                LethalConfigCompat.LethalConfigSetup();
            }

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("MelanieMelicious.2StoryShip"))
            {
                is2StoryHere = true;
                mls.LogMessage("Hi Mel, ñan you do all the hard work for me since 2StoryShip here?");
                TwoStoryShip.Init(harmony);
                return;
            }

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("TestAccount666.ShipWindows") || Chainloader.PluginInfos.ContainsKey("TestAccount666.ShipWindowsBeta"))
            {
                mls.LogMessage("Hi TestAccount666.");
                if (!(CompatibilityUtilities.IsInstalledVersionGreaterOrEquallThanNeeded("TestAccount666.ShipWindows", new Version(2, 0, 0)) || Chainloader.PluginInfos.ContainsKey("TestAccount666.ShipWindowsBeta")))
                    mls.LogError("Older version of ShipWindows is present. You need to change the version of WiderShipMod to 1.3.13");
                else
                {
                    mls.LogInfo("ShipWindowsBeta or ShipWindows 2.x.x found");
                    ShipWindowsUtils.RemoveRoofWindow();
                    harmony.PatchAll(typeof(ShipWindowsBetaPatches));
                }
            }

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("CelestialTint"))
            {
                mls.LogMessage("Celestial Tint found.");
                harmony.PatchAll(typeof(CelestialTintPatch));
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