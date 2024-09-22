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
                //needOffmesh = false;
                ShipPartsFunctions.GenerateNavMesh();
                cubePath = "Environment/NavMeshColliders/ShipVanilaNavmesh(Clone)/NavMeshColliders/PlayerShipNavmesh/";
                ladderPath = "Environment/NavMeshColliders/ShipVanilaNavmesh(Clone)/NavMeshColliders/OffMeshLinks/";
            }

            try
            {
                ///nav cubes stuff
                ///LEFT PART
                //PlayerShipNavmesh/
                ObjFunctions.CopyObj("Cube (17)", new Vector3(0f, 0f, 0f), cubePath).name += "_left";
                ObjFunctions.CopyObj("Cube (11)", new Vector3(0f, 0f, 0f), cubePath).name += "_left";
                ObjFunctions.CopyObj("Cube (13)", new Vector3(0f, 0f, 0f), cubePath).name += "_left";
                ObjFunctions.CopyObj("Cube (6)", new Vector3(0f, 0f, 0f), cubePath).name += "_left";
                ObjFunctions.CopyObj("Cube (12)", new Vector3(0f, 0f, 0f), cubePath).name += "_left";

                ObjFunctions.ScaleObj("Cube (17)(Clone)_left", new Vector3(11f, 0.4828268f, 5f), cubePath);
                ObjFunctions.ScaleObj("Cube (6)(Clone)_left", new Vector3(11f, 0.2792f, 6f), cubePath);
                ObjFunctions.ScaleObj("Cube (11)", new Vector3(8f, 5.727483f, 0.6064278f), cubePath);

                ObjFunctions.MoveObjToPoint("Cube (16)", new Vector3(15.9863f, -0.9428f, -5.19f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (12)", new Vector3(26.27f, -3.7588f, -0.87f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (11)", new Vector3(16.122f, -3.7588f, -0.23f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (17)(Clone)_left", new Vector3(17.07f, -0.63f, -2.32f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (11)(Clone)_left", new Vector3(16.7011f, -3.7588f, -5.39f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (13)", new Vector3(11.57f, -3.7588f, -2.574f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (6)(Clone)_left", new Vector3(17.2f, -5.55f, -2.091f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (12)(Clone)_left", new Vector3(22.316f, -3.7588f, -3.269f), cubePath);

                ObjFunctions.RotateObj("Cube (12)(Clone)_left", Vector3.up, cubePath, -60f);

                //prob forgot smth

                //PlayerShipNavmesh/SpaceBelowShip/
                ObjFunctions.CopyObj("MediumSpace", new Vector3(0f, 0f, -4f), "PlayerShipNavmesh/SpaceBelowShip/");
                ObjFunctions.CopyObj("SmallSpace", new Vector3(0f, 0f, -6f), "PlayerShipNavmesh/SpaceBelowShip/");

                ///nav cubes stuff
                ///RIGHT PART
                //PlayerShipNavmesh/
                GameObject.Find(cubePath + "Cube (10)").SetActive(false);

                ObjFunctions.CopyObj("Cube (17)", new Vector3(0f, 0f, 0f), cubePath).name += "_right";
                ObjFunctions.CopyObj("Cube (12)", new Vector3(0f, 0f, 0f), cubePath).name += "_right";
                ObjFunctions.CopyObj("Cube (12)(Clone)_left", new Vector3(0f, 0f, 0f), cubePath).name = "Cube (12)(Clone)_right (2)";
                ObjFunctions.CopyObj("Cube (11)(Clone)_left", new Vector3(0f, 0f, 0f), cubePath).name = "Cube (11)(Clone)_right (2)";
                ObjFunctions.CopyObj("Cube (11)", new Vector3(0f, 0f, 0f), cubePath).name += "_right";
                ObjFunctions.CopyObj("Cube (13)", new Vector3(0f, 0f, 0f), cubePath).name += "_right";
                ObjFunctions.CopyObj("Cube (6)", new Vector3(0f, 0f, 0f), cubePath).name += "_right";

                ObjFunctions.ScaleObj("Cube (17)(Clone)_right", new Vector3(11f, 0.4828268f, 5f), cubePath);
                ObjFunctions.ScaleObj("Cube (6)(Clone)_right", new Vector3(11f, 0.2792f, 6f), cubePath);

                ObjFunctions.MoveObjToPoint("Cube (18)", new Vector3(15.9863f, -0.9428f, 9.77f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (12)(Clone)_right", new Vector3(26.27f, -3.7588f, 5.957f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (12)(Clone)_right (2)", new Vector3(21.83f, -3.7588f, 7.89f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (11)(Clone)_right", new Vector3(16.122f, -3.7588f, 4.52f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (11)(Clone)_right (2)", new Vector3(16.7011f, -3.7588f, 9.860001f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (13)(Clone)_right", new Vector3(11.57f, -3.7588f, 7.426f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (17)(Clone)_right", new Vector3(17.07f, -0.63f, 6.93f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (6)(Clone)_right", new Vector3(17.2f, -5.55f, 6.659f), cubePath);

                ObjFunctions.SetAnglesObj("Cube (12)(Clone)_right (2)", new Vector3(0f, 60f, 0f), cubePath);

                //PlayerShipNavmesh/SpaceBelowShip/
                ObjFunctions.CopyObj("MediumSpace", new Vector3(0f, 0f, 4f), "PlayerShipNavmesh/SpaceBelowShip/");
                ObjFunctions.CopyObj("SmallSpace", new Vector3(0f, 0f, 6f), "PlayerShipNavmesh/SpaceBelowShip/");
                ObjFunctions.MoveObjToPoint("MediumSpace1", new Vector3(19.86f, -10.4383f, 11.54f), "PlayerShipNavmesh/SpaceBelowShip/");

                ///nav cubes stuff
                ///CATWALK
                GameObject.Find(cubePath + "Cube (1)").SetActive(false);

                ObjFunctions.ScaleObj("Cube", new Vector3(2.69503f, 0.2792f, 18f), cubePath);

                ObjFunctions.MoveObjToPoint("Cube", new Vector3(9.7117f, -5.75f, 3.69f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (2)", new Vector3(17.53f, -5.75f, 11.64f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (3)", new Vector3(25.3f, -5.75f, 7.1267f), cubePath);
                ObjFunctions.CopyObj("Cube (3)", new Vector3(0f, 0f, 0f), cubePath);
                ObjFunctions.MoveObjToPoint("Cube (3)", new Vector3(25.39f, -5.75f, 11f), cubePath);
                ObjFunctions.ScaleObj("Cube (3)", new Vector3(5f, 0.2792f, 5f), cubePath);
                ObjFunctions.ScaleObj("Cube (2)", new Vector3(12.93f, 0.2792f, 2.784142f), cubePath);

                GameObject go = GameObject.FindGameObjectWithTag("OutsideLevelNavMesh");
                if (go != null)
                {
                    go.GetComponent<NavMeshSurface>().BuildNavMesh();
                }
            }
            catch (Exception ex)
            {
                WiderShipPlugin.mls.LogWarning("Cant change navmesh on that scene!!!");
                WiderShipPlugin.mls.LogError(ex.ToString());
            }


            if (needOffmesh)
            {
                try
                {

                    //ladder nodes (off mesh)
                    ObjFunctions.MoveObjToPoint("A", new Vector3(1.89f, -4.72f, 3.1f), ladderPath + "ShipLadder/");
                    ObjFunctions.MoveObjToPoint("B", new Vector3(0.644f, -0.98f, 1.76f), ladderPath + "ShipLadder/");

                    ObjFunctions.MoveObjToPoint("A", new Vector3(-2.44f, -2.66f, -4.62f), ladderPath + "ShipLadder2/");
                    ObjFunctions.MoveObjToPoint("B", new Vector3(-2.44f, -1.054f, -2.972f), ladderPath + "ShipLadder2/");

                    ObjFunctions.MoveObjToPoint("A", new Vector3(-7.96f, -0.74f, 3.16f), ladderPath + "ShipLadder3/");
                    ObjFunctions.MoveObjToPoint("B", new Vector3(-8.11f, 4.473f, 1.559f), ladderPath + "ShipLadder3/");

                    //why is this not navmeshlink
                    string[] ladders = new string[3] { "ShipLadder", "ShipLadder2", "ShipLadder3" };

                    foreach (string ladder in ladders)
                    {
                        GameObject.Find(ladder).GetComponent<OffMeshLink>().UpdatePositions();
                    }
                }
                catch (Exception ex)
                {
                    WiderShipPlugin.mls.LogWarning("Cant change offmesh on that scene!!!");
                    WiderShipPlugin.mls.LogError(ex.ToString());
                }

            }
        }
    }
}