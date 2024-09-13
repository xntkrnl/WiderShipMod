using System;
using UnityEngine;

namespace WiderShipMod
{
    public class ObjFunctions
    {
        public static GameObject CreateShipObj(GameObject objOriginal, string objFile, int layer, string tag)
        {
            var newShipObj = WiderShipPlugin.Instantiate(WiderShipPlugin.mainAssetBundle.LoadAsset(objFile) as GameObject, objOriginal.transform.parent);
            //newShipObj.transform.position = objOriginal.transform.position;
            newShipObj.transform.position = objOriginal.transform.position;
            newShipObj.tag = tag; // "Aluminum"
            newShipObj.layer = layer; // 8 - Room
            WiderShipPlugin.mls.LogMessage($"{objFile} has been created with name {newShipObj.name}!");
            return newShipObj;
        }

        public static GameObject CopyObj(string objName, Vector3 vector, string pathToObj)
        {
            WiderShipPlugin.mls.LogMessage($"Copying an object {objName} to new position.");
            var obj = GameObject.Find(pathToObj + objName);

            var newObj = WiderShipPlugin.Instantiate(obj, obj.transform.parent);
            newObj.transform.position += vector;

            WiderShipPlugin.mls.LogInfo($"New obj name: {newObj.name}, parrent: {newObj.transform.parent.gameObject.name}");

            return newObj;
        }

        public static void MoveObj(string objName, Vector3 vector, string pathToObj)
        {
            WiderShipPlugin.mls.LogMessage($"Moving an object {objName} to new position.");
            var obj = GameObject.Find(pathToObj + objName);

            obj.transform.localPosition += vector;
        }

        public static void MoveObjToPoint(string objName, Vector3 vector, string pathToObj)
        {
            WiderShipPlugin.mls.LogMessage($"Moving an object {objName} to new point position.");
            var obj = GameObject.Find(pathToObj + objName);

            obj.transform.localPosition = vector;
        }

        public static void RotateObj(string objName, Vector3 axis, string pathToObj, float angle)
        {
            WiderShipPlugin.mls.LogMessage($"Rotating an object {objName}");
            var obj = GameObject.Find(pathToObj + objName);
            obj.transform.RotateAround(obj.transform.position, axis, angle);
        }

        public static void SetAnglesObj(string objName, Vector3 angles, string pathToObj)
        {
            WiderShipPlugin.mls.LogMessage($"Seting angle: object {objName}");
            var obj = GameObject.Find(pathToObj + objName);
            obj.transform.localEulerAngles = angles;
        }

        public static void ScaleObj(string objName, Vector3 scaleVector, string pathToObj)
        {
            WiderShipPlugin.mls.LogMessage($"Scaling an object {objName}");
            var obj = GameObject.Find(pathToObj + objName);
            obj.transform.localScale = scaleVector;
        }

        public static void SetChildObjToParentObj(string objChildName, string objParentName, string pathToChildObj, string pathToParentObj)
        {
            GameObject.Find(pathToChildObj + objChildName).transform.SetParent(GameObject.Find(pathToParentObj + objParentName).transform);
        }
    }

    public class ShipPartsFunctions : MonoBehaviour
    {
        #region
        public static GameObject vanilaSI;
        public static GameObject vanilaSR;
        public static GameObject vanilaSRP;
        public static GameObject vanilaPosters;
        public static GameObject vanilaCatwalk;
        public static GameObject vanilaCatwalkRLB;
        public static GameObject vanilaCatwalkRL;

        public static Material[] vanilaSIMaterials;
        public static Material[] vanilaSRMaterials;
        public static Material[] vanilaPostersMaterials;
        public static Material[] lampMaterials;
        public static Material[] moddedSIMaterials;
        public static Material[] vanilaCatwalkMaterials;
        public static Material[] vanilaCatwalkRLBMaterials;
        public static Material[] vanilaCatwalkRLMaterials;

        public static Material bulbOnMaterial;
        public static Material bulbOffMaterial;
        #endregion

