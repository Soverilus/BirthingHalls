using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static _StaticGameManager;
public class MedpackEvent : MonoBehaviour {
    private void Start() {
        float myChance = Random.Range(0, 1);
        if (myChance < 0f) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "_Player") {
            EventParsing.AddEvent("MedKit", 2);
            EventParsing.EventParse();
            Destroy(gameObject);
        }
    }
}
