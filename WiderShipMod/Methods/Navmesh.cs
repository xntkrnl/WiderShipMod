using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine;

namespace WiderShipMod.Methods
{
    public class NavmeshMethods
    {
        internal static string GetNavmeshName()
        {
            switch (WiderShipConfig.extendedSide.Value)
            {
                case Side.Left:
                    return "navmesh_left";

                case Side.Right:
                    return "navmesh_right";

                case Side.Both:
                    return "navmesh_both";
            }

            return null;
        }

        public static void PlaceNavmesh()
        {
            GameObject oldNavmesh = GameObject.Find("NavMeshColliders");
            GameObject oldNavmeshChild = GameObject.Find("PlayerShipNavmesh");

            foreach (Transform child in oldNavmesh.transform)
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.gameObject.name.Contains("ShipLadder"))
                    {
                        if (child2.gameObject.name == "ShipLadder3")
                        {
                            if (WiderShipConfig.extendedSide.Value != Side.Left)
                            {
                                child2.position = new Vector3(24.91f, -4.78f, 12.48f);
                                child2.gameObject.GetComponent<OffMeshLink>().UpdatePositions();
                            }
                        }
                        else GameObject.Destroy(child2.gameObject);
                    }
                }
            }

            var navmesh = WiderShipPlugin.Instantiate(WiderShipPlugin.mainAssetBundle.LoadAsset(GetNavmeshName() + ".prefab") as GameObject, oldNavmesh.transform);
            navmesh.transform.position = Vector3.zero;

            //thanks melanie for finding it in my old code
            foreach (Transform child in oldNavmeshChild.transform)
            {
                if (child.gameObject.GetComponent<NavMeshModifier>() && child.name != "Cube (6)")
                    child.gameObject.SetActive(false);
                else
                    child.SetParent(navmesh.transform);
            }

            //walls
            if (!WiderShipConfig.enableLeftInnerWall.Value && WiderShipConfig.extendedSide.Value != Side.Right)
                GameObject.Find($"{navmesh.name}/PlayerShipNavmesh/left_wall_navmesh").SetActive(false);
            if (!WiderShipConfig.enableRightInnerWall.Value && WiderShipConfig.extendedSide.Value != Side.Left)
                GameObject.Find($"{navmesh.name}/PlayerShipNavmesh/right_wall_navmesh").SetActive(false);
        }
    }
}
