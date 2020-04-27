using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    AudioSource mySource;

    bool fadingIn;
    bool fadingOut;
    void Start() {
        fadingIn = false;
        fadingOut = false;
        mySource = GetComponent<AudioSource>();
        mySource.volume = 0f;
    }

    public void StartPlaying() {
        if (!mySource.isPlaying) mySource.Play();
    }

    public void IntroFade() {
        mySource.volume = 1f;
    }

    public void FadeIn() {
        if (!fadingIn) {
            fadingIn = true;
            StartCoroutine(FadeInEnum());
        }
    }

    IEnumerator FadeInEnum() {
        StopCoroutine(FadeOutEnum());
        mySource.volume = Mathf.Clamp(mySource.volume + 0.001f, 0f, 1f);
        yield return new WaitForEndOfFrame();
        if (mySource.volume >= 1f) {
            
        }
        else {
            StartCoroutine(FadeInEnum());
        }
    }

    public void FadeOut() {
        if (!fadingOut) {
            fadingOut = true;
            StartCoroutine(FadeOutEnum());
        }
    }

    IEnumerator FadeOutEnum() {
        StopCoroutine(FadeInEnum());
        mySource.volume = Mathf.Clamp(mySource.volume - 0.001f, 0f, 1f);
        yield return new WaitForEndOfFrame();
        if (mySource.volume <= 0f) {
            //disable the ability to do this again.
        }
        else {
            StartCoroutine(FadeOutEnum());
        }
    }
}
