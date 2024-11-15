﻿using ShipWindows.Components;
using System;
using System.Collections;
using UnityEngine;

namespace WiderShipMod.Compatibility.ShipWindows
{
    internal class ShipWindowsCompat
    {
        //Window1 - left
        //Window2 - right
        //Window3 - floor

        public static GameObject window1;
        public static GameObject window2;
        public static GameObject window3;

        public static void RenamePosters()
        {
            var WSposter = GameObject.Find("Plane.001 (Old)");
            var SWposter = GameObject.Find("Plane.001"); //very cool names for variables

            WSposter.name = "Plane.001";
            SWposter.name = "Plane.001 (Old)";

            SWposter.SetActive(false);
            WSposter.SetActive(true);
        }

        public static void MoveScaleRotate(GameObject window)
        {
            ObjFunctions.MoveObjToPoint(window.name, new Vector3(3.6188f, -3.9069f, 1.37f), ""); //3,6188 -3,9069 1,37 local
            ObjFunctions.ScaleObj(window.name, new Vector3(1f, 0.3f, 1f), "");
            ObjFunctions.SetAnglesObj(window.name, new Vector3(0f, 0f, 62.2f), "");
        }

        public static void ReParentWindows(string name)
        {
            var oldWindowContainer = GameObject.Find($"{name}/WindowContainer");
            WiderShipPlugin.mls.LogInfo("1");
            if (oldWindowContainer != null)
                GameObject.Destroy(oldWindowContainer);

            WiderShipPlugin.mls.LogInfo("2");
            ObjFunctions.SetChildObjToParentObj("WindowContainer", name, "Environment/HangarShip/ShipInside/", "Environment/HangarShip/");
            WiderShipPlugin.mls.LogInfo("3");
        }

        public static void RemoveSWShip()
        {
            GameObject.Find("ShipInside").SetActive(false);
        }

        internal static GameObject GetShipGO()
        {
            switch (WiderShipConfig.extendedSide.Value)
            {
                case Side.Left:
                    return GameObject.Find("ShipLeft(Clone)");

                case Side.Right:
                    return GameObject.Find("ShipRight(Clone)");

                case Side.Both:
                    return GameObject.Find("ShipBoth(Clone)");
            }

            return null;
        }

        public static IEnumerator DoWindows()
        {
            yield return new WaitForEndOfFrame();
            try
            {
                window1 = GameObject.Find("Window1");
                window2 = GameObject.Find("Window2");
                window3 = GameObject.Find("Window3");
                var ship = GetShipGO();

                WiderShipPlugin.mls.LogInfo($"0 - {GetShipGO()}");
                WiderShipPlugin.mls.LogInfo($"{ship.name}");
                ReParentWindows(ship.name);

                if (window1 != null)
                {
                    WiderShipPlugin.windowGOs[0].SetActive(true);
                    WiderShipPlugin.vanilaGOs[0].SetActive(false);
                    WiderShipPlugin.mls.LogInfo("window1 done!");
                }

                if (window2 != null)
                {
                    WiderShipPlugin.windowGOs[1].SetActive(true);
                    WiderShipPlugin.vanilaGOs[1].SetActive(false);

                    if (WiderShipConfig.extendedSide.Value != Side.Left)
                    {
                        MoveScaleRotate(window2);
                    }
                    WiderShipPlugin.mls.LogInfo("window2 done!");
                }

                if (window3 != null)
                {
                    WiderShipPlugin.windowGOs[2].SetActive(true);
                    WiderShipPlugin.vanilaGOs[2].SetActive(false);
                    WiderShipPlugin.mls.LogInfo("window3 done!");
                }
            }
            catch
            {
                WiderShipPlugin.mls.LogError("Imposter!!!");
            }

            RemoveSWShip();
            //im going insane
        }
    }
}