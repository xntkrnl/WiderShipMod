using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WiderShipMod.Methods
{
    public class Walls
    { //class only because 2storyship
        public static void CreateWalls()
        {
            var side = WiderShipConfig.extendedSide.Value;

            if (side == Side.Left || side == Side.Both)
            {
                if (WiderShipConfig.enableLeftInnerWall.Value)
                {
                    var wall = WiderShipPlugin.Instantiate(WiderShipPlugin.mainAssetBundle.LoadAsset("wall_left.prefab") as GameObject, GameObject.Find("HangarShip").transform);

                    if (WiderShipConfig.enableLeftInnerWallSolidMode.Value)
                        GameObject.Find("wall_left(Clone)/Beams").SetActive(false);
                    else
                        GameObject.Find("wall_left(Clone)/Wall").SetActive(false);

                    wall.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    wall.transform.localPosition = new Vector3(-6f, 0.952f, -5.224f);
                }
            }

            if ((side == Side.Right || side == Side.Both) && !WiderShipPlugin.is2StoryHere)
            {
                if (WiderShipConfig.enableRightInnerWall.Value)
                {
                    var wall = WiderShipPlugin.Instantiate(WiderShipPlugin.mainAssetBundle.LoadAsset("wall_right.prefab") as GameObject, GameObject.Find("HangarShip").transform);

                    if (WiderShipConfig.enableRightInnerWallSolidMode.Value)
                        GameObject.Find("wall_right(Clone)/Beams").SetActive(false);
                    else
                        GameObject.Find("wall_right(Clone)/Wall").SetActive(false);

                    wall.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    wall.transform.localPosition = new Vector3(-6, 0.952f, -8.16f);
                }
            }
        }
    }
}
