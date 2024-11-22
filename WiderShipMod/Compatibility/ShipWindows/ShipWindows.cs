using ShipWindows.Components;
using System;
using System.Collections;
using UnityEngine;
using WiderShipMod.Methods;

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
            ObjMethods.MoveObjToPoint(window.name, new Vector3(3.6188f, -3.9069f, 1.37f), ""); //3,6188 -3,9069 1,37 local
            ObjMethods.ScaleObj(window.name, new Vector3(1f, 0.3f, 1f), "");
            ObjMethods.SetAnglesObj(window.name, new Vector3(0f, 0f, 62.2f), "");
        }

        public static void ParentWindows(string name)
        {
            var oldWindowContainer = GameObject.Find($"{name}/WindowContainer");
            var newWindowContainer = GameObject.Find("ShipInside/WindowContainer");

            if (oldWindowContainer == null)
            {
                newWindowContainer.transform.SetParent(GameObject.Find($"{name}").transform);
                return;
            }
            else
            {
                bool skip;
                foreach (Transform window in newWindowContainer.transform)
                {
                    skip = false;
                    foreach (Transform window_ in oldWindowContainer.transform)
                    {
                        if (window.name == window_.name)
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                    {
                        continue;
                    }
                    else
                    {
                        window.SetParent(oldWindowContainer.transform);
                    }
                }
            }
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
            //yield return new WaitForEndOfFrame();
            try
            {
                window1 = GameObject.Find("Window1");
                window2 = GameObject.Find("Window2");
                window3 = GameObject.Find("Window3");
                var ship = GetShipGO();

                ParentWindows(ship.name);

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
            yield return new WaitForEndOfFrame();
            //im going insane
        }
    }
}
