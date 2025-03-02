using HarmonyLib;
using ShipWindows;
using ShipWindows.Api;
using ShipWindows.Api.events;
using ShipWindows.ShutterSwitch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace WiderShipMod.Compatibility.ShipWindowsBeta
{
    internal class ShipWindowsUtils
    {
        internal static void RemoveRoofWindow()
        {
            /*Type type = typeof(WindowRegistry);
            FieldInfo fieldInfo = type.GetField("windows", BindingFlags.NonPublic | BindingFlags.Instance);

            var windows = (HashSet<WindowInfo>)fieldInfo.GetValue(ShipWindows.ShipWindows.windowRegistry);*/

            foreach (var window in ShipWindows.ShipWindows.windowRegistry.Windows)
            {
                WiderShipPlugin.mls.LogMessage($"{window.windowName}, {window.windowType}");
                if (window.windowName == "Roof Window")
                {
                    ShipWindows.ShipWindows.windowRegistry.UnregisterWindow(window);
                    break;
                }
            }
        }
    }

    internal class ShipWindowsBetaPatches
    {
        internal static GameObject ShipWindowsBetaCompatScriptGO;

        //HudManager.Awake/StartOfRound.Awake was too early to me so i changed my main patch (that one that calls shipsides stuff) to HudManager.Awake
        [HarmonyBefore("TestAccount666.ShipWindows", "TestAccount666.ShipWindowsBeta")]
        [HarmonyPostfix, HarmonyPatch(typeof(HUDManager), "Awake")]
        internal static void HudManagerAwake()
        {
            ShipWindowsBetaCompatScriptGO = new GameObject("WiderShipCompatGO");
            ShipWindowsBetaCompatScriptGO.AddComponent<ShipWindowsBetaCompatScript>();
            ShipWindowsBetaCompatScriptGO.hideFlags = HideFlags.HideAndDontSave;
        }

        [HarmonyAfter("TestAccount666.ShipWindows", "TestAccount666.ShipWindowsBeta")]
        [HarmonyPostfix, HarmonyPatch(typeof(HUDManager), "Awake")]
        internal static void DeactivateShipWindowsShip() // => ShipWindows.ShipWindows.windowManager.decapitatedShip.SetActive(false);
        {
            ShipWindows.ShipWindows.windowManager.decapitatedShip.transform.Find("Door").gameObject.SetActive(false);
            ShipWindows.ShipWindows.windowManager.decapitatedShip.transform.Find("Floor").gameObject.SetActive(false);
            ShipWindows.ShipWindows.windowManager.decapitatedShip.transform.Find("Roof").gameObject.SetActive(false);
            ShipWindows.ShipWindows.windowManager.decapitatedShip.transform.Find("SideBack").gameObject.SetActive(false);
            ShipWindows.ShipWindows.windowManager.decapitatedShip.transform.Find("SideLeft").gameObject.SetActive(false);
            ShipWindows.ShipWindows.windowManager.decapitatedShip.transform.Find("SideRight").gameObject.SetActive(false);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(StartOfRound), "OnDestroy")]
        internal static void StartOfRoundOnDestroy() => GameObject.Destroy(ShipWindowsBetaCompatScriptGO);

        [HarmonyPostfix, HarmonyPatch(typeof(ShutterSwitchBehavior), "Awake")]
        internal static void ShutterSwitchBehaviorAwake()
        {
            if (WiderShipConfig.extendedSide.Value == Side.Right || WiderShipConfig.extendedSide.Value == Side.Both)
                ShutterSwitchBehavior.Instance.transform.parent.GetComponent<AutoParentToShip>().positionOffset = new Vector3(-0.75f, 2.12f, 0.73f);
        }
    }
    

    internal class ShipWindowsBetaCompatScript : MonoBehaviour
    {
        internal static GameObject GetShipGO()
        {
            switch (WiderShipConfig.extendedSide.Value)
            {
                case Side.Left:
                    return GameObject.Find("ShipLeft");

                case Side.Right:
                    return GameObject.Find("ShipRight");

                case Side.Both:
                    return GameObject.Find("ShipBoth");
            }

            return null;
        }

        void Awake()
        {
            var widerShip = GetShipGO();
            transform.SetParent(widerShip.transform);

            EventAPI.AfterWindowSpawned += HandleAfterWindowSpawn;
        }

        internal void HandleAfterWindowSpawn(WindowEventArguments arguments)
        {
            WiderShipPlugin.mls.LogInfo($"window name = {arguments.windowInfo.windowName}");
            WiderShipPlugin.mls.LogInfo($"window type = {arguments.windowInfo.windowType}");

            string windowType = arguments.windowInfo.windowType;
            Transform windowAndWall = arguments.windowObject.transform;
            Transform window = null;

            if (windowType == "Door") return;

            window = windowAndWall.Find("Window");
            foreach (Transform child in windowAndWall)
                if (child != window)
                    child.gameObject.SetActive(false);

            if (windowType == "SideRight")
            {
                WiderShipPlugin.windowGOs[1].SetActive(true);
                WiderShipPlugin.vanilaGOs[1].SetActive(false);

                if(WiderShipConfig.extendedSide.Value == Side.Right || WiderShipConfig.extendedSide.Value == Side.Both)
                {
                    window.localPosition = new Vector3(5.367f, -0.975f, -0.143f);
                    window.localScale = new Vector3(0.01f, 0.01f, 0.003f);
                    window.localEulerAngles = new Vector3(-22.124f, -90f, -90f);
                }
            }

            if (windowType == "SideLeft")
            {
                WiderShipPlugin.windowGOs[0].SetActive(true);
                WiderShipPlugin.vanilaGOs[0].SetActive(false);
            }

            if (windowType == "Floor")
            {
                WiderShipPlugin.windowGOs[2].SetActive(true);
                WiderShipPlugin.vanilaGOs[2].SetActive(false);
            }
        }

        void OnDisable()
        {
           EventAPI.AfterWindowSpawned -= HandleAfterWindowSpawn;
        }
    }
}
