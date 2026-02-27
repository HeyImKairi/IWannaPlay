using DG.Tweening;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IWannaPlay
{
    public class BossAppearHelper : MonoBehaviour
    {
        private static BossAppearHelper _instance;
        public static BossAppearHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("BossAppearHelper");
                    _instance = go.AddComponent<BossAppearHelper>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [HarmonyPatch(typeof(CameraManager), nameof(CameraManager.Traveling))]
    public static class SpeedUpCameraPanning
    {
        static void Postfix(Sequence __result)
        {
            if (__result != null) __result.timeScale = 10f;
        }
    }

    [HarmonyPatch(typeof(EnemyFx), nameof(EnemyFx.BossAppear))]
    public static class SpeedUpBossAppear
    {
        static void Postfix(Sequence __result)
        {
            if (__result != null) __result.timeScale = 10f;
            BossAppearHelper.Instance.StartCoroutine(SpeedUpUnnamedSequence(__result));
        }

        static IEnumerator SpeedUpUnnamedSequence(Sequence travelingSequence)
        {
            yield return null;

            var tweenManagerType = typeof(DOTween).Assembly.GetType("DG.Tweening.Core.TweenManager");

            var activeTweens = Traverse.Create(tweenManagerType)
                .Field("_activeTweens")
                .GetValue<Tween[]>();

            int max = Traverse.Create(tweenManagerType)
                .Field("_maxActiveLookupId")
                .GetValue<int>();

            for (int i = max; i >= 0; i--)
            {
                Tween t = activeTweens[i];
                if (t != null && t is Sequence && t != travelingSequence && t.stringId == null)
                {
                    t.timeScale = 10f;
                    break;
                }
            }
        }
    }

    [HarmonyPatch(typeof(EnemyFx), nameof(EnemyFx.BossGetDestroyed))]
    public static class SpeedUpBossGetDestroyed
    {
        static void Postfix(Sequence __result)
        {
            if (__result == null) return;
            __result.timeScale = 10f;
        }
    }
}
