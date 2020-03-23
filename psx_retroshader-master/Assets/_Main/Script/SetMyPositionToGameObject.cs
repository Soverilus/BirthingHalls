using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMyPositionToGameObject : MonoBehaviour {
    public GameObject myTarget;
    public bool useTag = false;
    public string myTag = "MainCamera";

    void Update() {
        if (Camera.main) {
            if (useTag && !myTarget) {
                myTarget = GameObject.FindGameObjectWithTag(myTag);
            }
            transform.position = myTarget.transform.position;
        }
    }
}