        public static void Init()
        {
            vanilaSI = GameObject.Find("Environment/HangarShip/ShipInside");
            vanilaSR = GameObject.Find("Environment/HangarShip/ShipRails");
            vanilaSRP = GameObject.Find("Environment/HangarShip/ShipRailPosts");
            vanilaPosters = GameObject.Find("Environment/HangarShip/Plane.001");
            vanilaCatwalk = GameObject.Find("Environment/HangarShip/CatwalkShip");
            vanilaCatwalkRLB = GameObject.Find("Environment/HangarShip/CatwalkRailLiningB");
            vanilaCatwalkRL = GameObject.Find("Environment/HangarShip/CatwalkRailLining");

            vanilaSIMaterials = vanilaSI.GetComponent<MeshRenderer>().materials;
            vanilaSRMaterials = vanilaSR.GetComponent<MeshRenderer>().materials;
            vanilaCatwalkMaterials = vanilaCatwalk.GetComponent<MeshRenderer>().materials;
            vanilaCatwalkRLBMaterials = vanilaCatwalkRLB.GetComponent<MeshRenderer>().materials;
            vanilaCatwalkRLMaterials = vanilaCatwalkRL.GetComponent<MeshRenderer>().materials;
            lampMaterials = GameObject.Find("Environment/HangarShip/ShipElectricLights/HangingLamp (3)").GetComponent<MeshRenderer>().materials;
            vanilaPostersMaterials = vanilaPosters.GetComponent<MeshRenderer>().materials;
            moddedSIMaterials = new Material[vanilaSIMaterials.Length - 2];
            Array.Copy(vanilaSIMaterials, 0, moddedSIMaterials, 0, 3);

            bulbOnMaterial = lampMaterials[3];
            bulbOffMaterial = lampMaterials[0];
        }

