using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFadeIn : MonoBehaviour
{
    MeshRenderer me;
    Material myMat;
    float myScale = 0.001f;
    float waitForSeconds = 1;

    private void Start() {
        me = GetComponent<MeshRenderer>();
        myMat = me.material;
    }
    private void Update() {
        if (waitForSeconds > 0) {
            waitForSeconds = waitForSeconds - Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (waitForSeconds <= 0) {
            myMat.color = new Color(myMat.color.r, myMat.color.b, myMat.color.g, Mathf.Lerp(myMat.color.a, 0, myScale));
            myScale = myScale * 1.1f;
            if (myMat.color.a <= 0f) {
                Destroy(gameObject);
            }
        }
    }
}
