using HarmonyLib;
using WiderShipMod.Methods;
using WiderShipMod.Patches;

namespace WiderShipMod.Compatibility.TwoStoryShip
{
    internal class TwoStoryShip
    {
        internal static void Init(Harmony h)
        {
            //h.PatchAll(typeof(LightPatches));
            h.PatchAll(typeof(TwoStoryShip));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(StartOfRound), "Start")]
        static void TwoStoryShipStartPatch()
        {
            Walls.CreateWalls();
        }
    }
}