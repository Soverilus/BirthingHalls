using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfWalk : MonoBehaviour {
    [HideInInspector]
    public GameObject oldHall;

    public GameObject end;
    GameObject myPlayer;
    Vector3 bounds;

    GameObject myClone;
    void Start() {
        bounds = GetComponent<Collider>().bounds.extents;
    }
    void OnCollisionEnter(Collision collision) {
        if (!myPlayer) {
            myClone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + bounds.z * 2), transform.rotation);
            myPlayer = collision.gameObject;
            myClone.GetComponent<InfWalk>().oldHall = gameObject;
            if (oldHall) {
                Destroy(oldHall);
                //replace oldHall with ending backwards hall.
            }
        }
    }
}
