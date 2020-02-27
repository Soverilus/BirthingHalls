using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfWalk : MonoBehaviour {
    [HideInInspector] public GameObject oldHall;

    public GameObject end;
    GameObject myPlayer;
    Vector3 bounds;
    GameObject oldEnd;
    InfWalk cloneIW;
    bool oldIsVisible;

    public GameObject myClone;

    void Start() {
        bounds = GetComponent<Collider>().bounds.extents;
    }
    void OnCollisionEnter(Collision collision) {
        if (!myPlayer) {
            oldEnd = myClone;
            myClone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + bounds.z * 1.75f), transform.rotation);
            myPlayer = collision.gameObject;
            cloneIW = myClone.GetComponent<InfWalk>();
           cloneIW.oldHall = gameObject;
            if (oldHall) {
                //replace oldHall with ending backwards hall.
                StartCoroutine(ReplaceOldHall());
                
                //myClone.GetComponent<InfWalk>().myClone = Instantiate(end, oldHall.transform.position, transform.rotation);
                //Destroy(oldHall);
            }
        }
    }

    IEnumerator ReplaceOldHall() {
        yield return new WaitUntil(() => !oldHall.GetComponent<Renderer>().isVisible||Vector3.Distance(oldHall.transform.position, myPlayer.transform.position) >= 15f);
        Destroy(oldEnd);
        cloneIW.myClone = Instantiate(end, oldHall.transform.position, transform.rotation);
        Destroy(oldHall);
    }
}
