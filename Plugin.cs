using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoreFps {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin {
        public const string pluginGuid = "me.moruto.karlson.morefps";
        public const string pluginName = "MoreFps";
        public const string pluginVersion = "1.0.0";

        private ConfigEntry<bool> disableBloom;
        private ConfigEntry<bool> disableLights;

        public void Awake() {
            Logger.LogInfo("MoreFps by Moruto_ loaded!");
            SceneManager.sceneLoaded += sceneLoadEvent;

            disableBloom = Config.Bind("General", "disableBloom", true, "Toggle the Bloom (glowing lights) from doorFrames, lamps, weapons, etc...");
            disableLights = Config.Bind("General", "disableLights", true, "Toggle the lights from any lamp or light source");
        }

        private void sceneLoadEvent(Scene scene, LoadSceneMode mode) {
            if (GameState.Instance.GetGraphics() == true) return;

            foreach (GameObject obj in scene.GetRootGameObjects()) {
                if (obj.name.ToLower() == "bloom") {
                    if (disableBloom.Value) Destroy(obj);
                }

                if (disableLights.Value) {
                    if (obj.GetComponentsInChildren<Light>() != null) foreach (Light light in obj.GetComponentsInChildren<Light>()) { Destroy(light); }
                }
            }
        }
    }
}
