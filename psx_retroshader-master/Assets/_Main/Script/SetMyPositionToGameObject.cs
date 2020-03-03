using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMyPositionToGameObject : MonoBehaviour {
    public GameObject myTarget;
    void Update() {
        transform.position = myTarget.transform.position;
    }
}
