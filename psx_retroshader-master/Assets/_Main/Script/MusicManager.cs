using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    public AudioSource intro;
    public GameObject[] mainObj;
    public GameObject[] stalkedObj;

    AudioHandler[] main;
    AudioHandler[] stalked;

    private void Start() {
        main = GetAudioHandlers(mainObj);
        stalked = GetAudioHandlers(stalkedObj);
        StartCoroutine(LateStart());
    }

    //waits once until intro is done to play the first track.
    //"IntroFade" instantly sets it's volume to 1 instead of waiting, and plays all tracks.

    IEnumerator LateStart() {
        yield return new WaitForSecondsRealtime(1f);
        yield return new WaitUntil(() => gameObject.activeSelf == true);
        intro.Play();
        StartCoroutine(WaitForIntroFinish());
    }
    IEnumerator WaitForIntroFinish() {
        yield return new WaitUntil(() => intro.isPlaying == false);
        main[0].IntroFade();

        for (int i = 0; i < main.Length; i++) {
            main[i].StartPlaying();
        }
        for (int i = 0; i < stalked.Length; i++) {
            stalked[i].StartPlaying();
        }
        Destroy(intro.gameObject);
        StartCoroutine(MusicManagerUpdate());
    }
    //gets all the audiohandlers from their object files.
    //I want the object files in case I have to do anything with them later.
    AudioHandler[] GetAudioHandlers(GameObject[] originals) {
        AudioHandler[] myAH = new AudioHandler[originals.Length];
        for (int i = 0; i < originals.Length; i++) {
            myAH[i] = originals[i].GetComponent<AudioHandler>();
        }
        return myAH;
    }

    //fades out all tracks except for the exemption track.
    void FadeOutAll(AudioHandler exemption) {
        for (int i = 0; i < main.Length; i++) {
            if (main[i] != exemption)
                main[i].FadeOut();
        }
        for (int i = 0; i < stalked.Length; i++) {
            if (stalked[i] != exemption)
                stalked[i].FadeOut();
        }
    }

    void FadeIn(AudioHandler target) {
        target.FadeIn();
    }

    IEnumerator MusicManagerUpdate() {
        if (SceneManager.GetActiveScene().name == "end_Room") {
            FadeOutAll(null);
        }
        else {
            CheckForChanges();
            yield return new WaitForSecondsRealtime(1f);
            StartCoroutine(MusicManagerUpdate());
        }
    }

    void CheckForChanges() {
        if (_StaticGameManager.EventParsing.ContainsEvent("StalkingEye")) {
            AudioHandler myAH = stalked[MyHandlerLevel(stalked)];
            FadeOutAll(myAH);
            FadeIn(myAH);
        }
        else {
            AudioHandler myAH = main[MyHandlerLevel(main)];
            FadeOutAll(myAH);
            FadeIn(myAH);
        }
    }

    int MyHandlerLevel(AudioHandler[] myAH) {
        int r = 0;
        int s = Mathf.RoundToInt(_StaticGameManager.Doors.DoorsOpened);
        int m = _StaticGameManager.Doors.doorsUntilEnd;
        if (s < m) {
            float percent = s / m;
            int length = myAH.Length;
            r = Mathf.CeilToInt(length * percent);
        }
        return r;
    }
}
