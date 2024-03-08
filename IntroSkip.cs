using System.Reflection;
using HarmonyLib;
using MelonLoader;
using Stronghold1DE;

namespace Ritterschlag
{
    public static class IntroSkip
    {
        [HarmonyPatch(typeof(IntroSequence))]
        internal static class PATCH_IntroSequence
        {
            [HarmonyPostfix]
            [HarmonyPatch("StartVideo")]
            private static void POST_StartVideo(ref IntroSequence __instance)
            {
                MelonLogger.Msg("Skipping Intro");
                MethodInfo method = typeof(IntroSequence).GetMethod("EndVideo", AccessTools.all);
                if (method != null) method.Invoke(__instance, new object[] { });
                else __instance.ForceStopVideo();
            }
        }
    }
}