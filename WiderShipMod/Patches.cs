using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

namespace WiderShipMod
{
    public class WiderShipPatches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void StartPatch()
        {
            ShipPartsFunctions.Init();
            ShipPartsFunctions.CreateBothParts();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ShipLights), "SetShipLightsClientRpc"), HarmonyPatch(typeof(ShipLights), "ToggleShipLightsOnLocalClientOnly")]
        static void SetShipLightsClientRpcPatch(ref bool ___areLightsOn)
        {
            string[] lightSources = new string[8] { "Area Light (4)(Clone)_left", "Area Light (5)(Clone)_left", "Area Light (8)(Clone)_left", "Area Light (7)(Clone)_left",
                "Area Light (4)(Clone)_right", "Area Light (5)(Clone)_right", "Area Light (8)(Clone)_right", "Area Light (7)(Clone)_right" };
            foreach (string source in lightSources)
                try
                {
                    GameObject.Find("Environment/HangarShip/ShipElectricLights/" + source).GetComponent<Light>().enabled = ___areLightsOn;
                }
                catch
                {
                    WiderShipPlugin.mls.LogInfo($"Can't find a light source: {source}");
                }

            if (___areLightsOn)
                ShipPartsFunctions.lampMaterials[3] = ShipPartsFunctions.bulbOnMaterial;
            else
                ShipPartsFunctions.lampMaterials[3] = ShipPartsFunctions.bulbOffMaterial;

            string[] lamps = new string[4] { "HangingLamp (3)(Clone)_left", "HangingLamp (4)(Clone)_left", "HangingLamp (3)(Clone)_right", "HangingLamp (4)(Clone)_right" };
            foreach (string lamp in lamps)
                try
                {
                    var lampObjRend = GameObject.Find("Environment/HangarShip/ShipElectricLights/" + lamp).GetComponent<MeshRenderer>();
                    lampObjRend.materials = ShipPartsFunctions.lampMaterials;
                }
                catch
                {
                    WiderShipPlugin.mls.LogInfo($"Can't find a lamp: {lamp}");
                }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RoundManager), "FinishGeneratingLevel")]
        static void FinishGeneratingLevelPatch()
        {
            //i hate it so much
            bool needOffmesh = true;
            string cubePath = "Environment/NavMeshColliders/PlayerShipNavmesh/";
            string ladderPath = "";

            if (GameObject.Find("PlayerShipNavmesh/Cube (1)").GetComponent<Renderer>().isPartOfStaticBatch)
            {

                HUDManager.Instance.DisplayTip("WiderShip Error!", "This moon has static navigation surfaces that cannot be changed. Expect bugs. Proceed with caution.", isWarning: true, useSave: false, "WS_WarningTip");
                ShipPartsFunctions.GenerateNavMesh();
                cubePath = "Environment/NavMeshColliders/ShipVanilaNavmesh(Clone)/NavMeshColliders/PlayerShipNavmesh/";
            }

            try
            {


                GameObject go = GameObject.FindGameObjectWithTag("OutsideLevelNavMesh");
                if (go != null)
                {
                    go.GetComponent<NavMeshSurface>().BuildNavMesh();
                }
            }
            catch (Exception ex)
            {
                WiderShipPlugin.mls.LogWarning("Cant change navmesh on that scene!!! The error below is normal");
                WiderShipPlugin.mls.LogError(ex.ToString());
            }

            try
            {
                string[] ladders = new string[3] { "ShipLadder", "ShipLadder2", "ShipLadder3" };

                foreach (string ladder in ladders)
                {
                    GameObject.Find(ladder).GetComponent<OffMeshLink>().UpdatePositions();
                }
            }
            catch (Exception ex)
            {
                WiderShipPlugin.mls.LogWarning("Cant change offmesh on that scene!!! The error below is normal");
                WiderShipPlugin.mls.LogError(ex.ToString());
            }

        }
    }
}