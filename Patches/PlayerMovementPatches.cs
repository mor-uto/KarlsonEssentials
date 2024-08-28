using HarmonyLib;

[HarmonyPatch(typeof(PlayerMovement))]
internal class PlayerMovementPatches {
    [HarmonyPrefix]
    [HarmonyPatch("Look")]
    public static bool Prefix(PlayerMovement __instance) {
        return Game.Instance.playing && !Plugin.fixCameraPostWin.Value;
    }

    [HarmonyPrefix]
    [HarmonyPatch("SpeedLines")]
    public static bool RemoveSpeedLines(PlayerMovement __instance) {
        return Plugin.NoPlayerSpeedlines.Value ? false : true;
    }
}
