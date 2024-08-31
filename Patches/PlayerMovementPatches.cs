using HarmonyLib;

[HarmonyPatch(typeof(PlayerMovement))]
internal class PlayerMovementPatches {
    [HarmonyPrefix]
    [HarmonyPatch("Look")]
    public static bool Prefix(PlayerMovement __instance) {
        if (!Game.Instance.playing && Plugin.fixNotPlayingCamera.Value) return false;
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch("SpeedLines")]
    public static bool RemoveSpeedLines(PlayerMovement __instance) => !Plugin.noPlayerSpeedlines.Value;
}
