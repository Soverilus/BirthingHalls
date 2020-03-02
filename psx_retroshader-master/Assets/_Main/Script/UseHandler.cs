using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHandler : MonoBehaviour
{

    //GENERAL SETTINGS
    [Header("Player Use Settings")]
    [Tooltip("How close the player has to be in order to use an item, pick up something, or use an object in scene.")]
    public float reach = 4.0F;
    [Tooltip("Choose the Layer the raycast will Hit.")]
    public LayerMask mask;

    private void Update() {
        if (Input.GetAxisRaw("UseKey") > 0) {
            OnPressUseKey();
        }
    }

    public void OnPressUseKey() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reach, mask)) {
            OnRaycastActivation(hit.transform.gameObject);
        }
    }

    //Events that activate from the player

    public void OnRaycastActivation(GameObject objectHit) { //Activates on:
        if (objectHit.GetComponent<MonoBehaviour>()) {
            objectHit.gameObject.SendMessage("OnUse", gameObject, SendMessageOptions.DontRequireReceiver);
            //Debug.Log("successfully sent to object");
            //sends a message to invoke "PlayerRayhit" in any script of the collided object.
        }
    }
}
