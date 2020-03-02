using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameOnStart : MonoBehaviour {

    Canvas myCanvas;
    public Text doorText;
    private void Awake() {
        if (GameObject.FindGameObjectWithTag("Consistent")) {
            GameObject[] myObjects = GameObject.FindGameObjectsWithTag("Consistent");
            for (int i = 0; i < myObjects.Length; i++) {
                if (myObjects[i].name == "DDOL") {
                    Destroy(gameObject);
                    return;
                }
            }
        }
        _StaticGameManager.Scenes.CalcScenes();
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        name = "DDOL";
        doorText.text = "0";
    }
    private void OnLevelWasLoaded(int level) {
        doorText.text = _StaticGameManager.Doors.DoorsOpenedString;
    }
}
