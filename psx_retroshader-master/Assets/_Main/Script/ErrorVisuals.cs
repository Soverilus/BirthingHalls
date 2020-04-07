using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ErrorVisuals : MonoBehaviour
{
    GameObject player;
    Camera me;
    public Renderer[] myRender;
    float myPos;
    OpenDoor myOD;
    void Start() {
        myOD = GameObject.FindGameObjectWithTag("Door").GetComponent<OpenDoor>();
        me = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        
        if (player.transform.position.y < -5) {
            myPos = (player.transform.position.y + 5f) / -5000f;
            for (int i = 0; i < myRender.Length; i++) {
                myRender[i].material.color = Color.Lerp(myRender[i].material.color, Color.black, myPos);
                myRender[i].material.SetFloat("_Glossiness", Mathf.Lerp(myRender[i].material.GetFloat("_Glossiness"), 0f, myPos));
            }
        }
        if (player.transform.position.y < -1500f) {
            myOD.OnUse();
        }
    }
}
