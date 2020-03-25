using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEmittersWithMe : MonoBehaviour {
    public GameObject[] myEmitters;
    Light me;

    //float initIntensity;
    float initRange;
    float avgInitGlobalScale;

    private void Start() {
        me = GetComponent<Light>();
        //initIntensity = me.intensity;
        initRange = me.range;
        avgInitGlobalScale = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3f;
    }
    private void Update() {
        for (int i = 0; i < myEmitters.Length; i++) {
            myEmitters[i].transform.localScale = transform.lossyScale / avgInitGlobalScale;
        }
        float scaleMult = ((transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3f)/avgInitGlobalScale;
        //me.intensity = initIntensity * scaleMult;
        me.range = initRange * scaleMult;
    }
}
