using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;


namespace WiderShip
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class WiderShip : BaseUnityPlugin
    {
        // Mod Details
        private const string modGUID = "mborsh.WiderShipMod";
        private const string modName = "WiderShipMod";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static ManualLogSource mls;

        public static AssetBundle mainAssetBundle;
        public static GameObject newShipObj;

        private static WiderShip Instance;

        void Awake()
        {
            Instance = this;

            mls = BepInEx.Logging.Logger.CreateLogSource("Wider Ship");
            mls = Logger;

            mls.LogInfo("Wider Ship Mod loaded. Patching.");
            harmony.PatchAll(typeof(WiderShip));

            if (!LoadAssetBundle())
            {
                mls.LogError("Failed to load asset bundle! Abort mission!");
                return;
            }

            bool LoadAssetBundle()
            {
                mls.LogInfo("Loading AssetBundle...");
                string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                mainAssetBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "newship"));

                if (mainAssetBundle == null)
                    return false;

                mls.LogInfo($"AssetBundle {mainAssetBundle.name} loaded from {sAssemblyLocation}.");
                return true;
            }
        }
        //===================
        //     Patches
        //===================

        [HarmonyPostfix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void StartPatch()
        {
            var vanilaSI = GameObject.Find("Environment/HangarShip/ShipInside");
            var vanilaSR = GameObject.Find("Environment/HangarShip/ShipRails");
            var vanilaSRP = GameObject.Find("Environment/HangarShip/ShipRailPosts");
            //var catwalk = GameObject.Find("Environment/HangarShip/CatwalkShip");

            vanilaSI.SetActive(false);
            vanilaSR.SetActive(false);
            vanilaSRP.SetActive(false);

            ///Ship and rails
            CreateShipObj(vanilaSI, "ShipInsideEDITED.fbx", 8, "Aluminum"); //GameObject ShipInsideEDITED(Clone)
            CreateShipObj(vanilaSR, "ShipRailsEDITED.fbx", 8, "Untagged");// new Vector3(-10.19258f, 0.45f, -2.25f));
            CreateShipObj(vanilaSRP, "ShipRailPostsEDITED.fbx", 0, "Untagged");// new Vector3(-10.19258f, 0.4117996f, -2.25f));

            MoveObjToPoint("ShipRailsEDITED(Clone)", new Vector3(-10.19258f, 0.45f, -2.25f), "Environment/HangarShip/");
            MoveObjToPoint("ShipRailPostsEDITED(Clone)", new Vector3(-10.19258f, 0.4117996f, -2.25f), "Environment/HangarShip/");

            ///LadderShort (1)
            MoveObjToPoint("LadderShort (1)", new Vector3(-9f, -2.58f, -11.093f), "Environment/HangarShip/");

            ///ShipInnerRoomBoundsTrigger
            MoveObjToPoint("ShipInnerRoomBoundsTrigger", new Vector3(1.4367f, 1.781f, -9.1742f), "Environment/HangarShip/");
            ScaleObj("ShipInnerRoomBoundsTrigger", new Vector3(17.52326f, 5.341722f, 11f), "Environment/HangarShip/");

            ///Lamps
            string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (9)" };
            foreach (string lamp in lamps)
                CopyObj(lamp, new Vector3(0f, 0f, -4f), "Environment/HangarShip/ShipElectricLights/");

            ///SideMachineryLeft and stuff
            SetChildObjToParentObj("Pipework2.002", "SideMachineryLeft", "Environment/HangarShip/", "Environment/HangarShip/");
            //MoveObjToPoint("SideMachineryLeft", new Vector3(-6.475f, 1.6f, -9.286f), "Environment/HangarShip/");
            //GameObject.Find("Environment/HangarShip/SideMachineryLeft").transform.SetParent(GameObject.Find("Environment/HangarShip/ShipModels2b").transform);
            //Need to do this not in Start
            GameObject.Find("Environment/HangarShip/SideMachineryLeft").SetActive(false);

            ///Magnet
            MoveObjToPoint("GiantCylinderMagnet", new Vector3(-0.08f, 2.46f, -14.72f), "Environment/HangarShip/");

            ///ReverbTrigers(Not all)
            string[] triggers = new string[2] { "ShipDoorClosed", "ShipDoorOpened" };
            foreach (string trigger in triggers)
            {
                MoveObjToPoint(trigger, new Vector3(7.3f, 0f, -2.3f), "Environment/HangarShip/ReverbTriggers/");
                ScaleObj(trigger, new Vector3(16f, 1.76f, 10.5f), "Environment/HangarShip/ReverbTriggers/");
            }

            MoveObjToPoint("OutsideShip (1)", new Vector3(-2.519711f, 0f, -3.763971f), "Environment/HangarShip/ReverbTriggers/");
            ScaleObj("OutsideShip (1)", new Vector3(2.4f, 1.7614f, 1.064288f), "Environment/HangarShip/ReverbTriggers/");

            ///Vent
            MoveObjToPoint("VentEntrance", new Vector3(1.5f, 1f, -4.25f), "Environment/HangarShip/");

            ///ChargeStation
            RotateObj("ChargeStation", Vector3.up, "Environment/HangarShip/ShipModels2b/", -60f);
            MoveObjToPoint("ChargeStation", new Vector3(1.9f, 1.25f, -2.6f), "Environment/HangarShip/ShipModels2b/");

            ///Light (alarm)
            MoveObjToPoint("Light (3)", new Vector3(3f, 3.13f, -3.1f), "Environment/HangarShip/ShipModels2b/");
            MoveObjToPoint("Light (1)", new Vector3(4.742f, 3.249997f, -10.823f), "Environment/HangarShip/ShipModels2b/");
            RotateObj("Light (3)", Vector3.up, "Environment/HangarShip/ShipModels2b/", -85f);

            ///Plane.001 (Posters)
            ///Sadly, need to disable for now.
            GameObject.Find("Environment/HangarShip/Plane.001").SetActive(false);

            ///LeavingShip
            GameObject.Find("Environment/HangarShip/ReverbTriggers/LeavingShipTriggers/HorizontalTriggers/LeavingShip (6)").SetActive(false);

            ///LightSwitchContainer
            MoveObjToPoint("LightSwitchContainer", new Vector3(1.5f, 2f, -4.25f), "Environment/HangarShip/");

            ///WallInsulator2
            ///Not sure why we need this collider
            GameObject.Find("Environment/HangarShip/WallInsulator2").SetActive(false);

            ///Railing (Colliders)
            string[] colliders = new string[2] { "Cube (1)", "Cube (3)" };
            foreach (string collider in colliders)
                GameObject.Find("Environment/HangarShip/Railing/" + collider).SetActive(false);
            MoveObjToPoint("Cube (2)", new Vector3(-7.625f, 0.64f, -11.119f), "Environment/HangarShip/Railing/");
            CopyObj("Cube (2)", new Vector3(-9.625f, 0.64f, -11.119f), "Environment/HangarShip/Railing/");

            ///ShipBoundsTrigger
            MoveObjToPoint("ShipBoundsTrigger", new Vector3(1.4908f, 4.1675f, -8f), "Environment/HangarShip/");
            ScaleObj("ShipBoundsTrigger", new Vector3(23.75926f, 10.11447f, 14f), "Environment/HangarShip/");

            ///FogExclusionZone
            ///Not needed???

            ///AnimatedShipDoor
            MoveObj("AnimatedShipDoor", new Vector3(-0.25f, 0f, 0f), "Environment/HangarShip/");

            MoveObjToPoint("HangarDoorButtonPanel", new Vector3(-5.335f, 2.188215f, -4.323f), "Environment/HangarShip/AnimatedShipDoor/");
            SetAnglesObj("HangarDoorButtonPanel", new Vector3(90f, 0f, 0f), "Environment/HangarShip/AnimatedShipDoor/");

            ///SingleScreen
            SetAnglesObj("SingleScreen", new Vector3(-90f, -90f, -28f), "Environment/HangarShip/ShipModels2b/MonitorWall/");
            MoveObjToPoint("SingleScreen", new Vector3(-13.803f, -1.4f, -0.756f), "Environment/HangarShip/ShipModels2b/MonitorWall/");

            ///StickyNoteItem
            //MoveObjToPoint("StickyNoteItem", new Vector3(9.145f, 1.956f, -5.312f), "Environment/HangarShip/");
            //RotateObj("StickyNoteItem", Vector3.up, "Environment/HangarShip/", -90);
        }

        //===================
        //     Functions
        //===================
        public static void CreateShipObj(GameObject objOriginal, string objFile, int layer, string tag)
        {
            mls.LogMessage($"Trying to create {objFile}...");
            mls.LogInfo($"Parent: {objOriginal.transform.parent.gameObject.name}...");
            newShipObj = Instantiate(mainAssetBundle.LoadAsset(objFile) as GameObject, objOriginal.transform.parent);
            //newShipObj.transform.position = objOriginal.transform.position;
            newShipObj.transform.position = objOriginal.transform.position;
            newShipObj.tag = tag; // "Aluminum"
            newShipObj.layer = layer; // 8 - Room
            mls.LogMessage($"{objFile} has been created with name {newShipObj.name}!");
        }

        public static void CopyObj(string objName, Vector3 vector, string pathToObj)
        {
            mls.LogMessage($"Copying an object {objName} to new position.");
            var obj = GameObject.Find(pathToObj + objName);

            Vector3 tmp = obj.transform.position;
            mls.LogInfo($"obj Vector3 postion: {tmp.x}, {tmp.y}, {tmp.z}");

            var newObj = Instantiate(obj, obj.transform.parent);
            newObj.transform.position += vector;

            tmp = newObj.transform.position;
            mls.LogInfo($"newObj Vector3 postion: {tmp.x}, {tmp.y}, {tmp.z}");
        }

        public static void MoveObj(string objName, Vector3 vector, string pathToObj)
        {
            mls.LogMessage($"Moving an object {objName} to new position.");
            var obj = GameObject.Find(pathToObj + objName);
            Vector3 tmp = obj.transform.position;
            mls.LogInfo($"obj Vector3 postion: {tmp.x}, {tmp.y}, {tmp.z}");

            obj.transform.localPosition += vector;
            tmp = obj.transform.position;
            mls.LogInfo($"obj new Vector3 postion: {tmp.x}, {tmp.y}, {tmp.z}");
        }

        public static void MoveObjToPoint(string objName, Vector3 vector, string pathToObj)
        {
            mls.LogMessage($"Moving an object {objName} to new point position.");
            var obj = GameObject.Find(pathToObj + objName);
            Vector3 tmp = obj.transform.position;
            mls.LogInfo($"obj Vector3 postion: {tmp.x}, {tmp.y}, {tmp.z}");

            obj.transform.localPosition = vector;
            tmp = obj.transform.position;
            mls.LogInfo($"obj new Vector3 postion: {tmp.x}, {tmp.y}, {tmp.z}");
        }

        public static void RotateObj(string objName, Vector3 axis, string pathToObj, float angle)
        {
            mls.LogMessage($"Rotating an object {objName}");
            var obj = GameObject.Find(pathToObj + objName);
            obj.transform.RotateAround(obj.transform.position, axis, angle);
        }

        public static void SetAnglesObj(string objName, Vector3 angles, string pathToObj)
        {
            mls.LogMessage($"Seting angle: object {objName}");
            var obj = GameObject.Find(pathToObj + objName);
            obj.transform.localEulerAngles = angles;
        }

        public static void ScaleObj(string objName, Vector3 scaleVector, string pathToObj)
        {
            mls.LogMessage($"Scaling an object {objName}");
            var obj = GameObject.Find(pathToObj + objName);
            obj.transform.localScale = scaleVector;
        }

        public static void SetChildObjToParentObj(string objChildName, string objParentName, string pathToChildObj, string pathToParentObj)
        {
            GameObject.Find(pathToChildObj + objChildName).transform.SetParent(GameObject.Find(pathToParentObj + objParentName).transform);
        }
    }
}
