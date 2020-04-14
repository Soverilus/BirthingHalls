using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEvent : EventHolder
{
    string doorString;
    const string glyphs = 
        "abcdefghijklmnopqrstuvwxyz" +
        "012345666789♫-+=*()&^%$#@!:',.+/?|}{[]" +
        "abcdefghijklmnopqrstuvwxyz" +
        "012345666789♫-+=*()&^%$#@!:',.+/?|}{[]";

    protected override void Event() {
        _StaticGameManager.Doors._RemoveDoor();
        StartCoroutine(SetDoorStringRepeating());
    }

    /*private void Start() {
        Debug.Log("REMOVE THIS WHEN FINISHED WITH TESTING");
            StartCoroutine(SetDoorStringRepeating());
    }*/

    private void SetDoorString() {
        //create situation for (first three numbers, then e+, then three more numbers)
        //create situation for specific formulas with random numbers.
        int charAmount = Random.Range(2, 26);
        doorString = "";
        for (int i = 0; i < charAmount; i++) {
            doorString += glyphs[Random.Range(0, glyphs.Length)];
            //Debug.Log(doorString);
        }
    }

    private IEnumerator SetDoorStringRepeating() {
        SetDoorString();
        _StaticGameManager.Doors._SetDoorString(doorString);
        Debug.Log(doorString);
        yield return new WaitForSecondsRealtime(Random.Range(0.5f, 2f));
        StartCoroutine(SetDoorStringRepeating());
    }
    private void OnDestroy() {
        _StaticGameManager.Doors._UnsetDoorString();
    }
}
