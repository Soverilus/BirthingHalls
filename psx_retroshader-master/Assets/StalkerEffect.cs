using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        if (!_StaticGameManager.EventParsing.ContainsEvent("StalkingEye")) {
            Destroy(gameObject);
        }
    }
}
