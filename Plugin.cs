using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoreFps {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    internal class Plugin : BaseUnityPlugin {
        public const string pluginGuid = "me.moruto.karlson.morefps";
        public const string pluginName = "MoreFps";
        public const string pluginVersion = "1.0.1";

        private ConfigEntry<bool> disableBloom;
        private ConfigEntry<bool> disableLights;

        private ConfigEntry<string> CustomResolution;
        private ConfigEntry<bool> isFullscreen;

        private ConfigEntry<bool> NoPlayerSpeedlines;

        public void Awake() {
            Logger.LogInfo("MoreFps by Moruto loaded!");
            SceneManager.sceneLoaded += SceneLoadEvent;

            disableBloom = Config.Bind("Lighting", "DisableBloom", true, "Toggle the Bloom (glowing lights) from doorFrames, lamps, weapons, etc...");
            disableLights = Config.Bind("Lighting", "DisableLights", true, "Toggle the lights from any lamp or light source");

            CustomResolution = Config.Bind("Resolution", "Resolution", "1920x1080", "Change the resolution of the game");
            isFullscreen = Config.Bind("Resolution", "isFullscreen", true, "Make the game window fullscreen or not");

            NoPlayerSpeedlines = Config.Bind("Particles", "NoPlayerSpeedlines", false, "Toggle the player's speed lines");
        }

        public void Update() {
            string[] resolutionVals = CustomResolution.Value.Split('x');
            if (Screen.currentResolution.width.ToString() == resolutionVals[0] && Screen.currentResolution.height.ToString() == resolutionVals[1]) return;

            Screen.SetResolution(int.Parse(resolutionVals[0]), int.Parse(resolutionVals[1]), isFullscreen.Value);
        }

        private void SceneLoadEvent(Scene scene, LoadSceneMode mode) {
            foreach (GameObject obj in scene.GetRootGameObjects()) {

                if (obj.name == "Camera") if (NoPlayerSpeedlines.Value) Destroy(obj.GetComponentInChildren<ParticleSystem>());

                if (GameState.Instance.GetGraphics() == true) return;
                if (obj.name.ToLower() == "bloom") if (disableBloom.Value) Destroy(obj);
                if (disableLights.Value) if (obj.GetComponentsInChildren<Light>() != null) foreach (Light light in obj.GetComponentsInChildren<Light>()) { Destroy(light); }
            }
        }
    }
}
