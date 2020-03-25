using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject player;
    public GameObject lights;
    public GameObject DDOL;
    // Start is called before the first frame update
    void Start() {
        player.SetActive(false);
        DDOL.SetActive(false);
    }

    public void StartGame() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        DDOL.SetActive(true);
        player.SetActive(true);
        Destroy(lights);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
