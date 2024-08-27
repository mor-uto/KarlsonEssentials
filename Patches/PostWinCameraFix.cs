using HarmonyLib;

[HarmonyPatch(typeof(PlayerMovement))]
[HarmonyPatch("Look")]
internal class PostWinCameraFix {
    [HarmonyPrefix]
    public static bool Prefix(PlayerMovement __instance) {
        if (!Game.Instance.playing) {
            return false;
        }

        return true;
    }
}
