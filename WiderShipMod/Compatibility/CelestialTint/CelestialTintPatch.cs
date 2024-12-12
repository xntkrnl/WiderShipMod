using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WiderShipMod.Compatibility.CelestialTint
{
    internal class CelestialTint
    {
        //wanted to publicize this but meh
        static List<string> parts = new List<string>() { "ShipLightsPost", "OutsideShipRoom", "ThrusterBackLeft", "ThrusterBackRight", "ThrusterFrontLeft", "ThrusterFrontRight", "SideMachineryLeft", "SideMachineryRight", "ShipSupportBeams", "ShipSupportBeams.001", "MeterBoxDevice.001" };

        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void CelestialTintPartsPatch()
        {
            foreach (string part in parts)
                try
                {
                    GameObject.Find(part + "_copy").transform.position = GameObject.Find(part).transform.position;
                }
                catch
                { }

        }
    }
}
