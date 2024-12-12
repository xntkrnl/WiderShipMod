using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WiderShipMod.Compatibility.CelestialTint
{
    internal class CelestialTintPatch
    {
        //publicized
        static List<GameObject> go = new List<GameObject>();

        [HarmonyPostfix, HarmonyPatch(typeof(ShipPartsLoader), "ActivateSpaceProps")]
        static void ShipPartsLoaderPatch(ref List<GameObject> ___spacePropsCopies, ref List<string> ___propNames)
        {
            GameObject sideright = null;
            GameObject meterbox = null;
            for (int i = 0; i < ___spacePropsCopies.Count; i++)
            {
                if (go.Count < ___spacePropsCopies.Count)
                    go.Add(GameObject.Find(___propNames[i]));

                if (___propNames[i] == "SideMachineryRight")
                    sideright = ___spacePropsCopies[i];

                if (___propNames[i] == "MeterBoxDevice.001")
                    meterbox = ___spacePropsCopies[i];

                ___spacePropsCopies[i].transform.position = go[i].transform.position;
                ___spacePropsCopies[i].transform.localScale = go[i].transform.localScale;
                ___spacePropsCopies[i].transform.rotation = go[i].transform.rotation;
            }

            meterbox.transform.SetParent(sideright.transform);
        }
    }
}
