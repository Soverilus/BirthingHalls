using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfWalk : MonoBehaviour {
    [HideInInspector] public GameObject oldHall;

    public GameObject end;
    GameObject myPlayer;
    Vector3 extents;
    Vector3 endExtents;
    GameObject oldEnd;
    InfWalk cloneIW;
    bool oldIsVisible;

    public GameObject myClone;

    void Start() {
        extents = GetComponent<Collider>().bounds.extents;
        endExtents = end.GetComponent<Collider>().bounds.extents;
    }
    void OnCollisionEnter(Collision collision) {
        if (!myPlayer) {
            oldEnd = myClone;
            myClone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + extents.z * 1.99f), transform.rotation);
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
        yield return new WaitUntil(() => !oldHall.GetComponent<Renderer>().isVisible || Vector3.Distance(oldHall.transform.position, myPlayer.transform.position) >= 15f);
        Destroy(oldEnd);
        Vector3 myNewPos;
        myNewPos = new Vector3(oldHall.transform.position.x, oldHall.transform.position.y, (oldHall.transform.position.z + extents.z * 2f) - extents.z - endExtents.z);
        cloneIW.myClone = Instantiate(end, myNewPos, transform.rotation);
        Destroy(oldHall);
    }
}
