using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StalkerEvent : EventHolder
{
    GameObject[] mySt;
    GameObject[] myDoors;
    OpenDoor[] myDScripts;
    Light[] myLights;
    Color[] myColors;
    Color targetColor = Color.red;
    protected override void Event() {
        _StaticGameManager.Doors.stalker = true;
        _StaticGameManager.EventParsing.AddEvent("DoorCounter", 1);
        mySt = GameObject.FindGameObjectsWithTag("Stalker");
        //Debug.Log(mySt);
        myDoors = GameObject.FindGameObjectsWithTag("Door");
        myDScripts = new OpenDoor[myDoors.Length];
        myLights = new Light[mySt.Length];
        myColors = new Color[mySt.Length];
        for (int i = 0; i < myLights.Length; i++) {
            
            myLights[i] = mySt[i].GetComponent<Light>();
            myColors[i] = myLights[i].color;
        }
        for (int i = 0; i < myDoors.Length; i++) {
            myDScripts[i] = myDoors[i].GetComponent<OpenDoor>();
            myDScripts[i].ReCalc();
        }
    }

    private void Update() {
        if (mySt.Length > 0) StalkerMove();
    }
    float myDist;
    void StalkerMove() {
        float oldDist = 100f;
        for (int i = 0; i < mySt.Length; i++) {
            if (mySt[i]) {
                mySt[i].transform.position = Vector3.Slerp(mySt[i].transform.position, transform.position, 0.001f);
                //Debug.Log(Vector3.Magnitude(mySt[i].transform.position - transform.position));
                if (myDist < oldDist) {
                    myDist = Vector3.Magnitude(mySt[i].transform.position - transform.position);
                    oldDist = myDist;
                }
            }
            if (myDist <= 12f) {
                Time.timeScale = Mathf.Clamp(myDist/5f, 0.25f, 1f);
                StalkerIntensity();
                if (myDist <= 1.45f) {
                    SceneManager.LoadScene(_StaticGameManager.Scenes.LoseScene);
                    Debug.Log("YouLose");
                }
            }
        }
    }

    void StalkerIntensity() {
        for (int i = 0; i < myLights.Length; i++) {
            myLights[i].color = Color.Lerp(targetColor, myColors[i],  myDist / 6f);
        }
    }

    private void OnDestroy() {
        _StaticGameManager.Doors.stalker = false;
    }
}
