using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerDDOL : MonoBehaviour
{
    GameObject playerCamera;

    private void Awake() {
        playerCamera = Camera.main.gameObject;
        RemoveMainCameraListener();
    }
    private void OnLevelWasLoaded(int level) {
        playerCamera = Camera.main.gameObject;
        RemoveMainCameraListener();
    }
    void RemoveMainCameraListener() {
        if (playerCamera.GetComponent<AudioListener>()) {
            playerCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void Update() {
        if (playerCamera) {
            transform.position = playerCamera.transform.position;
        }
    }
}
