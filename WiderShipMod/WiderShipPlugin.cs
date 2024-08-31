using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;


namespace WiderShipMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WiderShipPlugin : BaseUnityPlugin
    {
        // Mod Details
        private const string modGUID = "mborsh.WiderShipMod";
        private const string modName = "WiderShipMod";
        private const string modVersion = "1.0.5";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static ManualLogSource mls;

        public static AssetBundle mainAssetBundle;
        public static GameObject newShipObj;

        public static Material[] lampMaterials;
        public static Material bulbOnMaterial;
        public static Material bulbOffMaterial;

        private static WiderShipPlugin Instance;

        void Awake()
        {
            Instance = this;

            mls = BepInEx.Logging.Logger.CreateLogSource("Wider Ship");
            mls = Logger;

            mls.LogInfo("Wider Ship Mod loaded. Patching.");
            //harmony.PatchAll(typeof(WiderShipObjFunctions));
            harmony.PatchAll(typeof(WiderShipPatches));

            if (!LoadAssetBundle())
            {
                mls.LogError("Failed to load asset bundle! Abort mission!");
                return;
            }

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