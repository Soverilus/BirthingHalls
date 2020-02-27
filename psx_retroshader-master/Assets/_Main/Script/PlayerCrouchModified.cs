using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerCrouchModified : MonoBehaviour {
    public CharacterController controller;
    public Camera currentCamera;
    [Range(0, 1)] public float percentageFromFloor = 0.75f;
    Vector3 myCameraPosition;
    Vector3 crouchCameraPosition;
    public bool holdToCrouch = true;
    FirstPersonController myCC;
    Footsteps.CharacterFootsteps myCF;
    public bool tapSwitch = true;
    // public float speed=3.0f;
    public bool isCrouch = false;

    public Vector2 volMinMax;


    private Vector3 direction = Vector3.zero;

    void Start() {
        currentCamera = Camera.main;
        myCameraPosition = currentCamera.transform.localPosition;
        crouchCameraPosition = new Vector3(myCameraPosition.x, myCameraPosition.y * percentageFromFloor, myCameraPosition.z);
        myCC = GetComponent<FirstPersonController>();
        myCF = GetComponent<Footsteps.CharacterFootsteps>();
        // motor = GetComponent<charactermotor>();
    }

    void Update() {
        if (Input.GetAxis("Crouch") == 0) {
            tapSwitch = true;
        }
        if (holdToCrouch) {
            HoldCrouch();
        }
        else {
            TapCrouch();
        }
        if (myCC.isCrouch != isCrouch) {
            myCC.isCrouch = isCrouch;
        }
        if (isCrouch) {
            if (currentCamera.transform.localPosition != crouchCameraPosition) {
                currentCamera.transform.localPosition = Vector3.Lerp(currentCamera.transform.localPosition, crouchCameraPosition, 0.1f);
            }
            myCF.minVolMult = volMinMax.x;
            myCF.maxVolMult = volMinMax.y;
            // if (script) script.enabled = false;
            //  GetComponent<CharacterMotor>().enabled = false;
            // speed = 1.0f;
            //  controller.enabled = !controller.enabled;
            // Debug.Log("c_func 1");;
        }
        else {
            if (currentCamera.transform.localPosition != myCameraPosition) {
                currentCamera.transform.localPosition = Vector3.Lerp(currentCamera.transform.localPosition, myCameraPosition, 0.1f);
            }
            else {
                myCC.waitForCamera = false;
                myCF.minVolMult = 1f;
                myCF.maxVolMult = 1f;
            }
            //  GetComponent<CharacterController>().enabled=true;


            // if (script) script.enabled = true;
            //  GetComponent<CharacterMotor>().enabled = true;
            // speed = 3.0f;
            // controller.enabled = !controller.enabled;
            // Debug.Log("c_func 0");;
        }
    }

    void HoldCrouch() {
        if (Input.GetAxis("Crouch") > 0 && !isCrouch && tapSwitch) {
            isCrouch = true;
            tapSwitch = false;

        }
        else {
            if (tapSwitch) {
                isCrouch = false;
            }

        }
    }

    void TapCrouch() {
        if (Input.GetAxis("Crouch") > 0) {
            if (tapSwitch) {
                if (isCrouch) {
                    isCrouch = false;
                }
                else {
                    isCrouch = true;
                }
                tapSwitch = false;
            }
        }
    }
}
//}
/*
 public class Player_Crouch : MonoBehaviour
 {
     [SerializeField]
     private float speed;
     [SerializeField]
     private float crouchingHeight;
     [SerializeField]
     private float standingHeight;
     [SerializeField]
     private float camStandingHeight;
     [SerializeField]
     private float camCrouchingHeight;
     [SerializeField]
     private bool isCrouchTransitionInProgress = false;
     [SerializeField]
     private CharacterController characterController;

     private bool isCrouching = false;

     private void FixedUpdate()
     {        
         if (Input.GetButtonDown("Crouch"))
         {
             isCrouchTransitionInProgress = true;

             if (isCrouching)
             {
                 isCrouching = false;
                 characterController.height = standingHeight;
                 characterController.center = new Vector3(0, 1.0f, 0);
             }
             else
             {
                 isCrouching = true;
                 characterController.height = crouchingHeight;
                 characterController.center = new Vector3(0, 0.5f, 0);
             }
         }

         if (isCrouchTransitionInProgress)
         {
             Vector3 camPosition = transform.position;
             Vector3 standCamPosition = new Vector3(camPosition.x, camStandingHeight, camPosition.z);
             Vector3 crouchCamPosition = new Vector3(camPosition.x, camCrouchingHeight, camPosition.z);

             if (isCrouching)
             {
                 CamLerpToPosition(camPosition, crouchCamPosition);
             }
             else
             {
                 CamLerpToPosition(camPosition, standCamPosition);
             }
         }
     }

     private void CamLerpToPosition(Vector3 currentPosition, Vector3 targetPosition)
     {
         transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * speed);

         if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.01f)
         {
             isCrouchTransitionInProgress = false;
             Debug.Log("Reached " + (isCrouching ? "crouching" : "standing") + " height");
         }
     }
 }
 }*/
