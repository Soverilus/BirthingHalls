using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChallenge : MonoBehaviour {
    public GameObject cake;
    GameObject player;
    public GameObject table;
    public GameObject tableScalePos;
    public GameObject scalePos;
    Vector3 myDownSizeTarget;
    int targetDownsizeInt = 9;

    AudioSource myAS;
    bool AudioStart = false;

    float reductionSize;

    bool scaleDown = false;
    bool waitFrame = true;

    
    float oldDist;
    float newDist;
    public float maxScale;
    float myDistance = 0;
    float initialDistance;
    Vector3 myOgScale;
    Vector3 targetScale;
    float myScale;
    float roomScale;
    float percentScale;
    void Start() {
        myAS = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        oldDist = Vector3.Distance(player.transform.position, cake.transform.position);
        myOgScale = transform.localScale;
        targetScale = new Vector3(maxScale, maxScale, maxScale);
        reductionSize = table.GetComponent<Collider>().bounds.size.magnitude;

        //Debug.Log("roomscale = " + roomScale);
        initialDistance = Vector3.SqrMagnitude(table.transform.position - player.transform.position);
    }

    // Update is called once per frame
    void Update() {
        RoomSizeChange();
        AudioChange();
    }

    void RoomSizeChange() {
        if (!waitFrame) {
            if (!scaleDown) {
                roomScale = transform.lossyScale.sqrMagnitude / myOgScale.sqrMagnitude;
                newDist = Vector3.Distance(player.transform.position, cake.transform.position);
                if (newDist < oldDist) {
                    myDistance = Vector3.SqrMagnitude(cake.transform.position - player.transform.position);
                    //myScale = myDistance/(initialDistance*percentScale);
                    //initialDistance = initialDistance * myScale;
                    myScale = (myDistance / initialDistance) / roomScale;
                }

                /*Debug.Log("myScale = " + myScale);
                Debug.Log("initialDistance = " + initialDistance);
                Debug.Log("myDistance = " + myDistance);
                Debug.Log("roomScale = " + roomScale)*/
                scalePos.transform.position = player.transform.position;
                transform.localScale = Vector3.Slerp(targetScale, myOgScale, myScale);
                //Debug.Log(transform.localScale + " and my distance is " + myDistance);
                player.transform.position = scalePos.transform.position;
                oldDist = newDist;
                //Debug.Log(transform.localScale.x + " " + maxScale);
                if (transform.localScale.x >= maxScale - 1) {

                    scaleDown = true;
                    Debug.Log(scaleDown);
                    myDownSizeTarget = transform.localScale;
                }
            }
            else {
                newDist = Vector3.Distance(player.transform.position, cake.transform.position);
                if (newDist < oldDist) {
                    myDistance = Vector3.SqrMagnitude(cake.transform.position - player.transform.position);
                    oldDist = newDist;
                }
                    scalePos.transform.position = player.transform.position;
                tableScalePos.transform.position = table.transform.position;
                transform.localScale = Vector3.Lerp(transform.localScale, myDownSizeTarget, 0.1f);
                //Debug.Log(myDownSizeTarget);
                player.transform.position = scalePos.transform.position;
                table.transform.position = tableScalePos.transform.position;
            }
        }
        waitFrame = false;
    }
    void AudioChange() {
        //if (AudioStart) {
            myAS.pitch = Mathf.Clamp(Mathf.Lerp(1f, 0f, myDistance / initialDistance), 0.01f, 1f);
            myAS.volume = Mathf.Clamp(Mathf.Lerp(1f, 0f, myDistance / initialDistance), 0.01f, 1f);
        //}
    }

    IEnumerable WaitFrame() {
        yield return new WaitForEndOfFrame();
        waitFrame = false;
    }

    public void ScaleDown() {
        targetDownsizeInt--;
        Mathf.Clamp(targetDownsizeInt, 1, 8);
        myDownSizeTarget = Vector3.Lerp(targetScale, myOgScale, 1f / targetDownsizeInt);
        
    }

    //room og size is 15 scale
    //scales up to 200

    //distance is 411

    //411 - 0 = 100% - 0%
    // so divide distance by initial distance to get distance as a percentage
    //HOWEVER, distance is changed once scale changes. need to multiply InitialDistance by roomscale
    //HOWEVER, roomscale only represents our original scale ratio. we need to multiply by our current roomscale as a percentage of the original scale ratio.

    //WHY ISN'T ANYTHING WORKING
    //okay it works now
}
