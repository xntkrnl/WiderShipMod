using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Collections.Generic;
using System.Linq;
using WiderShipMod.Methods;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Reflection;

namespace WiderShipMod.Patches
{
    public class WiderShipPatches
    {
        static bool needBake = false;

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

            if (whitelist.Contains(TimeOfDay.Instance.currentLevel.PlanetName) && needBake)
            {
                GameObject.FindGameObjectWithTag("OutsideLevelNavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
                //im lazy to fix it in unity
                var offmesh = GameObject.Find("ShipLadder3");
                offmesh.transform.position = new Vector3(7.3302f, 1.069f, -3.9058f);
                offmesh.GetComponent<OffMeshLink>().UpdatePositions();

                //HUDManager.Instance.DisplayTip("navmesh rebaked", $"needRebake = {needBake}", prefsKey: "WiderShip"); //debug info
                return;
            }

            if (blacklist.Contains(TimeOfDay.Instance.currentLevel.PlanetName))
                return; //SHOULD be builded already
        }

        //It would be (prob) easier to remove "if (num2 > 0)" but what if there are other mods that are looking for it?
        [HarmonyTranspiler, HarmonyPatch(typeof(RoundManager), "SpawnOutsideHazards")]
        static IEnumerable<CodeInstruction> SpawnOutsideHazardsPatch(IEnumerable<CodeInstruction> instructions)
        {
            var captureMethod = AccessTools.Method(typeof(WiderShipPatches), nameof(CaptureMethod));
            var codeMatcher = new CodeMatcher(instructions);

            codeMatcher.MatchEndForward(
                new CodeMatch(OpCodes.Ret)
            )
            .Insert(
                    new CodeInstruction(OpCodes.Ldloc_S, 5),
                    new CodeInstruction(OpCodes.Call, captureMethod)
            );

            return codeMatcher.InstructionEnumeration();
        }

        static void CaptureMethod(int num2)
        {
            if (num2 <= 0)
                needBake = true;
            else
                needBake = false;
        }
    }
}