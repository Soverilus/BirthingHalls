using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHolder : MonoBehaviour
{

    public void EventParse() {
        Event();
        Debug.Log("successfully added " + gameObject.GetComponent(typeof (EventHolder)));
    }

    protected virtual void Event() {
        //do things
    }
}
