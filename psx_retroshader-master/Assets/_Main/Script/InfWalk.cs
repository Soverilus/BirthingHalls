using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfWalk : MonoBehaviour {
    [HideInInspector]
    public GameObject oldHall;

    public GameObject end;
    GameObject myPlayer;
    Vector3 bounds;

    InfWalk cloneIW;
    bool oldIsVisible;

    [HideInInspector]
    public GameObject myClone;

    void Start() {
        bounds = GetComponent<Collider>().bounds.extents;
    }
    void OnCollisionEnter(Collision collision) {
        if (!myPlayer) {
            Destroy(myClone);
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
        yield return new WaitUntil(() => !oldHall.GetComponent<Renderer>().isVisible);
        cloneIW.myClone = Instantiate(end, oldHall.transform.position, transform.rotation);
        Destroy(oldHall);
    }
}
