using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUse : MonoBehaviour
{
    public LibraryPuzzle myParent;
    Vector3 myTarget;
    Vector3 myOriginalPosition;
    Quaternion myOriginalRotation;
    bool used = false;
    Rigidbody myRB;
    BookUse me;
    Collider myCol;
    float distance;

    private void Start() {
        myTarget = transform.localPosition + transform.right;
        myOriginalPosition = transform.position;
        myOriginalRotation = transform.rotation;
        myRB = gameObject.AddComponent<Rigidbody>();
        myRB.isKinematic = true;
        me = GetComponent<BookUse>();
        myCol = GetComponent<Collider>();
        distance = myCol.bounds.size.x * 1.25f;
    }

    public void SetLibPuzzle(LibraryPuzzle thing) {
        myParent = thing;
    }

    public void OnUse() {
        if (!used) {
            used = true;
        }
    }

    private void Update() {
        if (used) {
            if (Vector3.Magnitude(myOriginalPosition-transform.position) < distance) {
                Debug.Log(gameObject.name + " Is moving");
                if (myCol.enabled) {
                    myCol.enabled = false;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + transform.right, 0.01f);
            }
            if (Vector3.Magnitude(myOriginalPosition - transform.position) >= distance && myRB.isKinematic) {
                Debug.Log(gameObject.name + " has finished moving and will now become a rigidbody");
                myCol.enabled = true;
                myRB.isKinematic = false;
                myParent.OnBookUse(me);
            }
        }
        else {
            transform.position = Vector3.Slerp(transform.position, myOriginalPosition, 0.05f);
            transform.rotation = Quaternion.Slerp(transform.rotation, myOriginalRotation, 0.05f);
        }
    }

    public void ResetBooks() {
        used = false;
        myRB.isKinematic = true;
    }
}
