using HarmonyLib;
using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace WiderShipMod
{
    public class WiderShipPatches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void StartPatch()
        {
            var vanilaSI = GameObject.Find("Environment/HangarShip/ShipInside");
            var vanilaSR = GameObject.Find("Environment/HangarShip/ShipRails");
            var vanilaSRP = GameObject.Find("Environment/HangarShip/ShipRailPosts");
            //var catwalk = GameObject.Find("Environment/HangarShip/CatwalkShip");

            var vanilaSIMaterials = vanilaSI.GetComponent<MeshRenderer>().materials;
            var vanilaSRMaterials = vanilaSR.GetComponent<MeshRenderer>().materials;
            var moddedSIMaterials = new Material[vanilaSIMaterials.Length - 2];
            Array.Copy(vanilaSIMaterials, 0, moddedSIMaterials, 0, 3);

            ///Ship and rails

            //WiderShipObjFunctions.CreateShipObj(vanilaSI, "ShipInsideMiddleVanila.fbx", vanilaSI.layer, vanilaSI.tag);
            //WiderShipObjFunctions.CreateShipObj(vanilaSI, "Floor.fbx", vanilaSI.layer, vanilaSI.tag);
            string wallName;
            var ShipInsideLeft = WiderShipObjFunctions.CreateShipObj(vanilaSI, "ShipInsideLeftVanila.fbx", vanilaSI.layer, vanilaSI.tag);
            var FloorLeft = WiderShipObjFunctions.CreateShipObj(vanilaSI, "FloorLeft.fbx", vanilaSI.layer, vanilaSI.tag);
            //WiderShipObjFunctions.CreateShipObj(vanilaSR, "ShipRailsEDITED.fbx", vanilaSR.layer, vanilaSR.tag);
            //WiderShipObjFunctions.CreateShipObj(vanilaSRP, "ShipRailPostsEDITED.fbx", vanilaSRP.layer, vanilaSRP.tag);

            if (WiderShipConfig.enableLeftInnerWall.Value == true)
            {
                if (WiderShipConfig.enableLeftInnerWallSolidMode.Value == true)
                    wallName = WiderShipObjFunctions.CreateShipObj(vanilaSI, "Wall.fbx", vanilaSI.layer, vanilaSI.tag);
                else
                    wallName = WiderShipObjFunctions.CreateShipObj(vanilaSI, "Beams.fbx", vanilaSI.layer, vanilaSI.tag);

                WiderShipObjFunctions.SetAnglesObj(wallName, new Vector3(-89.98f, 0f, 0f), "Environment/HangarShip/");
                WiderShipObjFunctions.MoveObjToPoint(wallName, new Vector3(0f, 0.952f, -5.224f), "Environment/HangarShip/");
                GameObject.Find("Environment/HangarShip/" + wallName).GetComponent<MeshRenderer>().material = moddedSIMaterials[1];
            }

            //WiderShipObjFunctions.MoveObjToPoint("ShipRailsEDITED(Clone)", new Vector3(-10.19258f, 0.45f, -2.25f), "Environment/HangarShip/");
            //WiderShipObjFunctions.MoveObjToPoint("ShipRailPostsEDITED(Clone)", new Vector3(-10.19258f, 0.4117996f, -2.25f), "Environment/HangarShip/");

            GameObject.Find("Environment/HangarShip/FloorLeft(Clone)").GetComponent<MeshRenderer>().materials = moddedSIMaterials;
            GameObject.Find("Environment/HangarShip/ShipInsideLeftVanila(Clone)").GetComponent<MeshRenderer>().materials = moddedSIMaterials;
            //GameObject.Find("Environment/HangarShip/ShipRailsEDITED(Clone)").GetComponent<MeshRenderer>().materials = vanilaSRMaterials;
            //GameObject.Find("Environment/HangarShip/ShipRailPostsEDITED(Clone)").GetComponent<MeshRenderer>().materials = vanilaSRMaterials;


            vanilaSI.SetActive(false);
            //vanilaSR.SetActive(false);
            //vanilaSRP.SetActive(false);

            ///LadderShort (1)
            WiderShipObjFunctions.MoveObjToPoint("LadderShort (1)", new Vector3(-9f, -2.58f, -11.093f), "Environment/HangarShip/");

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (9)" };
            foreach (string lamp in lamps)
                WiderShipObjFunctions.CopyObj(lamp, new Vector3(0f, 0f, -4.5f), "Environment/HangarShip/ShipElectricLights/");

            WiderShipPlugin.lampMaterials = GameObject.Find("Environment/HangarShip/ShipElectricLights/HangingLamp (3)").GetComponent<MeshRenderer>().materials;
            WiderShipPlugin.bulbOnMaterial = WiderShipPlugin.lampMaterials[3];
            WiderShipPlugin.bulbOffMaterial = WiderShipPlugin.lampMaterials[0];

            ///ShipInnerRoomBoundsTrigger
            WiderShipObjFunctions.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -9.1742f), "Environment/HangarShip/");
            WiderShipObjFunctions.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 11f), "Environment/HangarShip/");

            ///SideMachineryLeft and stuff
            WiderShipObjFunctions.SetChildObjToParentObj("Pipework2.002", "SideMachineryLeft", "Environment/HangarShip/", "Environment/HangarShip/");
            WiderShipObjFunctions.MoveObjToPoint("SideMachineryLeft", new Vector3(8.304f, 1.6f, -2.597f), "Environment/HangarShip/");
            WiderShipObjFunctions.SetAnglesObj("SideMachineryLeft", new Vector3(180f, 90f, -90f), "Environment/HangarShip/");

            ///Magnet
            WiderShipObjFunctions.MoveObjToPoint("GiantCylinderMagnet", new Vector3(-0.08f, 2.46f, -14.72f), "Environment/HangarShip/");

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                WiderShipObjFunctions.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -2.3f), "Environment/HangarShip/ReverbTriggers/");
                WiderShipObjFunctions.ScaleObj(trigger, new Vector3(16f, 1.76f, 10.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            WiderShipObjFunctions.MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            WiderShipObjFunctions.ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");

            ///Vent
            WiderShipObjFunctions.MoveObjToPoint("VentEntrance", new Vector3(5.715f, 2.104f, -3.376f), "Environment/HangarShip/");

            ///ChargeStation
            WiderShipObjFunctions.RotateObj("ChargeStation", Vector3.up, "Environment/HangarShip/ShipModels2b/", -60f);
            WiderShipObjFunctions.MoveObjToPoint("ChargeStation", new Vector3(1.9f, 1.25f, -2.6f), "Environment/HangarShip/ShipModels2b/");

            ///Light (alarm)
            WiderShipObjFunctions.MoveObjToPoint("Light (3)", new Vector3(3f, 3.13f, -3.1f), "Environment/HangarShip/ShipModels2b/");
            WiderShipObjFunctions.MoveObjToPoint("Light (1)", new Vector3(4.742f, 3.249997f, -10.823f), "Environment/HangarShip/ShipModels2b/");
            WiderShipObjFunctions.RotateObj("Light (3)", Vector3.up, "Environment/HangarShip/ShipModels2b/", -85f);

            ///Plane.001 (Posters)
            ///Sadly, need to disable for now.
            GameObject.Find("Environment/HangarShip/Plane.001").SetActive(false);

            ///LeavingShip
            GameObject.Find("Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/LeavingShip (6)").SetActive(false);
            //leaving ship (3) pos -2.817 3.2812 -5.797956    scale 2.983845 12.2338 0.8325825
            WiderShipObjFunctions.MoveObjToPoint("LeavingShip (3)", new Vector3(-2.74f, 3.2812f, -5.7979f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            WiderShipObjFunctions.ScaleObj("LeavingShip (3)", new Vector3(3f, 12.2338f, 0.8325825f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///LightSwitchContainer
            WiderShipObjFunctions.MoveObjToPoint("LightSwitchContainer", new Vector3(1.5f, 2f, -4.25f), "Environment/HangarShip/");

            ///WallInsulator2
            ///Not sure why we need this collider
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);

            ///Railing (Colliders)
            string[] colliders = new string[2] { "Cube (1)", "Cube (3)" };
            foreach (string collider in colliders)
                GameObject.Find("Environment/HangarShip/Railing/" + collider).SetActive(false);
            WiderShipObjFunctions.MoveObjToPoint("Cube (2)", new Vector3(-7.625f, 0.64f, -11.119f), "Environment/HangarShip/Railing/");
            WiderShipObjFunctions.CopyObj("Cube (2)", new Vector3(-9.625f, 0.64f, -11.119f), "Environment/HangarShip/Railing/");

            ///ShipBoundsTrigger
            WiderShipObjFunctions.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -8f), "Environment/HangarShip/");
            WiderShipObjFunctions.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 14f), "Environment/HangarShip/");

            ///FogExclusionZone
            //FogExclusionZone copy     pos -2.75 2.23  -11.84          volume 12.8 6.85 7.26
            WiderShipObjFunctions.CopyObj("FogExclusionZone", new Vector3(-2.75f, 2.23f, -11.84f), "Environment/HangarShip/");
            //idk how to rescale volume so.. =(


            ///AnimatedShipDoor
            WiderShipObjFunctions.MoveObj("AnimatedShipDoor", new Vector3(-0.25f, 0f, 0f), "Environment/HangarShip/");

            WiderShipObjFunctions.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(-5.335f, 2.188215f, -4.323f), "Environment/HangarShip/AnimatedShipDoor/");
            WiderShipObjFunctions.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            ///SingleScreen
            WiderShipObjFunctions.SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            WiderShipObjFunctions.MoveObjToPoint("SingleScreen", new Vector3(-13.803f, -1.4f, -0.756f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ShipLights), "SetShipLightsClientRpc"), HarmonyPatch(typeof(ShipLights), "ToggleShipLightsOnLocalClientOnly")]
        static void SetShipLightsClientRpcPatch(ref bool ___areLightsOn)
        {
            string[] lightSources = new string[4] { "Area Light (4)(Clone)", "Area Light (5)(Clone)", "Area Light (8)(Clone)", "Area Light (9)(Clone)" };
            foreach (string source in lightSources)
                GameObject.Find("Environment/HangarShip/ShipElectricLights/" + source).GetComponent<Light>().enabled = ___areLightsOn;

            if (___areLightsOn)
                WiderShipPlugin.lampMaterials[3] = WiderShipPlugin.bulbOnMaterial;
            else
                WiderShipPlugin.lampMaterials[3] = WiderShipPlugin.bulbOffMaterial;

            string[] lamps = new string[2] { "HangingLamp (3)(Clone)", "HangingLamp (4)(Clone)" };
            foreach (string lamp in lamps)
            {
                var lampObjRend = GameObject.Find("Environment/HangarShip/ShipElectricLights/" + lamp).GetComponent<MeshRenderer>();
                lampObjRend.materials = WiderShipPlugin.lampMaterials;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RoundManager), "SpawnOutsideHazards")]
        static void SpawnOutsideHazardsPatch()
        {
            //i hate it so much
            try
            {
                ///nav cubes stuff
                //Environment/NavMeshColliders/PlayerShipNavmesh/
                WiderShipObjFunctions.CopyObj("Cube (17)", new Vector3(0f, 0f, 0f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.CopyObj("Cube (11)", new Vector3(0f, 0f, 0f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.CopyObj("Cube (13)", new Vector3(0f, 0f, 0f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.CopyObj("Cube (6)", new Vector3(0f, 0f, 0f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.CopyObj("Cube (12)", new Vector3(0f, 0f, 0f), "Environment/NavMeshColliders/PlayerShipNavmesh/");

                WiderShipObjFunctions.ScaleObj("Cube (17)(Clone)", new Vector3(11f, 0.4828268f, 5f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.ScaleObj("Cube (6)(Clone)", new Vector3(11f, 0.2792f, 6f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.ScaleObj("Cube (11)", new Vector3(8f, 5.727483f, 0.6064278f), "Environment/NavMeshColliders/PlayerShipNavmesh/");

                WiderShipObjFunctions.MoveObjToPoint("Cube (16)", new Vector3(15.9863f, -0.9428f, -5.19f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (12)", new Vector3(26.27f, -3.7588f, -0.87f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (11)", new Vector3(16.122f, -3.7588f, -0.23f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (17)(Clone)", new Vector3(17.07f, -0.63f, -2.32f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (11)(Clone)", new Vector3(16.7011f, -3.7588f, -5.39f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (13)", new Vector3(11.57f, -3.7588f, -2.574f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (6)(Clone)", new Vector3(17.2f, -5.55f, -2.091f), "Environment/NavMeshColliders/PlayerShipNavmesh/");
                WiderShipObjFunctions.MoveObjToPoint("Cube (12)(Clone)", new Vector3(22.316f, -3.7588f, -3.269f), "Environment/NavMeshColliders/PlayerShipNavmesh/");

                WiderShipObjFunctions.RotateObj("Cube (12)(Clone)", Vector3.up, "Environment/NavMeshColliders/PlayerShipNavmesh/", -60f);

                GameObject.Find("Environment/NavMeshColliders/PlayerShipNavmesh/Cube (1)").SetActive(false);
                //prob forgot smth

                //Environment/NavMeshColliders/PlayerShipNavmesh/SpaceBelowShip/
                WiderShipObjFunctions.CopyObj("MediumSpace", new Vector3(0f, 0f, -4f), "Environment/NavMeshColliders/PlayerShipNavmesh/SpaceBelowShip/");
                WiderShipObjFunctions.CopyObj("SmallSpace", new Vector3(0f, 0f, -6f), "Environment/NavMeshColliders/PlayerShipNavmesh/SpaceBelowShip/");

                GameObject go = GameObject.FindGameObjectWithTag("OutsideLevelNavMesh");
                if (go != null)
                {
                    go.GetComponent<NavMeshSurface>().BuildNavMesh();
                }
            }
            catch
            {
                WiderShipPlugin.mls.LogWarning("Cant change navmesh on that scene!!!");
            }
        }
    }
}
