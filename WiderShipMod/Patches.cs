using HarmonyLib;
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

            ///For future:

            //wall left: 1.269 1.233 -12.723
            //wall right: -4.673 1.233 -15.639
            //(need to be adjusted)

            vanilaSI.SetActive(false);
            vanilaSR.SetActive(false);
            vanilaSRP.SetActive(false);

            var vanilaSIMaterials = vanilaSI.GetComponent<MeshRenderer>().materials;

            ///Ship and rails
            WiderShipObjFunctions.CreateShipObj(vanilaSI, "ShipInsideEDITED.fbx", vanilaSI.layer, vanilaSI.tag); //GameObject ShipInsideEDITED(Clone)
            WiderShipObjFunctions.CreateShipObj(vanilaSR, "ShipRailsEDITED.fbx", vanilaSR.layer, vanilaSR.tag);// new Vector3(-10.19258f, 0.45f, -2.25f));
            WiderShipObjFunctions.CreateShipObj(vanilaSRP, "ShipRailPostsEDITED.fbx", vanilaSRP.layer, vanilaSRP.tag);// new Vector3(-10.19258f, 0.4117996f, -2.25f));

            WiderShipObjFunctions.MoveObjToPoint("ShipRailsEDITED(Clone)", new Vector3(-10.19258f, 0.45f, -2.25f), "Environment/HangarShip/");
            WiderShipObjFunctions.MoveObjToPoint("ShipRailPostsEDITED(Clone)", new Vector3(-10.19258f, 0.4117996f, -2.25f), "Environment/HangarShip/");

            GameObject.Find("Environment/HangarShip/ShipInsideEDITED(Clone)").GetComponent<MeshRenderer>().materials = vanilaSIMaterials;

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
            WiderShipObjFunctions.MoveObjToPoint("VentEntrance", new Vector3(1.5f, 1f, -4.25f), "Environment/HangarShip/");

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
            ///Not needed???

            ///AnimatedShipDoor
            WiderShipObjFunctions.MoveObj("AnimatedShipDoor", new Vector3(-0.25f, 0f, 0f), "Environment/HangarShip/");

            WiderShipObjFunctions.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(-5.335f, 2.188215f, -4.323f), "Environment/HangarShip/AnimatedShipDoor/");
            WiderShipObjFunctions.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            ///SingleScreen
            WiderShipObjFunctions.SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            WiderShipObjFunctions.MoveObjToPoint("SingleScreen", new Vector3(-13.803f, -1.4f, -0.756f), "Environment/HangarShip/ShipModels2b/MonitorWall/");

            ///StickyNoteItem
            //MoveObjToPoint("StickyNoteItem", new Vector3(9.145f, 1.956f, -5.312f), "Environment/HangarShip/");
            //RotateObj("StickyNoteItem", Vector3.up, "Environment/HangarShip/", -90);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ShipLights), "SetShipLightsClientRpc")]
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
    }
}
