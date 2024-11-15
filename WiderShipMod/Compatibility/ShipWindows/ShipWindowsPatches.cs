using HarmonyLib;
using ShipWindows.Utilities;
using System.Reflection;
using UnityEngine;

namespace WiderShipMod.Compatibility.ShipWindows
{
    internal class ShipWindowsPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(ShipReplacer), "ReplaceShip")]
        public static void ReplaceShipPatch()
        {
            //publicizer
            StartOfRound.Instance.StartCoroutine(ShipWindowsCompat.DoWindows());
            WiderShipPlugin.mls.LogMessage("I'M ALIVE!!!");
        }
    }
}
