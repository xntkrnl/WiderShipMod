using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Collections.Generic;

namespace WiderShipMod.Patches
{
    public class WiderShipPatches
    {
        [HarmonyAfter("TestAccount666.ShipWindows")]
        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void StartPatch()
        {
            ShipPartsFunctions.Init();
            ShipPartsFunctions.CreateShip();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RoundManager), "FinishGeneratingLevel")]
        static void FinishGeneratingLevelPatch()
        {
            if (!WiderShipConfig.enableBuildNewNavmesh.Value) return;

            if (GameNetworkManager.Instance.isHostingGame || !WiderShipConfig.enableClientBuildNavmeshToo.Value) return;
        }
    }
}