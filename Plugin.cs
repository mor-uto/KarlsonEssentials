using BepInEx;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoreFps {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin {
        public const string pluginGuid = "me.moruto.karlson.morefps";
        public const string pluginName = "MoreFps";
        public const string pluginVersion = "1.0.0";        

        public void Awake() {
            Logger.LogInfo("MoreFps by Moruto_ loaded!");
            SceneManager.sceneLoaded += sceneLoadEvent;
        }

        private void sceneLoadEvent(Scene scene, LoadSceneMode mode) {
            foreach (GameObject obj in scene.GetRootGameObjects()) {
                if (obj.name.ToLower() == "bloom") {
                    Destroy(obj);
                }

                if (obj.GetComponentsInChildren<Light>() != null) {
                    foreach (Light light in obj.GetComponentsInChildren<Light>()) { Destroy(light); }
                }
            }
        }
    }
}
