using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Collections.Generic;
using System.Linq;
using WiderShipMod.Methods;

namespace WiderShipMod.Patches
{
    public class WiderShipPatches
    {
        

        [HarmonyAfter("TestAccount666.ShipWindows")]
        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void StartPatch()
        {
            ShipSidesMethods.Init();
            ShipSidesMethods.CreateShip();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RoundManager), "FinishGeneratingLevel")]
        static void FinishGeneratingLevelPrefix()
        {
            if (!WiderShipConfig.enableBuildNewNavmesh.Value) return; //do not build anything

            if (!GameNetworkManager.Instance.isHostingGame || WiderShipConfig.enableClientBuildNavmeshToo.Value) return;

            string[] whitelist = WiderShipConfig.whitelist.Value.Split(','); //slow
            string[] blacklist = WiderShipConfig.blacklist.Value.Split(','); //fast

            if (!whitelist.Contains(TimeOfDay.Instance.currentLevel.PlanetName) && !blacklist.Contains(TimeOfDay.Instance.currentLevel.PlanetName))
            {
                WiderShipPlugin.mls.LogWarning("This moon not in whitelist or blacklits!!! Adding this moon to the Wider Ship whitelist...");
                WiderShipConfig.whitelist.Value += $",{TimeOfDay.Instance.currentLevel.PlanetName}";
            }

            NavmeshMethods.PlaceNavmesh();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RoundManager), "FinishGeneratingLevel")]
        static void FinishGeneratingLevelPost()
        {
            if (!WiderShipConfig.enableBuildNewNavmesh.Value) return; //do not build anything

            if (!GameNetworkManager.Instance.isHostingGame || WiderShipConfig.enableClientBuildNavmeshToo.Value) return;

            string[] whitelist = WiderShipConfig.whitelist.Value.Split(','); //slow
            string[] blacklist = WiderShipConfig.blacklist.Value.Split(','); //fast

            if (whitelist.Contains(TimeOfDay.Instance.currentLevel.PlanetName))
            {
                GameObject.Find("Environment").GetComponent<NavMeshSurface>().BuildNavMesh();
                return;
            }

            if (blacklist.Contains(TimeOfDay.Instance.currentLevel.PlanetName))
                return; //SHOULD be builded already

            WiderShipPlugin.mls.LogError("How did you even get there? THIS IS SUPER BAD!!!");
        }
    }
}