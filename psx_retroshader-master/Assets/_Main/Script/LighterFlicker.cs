using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterFlicker : MonoBehaviour {
    bool lightAttempt = false;
    bool lightEnabled = false;
    int attemptNo;
    public int attemptMax;
    Light myLight;
    AudioSource myAudio;
    public AudioClip[] myClips;
    float baseIntensity = 5f;
    float myIntensity = 0f;
    float timer;
    float maxTimer = 0.75f;

    private void Start() {
        myAudio = GetComponent<AudioSource>();
        myLight = GetComponent<Light>();
        myLight.intensity = 0f;
    }

    void Update() {
            Cooldown();
            if (timer >= maxTimer) {
                LightAction();
            }
            LightIntensity();
    }

    void Cooldown() {
        if (timer < maxTimer) {
            timer += Time.unscaledDeltaTime;
        }
    }

    void LightIntensity() {
        float targetIntensity = myIntensity - Random.Range(0f, baseIntensity * 0.75f);
        myLight.intensity = Mathf.Clamp(Mathf.Lerp(myLight.intensity, targetIntensity, 0.25f), 0, baseIntensity);
    }

    void LightAction() {
        if (Input.GetAxis("Lighter") <= 0 && lightAttempt) {
            lightAttempt = false;
        }
        if (Input.GetAxis("Lighter") > 0 && !lightAttempt) {
            lightAttempt = true;
            timer = 0f;
            if (lightEnabled) {
                myIntensity = 0f;
                attemptNo = 0;
                lightEnabled = false;
                return;
            }
            else {
                PlaySound();
            }
            int i = Random.Range(0, attemptMax + 1) + attemptNo;
            attemptNo++;
            if (i >= attemptMax && !lightEnabled) {
                StartCoroutine(ActivateLight());
                attemptNo = 0;
            }
        }
    }

    IEnumerator ActivateLight() {
        yield return new WaitForSecondsRealtime(0.5f);
        myIntensity = baseIntensity;
        lightEnabled = true;
    }

    void PlaySound() {
        myAudio.clip = myClips[Random.Range(0, 3)];
        myAudio.Play();
    }
}
