﻿using HarmonyLib;
using UnityEngine;
using WiderShipMod.Methods;

namespace WiderShipMod.Patches
{
    //This class exist only because 2StoryShip
    public class LightPatches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void StartLightPatch()
        {
            ShipSidesMethods.Init();

            WiderShipPlugin.lampMaterials = GameObject.Find("Environment/HangarShip/ShipElectricLights/HangingLamp (3)").GetComponent<MeshRenderer>().materials;
            WiderShipPlugin.bulbOnMaterial = WiderShipPlugin.lampMaterials[3];
            WiderShipPlugin.bulbOffMaterial = WiderShipPlugin.lampMaterials[0];

            if (WiderShipConfig.extendedSide.Value == Side.Left || WiderShipConfig.extendedSide.Value == Side.Both || WiderShipPlugin.is2StoryHere)
            {
                string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
                foreach (string lamp in lamps)
                    ObjMethods.CopyObj(lamp, new Vector3(0f, 0f, -4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_left";
            }

            if (WiderShipConfig.extendedSide.Value == Side.Right || WiderShipConfig.extendedSide.Value == Side.Both || WiderShipPlugin.is2StoryHere)
            {
                string[] lamps = new string[6] { "HangingLamp (3)", "HangingLamp (4)", "Area Light (4)", "Area Light (5)", "Area Light (8)", "Area Light (7)" };
                foreach (string lamp in lamps)
                    ObjMethods.CopyObj(lamp, new Vector3(0f, 0f, 4.5f), "Environment/HangarShip/ShipElectricLights/").name += "_right";
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ShipLights), "SetShipLightsClientRpc"), HarmonyPatch(typeof(ShipLights), "ToggleShipLightsOnLocalClientOnly")]
        static void SetShipLightsClientRpcPatch(ref bool ___areLightsOn)
        {
            var ShipElectricLight = GameObject.Find("Environment/HangarShip/ShipElectricLights").transform;

            if (___areLightsOn)
                ShipSidesMethods.lampMaterials[3] = ShipSidesMethods.bulbOnMaterial;
            else
                ShipSidesMethods.lampMaterials[3] = ShipSidesMethods.bulbOffMaterial;

            foreach (Transform child in ShipElectricLight)
            {
                var light = child.GetComponent<Light>();
                if (light != null)
                {
                    light.enabled = ___areLightsOn;
                    continue;
                }

                var meshRenderer = child.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.materials = ShipSidesMethods.lampMaterials;
                    continue;
                }

                WiderShipPlugin.mls.LogDebug($"Huh? What is {child.name} ? Send this to Wider Ship dev please :)");
            }
        }
    }
}
