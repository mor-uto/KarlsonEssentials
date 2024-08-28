using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

[BepInProcess("Karlson.exe")]
[BepInPlugin("me.moruto.plugins.karlsonessentials", "KarlsonEssentials", "1.0")]
public class Plugin : BaseUnityPlugin {
    public static new ManualLogSource Logger;

    private FpsBoost fpsBoost;

    public static ConfigEntry<bool> disableBloom;
    public static ConfigEntry<bool> disableLights;
    public static ConfigEntry<string> CustomResolution;
    public static ConfigEntry<bool> isWindowed;
    public static ConfigEntry<bool> NoPlayerSpeedlines;
    public static ConfigEntry<bool> NoJumpPadEffect;
    public static ConfigEntry<bool> removeWatermark;
    public static ConfigEntry<bool> fixCameraPostWin;
    public static ConfigEntry<bool> removeJumpPadAnimation;

    private void Awake() {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin KarlsonMod has been loaded!");
        InitConfig();
        new Harmony("me.moruto.karlsonessentials").PatchAll();
        fpsBoost = new FpsBoost();
    }

    public void Start() {
        MethodInfo updateSettingsMethod = AccessTools.Method(typeof(GameState), "UpdateSettings");
        if (updateSettingsMethod != null) updateSettingsMethod.Invoke(GameState.Instance, null);
    }

    public void Update() {
        fpsBoost.Update();
    }

    private void InitConfig() {
        disableBloom = Config.Bind("Graphics", "DisableBloom", true, "Toggle the Bloom (glowing lights) from doorFrames, lamps, weapons, etc...");
        disableLights = Config.Bind("Graphics", "DisableLights", true, "Toggle the lights from any lamp or light source");
        
        CustomResolution = Config.Bind("Window", "Resolution", "1920x1080", "Change the resolution of the game");
        isWindowed = Config.Bind("Window", "isWindowed", false, "Make the game windowed");
        
        NoPlayerSpeedlines = Config.Bind("Particles", "NoPlayerSpeedlines", false, "Toggle the player's speed lines");
        NoJumpPadEffect = Config.Bind("Particles", "NoJumpPadEffect", false, "Toggle the particle effects from jump pads");

        removeWatermark = Config.Bind("Misc", "RemoveWatermark", true, "Remove the developer's logo in the bottom left");
        removeJumpPadAnimation = Config.Bind("Misc", "RemoveJumpPadAnimation", false, "Removes the floating animation in jump pads");


        fixCameraPostWin = Config.Bind("Fixes", "FixNotPlayingCamera", true, "Makes it so u cant move your camera if the game is won/lost/paused");
    }
}
