using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject[] myEyes;

    private void Update() {
        for (int i = 0; i < myEyes.Length; i++) {
            //myEyes[i].transform.LookAt(transform.position, myEyes[i].transform.position.normalized);
            myEyes[i].transform.rotation = Quaternion.LookRotation(myEyes[i].transform.position - transform.position);
            myEyes[i].transform.Rotate(Vector3.up, Time.timeScale);
            myEyes[i].transform.localScale = Vector3.Lerp(Vector3.one * 0.5f * myEyes[i].transform.localScale.magnitude, myEyes[i].transform.localScale, Vector3.Magnitude(Vector3.one - myEyes[i].transform.localScale)*10f);
        }
    }
}
