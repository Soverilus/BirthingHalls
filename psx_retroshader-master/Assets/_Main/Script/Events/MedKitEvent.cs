using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MedKitEvent : EventHolder {
    override protected void Event() {

    }

    private GUIParse myGUI;
    private Slider myHealth;
    private Slider fakeHealth;
    bool fullyHealed = false;
    float hpIncr = 0f;
    float fakeHPIncr = 0f;
    bool showingGameOver = false;
    private void Start() {
        //Debug.Log("successfully added MedKitEvent to " + gameObject.name);
        myGUI = GameObject.FindGameObjectWithTag("GUIParse").GetComponent<GUIParse>();
        fakeHealth = myGUI.fakeHealth;
        myHealth = myGUI.Health;
        fakeHealth.gameObject.SetActive(true);
        myHealth.gameObject.SetActive(true);
        fakeHealth.value = 0f;
        myHealth.value = (myHealth.minValue + myHealth.maxValue) / 2f;
        hpIncr = (myHealth.minValue + myHealth.maxValue) / 5000f;
        fakeHPIncr = (fakeHealth.minValue + fakeHealth.maxValue) / 5000f;
    }

    private void Update() {
        switch (fullyHealed) {
            case false:
                NotHealed();
                break;

            case true:
                Healed();
                break;
        }
    }

    void NotHealed() {
        myHealth.value += hpIncr;
        if (myHealth.value >= myHealth.maxValue) {
            fullyHealed = true;
            myHealth.value = 0f;
            myHealth.fillRect.gameObject.GetComponent<Image>().enabled = false;
            fakeHealth.value = fakeHealth.maxValue;
            if (!showingGameOver) {
                showingGameOver = true;
                Debug.Log("Add Game Over Screen Here");
                //do GAME OVER YEAAAAAAA
            }
        }
    }
    void Healed() {
        fakeHealth.value -= fakeHPIncr;
        if (fakeHealth.value <= 0) {
            fullyHealed = false;
            myHealth.fillRect.gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
