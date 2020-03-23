using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEmittersWithMe : MonoBehaviour {
    public GameObject[] myEmitters;

    private void Update() {
        for (int i = 0; i < myEmitters.Length; i++) {
            myEmitters[i].transform.localScale = transform.localScale;
        }
    }
}
