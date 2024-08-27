using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace MoreFps {
    [BepInPlugin("me.moruto.karlson.morefps", "MoreFps", "1.0.1")]
    internal class Plugin : BaseUnityPlugin {
        public ConfigEntry<bool> disableBloom;
        public ConfigEntry<bool> disableLights;
        public ConfigEntry<string> CustomResolution;
        public ConfigEntry<bool> isFullscreen;
        public ConfigEntry<bool> NoPlayerSpeedlines;

        private FpsBoost fpsBoost;

        public void Awake() {
            Logger.LogInfo("MoreFps by Moruto loaded!");
            InitConfig();
            new Harmony("me.moruto.karlson").PatchAll();
            fpsBoost = new FpsBoost(this);
        }

        public void Update() {
            fpsBoost.Update();
        }

        private void InitConfig() {
            disableBloom = Config.Bind("Lighting", "DisableBloom", true, "Toggle the Bloom (glowing lights) from doorFrames, lamps, weapons, etc...");
            disableLights = Config.Bind("Lighting", "DisableLights", true, "Toggle the lights from any lamp or light source");

            CustomResolution = Config.Bind("Resolution", "Resolution", "1920x1080", "Change the resolution of the game");
            isFullscreen = Config.Bind("Resolution", "isFullscreen", true, "Make the game window fullscreen or not");

            NoPlayerSpeedlines = Config.Bind("Particles", "NoPlayerSpeedlines", false, "Toggle the player's speed lines");
        }
    }
}
