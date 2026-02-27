using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace IWannaPlay
{
    [HarmonyPatch(typeof(GameManager), "PlayIntroCutscene")]
    public static class SkipIntroCutscene
    {
        static bool Prefix()
        {
            GameManager.State = GameState.Game;
            GameManager.Unfreeze();
            Player.Instance.Restore(true, true);
            Traverse.Create(GameManager.Instance)
                .Method("EndIntro")
                .GetValue();
            return false;
        }
    }
}
