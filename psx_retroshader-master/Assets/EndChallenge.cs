using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChallenge : MonoBehaviour {
    public GameObject cake;
    GameObject player;
    public GameObject scalePos;

    float oldDist;
    float newDist;
    public float maxScale;
    float myDifference = 0;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        oldDist = Vector3.Distance(player.transform.position, cake.transform.position);
    }

    // Update is called once per frame
    void Update() {
        newDist = Vector3.Distance(player.transform.position, cake.transform.position);
        if (newDist < oldDist) {
            //lerp scale by the difference between the two
            myDifference = oldDist - newDist;
        }
        scalePos.transform.position = player.transform.position;
        transform.localScale = Vector3.Lerp(transform.localScale, TargetVector3(myDifference), 0.1f);
        transform.localScale = new Vector3(
            Mathf.Clamp(transform.localScale.x, 0f, maxScale),
            Mathf.Clamp(transform.localScale.y, 0f, maxScale),
            Mathf.Clamp(transform.localScale.z, 0f, maxScale)
        );
        player.transform.position = scalePos.transform.position;
        oldDist = newDist;
    }

    Vector3 TargetVector3(float diff) {
        Vector3 returnValue;
        returnValue = new Vector3(
            transform.localScale.x + diff * 5,
            transform.localScale.y + diff * 5,
            transform.localScale.z + diff * 5
            );
        return returnValue;
    }
}
