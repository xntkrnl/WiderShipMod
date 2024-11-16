using System;
using Unity.AI.Navigation;
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

    public class ShipPartsFunctions
    {
        #region
        public static GameObject vanilaSI;
        public static GameObject vanilaSR;
        public static GameObject vanilaSRP;
        public static GameObject vanilaPosters;
        public static GameObject vanilaCatwalk;
        public static GameObject vanilaCatwalkRLB;
        public static GameObject vanilaCatwalkRL;
        public static GameObject moddedPoster;

        public static Material[] lampMaterials;
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

            lampMaterials = GameObject.Find("Environment/HangarShip/ShipElectricLights/HangingLamp (3)").GetComponent<MeshRenderer>().materials;

            bulbOnMaterial = lampMaterials[3];
            bulbOffMaterial = lampMaterials[0];
        }

        public static void DestroyStuff()
        {
            //GameObject.Destroy(vanilaSI);
            vanilaSI.SetActive(false);
            GameObject.Destroy(vanilaSR);
            GameObject.Destroy(vanilaSRP);
            GameObject.Destroy(vanilaCatwalk);
            GameObject.Destroy(vanilaCatwalkRLB);
            GameObject.Destroy(vanilaCatwalkRL);
            moddedPoster.name = vanilaPosters.name;
            GameObject.Destroy(vanilaPosters);
        }

        public static void DisableAndSave()
        {
            WiderShipPlugin.windowGOs[0] = GameObject.Find("left_window");
            WiderShipPlugin.windowGOs[1] = GameObject.Find("right_window");
            WiderShipPlugin.windowGOs[2] = GameObject.Find("floor_window");

            WiderShipPlugin.vanilaGOs[0] = GameObject.Find("left_vanila");
            WiderShipPlugin.vanilaGOs[1] = GameObject.Find("right_vanila");
            WiderShipPlugin.vanilaGOs[2] = GameObject.Find("floor_vanila");

            foreach (GameObject go in WiderShipPlugin.windowGOs)
                go.SetActive(false);
        }

        public static void CreateShip()
        {
            //network stuff for walls here somewhere or in other place? . . .

            //. . .

            switch (WiderShipConfig.extendedSide.Value)
            {
                case Side.Left:
                    {
                        CreateLeftSide();
                        break;
                    }

                case Side.Right:
                    {
                        CreateRightSide();
                        break;
                    }

                case Side.Both:
                    {
                        CreateBothSides();
                        break;
                    }
            }

            DisableAndSave();

            DestroyStuff();
        }

        public static void CreateLeftSide()
        {
            //thank god i made left side in 1.0.0
            //TODO: other sides, posters, and many many more :pensive:

            var ship = ObjFunctions.CreateShipObj(vanilaSI, "ShipLeft.prefab", vanilaSI.layer, vanilaSI.tag);
            moddedPoster = ObjFunctions.CreateShipObj(vanilaPosters, "Plane.001Left.fbx", vanilaPosters.layer, vanilaPosters.tag);

            ship.transform.localPosition = Vector3.zero;
            moddedPoster.transform.localPosition = new Vector3(-8.34465e-07f, 2.157811f, -5.229f);

            ///Railing (collider)
            GameObject.Destroy(GameObject.Find("HangarShip/Railing"));

            ///LadderShort (1)
            ObjFunctions.MoveObjToPoint("LadderShort (1)", new Vector3(-6.93f, -2.58f, -16.156f), "Environment/HangarShip/");

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (9)" };
            foreach (string lamp in lamps)
                ObjFunctions.CopyObj(lamp, new Vector3(0f, 0f, -4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_left";

            ///ShipInnerRoomBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -9.1742f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 11f), "Environment/HangarShip/");

            ///SideMachineryLeft and stuff
            ObjFunctions.SetChildObjToParentObj("Pipework2.002", "SideMachineryLeft", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("SideMachineryLeft", new Vector3(8.304f, 1.6f, -2.597f), "Environment/HangarShip/");
            ObjFunctions.SetAnglesObj("SideMachineryLeft", new Vector3(180f, 90f, -90f), "Environment/HangarShip/");

            ///Magnet
            ObjFunctions.MoveObjToPoint("GiantCylinderMagnet", new Vector3(-0.08f, 2.46f, -14.72f), "Environment/HangarShip/");

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjFunctions.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -2.3f), "Environment/HangarShip/ReverbTriggers/");
                ObjFunctions.ScaleObj(trigger, new Vector3(16f, 1.76f, 10.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ObjFunctions.MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            ObjFunctions.ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");

            ObjFunctions.MoveObjToPoint("LeavingShip (2)", new Vector3(-5.030502f, 3.2812f, -1.4f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjFunctions.ScaleObj("LeavingShip (2)", new Vector3(0.8544563f, 12.2338f, 15.21f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ObjFunctions.MoveObjToPoint("LeavingShip (3)", new Vector3(-1.187546f, 3.2812f, -8.83f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjFunctions.ScaleObj("LeavingShip (3)", new Vector3(5.967689f, 12.2338f, 0.8325825f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///Vent
            ObjFunctions.MoveObjToPoint("VentEntrance", new Vector3(1.5f, 1f, -4.25f), "Environment/HangarShip/");

            ///ChargeStation
            ObjFunctions.RotateObj("ChargeStation", Vector3.up, "Environment/HangarShip/ShipModels2b/", -61.5f);
            ObjFunctions.MoveObjToPoint("ChargeStation", new Vector3(4.194f, 1.25f, -3.788f), "Environment/HangarShip/ShipModels2b/");

            ///Light (alarm)
            ObjFunctions.MoveObjToPoint("Light (3)", new Vector3(3f, 3.13f, -3.1f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Light (1)", new Vector3(4.742f, 3.249997f, -10.823f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.RotateObj("Light (3)", Vector3.up, "Environment/HangarShip/ShipModels2b/", -85f);

            ///LeavingShip
            GameObject.Find("Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/LeavingShip (6)").SetActive(false);

            ///LightSwitchContainer
            ObjFunctions.MoveObjToPoint("LightSwitchContainer", new Vector3(1.5f, 2f, -4.25f), "Environment/HangarShip/");

            ///WallInsulator2
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);

            ///ShipBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -8f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 14f), "Environment/HangarShip/");

            ///AnimatedShipDoor
            ObjFunctions.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(-5.59f, 2.188215f, -4.323f), "Environment/HangarShip/AnimatedShipDoor/");
            ObjFunctions.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            ///SingleScreen
            ObjFunctions.SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            ObjFunctions.MoveObjToPoint("SingleScreen", new Vector3(-13.803f, -1.4f, -0.756f), "Environment/HangarShip/ShipModels2b/MonitorWall/");

            ///Suits
            //ObjFunctions.MoveObjToPoint("NurbsPath.002", new Vector3(-4.781778f, 0.2743004f, -13.58593f), "Environment/HangarShip/");
            //ObjFunctions.MoveObjToPoint("RightmostSuitPlacement", new Vector3(-4.928537f, 3.028439f, -21.108f), "Environment/HangarShip/");
            //(not)thanks zeeker for AutoParrentToShip for costumes
        }

        public static void CreateBothSides()
        {
            var ship = ObjFunctions.CreateShipObj(vanilaSI, "ShipBoth.prefab", vanilaSI.layer, vanilaSI.tag);
            moddedPoster = ObjFunctions.CreateShipObj(vanilaPosters, "Plane.001Both.fbx", vanilaPosters.layer, vanilaPosters.tag);

            ship.transform.localPosition = Vector3.zero;
            moddedPoster.transform.localPosition = new Vector3(6.913f, 2.157811f, -9.453f);

            ///Railing (collider)
            GameObject.Destroy(GameObject.Find("HangarShip/Railing"));

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjFunctions.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -0.25f), "Environment/HangarShip/ReverbTriggers/");
                ObjFunctions.ScaleObj(trigger, new Vector3(16f, 1.76f, 15.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ObjFunctions.MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            ObjFunctions.ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");
            ObjFunctions.MoveObjToPoint("OutsideShip (2)", new Vector3(2.304527f, 3.1895f, 8.89f), "Environment/HangarShip/ReverbTriggers/");
            ObjFunctions.MoveObjToPoint("OutsideShip (3)", new Vector3(14.13f, 3.1895f, 6.08f), "Environment/HangarShip/ReverbTriggers/");

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
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);
            GameObject.Find("Environment/HangarShip/WallInsulator").SetActive(false);

            ///ShipBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -6.73f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 22f), "Environment/HangarShip/");

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

            ///SideMachineryRight and stuff
            ObjFunctions.SetChildObjToParentObj("MeterBoxDevice.001", "SideMachineryRight", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("SideMachineryRight", new Vector3(-4f, 1.947363f, 1.08f), "Environment/HangarShip/");

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

            ///ShipInnerRoomBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -6.64f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 16f), "Environment/HangarShip/");

            ///Cube.005 (2) & (1)
            ObjFunctions.MoveObjToPoint("Cube.005 (2)", new Vector3(-5.92f, 1.907f, -1.103f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Cube.005 (1)", new Vector3(0.674f, 2.757f, -0.221f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.RotateObj("Cube.005 (1)", Vector3.up, "Environment/HangarShip/ShipModels2b/", 180f);

            ///LadderShort (1)
            ObjFunctions.MoveObjToPoint("LadderShort (1)", new Vector3(-6.93f, -2.58f, -16.156f), "Environment/HangarShip/");

            ///LadderShort
            ObjFunctions.MoveObjToPoint("LadderShort", new Vector3(7.868f, -2.58f, 1.116f), "Environment/HangarShip/");
            ObjFunctions.SetAnglesObj("LadderShort", new Vector3(0f, 120f, 0f), "Environment/HangarShip/");

            ///AnimatedShipDoor
            //TODO: redo door panel
            ObjFunctions.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(6.412f, 2.546f, -3.328f), "Environment/HangarShip/AnimatedShipDoor/");
            ObjFunctions.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");
        }

        public static void CreateRightSide()
        {
            var ship = ObjFunctions.CreateShipObj(vanilaSI, "ShipRight.prefab", vanilaSI.layer, vanilaSI.tag);
            moddedPoster = ObjFunctions.CreateShipObj(vanilaPosters, "Plane.001Right.fbx", vanilaPosters.layer, vanilaPosters.tag);

            ship.transform.localPosition = Vector3.zero;
            moddedPoster.transform.localPosition = new Vector3(6.913f, 2.157811f, -9.453f);

            ///Railing (collider)
            GameObject.Destroy(GameObject.Find("HangarShip/Railing"));

            ///Cube.005 & 006
            ObjFunctions.MoveObjToPoint("Cube.005", new Vector3(5.027f, 3.469644f, -2.696f), "Environment/HangarShip/");
            ObjFunctions.MoveObjToPoint("Cube.006", new Vector3(4.724743f, 3.469644f, -2.696f), "Environment/HangarShip/");

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjFunctions.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, 2.4f), "Environment/HangarShip/ReverbTriggers/");
                ObjFunctions.ScaleObj(trigger, new Vector3(16f, 1.76f, 10.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ///OutsideShipRoom
            ObjFunctions.MoveObj("OutsideShipRoom", new Vector3(0f, 0f, 5f), "Environment/HangarShip/");

            ///Light & (2)
            ObjFunctions.MoveObjToPoint("Light", new Vector3(-8.672f, 3.13f, -3.295f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.SetAnglesObj("Light", new Vector3(-90f, 0f, -120f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Light (2)", new Vector3(-9.911f, 3.25f, -11.063f), "Environment/HangarShip/ShipModels2b/");

            ///LeavingShip
            ObjFunctions.MoveObjToPoint("LeavingShip (1)", new Vector3(0.8309021f, 3.2812f, 11.34f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjFunctions.MoveObjToPoint("LeavingShip (4)", new Vector3(13.72397f, 3.2812f, 12.72f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ObjFunctions.MoveObjToPoint("LeavingShip (2)", new Vector3(-5.030502f, 3.2812f, 2.8f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjFunctions.ScaleObj("LeavingShip (2)", new Vector3(0.8544563f, 12.2338f, 15.21f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///OutsideShip
            ObjFunctions.MoveObjToPoint("OutsideShip (2)", new Vector3(2.304527f, 3.1895f, 8.89f), "Environment/HangarShip/ReverbTriggers/");
            ObjFunctions.MoveObjToPoint("OutsideShip (3)", new Vector3(14.13f, 3.1895f, 6.08f), "Environment/HangarShip/ReverbTriggers/");

            ///Ladder Short
            ObjFunctions.MoveObjToPoint("LadderShort", new Vector3(7.868f, -2.58f, 1.116f), "Environment/HangarShip/");
            ObjFunctions.SetAnglesObj("LadderShort", new Vector3(0f, 120f, 0f), "Environment/HangarShip/");

            ///Cube.005 (2) & (1)
            ObjFunctions.MoveObjToPoint("Cube.005 (2)", new Vector3(-5.92f, 1.907f, -1.103f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.MoveObjToPoint("Cube.005 (1)", new Vector3(0.674f, 2.757f, -0.221f), "Environment/HangarShip/ShipModels2b/");
            ObjFunctions.RotateObj("Cube.005 (1)", Vector3.up, "Environment/HangarShip/ShipModels2b/", 180f);

            ///ShipBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -4.657f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 14f), "Environment/HangarShip/");

            ///ShipInnerRoomBoundsTrigger
            ObjFunctions.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -3.9242f), "Environment/HangarShip/");
            ObjFunctions.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 11f), "Environment/HangarShip/");

            ///WallInsulator
            GameObject.Find("Environment/HangarShip/WallInsulator").SetActive(false);

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
            foreach (string lamp in lamps)
                ObjFunctions.CopyObj(lamp, new Vector3(0f, 0f, 4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_right";
        }
    }

    public class NavmeshFunctions
    {
        public static GameObject navmesh;
        internal static string GetNavmeshName()
        {
            switch (WiderShipConfig.extendedSide.Value)
            {
                case Side.Left:
                    return "navmesh_left.prefab";

                case Side.Right:
                    return "navmesh_right.prefab";

                case Side.Both:
                    return "navmesh_both.prefab";
            }

            return null;
        }

        public static void PlaceNavmesh()
        {
            GameObject oldNavmesh = GameObject.Find("NavMeshColliders");
            GameObject oldNavmeshChild = GameObject.Find("PlayerShipNavmesh");

            foreach (Transform child in oldNavmeshChild.transform)
            {
                child.gameObject.SetActive(false);
            }

            foreach (Transform child in oldNavmesh.transform)
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.gameObject.name.Contains("ShipLadder"))
                        GameObject.Destroy(child2.gameObject);
                }
            }

            navmesh = WiderShipPlugin.Instantiate(WiderShipPlugin.mainAssetBundle.LoadAsset(GetNavmeshName()) as GameObject, oldNavmesh.transform);
            navmesh.transform.localPosition = new Vector3(17.45f, -7.6f, 16f);

        }
    }

    //TODO: separate .cs for classes
}