        public static void CreateBothParts()
        {
            ObjFunctions.CreateShipObj(vanilaSI, "ShipInsideBothVanila.fbx", vanilaSI.layer, vanilaSI.tag).name = "ShipInsideBoth";
            ObjFunctions.CreateShipObj(vanilaSI, "FloorBoth.fbx", vanilaSI.layer, vanilaSI.tag).name = "FloorBoth";
            ObjFunctions.CreateShipObj(vanilaSR, "ShipRailsRight.fbx", vanilaSR.layer, vanilaSR.tag).name = "ShipRailsBoth";
            ObjFunctions.CreateShipObj(vanilaSRP, "ShipRailPostsRight.fbx", vanilaSRP.layer, vanilaSRP.tag).name = "ShipRailPostsBoth";
            ObjFunctions.CreateShipObj(vanilaCatwalk, "CatwalkBoth.fbx", vanilaSRP.layer, vanilaSRP.tag).name = "CatwalkBoth";
            ObjFunctions.CreateShipObj(vanilaCatwalkRLB, "CatwalkUnderneathLiningBRight.fbx", vanilaSRP.layer, vanilaSRP.tag).name = "CatwalkRLBBoth";
            ObjFunctions.CreateShipObj(vanilaCatwalkRL, "CatwalkUnderneathLiningRight.fbx", vanilaSRP.layer, vanilaSRP.tag).name = "CatwalkRLBoth";
            ObjFunctions.CreateShipObj(vanilaPosters, "Plane.001Both.fbx", vanilaPosters.layer, vanilaPosters.tag).name = vanilaPosters.name;

            ObjFunctions.MoveObjToPoint("ShipRailsBoth", new Vector3(-10.19258f, 0.45f, -2.25f), "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("ShipRailPostsBoth", new Vector3(-10.19258f, 0.4117996f, -0.7f), "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("Plane.001", new Vector3(6.887688f, 2.157812f, -9.47f), "Environment/HangarShip/");

            GameObject.Find("Environment/HangarShip/FloorBoth").GetComponent<MeshRenderer>().materials = moddedSIMaterials;
            GameObject.Find("Environment/HangarShip/ShipInsideBoth").GetComponent<MeshRenderer>().materials = moddedSIMaterials;
            GameObject.Find("Environment/HangarShip/ShipRailsBoth").GetComponent<MeshRenderer>().materials = vanilaSRMaterials;
            GameObject.Find("Environment/HangarShip/ShipRailPostsBoth").GetComponent<MeshRenderer>().materials = vanilaSRMaterials;
            GameObject.Find("Environment/HangarShip/CatwalkBoth").GetComponent<MeshRenderer>().materials = vanilaCatwalkMaterials;
            GameObject.Find("Environment/HangarShip/CatwalkRLBoth").GetComponent<MeshRenderer>().materials = vanilaCatwalkRLMaterials;
            GameObject.Find("Environment/HangarShip/CatwalkRLBBoth").GetComponent<MeshRenderer>().materials = vanilaCatwalkRLBMaterials;
            GameObject.Find("Environment/HangarShip/Plane.001Both").GetComponent<MeshRenderer>().materials = vanilaPostersMaterials;

            if (WiderShipConfig.enableRightInnerWall.Value)
            {
                GameObject wall;

                if (WiderShipConfig.enableRightInnerWallSolidMode.Value)
                    wall = ObjFunctions.CreateShipObj(vanilaSI, "Wall.fbx", vanilaSI.layer, vanilaSI.tag);
                else
                    wall = ObjFunctions.CreateShipObj(vanilaSI, "Beams.fbx", vanilaSI.layer, vanilaSI.tag);

                wall.name = "Right wall";
                ObjFunctions.SetAnglesObj(wall.name, new Vector3(-89.98f, 0f, 0f), "Environment/HangarShip/");
                ObjFunctions.MoveObjToPoint(wall.name, new Vector3(0f, 0.952f, -8.16f), "Environment/HangarShip/");
                ObjFunctions.ScaleObj(wall.name, new Vector3(1, -1, 1), "Environment/HangarShip/");
                GameObject.Find("Environment/HangarShip/" + wall.name).GetComponent<MeshRenderer>().material = moddedSIMaterials[1];
            }

            if (WiderShipConfig.enableLeftInnerWall.Value)
            {
                GameObject wall;

                if (WiderShipConfig.enableLeftInnerWallSolidMode.Value)
                    wall = ObjFunctions.CreateShipObj(vanilaSI, "Wall.fbx", vanilaSI.layer, vanilaSI.tag);
                else
                    wall = ObjFunctions.CreateShipObj(vanilaSI, "Beams.fbx", vanilaSI.layer, vanilaSI.tag);

                wall.name = "Left wall";
                ObjFunctions.SetAnglesObj(wall.name, new Vector3(-89.98f, 0f, 0f), "Environment/HangarShip/");
                ObjFunctions.MoveObjToPoint(wall.name, new Vector3(0f, 0.952f, -5.224f), "Environment/HangarShip/");
                GameObject.Find("Environment/HangarShip/" + wall.name).GetComponent<MeshRenderer>().material = moddedSIMaterials[1];
            }

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjFunctions.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -0.25f), "Environment/HangarShip/ReverbTriggers/");
                ObjFunctions.ScaleObj(trigger, new Vector3(16f, 1.76f, 15.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ObjFunctions.MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            ObjFunctions.ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");

            ///Vent
            ObjFunctions.MoveObjToPoint("VentEntrance", new Vector3(-1.37f, 0.567f, 0.721f), "Environment/HangarShip/");

            ///ChargeStation
            ObjFunctions.RotateObj("ChargeStation", Vector3.up, "Environment/HangarShip/ShipModels2b/", -60f);
            ObjFunctions.MoveObjToPoint("ChargeStation", new Vector3(4.201f, 1.25f, -3.774f), "Environment/HangarShip/ShipModels2b/");

            ///Light (alarm)
            ObjFunctions.MoveObjToPoint("Light (3)", new Vector3(3f, 3.13f, -3.1f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Light (1)", new Vector3(4.742f, 3.249997f, -10.823f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.RotateObj("Light (3)", Vector3.up, "Environment/HangarShip/ShipModels2b/", -85f);

            ///LeavingShip
            GameObject.Find("Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/LeavingShip (6)").SetActive(false);
            ObjFunctions.MoveObjToPoint("LeavingShip (3)", new Vector3(-2.817f, 3.2812f, -5.797956f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjFunctions.ScaleObj("LeavingShip (3)", new Vector3(2.983845f, 12.2338f, 0.8325825f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///WallInsulator
            ///Not sure why we need this collider
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);
            GameObject.Find("Environment/HangarShip/WallInsulator").SetActive(false);

            ///Railing (Colliders)
            string[] colliders = new string[2] { "Cube (1)", "Cube (3)" };
            foreach (string collider in colliders)
                GameObject.Find("Environment/HangarShip/Railing/" + collider).SetActive(false);
            ObjFunctions.MoveObjToPoint("Cube (2)", new Vector3(-7.625f, 0.64f, -11.119f), "Environment/HangarShip/Railing/");

            ///ShipBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -7.03f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 16f), "Environment/HangarShip/");

            ///AnimatedShipDoor
            ObjFunctions.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(6.39f, 2.546f, -3.328f), "Environment/HangarShip/AnimatedShipDoor/");
            ObjFunctions.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            ///ShipInnerRoomBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -6.661f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 16f), "Environment/HangarShip/");

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
            foreach (string lamp in lamps)
                ObjFunctions.CopyObj(lamp, new Vector3(0f, 0f, -4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_left";

            lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
            foreach (string lamp in lamps)
                ObjFunctions.CopyObj(lamp, new Vector3(0f, 0f, 4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_right";

            ///SideMachineryLeft and stuff
            ObjFunctions.SetChildObjToParentObj("Pipework2.002", "SideMachineryLeft", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("SideMachineryLeft", new Vector3(8.304f, 1.6f, -2.597f), "Environment/HangarShip/");
            ObjFunctions.SetAnglesObj("SideMachineryLeft", new Vector3(180f, 90f, -90f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("SideMachineryLeft", new Vector3(0.5793436f, 0.4392224f, 0.1953938f), "Environment/HangarShip/");

            ///Magnet
            ObjFunctions.MoveObjToPoint("GiantCylinderMagnet", new Vector3(-0.08f, 2.46f, -14.72f), "Environment/HangarShip/");

            ///LadderShort (1)
            ObjFunctions.MoveObjToPoint("LadderShort (1)", new Vector3(-9f, -2.58f, -11.093f), "Environment/HangarShip/");

            ///SideMachineryRight and stuff
            ObjFunctions.SetChildObjToParentObj("MeterBoxDevice.001", "SideMachineryRight", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("SideMachineryRight", new Vector3(-4f, 1.947363f, 4.655f), "Environment/HangarShip/");

            ///SingleScreen
            ObjFunctions.SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            ObjFunctions.MoveObjToPoint("SingleScreen", new Vector3(-3.7253f, -1.017f, 1.9057f), "Environment/HangarShip/ShipModels2b/MonitorWall/");

            ///Cube.005 & 006
            ObjFunctions.MoveObjToPoint("Cube.005", new Vector3(5.027f, 3.469644f, -2.696f), "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("Cube.006", new Vector3(4.724743f, 3.469644f, -2.696f), "Environment/HangarShip/");

            ///OutsideShipRoom
            ObjFunctions.MoveObj("OutsideShipRoom", new Vector3(0f, 0f, 5f), "Environment/HangarShip/");

            ///Light & (2)
            ObjFunctions.MoveObjToPoint("Light", new Vector3(-8.672f, 3.13f, -3.295f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.SetAnglesObj("Light", new Vector3(-90f, 0f, -120f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Light (2)", new Vector3(-9.911f, 3.25f, -11.063f), "Environment/HangarShip/ShipModels2b/");

            ///OutsideShip & (1)
            //ObjFunctions.MoveObjToPoint("OusideShip", new Vector3(-8.672f, 3.13f, -3.295f), "Environment/HangarShip/ReverbTriggers/");
            //GameObject.Find("Environment/HangarShip/ReverbTriggers/OutsideShip (1)").SetActive(false);

            ///LeavingShip (1) & (4)
            ObjFunctions.MoveObjToPoint("LeavingShip (1)", new Vector3(0.8309021f, 3.2812f, 11.34f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjFunctions.MoveObjToPoint("LeavingShip (4)", new Vector3(13.72397f, 3.2812f, 12.72f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///CatwalkUnderneathSupports
            ObjFunctions.MoveObjToPoint("CatwalkUnderneathSupports", new Vector3(-7.093815f, -0.1557276f, -2.39f), "Environment/HangarShip/");

            ///Railing
            ObjFunctions.MoveObjToPoint("Cube", new Vector3(-10.2128f, 0.6394f, -5.46f), "Environment/HangarShip/Railing/");
            ObjFunctions.ScaleObj("Cube", new Vector3(0.1611558f, 1.368879f, 18f), "Environment/HangarShip/Railing/");
            ObjFunctions.MoveObjToPoint("Cube (2)", new Vector3(-7.625f, 0.6394f, -14.43f), "Environment/HangarShip/Railing/");
            ObjFunctions.MoveObjToPoint("Cube (4)", new Vector3(-3.25f, 0.6394f, 3.46f), "Environment/HangarShip/Railing/");
            ObjFunctions.ScaleObj("Cube (4)", new Vector3(14f, 1.368879f, 0.1241122f), "Environment/HangarShip/Railing/");
            ObjFunctions.MoveObjToPoint("Cube (6)", new Vector3(4.6534f, 0.6394f, 4.476f), "Environment/HangarShip/Railing/");
            ObjFunctions.MoveObjToPoint("Cube (7)", new Vector3(5.37f, 0.6394f, 2.67f), "Environment/HangarShip/Railing/");
            ObjFunctions.SetAnglesObj("Cube (7)", new Vector3(3.460528f, 1.368879f, 0.1241122f), "Environment/HangarShip/Railing/");
            ObjFunctions.MoveObjToPoint("Cube (8)", new Vector3(10.68f, 0.6394f, -0.62f), "Environment/HangarShip/Railing/");
            ObjFunctions.ScaleObj("Cube (8)", new Vector3(6f, 1.368879f, 0.1241122f), "Environment/HangarShip/Railing/");
            ObjFunctions.MoveObjToPoint("Cube (9)", new Vector3(13.12f, 0.6394f, -6.561f), "Environment/HangarShip/Railing/");
            ObjFunctions.ScaleObj("Cube (9)", new Vector3(8.6f, 1.368879f, 0.1241122f), "Environment/HangarShip/Railing/");

            ///ShipInnerRoomBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -6.64f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 16f), "Environment/HangarShip/");

            ///Cube.005 (2) & (1)
            ObjFunctions.MoveObjToPoint("Cube.005 (2)", new Vector3(-5.92f, 1.907f, -1.103f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Cube.005 (1)", new Vector3(0.674f, 2.757f, -0.221f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.RotateObj("Cube.005 (1)", Vector3.up, "Environment/HangarShip/ShipModels2b/", 180f);

            ///LadderShort
            ObjFunctions.MoveObjToPoint("LadderShort (1)", new Vector3(-9f, -2.58f, -14.562f), "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("LadderShort", new Vector3(7.868f, -2.58f, 1.116f), "Environment/HangarShip/");
            ObjFunctions.SetAnglesObj("LadderShort", new Vector3(0f, 120f, 0f), "Environment/HangarShip/");

            ///LadderShort
            ObjFunctions.MoveObjToPoint("LadderShort", new Vector3(6.629997f, -2.58f, 4.546f), "Environment/HangarShip/");

            ///AnimatedShipDoor
            ObjFunctions.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(5.533f, 2.546f, -3.328f), "Environment/HangarShip/AnimatedShipDoor/");
            ObjFunctions.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            MiscStuff();
        }

        public static void MoveLightSwitch()
        {
            var lightswitch = GameObject.Find("Environment/HangarShip/LightSwitchContainer");
            if (lightswitch.transform.position == new Vector3(0.993536f, 1.417171f, -4.236f))
                lightswitch.transform.position = new Vector3(4.723f, 1.417171f, -3.327f);
        }

        public static void MiscStuff()
        {
            vanilaSI.SetActive(false);
            vanilaSR.SetActive(false);
            vanilaSRP.SetActive(false);
            vanilaCatwalk.SetActive(false);
            vanilaCatwalkRLB.SetActive(false);
            vanilaCatwalkRL.SetActive(false);
            Destroy(vanilaPosters);
        }
    }
}
