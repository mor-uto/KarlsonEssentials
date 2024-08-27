using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

[BepInProcess("Karlson.exe")]
[BepInPlugin("me.moruto.plugins.karlsonessentials", "KarlsonEssentials", "1.0")]
public class Plugin : BaseUnityPlugin {
    public static new ManualLogSource Logger;

    private FpsBoost fpsBoost;

    //Config
    public ConfigEntry<bool> disableBloom;
    public ConfigEntry<bool> disableLights;
    public ConfigEntry<string> CustomResolution;
    public ConfigEntry<bool> isFullscreen;
    public ConfigEntry<bool> NoPlayerSpeedlines;

    private void Awake() {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin KarlsonMod has been loaded!");
        new Harmony("me.moruto.karlsonessentials").PatchAll();
        initConfig();
        fpsBoost = new FpsBoost(this);
    }

    public void Update() {
        fpsBoost.Update();
    }

    private void initConfig() {
        disableBloom = Config.Bind("Lighting", "DisableBloom", true, "Toggle the Bloom (glowing lights) from doorFrames, lamps, weapons, etc...");
        disableLights = Config.Bind("Lighting", "DisableLights", true, "Toggle the lights from any lamp or light source");
        
        CustomResolution = Config.Bind("Resolution", "Resolution", "1920x1080", "Change the resolution of the game");
        isFullscreen = Config.Bind("Resolution", "isFullscreen", true, "Make the game window fullscreen or not");
        
        NoPlayerSpeedlines = Config.Bind("Particles", "NoPlayerSpeedlines", false, "Toggle the player's speed lines");
    }
}
