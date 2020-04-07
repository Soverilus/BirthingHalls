using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceRotat : MonoBehaviour
{
    bool rotate = false;
    void Start() {
        int i = Random.Range(0, 1);
        if (i == 0) {
            StartCoroutine(WaitBeforeYouWeight());
        }
    }
    IEnumerator WaitBeforeYouWeight() {
        yield return new WaitForSeconds(5f);
        rotate = true;
    }
    void Update() {
        if (rotate) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.001f);
        }
    }
}
