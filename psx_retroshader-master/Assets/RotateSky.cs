using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    Camera myc;
    private void Start() {
        if (GetComponent<Camera>()) {
            myc = GetComponent<Camera>();
        }
    }
    private void Update() {
        if (myc) {
            transform.RotateAround(transform.localPosition, Vector3.up, Time.timeScale);
        }
    }
}