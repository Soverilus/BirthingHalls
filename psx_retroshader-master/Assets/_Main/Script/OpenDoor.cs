using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour {
    public string[] mySceneTypes;
    [Range(0,1)] public float[] mySceneChances;

    public void OnUse() {
        //change scene.
        //Load scene by string - string.contains(mySceneTypes.SelectedString); (bool)
        //..set scene likeliness in editor???
        //hmm..
    }
}
