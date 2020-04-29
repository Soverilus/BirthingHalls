using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHolder : MonoBehaviour
{
    protected virtual void Start() {
        EventParse();
    }
    public void EventParse() {
        Event();
        //Debug.Log("successfully added " + gameObject.GetComponent(typeof (EventHolder)));
    }

    protected virtual void Event() {
        //do things
    }
}
