using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTable : MonoBehaviour {
    float distanceFromPlayer;
    public float distanceToActivate;
    GameObject player;
    bool active = true;
    public GameObject[] deleteOnActive;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (active) {
            ActivateTable();
        }
    }

    void ActivateTable() {
        distanceFromPlayer = Vector3.SqrMagnitude(player.transform.position - transform.position);
        if (distanceFromPlayer < distanceToActivate) {
            GetComponent<Rigidbody>().isKinematic = false;
            _StaticGameManager.ChangeLayersRecursively(transform);
           // gameObject.layer = 0;
            /*GameObject[] myArray = gameObject.GetComponentsInChildren<GameObject>(true);
            for (int i = 0; i < myArray.Length; i++) {
                myArray[i].gameObject.layer = 0;
            }*/
            for (int i = 0; i< deleteOnActive.Length; i++) {
                Destroy(deleteOnActive[i]);
            }
            active = false;
        }
    }
}
