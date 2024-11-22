using UnityEngine;

namespace WiderShipMod.Methods
{
    public class ObjMethods
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
}