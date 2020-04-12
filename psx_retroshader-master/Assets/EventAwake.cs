using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAwake : MonoBehaviour
{

    private void Start() {
        //myEvent, myMaxChance, minDuration, maxDuration:

        /*
         * myEvent = event name
         * myMaxChance = 1/myMaxChance chance for event to happen.
         * minDuration = the minimum amount of time (measured in scenes) that the event will last.
         * maxDuration = the maximum amount of time (measured in scenes) that the event will last.
        */

        AddEventParse("DoorCounter", 3, 0, 4);
        AddEventParse("StalkingEye", 5, 2, 20);
        
    }

    void AddEventParse(string myEvent, int myMaxChance, int minDuration, int maxDuration) {
        int c = Random.Range(0, myMaxChance);
        if (c == 0) {
            _StaticGameManager.EventParsing.AddEvent(myEvent, Random.Range(minDuration, maxDuration) + 1);
        }
    }
}
