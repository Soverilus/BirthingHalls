using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour {
    public string[] mySceneTypes;
    [Range(0.01f, 1f)] public float[] mySceneChances;

    string[] myScenes;
    int sceneToLoad;
    string sceneTypeToLoad;

    private void Start() {
        myScenes = _StaticGameManager.Scenes.scenes;
        WhichSceneToLoad();
    }

    void WhichSceneToLoad() {
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
        //then randomly rolls between those scenes to decide which one to choose.
        List<int> indexToLoad = new List<int>();
        List<string> scenesList = new List<string>();
        for (int i = 0; i < myScenes.Length; i++) {
            scenesList.Add(myScenes[i]);
        }
        for (int i = 0; i < myScenes.Length; i++) {
            //Debug.Log(scenesList[i]); Debug.Log(scenesList[i].Contains(sceneTypeToLoad));
            if (scenesList[i].Contains(sceneTypeToLoad.ToString())) {
                indexToLoad.Add(i);
            }
        }
        sceneToLoad = indexToLoad[Random.Range(0, indexToLoad.Count)];
    }

    public void OnUse() {
        _StaticGameManager.Doors._AddDoor();
        SceneManager.LoadScene(sceneToLoad);
    }
}
