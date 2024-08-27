using UnityEngine.SceneManagement;
using UnityEngine;

namespace MoreFps {
    internal class FpsBoost {
        private Plugin plugin;
        public FpsBoost(Plugin plugin) {
            this.plugin = plugin;
            SceneManager.sceneLoaded += SceneLoadEvent;
        }

        public void Update() {
            string[] resolutionVals = plugin.CustomResolution.Value.Split('x');
            if (Screen.currentResolution.width.ToString() == resolutionVals[0] && Screen.currentResolution.height.ToString() == resolutionVals[1]) return;
            Screen.SetResolution(int.Parse(resolutionVals[0]), int.Parse(resolutionVals[1]), plugin.isFullscreen.Value);
        }

        private void SceneLoadEvent(Scene scene, LoadSceneMode mode) {
            foreach (GameObject obj in scene.GetRootGameObjects()) {
                if (obj.name == "Camera") if (plugin.NoPlayerSpeedlines.Value) GameObject.Destroy(obj.GetComponentInChildren<ParticleSystem>());

                if (GameState.Instance.GetGraphics() == true) return;
                if (obj.name.ToLower() == "bloom") if (plugin.disableBloom.Value) GameObject.Destroy(obj);
                if (plugin.disableLights.Value) if (obj.GetComponentsInChildren<Light>() != null) foreach (Light light in obj.GetComponentsInChildren<Light>()) { GameObject.Destroy(light); }
            }
        }
    }
}
