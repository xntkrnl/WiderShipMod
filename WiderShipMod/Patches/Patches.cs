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
        

        [HarmonyBefore("TestAccount666.ShipWindows", "TestAccount666.ShipWindowsBeta")]
        [HarmonyPrefix, HarmonyPatch(typeof(HUDManager), "Awake")]
        static void HudManagerAwakePatch()
        {
            ShipSidesMethods.Init();
            ShipSidesMethods.CreateShip();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RoundManager), "FinishGeneratingLevel")]
        static void FinishGeneratingLevelPrefix()
        {
            if (!WiderShipConfig.enableBuildNewNavmesh.Value) return; //do not build anything

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

            string[] whitelist = WiderShipConfig.whitelist.Value.Split(','); //slow
            string[] blacklist = WiderShipConfig.blacklist.Value.Split(','); //fast

            if (whitelist.Contains(TimeOfDay.Instance.currentLevel.PlanetName))
            {
                GameObject.FindGameObjectWithTag("OutsideLevelNavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
                //im lazy to fix it in unity
                var offmesh = GameObject.Find("ShipLadder3");
                offmesh.transform.position = new Vector3(7.3302f, 1.069f, -3.9058f);
                offmesh.GetComponent<OffMeshLink>().UpdatePositions();

                return;
            }

            if (blacklist.Contains(TimeOfDay.Instance.currentLevel.PlanetName))
                return; //SHOULD be builded already

            WiderShipPlugin.mls.LogError("How did you even get there? THIS IS SUPER BAD!!!");
        }
    }
}