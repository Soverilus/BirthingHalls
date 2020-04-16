using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterObj : MonoBehaviour {
    ParticleSystem myPS;
    public bool test = true;
    private void Start() {
        myPS = GetComponent<ParticleSystem>();
        if (test) {
            myPS.Stop();
        }
    }

    private void Update() {
        if (test && myPS.isPlaying) {
            myPS.Stop();
        }
    }
}
