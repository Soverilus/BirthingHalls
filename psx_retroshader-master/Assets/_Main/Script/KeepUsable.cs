using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUsable : MonoBehaviour
{
    private void Update() {
        if (gameObject.layer != 8) {
            gameObject.layer = 8;
        }
    }
}
