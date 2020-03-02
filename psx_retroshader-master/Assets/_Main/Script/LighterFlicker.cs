using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterFlicker : MonoBehaviour {
    bool lightAttempt = false;
    bool lightEnabled = false;

    public GameObject lighterObject;
    int attemptNo;
    public int attemptMax;
    Light myLight;
    AudioSource myAudio;
    public AudioClip[] myClips;
    public float baseIntensity = 5f;
    [Range(0, 1)] public float percentFlicker = 0.75f;
    [Range(0, 1)] public float colorFlicker = 0.75f;
    [Range(0, 1)] public float percentFlickerSpeed = 0.25f;
    [Range(0, 1)] public float colorFlickerSpeed = 0.1f;
    float myIntensity = 0f;
    float timer;
    float maxTimer = 0.75f;

    Color myColor;
    public Color targetColor;

    private void Start() {
        lighterObject.SetActive(false);
        myAudio = GetComponent<AudioSource>();
        myLight = GetComponent<Light>();
        myLight.intensity = 0f;
        myColor = myLight.color;
        if (targetColor == null) {
            targetColor = myColor;
        }
        if (_StaticGameManager.PlayerStats.keepLighter) {
            ForceEnableLighter();
        }
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
        float targetIntensity = myIntensity - Random.Range(0, baseIntensity * percentFlicker);
        myLight.intensity = Mathf.Clamp(Mathf.Lerp(myLight.intensity, targetIntensity, percentFlickerSpeed), 0, baseIntensity);
        Color colorTarget = Color.Lerp(myColor, targetColor, Random.Range(0, colorFlicker));
        myLight.color = Color.Lerp(myLight.color, colorTarget, colorFlickerSpeed);
    }

    void LightAction() {
        if (Input.GetAxis("RightClick") <= 0 && lightAttempt) {
            lightAttempt = false;
        }
        if (Input.GetAxis("RightClick") > 0 && !lightAttempt) {
            lightAttempt = true;
            timer = 0f;
            if (lightEnabled) {
                myIntensity = 0f;
                attemptNo = 0;
                lightEnabled = false;
                lighterObject.SetActive(false);
                _StaticGameManager.PlayerStats.keepLighter = false;
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

    public void ForceEnableLighter() {
        myIntensity = baseIntensity;
        lightEnabled = true;
        lighterObject.SetActive(true);
    }
    public void ForceDisableLighter() {
        myIntensity = 0f;
        attemptNo = 0;
        lightEnabled = false;
        lighterObject.SetActive(false);
    }

    IEnumerator ActivateLight() {
        yield return new WaitForSecondsRealtime(0.5f);
        myIntensity = baseIntensity;
        lightEnabled = true;
        lighterObject.SetActive(true);
        _StaticGameManager.PlayerStats.keepLighter = true;
    }

    void PlaySound() {
        myAudio.clip = myClips[Random.Range(0, 3)];
        myAudio.Play();
    }
}
