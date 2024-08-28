using UnityEngine;
using UnityEngine.SceneManagement;

internal class FpsBoost {
    public FpsBoost() {
        SceneManager.sceneLoaded += SceneLoadEvent;
    }

    public void Update() {
        string[] resolutionVals = Plugin.CustomResolution.Value.Split('x');
        if (Screen.currentResolution.width.ToString() == resolutionVals[0] && Screen.currentResolution.height.ToString() == resolutionVals[1]) return;
        Screen.SetResolution(int.Parse(resolutionVals[0]), int.Parse(resolutionVals[1]), !Plugin.isWindowed.Value);
    }

     private void SceneLoadEvent(Scene scene, LoadSceneMode mode) {
        foreach (GameObject obj in scene.GetRootGameObjects()) {
            if (obj.name == "Camera") if (Plugin.NoPlayerSpeedlines.Value) UnityEngine.Object.Destroy(obj.GetComponentInChildren<ParticleSystem>());

            if (Plugin.NoJumpPadEffect.Value && obj.name == "World") {
                foreach (Transform child in obj.transform) {
                    if (child.name.Contains("JumpPad")) {
                        foreach (Transform grandchild in child) {
                            if (grandchild.name.Contains("Effect") && grandchild.GetComponent<ParticleSystem>() != null) {
                                GameObject.Destroy(grandchild.gameObject);
                            }

                            if (!Plugin.removeJumpPadAnimation.Value) continue;
                            Animator animator = grandchild.GetComponent<Animator>();
                            if (animator != null) GameObject.Destroy(animator);
                        }
                    }
                }
            }


            if (GameState.Instance.GetGraphics() == true) return;

            if (obj.name.ToLower() == "bloom") if (Plugin.disableBloom.Value) UnityEngine.Object.Destroy(obj);
            if (Plugin.disableLights.Value) if (obj.GetComponentsInChildren<Light>() != null) foreach (Light light in obj.GetComponentsInChildren<Light>()) { UnityEngine.Object.Destroy(light); }
        }
    }
}
