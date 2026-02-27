using DG.Tweening;
using HarmonyLib;

namespace IWannaPlay
{
    public static class CutsceneState
    {
        public static bool IsSpeedingUp = false;
    }

    [HarmonyPatch(typeof(PostProcessManager), nameof(PostProcessManager.Bloom))]
    public static class SpeedUpBloom
    {
        static void Prefix(ref float duration)
        {
            if (CutsceneState.IsSpeedingUp)
                duration /= 10f;
        }
    }

    [HarmonyPatch(typeof(Fx), nameof(Fx.DungeonCleanse))]
    public static class SpeedUpDungeonCleanse
    {
        static void Postfix(Sequence __result)
        {
            if (__result == null) return;
            __result.timeScale = 10f;
            __result.OnComplete(() => CutsceneState.IsSpeedingUp = false);
        }
    }

    [HarmonyPatch(typeof(BossDoorFinal), nameof(BossDoorFinal.OpenAnimation))]
    public static class SpeedUpBossDoorOpen
    {
        static void Postfix(Sequence __result)
        {
            if (__result == null) return;
            __result.timeScale = 10f;
        }
    }

    [HarmonyPatch(typeof(Fx), nameof(Fx.PickupParam))]
    public static class SpeedUpMajorPickup
    {
        static void Prefix(Pickup pickup)
        {
            if (pickup.Rarity == Rarity.Major && pickup.Type == PickupType.CrystalBoss)
                CutsceneState.IsSpeedingUp = true;
        }

        static void Postfix(Pickup pickup, Sequence __result)
        {
            if (__result == null) return;
            if (pickup.Rarity == Rarity.Major)
                __result.timeScale = 10f;
        }
    }

    [HarmonyPatch(typeof(Fx), nameof(Fx.ScreenWhite))]
    public static class SpeedUpScreenWhite
    {
        static void Prefix(ref float duration)
        {
            if (CutsceneState.IsSpeedingUp)
                duration /= 10f;
        }
    }
}