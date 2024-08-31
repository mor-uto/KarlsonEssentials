using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

[BepInProcess("Karlson.exe")]
[BepInPlugin("me.moruto.plugins.karlsonessentials", "KarlsonEssentials", "1.0.0")]
public class Plugin : BaseUnityPlugin {
    public static new ManualLogSource Logger;

    private FpsBoost fpsBoost;

    public static ConfigEntry<bool> disableBloom;
    public static ConfigEntry<bool> disableLights;
    public static ConfigEntry<bool> disableReflections;
    public static ConfigEntry<string> customResolution;
    public static ConfigEntry<bool> isWindowed;
    public static ConfigEntry<bool> noPlayerSpeedlines;
    public static ConfigEntry<bool> noJumpPadEffect;
    public static ConfigEntry<bool> fixNotPlayingCamera;
    public static ConfigEntry<bool> fixConfigOnStartup;
    public static ConfigEntry<bool> removeJumpPadAnimation;

    public void Awake() {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin KarlsonMod has been loaded!");
        InitConfig();
        new Harmony("me.moruto.karlsonessentials").PatchAll();
        fpsBoost = new FpsBoost();

        if (!fixConfigOnStartup.Value) return;
        MethodInfo updateSettingsMethod = AccessTools.Method(typeof(GameState), "UpdateSettings");
        if (updateSettingsMethod != null) updateSettingsMethod.Invoke(GameState.Instance, null);
    }

    public void Update() {
        fpsBoost.Update();
    }

    private void InitConfig() {
        disableBloom = Config.Bind("Graphics", "DisableBloom", true, "Toggle the Bloom (glowing lights) from doorFrames, lamps, weapons, etc...");
        disableLights = Config.Bind("Graphics", "DisableLights", true, "Toggle the lights from any lamp or light source");
        disableReflections = Config.Bind("Graphics", "DisableReflections", false, "Disables Reflections (Not Recommended)");

        customResolution = Config.Bind("Window", "Resolution", "1920x1080", "Change the resolution of the game");
        isWindowed = Config.Bind("Window", "isWindowed", false, "Make the game windowed");
        
        noPlayerSpeedlines = Config.Bind("Particles", "NoPlayerSpeedlines", false, "Toggle the player's speed lines");
        noJumpPadEffect = Config.Bind("Particles", "NoJumpPadEffect", false, "Toggle the particle effects from jump pads");

        removeJumpPadAnimation = Config.Bind("Misc", "RemoveJumpPadAnimation", false, "Removes the floating animation in jump pads");

        fixNotPlayingCamera = Config.Bind("Fixes", "FixNotPlayingCamera", true, "Makes it so u cant move your camera if the game is won/lost/paused");
        fixConfigOnStartup = Config.Bind("Fixes", "FixConfigStartup", true, "Fixes the issue where sound settings dont first load until you open the settings menu");
    }
}
