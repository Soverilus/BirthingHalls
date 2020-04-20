using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameOnStart : MonoBehaviour {

    Canvas myCanvas;
    public Text doorText;
    GameObject myPlayer;
    AudioSource[] myAS;
    public Renderer myFadePanel;
    Color FadedOut = Color.black;
    Color FadedIn = Color.clear;
    float fadeFloat;
    private void Awake() {
        myAS = GetComponents<AudioSource>();
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
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        //Debug.Log(myPlayer.gameObject.name);
        name = "DDOL";
        doorText.text = "0";
        StartCoroutine(FadeIn());
    }
    private void OnLevelWasLoaded(int level) {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        doorText.text = _StaticGameManager.Doors.DoorsOpenedString;
        _StaticGameManager.EventParsing.EventParse();
        Time.timeScale = 1f;
        if (level != 0) {
            myAS[0].Play();
            StartCoroutine(FadeIn());
        }
    }

    public void PlayOpenDoor() {
        myAS[1].Play();
    }
    IEnumerator FadeIn() {
        myFadePanel.material.color = Color.Lerp(FadedOut, FadedIn, fadeFloat);
        yield return new WaitForEndOfFrame();
        if (myFadePanel.material.color.a > FadedIn.a) {
            StartCoroutine(FadeIn());
            fadeFloat = Mathf.Clamp(fadeFloat + 0.001f, 0f, 1f);
        }
    }

    public IEnumerator FadeOut() {
        myFadePanel.material.color = Color.Lerp(FadedOut, FadedIn, fadeFloat);
        yield return new WaitForEndOfFrame();
        if (myFadePanel.material.color.a > FadedIn.a) {
            StartCoroutine(FadeOut());
            fadeFloat = Mathf.Clamp(fadeFloat - 0.001f, 0f, 1f);
        }
    }

    public void ParseDoorChange() {
        doorText.text = _StaticGameManager.Doors.DoorsOpenedString;
    }

    public void EventParser(string eventName) {
        //Debug.Log("test");
        StartCoroutine(eventName);
    }

    //this is bad and I know it's bad
    IEnumerator TestEvent() {

        yield return new WaitForSeconds(1);
    }

    IEnumerator MedKit() {
        if (!myPlayer) {
            myPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        if (!myPlayer.GetComponent<MedKitEvent>())
        myPlayer.AddComponent<MedKitEvent>();
        Debug.Log("Added MedKitEvent to " + gameObject.name);
        yield return new WaitForSeconds(1);
    }

    IEnumerator DoorCounter() {
        if (!myPlayer) {
            myPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        if (!myPlayer.GetComponent<CounterEvent>())
            myPlayer.AddComponent<CounterEvent>();
        Debug.Log("Added CounterEvent to " + gameObject.name);
        yield return new WaitForSeconds(1);
    }

    IEnumerator StalkingEye() {
        if (!myPlayer) {
            myPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        if (!myPlayer.GetComponent<StalkerEvent>())
            myPlayer.AddComponent<StalkerEvent>();
        Debug.Log("Added StalkerEvent to " + gameObject.name);
        yield return new WaitForSeconds(1);
    }
}
