﻿using HarmonyLib;
using ShipWindows;
using ShipWindows.Components;
using ShipWindows.Utilities;

namespace WiderShipMod.Compatibility.ShipWindows
{
    internal class ShipWindowsPatches
    {
        //publicizer
        [HarmonyPostfix, HarmonyPatch(typeof(ShipReplacer), "ReplaceShip")]
        public static void ReplaceShipPatch()
        {
            StartOfRound.Instance.StartCoroutine(ShipWindowsCompat.DoWindows());
            WiderShipPlugin.mls.LogMessage("I'M ALIVE!!!");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(ShipWindow), "OnStart")]
        public static void OnStartPatch()
        {
            if (WiderShipConfig.enableForceDontMovePosters.Value)
                WindowConfig.dontMovePosters.Value = true;
        }
    }
}
