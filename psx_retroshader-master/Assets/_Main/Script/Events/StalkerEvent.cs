using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerEvent : EventHolder
{
    protected override void Event() {
        _StaticGameManager.EventParsing.AddEvent("DoorCounter", 1);
    }
}
