using ShipWindows.Components;
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
            ObjFunctions.MoveObjToPoint(window.name, new Vector3(4.8903f, 2.6117f, -8.8183f), "Environment/HangarShip/ShipBoth(Clone)/"); //3,6188 -3,9069 1,37 local
            ObjFunctions.ScaleObj(window.name, new Vector3(1f, 0.3f, 1f), "Environment/HangarShip/ShipBoth(Clone)/");
            ObjFunctions.SetAnglesObj(window.name, new Vector3(0f, 0f, 62.2f), "Environment/HangarShip/ShipBoth(Clone)/");
        }

        public static void ReParentWindows()
        {
            ObjFunctions.SetChildObjToParentObj("WindowContainer", "ShipBoth(Clone)", "Environment/HangarShip/ShipInside/", "Environment/HangarShip/");
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
                    return GameObject.Find("ShipBoth(Clone)");

                case Side.Right:
                    return GameObject.Find("ShipRight(Clone)");

                case Side.Both:
                    return GameObject.Find("ShipLeft(Clone)");
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

                if (window1 != null)
                {
                    WiderShipPlugin.mls.LogInfo("window1!");
                    GameObject.Find("left_vanila").SetActive(false);
                    WiderShipPlugin.mls.LogInfo("window1 half!");
                    GameObject.Find("left_window").SetActive(true);
                    WiderShipPlugin.mls.LogInfo("window1 done!");
                }

                if (window2 != null)
                {
                    WiderShipPlugin.mls.LogInfo("window2!");
                    GameObject.Find("right_vanila").SetActive(false);
                    WiderShipPlugin.mls.LogInfo("window2 half!");
                    GameObject.Find("right_window").SetActive(true);

                    if (WiderShipConfig.extendedSide.Value != Side.Left)
                    {
                        //MoveScaleRotate(window2);
                    }
                    WiderShipPlugin.mls.LogInfo("window2 done!");
                }

                if (window3 != null)
                {
                    WiderShipPlugin.mls.LogInfo("window3!");
                    GameObject.Find("floor_vanila").SetActive(false);
                    WiderShipPlugin.mls.LogInfo("window3 half!");
                    GameObject.Find("floor_window").SetActive(true);
                    WiderShipPlugin.mls.LogInfo("window3 done!");
                }
            }
            catch
            {
                WiderShipPlugin.mls.LogMessage("Shut up imposter!");
            }

            //RemoveSWShip();
            //im going insane
        }
    }
}
