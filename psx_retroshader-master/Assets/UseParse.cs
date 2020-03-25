using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseParse : MonoBehaviour
{
    public GameObject parseTarget;
    public void OnUse() { //Activates on:
        if (parseTarget.GetComponent<MonoBehaviour>()) {
            parseTarget.SendMessage("OnUse", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
