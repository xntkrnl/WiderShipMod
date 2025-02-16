using BepInEx.Bootstrap;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiderShipMod.Compatibility
{
    internal static class CompatibilityUtilities
    {
        internal static bool IsInstalledVersionGreaterOrEquallThanNeeded(string guid, Version version) => Chainloader.PluginInfos.TryGetValue(guid, out var info) && info.Metadata.Version >= version;
    }
}
