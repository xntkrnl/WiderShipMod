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
                    wall.name = "wall_left";

                    if (WiderShipConfig.enableLeftInnerWallSolidMode.Value)
                        GameObject.Find("wall_left/Beams").SetActive(false);
                    else
                        GameObject.Find("wall_left/Wall").SetActive(false);

                    wall.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    wall.transform.localPosition = new Vector3(-6f, 0.952f, -5.224f);
                }
            }

            if ((side == Side.Right || side == Side.Both) && !WiderShipPlugin.is2StoryHere)
            {
                if (WiderShipConfig.enableRightInnerWall.Value)
                {
                    var wall = WiderShipPlugin.Instantiate(WiderShipPlugin.mainAssetBundle.LoadAsset("wall_right.prefab") as GameObject, GameObject.Find("HangarShip").transform);
                    wall.name = "wall_right";

                    if (WiderShipConfig.enableRightInnerWallSolidMode.Value)
                        GameObject.Find("wall_right/Beams").SetActive(false);
                    else
                        GameObject.Find("wall_right/Wall").SetActive(false);

                    wall.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    wall.transform.localPosition = new Vector3(-6, 0.952f, -8.16f);
                }
            }
        }
    }
}
