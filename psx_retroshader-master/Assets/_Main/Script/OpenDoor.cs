using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class OpenDoor : MonoBehaviour {
    bool used = false;
    public string[] mySceneTypes = new string[2];
    [Range(0.01f, 1f)] public float[] mySceneChances;

    string[] myScenes;
    int sceneToLoad;
    string sceneTypeToLoad;

    int randomDoorText;

    private void Start() {
        myScenes = _StaticGameManager.Scenes.scenes;
        WhichSceneToLoad();
        randomDoorText = Random.Range(0, 10);
    }

    private void Update() {
        if (used) {
            if (_StaticGameManager.GetFadeAmount() <= 0f) {
                _StaticGameManager.Doors._AddDoor();
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public void ReCalc() {
        WhichSceneToLoad();
    }

    void WhichSceneToLoad() {
        DoorCalc();
    }

    void DoorCalc() {
        //This function sets the "sceneToLoad" by getting a string and then comparing that string to any string in "myScenes"
        //First, get the scenetype to load
        //checks which type of scene to load.
        //Does this by checking which in a random.range is highest based on the Door's in-scene chances for a type.
        if (_StaticGameManager.Doors.DoorsOpened < _StaticGameManager.Doors.doorsUntilEnd) {
            //Debug.Log(_StaticGameManager.Doors.DoorsOpened + " is smaller than " + _StaticGameManager.Doors.doorsUntilEnd);
            if (!_StaticGameManager.Doors.stalker) {
                TypeCalc();
            }
            else {
                sceneTypeToLoad = "stalker";
            }
        }
        else {
            sceneTypeToLoad = "end";
        }

        switch (sceneTypeToLoad) {
            default:
                break;
            case "puzzle":
                //add injection for puzzle chances based on which have already been visited?
                break;

            case "travel":

                break;
        }

        //then create an array with all of those scenes inside it, but remove the current scene
        int howManyOfType = 0;
        for (int i = 0; i < myScenes.Length; i++) {
            if (myScenes[i].Contains(sceneTypeToLoad.ToString())) {
                howManyOfType++;
                if (i == SceneManager.GetActiveScene().buildIndex/*the scene target contains our current scene*/) {
                    howManyOfType--;
                }
            }
        }
        //Debug.Log("Typesize: " + howManyOfType);
        //Debug.Log("All Scenesize: " + myScenes.Length);

        //create an array of our current, narrowed down list of candidate scenes by type.
        int myCurrentArrayPoint = 0;
        string[] myTypeScenes = new string[howManyOfType];
        for (int i = 0; i < myScenes.Length; i++) {
            if (myScenes[i].Contains(sceneTypeToLoad.ToString())) {
                //Debug.Log(sceneTypeToLoad + " ?= " + myScenes[i]);
                if (i != SceneManager.GetActiveScene().buildIndex) {
                    myTypeScenes[myCurrentArrayPoint] = myScenes[i];
                    myCurrentArrayPoint++;
                }
            }
        }

        //now that we have a list of our possible scenes by type, we can decide on a victor by randomly rolling against their own chances.
        //for scenes that potentially don't have an innate chance, we set them to 0.5f;
        //then, it randomly rolls between 0 and that value, then sets the target string scene to the highest number each loop
        //until there is a victor.

        string victorScene = "N/A";
        float currentHighest = 0f;
        for (int i = 0; i < myTypeScenes.Length; i++) {
            float value;
            float outNum;
            if (float.TryParse(Regex.Match(myTypeScenes[i], "[\\+\\-]?\\d+\\.?\\d+").Value, out outNum)) {
                value = float.Parse(Regex.Match(myTypeScenes[i], "[\\+\\-]?\\d+\\.?\\d+").Value);
            }
            else {
                value = 0.5f;
            }
            float newNum = Random.Range(0, value);
            if (newNum > currentHighest) {
                currentHighest = newNum;
                victorScene = myTypeScenes[i];
            }
        }

        //now, our target scene is whatever scene "victorScene" is, so the operation is simple:
        bool success = false;
        for (int i = 0; i < myScenes.Length; i++) {
            if (victorScene == myScenes[i]) {
                sceneToLoad = i;
                success = true;
            }
        }

        if (!success) {
            Debug.LogError("No Scene Was Selected!");
            if (SceneManager.GetActiveScene().buildIndex == 1) {
                sceneToLoad = 2;
            }
            else {
                sceneToLoad = 1;
            }
        }
    }

    void TypeCalc() {
        float[] myResult = new float[mySceneChances.Length];
        for (int i = 0; i < mySceneChances.Length; i++) {
            myResult[i] = Random.Range(0, mySceneChances.Length);
        }
        int largest = 0;
        for (int i = 0; i < myResult.Length; i++) {
            if (myResult[i] > myResult[largest]) {
                largest = i;
            }
        }
        sceneTypeToLoad = mySceneTypes[largest];
    }

    public void OnUse() {
        if (!used) {
            _StaticGameManager.FadeOut();
            used = true;
        }
    }
/*
    void OldSceneCalc() {
        //checks which type of scene to load.
        //Does this by checking which in a random.range is highest
        float[] myResult = new float[mySceneChances.Length];
        for (int i = 0; i < mySceneChances.Length; i++) {
            myResult[i] = Random.Range(0, mySceneChances.Length);
        }
        int largest = 0;
        for (int i = 0; i < myResult.Length; i++) {
            if (myResult[i] > myResult[largest]) {
                largest = i;
            }
        }
        sceneTypeToLoad = mySceneTypes[largest];

        //now that it has the type to load, it has to choose which scene in a list loads.
        //first, creates a list of the scenes containing in their name the sceneType
        //then randomly rolls between those scenes, with their custom modifier set to the max possible for each roll.
        List<int> indexToLoad = new List<int>();
        List<string> scenesList = new List<string>();
        for (int i = 0; i < myScenes.Length; i++) {
            scenesList.Add(myScenes[i]);
        }
        for (int i = 0; i < myScenes.Length; i++) {
            //Debug.Log(scenesList[i]); Debug.Log(scenesList[i].Contains(sceneTypeToLoad));
            if (scenesList[i].Contains(sceneTypeToLoad.ToString())) {
                if (i != SceneManager.GetActiveScene().buildIndex) {
                    indexToLoad.Add(i);
                }
            }
        }

        //First, add all the results to a final, concrete list of results.
        //Find each stage's personal chance, if none exist, set it to 0.5;
        float[] finalResult = new float[indexToLoad.Count];
        for (int i = 0; i < indexToLoad.Count; i++) {
            float value;
            float outNum;
            if (float.TryParse(Regex.Match(scenesList[indexToLoad[i]], "[\\+\\-]?\\d+\\.?\\d+").Value, out outNum)) {
                value = float.Parse(Regex.Match(scenesList[indexToLoad[i]], "[\\+\\-]?\\d+\\.?\\d+").Value);
            }
            else {
                value = 0f;
            }
            Debug.Log("For scene " + scenesList[indexToLoad[i]] + " we set the chance to " + Random.Range(0, value));
            finalResult[i] = Random.Range(0, value);
        }

        //now that we have our list of possible candidates, further break it down by finding out which
        //randomly generated value between 0 and it's number it approached!
        int finalLargest = 0;
        for (int i = 0; i < finalResult.Length; i++) {
            if (finalResult[i] > finalResult[finalLargest]) {
                finalLargest = i;
            }
        }
        sceneToLoad = indexToLoad[finalLargest];
    }
*/
}