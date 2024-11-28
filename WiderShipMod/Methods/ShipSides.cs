using UnityEngine;

namespace WiderShipMod.Methods
{
    public class ShipSidesMethods
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

            Walls.CreateWalls();
            DisableAndSave();
            DestroyStuff();
        }

        public static void CreateLeftSide()
        {
            //thank god i made left side in 1.0.0

            var ship = ObjMethods.CreateShipObj(vanilaSI, "ShipLeft.prefab", vanilaSI.layer, vanilaSI.tag);
            moddedPoster = ObjMethods.CreateShipObj(vanilaPosters, "Plane.001Left.fbx", vanilaPosters.layer, vanilaPosters.tag);

            ship.name = "ShipLeft";
            ship.transform.localPosition = Vector3.zero;
            moddedPoster.transform.localPosition = new Vector3(-8.34465e-07f, 2.157811f, -5.229f);


            ///Railing (collider)
            GameObject.Destroy(GameObject.Find("HangarShip/Railing"));

            ///LadderShort (1)
            ObjMethods.MoveObjToPoint("LadderShort (1)", new Vector3(-6.93f, -2.58f, -16.156f), "Environment/HangarShip/");

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (9)" };
            foreach (string lamp in lamps)
                ObjMethods.CopyObj(lamp, new Vector3(0f, 0f, -4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_left";

            ///ShipInnerRoomBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -9.1742f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 11f), "Environment/HangarShip/");

            ///SideMachineryLeft and stuff
            ObjMethods.SetChildObjToParentObj("Pipework2.002", "SideMachineryLeft", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjMethods.MoveObjToPoint("SideMachineryLeft", new Vector3(8.304f, 1.6f, -2.597f), "Environment/HangarShip/");
            ObjMethods.SetAnglesObj("SideMachineryLeft", new Vector3(180f, 90f, -90f), "Environment/HangarShip/");

            ///Magnet
            ObjMethods.MoveObjToPoint("GiantCylinderMagnet", new Vector3(-0.08f, 2.46f, -14.72f), "Environment/HangarShip/");

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjMethods.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -2.3f), "Environment/HangarShip/ReverbTriggers/");
                ObjMethods.ScaleObj(trigger, new Vector3(16f, 1.76f, 10.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ObjMethods.MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            ObjMethods.ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");

            ObjMethods.MoveObjToPoint("LeavingShip (2)", new Vector3(-5.030502f, 3.2812f, -1.4f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjMethods.ScaleObj("LeavingShip (2)", new Vector3(0.8544563f, 12.2338f, 15.21f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ObjMethods.MoveObjToPoint("LeavingShip (3)", new Vector3(-1.187546f, 3.2812f, -8.83f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjMethods.ScaleObj("LeavingShip (3)", new Vector3(5.967689f, 12.2338f, 0.8325825f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///Vent
            ObjMethods.MoveObjToPoint("VentEntrance", new Vector3(1.5f, 1f, -4.25f), "Environment/HangarShip/");

            ///ChargeStation
            ObjMethods.RotateObj("ChargeStation", Vector3.up, "Environment/HangarShip/ShipModels2b/", -61.5f);
            ObjMethods.MoveObjToPoint("ChargeStation", new Vector3(4.194f, 1.25f, -3.788f), "Environment/HangarShip/ShipModels2b/");

            ///Light (alarm)
            ObjMethods.MoveObjToPoint("Light (3)", new Vector3(3f, 3.13f, -3.1f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.MoveObjToPoint("Light (1)", new Vector3(4.742f, 3.249997f, -10.823f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.RotateObj("Light (3)", Vector3.up, "Environment/HangarShip/ShipModels2b/", -85f);

            ///LeavingShip
            GameObject.Find("Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/LeavingShip (6)").SetActive(false);

            ///LightSwitchContainer
            ObjMethods.MoveObjToPoint("LightSwitchContainer", new Vector3(1.5f, 2f, -4.25f), "Environment/HangarShip/");

            ///WallInsulator2
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);

            ///ShipBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -8f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 14f), "Environment/HangarShip/");

            ///AnimatedShipDoor
            ObjMethods.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(-5.59f, 2.188215f, -4.323f), "Environment/HangarShip/AnimatedShipDoor/");
            ObjMethods.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            ///SingleScreen
            ObjMethods.SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            ObjMethods.MoveObjToPoint("SingleScreen", new Vector3(-13.803f, -1.4f, -0.756f), "Environment/HangarShip/ShipModels2b/MonitorWall/");

            ///Suits
            //ObjMethods.MoveObjToPoint("NurbsPath.002", new Vector3(-4.781778f, 0.2743004f, -13.58593f), "Environment/HangarShip/");
            //ObjMethods.MoveObjToPoint("RightmostSuitPlacement", new Vector3(-4.928537f, 3.028439f, -21.108f), "Environment/HangarShip/");
            //(not)thanks zeeker for AutoParrentToShip for costumes
        }

        public static void CreateBothSides()
        {
            var ship = ObjMethods.CreateShipObj(vanilaSI, "ShipBoth.prefab", vanilaSI.layer, vanilaSI.tag);
            moddedPoster = ObjMethods.CreateShipObj(vanilaPosters, "Plane.001Both.fbx", vanilaPosters.layer, vanilaPosters.tag);

            ship.name = "ShipBoth";
            ship.transform.localPosition = Vector3.zero;
            moddedPoster.transform.localPosition = new Vector3(6.913f, 2.157811f, -9.453f);

            ///Railing (collider)
            GameObject.Destroy(GameObject.Find("HangarShip/Railing"));

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjMethods.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -0.25f), "Environment/HangarShip/ReverbTriggers/");
                ObjMethods.ScaleObj(trigger, new Vector3(16f, 1.76f, 15.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ObjMethods.MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            ObjMethods.ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");
            ObjMethods.MoveObjToPoint("OutsideShip (2)", new Vector3(2.304527f, 3.1895f, 8.89f), "Environment/HangarShip/ReverbTriggers/");
            ObjMethods.MoveObjToPoint("OutsideShip (3)", new Vector3(14.13f, 3.1895f, 6.08f), "Environment/HangarShip/ReverbTriggers/");

            ///Vent
            ObjMethods.MoveObjToPoint("VentEntrance", new Vector3(-1.37f, 0.567f, 0.721f), "Environment/HangarShip/");

            ///ChargeStation
            ObjMethods.RotateObj("ChargeStation", Vector3.up, "Environment/HangarShip/ShipModels2b/", -60f);
            ObjMethods.MoveObjToPoint("ChargeStation", new Vector3(4.201f, 1.25f, -3.774f), "Environment/HangarShip/ShipModels2b/");

            ///Light (alarm)
            ObjMethods.MoveObjToPoint("Light (3)", new Vector3(3f, 3.13f, -3.1f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.MoveObjToPoint("Light (1)", new Vector3(4.742f, 3.249997f, -10.823f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.RotateObj("Light (3)", Vector3.up, "Environment/HangarShip/ShipModels2b/", -85f);

            ///LeavingShip
            GameObject.Find("Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/LeavingShip (6)").SetActive(false);
            ObjMethods.MoveObjToPoint("LeavingShip (3)", new Vector3(-2.817f, 3.2812f, -5.797956f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjMethods.ScaleObj("LeavingShip (3)", new Vector3(2.983845f, 12.2338f, 0.8325825f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///WallInsulator
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);
            GameObject.Find("Environment/HangarShip/WallInsulator").SetActive(false);

            ///ShipBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -6.73f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 22f), "Environment/HangarShip/");

            ///ShipInnerRoomBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -6.661f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 16f), "Environment/HangarShip/");

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
            foreach (string lamp in lamps)
                ObjMethods.CopyObj(lamp, new Vector3(0f, 0f, -4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_left";

            lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
            foreach (string lamp in lamps)
                ObjMethods.CopyObj(lamp, new Vector3(0f, 0f, 4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_right";

            ///SideMachineryLeft and stuff
            ObjMethods.SetChildObjToParentObj("Pipework2.002", "SideMachineryLeft", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjMethods.MoveObjToPoint("SideMachineryLeft", new Vector3(8.304f, 1.6f, -2.597f), "Environment/HangarShip/");
            ObjMethods.SetAnglesObj("SideMachineryLeft", new Vector3(180f, 90f, -90f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("SideMachineryLeft", new Vector3(0.5793436f, 0.4392224f, 0.1953938f), "Environment/HangarShip/");

            ///Magnet
            ObjMethods.MoveObjToPoint("GiantCylinderMagnet", new Vector3(-0.08f, 2.46f, -14.72f), "Environment/HangarShip/");

            ///SideMachineryRight and stuff
            ObjMethods.SetChildObjToParentObj("MeterBoxDevice.001", "SideMachineryRight", "Environment/HangarShip/", "Environment/HangarShip/");
            ObjMethods.MoveObjToPoint("SideMachineryRight", new Vector3(-4f, 1.947363f, 1.08f), "Environment/HangarShip/");

            ///SingleScreen
            ObjMethods.SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            ObjMethods.MoveObjToPoint("SingleScreen", new Vector3(-3.7253f, -1.017f, 1.9057f), "Environment/HangarShip/ShipModels2b/MonitorWall/");

            ///Cube.005 & 006
            ObjMethods.MoveObjToPoint("Cube.005", new Vector3(5.027f, 3.469644f, -2.696f), "Environment/HangarShip/");
            ObjMethods.MoveObjToPoint("Cube.006", new Vector3(4.724743f, 3.469644f, -2.696f), "Environment/HangarShip/");

            ///OutsideShipRoom
            ObjMethods.MoveObj("OutsideShipRoom", new Vector3(0f, 0f, 5f), "Environment/HangarShip/");

            ///Light & (2)
            ObjMethods.MoveObjToPoint("Light", new Vector3(-8.672f, 3.13f, -3.295f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.SetAnglesObj("Light", new Vector3(-90f, 0f, -120f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.MoveObjToPoint("Light (2)", new Vector3(-9.911f, 3.25f, -11.063f), "Environment/HangarShip/ShipModels2b/");

            ///OutsideShip & (1)
            //ObjMethods.MoveObjToPoint("OusideShip", new Vector3(-8.672f, 3.13f, -3.295f), "Environment/HangarShip/ReverbTriggers/");
            //GameObject.Find("Environment/HangarShip/ReverbTriggers/OutsideShip (1)").SetActive(false);

            ///LeavingShip (1) & (4)
            ObjMethods.MoveObjToPoint("LeavingShip (1)", new Vector3(0.8309021f, 3.2812f, 11.34f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjMethods.MoveObjToPoint("LeavingShip (4)", new Vector3(13.72397f, 3.2812f, 12.72f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///CatwalkUnderneathSupports
            ObjMethods.MoveObjToPoint("CatwalkUnderneathSupports", new Vector3(-7.093815f, -0.1557276f, -2.39f), "Environment/HangarShip/");

            ///ShipInnerRoomBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -6.64f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 16f), "Environment/HangarShip/");

            ///Cube.005 (2) & (1)
            ObjMethods.MoveObjToPoint("Cube.005 (2)", new Vector3(-5.92f, 1.907f, -1.103f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.MoveObjToPoint("Cube.005 (1)", new Vector3(0.674f, 2.757f, -0.221f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.RotateObj("Cube.005 (1)", Vector3.up, "Environment/HangarShip/ShipModels2b/", 180f);

            ///LadderShort (1)
            ObjMethods.MoveObjToPoint("LadderShort (1)", new Vector3(-6.93f, -2.58f, -16.156f), "Environment/HangarShip/");

            ///LadderShort
            ObjMethods.MoveObjToPoint("LadderShort", new Vector3(6.568f, -2.58f, 2f), "Environment/HangarShip/");
            ObjMethods.SetAnglesObj("LadderShort", new Vector3(0f, 120f, 0f), "Environment/HangarShip/");

            ///AnimatedShipDoor
            //TODO: redo door panel
            ObjMethods.MoveObjToPoint("HangarDoorButtonPanel", new Vector3(6.412f, 2.546f, -3.328f), "Environment/HangarShip/AnimatedShipDoor/");
            ObjMethods.SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");
        }

        public static void CreateRightSide()
        {
            var ship = ObjMethods.CreateShipObj(vanilaSI, "ShipRight.prefab", vanilaSI.layer, vanilaSI.tag);
            moddedPoster = ObjMethods.CreateShipObj(vanilaPosters, "Plane.001Right.fbx", vanilaPosters.layer, vanilaPosters.tag);

            ship.name = "ShipRight";
            ship.transform.localPosition = Vector3.zero;
            moddedPoster.transform.localPosition = new Vector3(6.913f, 2.157811f, -9.453f);

            ///Railing (collider)
            GameObject.Destroy(GameObject.Find("HangarShip/Railing"));

            ///Cube.005 & 006
            ObjMethods.MoveObjToPoint("Cube.005", new Vector3(5.027f, 3.469644f, -2.696f), "Environment/HangarShip/");
            ObjMethods.MoveObjToPoint("Cube.006", new Vector3(4.724743f, 3.469644f, -2.696f), "Environment/HangarShip/");

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                ObjMethods.MoveObjToPoint(trigger, new Vector3(7.3f, 0f, 2.4f), "Environment/HangarShip/ReverbTriggers/");
                ObjMethods.ScaleObj(trigger, new Vector3(16f, 1.76f, 10.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            ///OutsideShipRoom
            ObjMethods.MoveObj("OutsideShipRoom", new Vector3(0f, 0f, 5f), "Environment/HangarShip/");

            ///Light & (2)
            ObjMethods.MoveObjToPoint("Light", new Vector3(-8.672f, 3.13f, -3.295f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.SetAnglesObj("Light", new Vector3(-90f, 0f, -120f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.MoveObjToPoint("Light (2)", new Vector3(-9.911f, 3.25f, -11.063f), "Environment/HangarShip/ShipModels2b/");

            ///LeavingShip
            ObjMethods.MoveObjToPoint("LeavingShip (1)", new Vector3(0.8309021f, 3.2812f, 11.34f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjMethods.MoveObjToPoint("LeavingShip (4)", new Vector3(13.72397f, 3.2812f, 12.72f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ObjMethods.MoveObjToPoint("LeavingShip (2)", new Vector3(-5.030502f, 3.2812f, 2.8f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");
            ObjMethods.ScaleObj("LeavingShip (2)", new Vector3(0.8544563f, 12.2338f, 15.21f), "Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/");

            ///OutsideShip
            ObjMethods.MoveObjToPoint("OutsideShip (2)", new Vector3(2.304527f, 3.1895f, 8.89f), "Environment/HangarShip/ReverbTriggers/");
            ObjMethods.MoveObjToPoint("OutsideShip (3)", new Vector3(14.13f, 3.1895f, 6.08f), "Environment/HangarShip/ReverbTriggers/");

            ///Ladder Short
            ObjMethods.MoveObjToPoint("LadderShort", new Vector3(6.568f, -2.58f, 2f), "Environment/HangarShip/");
            ObjMethods.SetAnglesObj("LadderShort", new Vector3(0f, 120f, 0f), "Environment/HangarShip/");

            ///Cube.005 (2) & (1)
            ObjMethods.MoveObjToPoint("Cube.005 (2)", new Vector3(-5.92f, 1.907f, -1.103f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.MoveObjToPoint("Cube.005 (1)", new Vector3(0.674f, 2.757f, -0.221f), "Environment/HangarShip/ShipModels2b/");
            ObjMethods.RotateObj("Cube.005 (1)", Vector3.up, "Environment/HangarShip/ShipModels2b/", 180f);

            ///ShipBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -4.657f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 14f), "Environment/HangarShip/");

            ///ShipInnerRoomBoundsTrigger
            ObjMethods.MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -3.9242f), "Environment/HangarShip/");
            ObjMethods.ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 11f), "Environment/HangarShip/");

            ///WallInsulator
            GameObject.Find("Environment/HangarShip/WallInsulator").SetActive(false);

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
            foreach (string lamp in lamps)
                ObjMethods.CopyObj(lamp, new Vector3(0f, 0f, 4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_right";
        }
    }
}
