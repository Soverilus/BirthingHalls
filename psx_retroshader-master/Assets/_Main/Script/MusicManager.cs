using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{
/*
    public AudioClip[] myClips;
    AudioSource[] myMusicPlayers;

    AudioSource MP1;
    AudioSource MP2;

    AudioClip mainClip;
    AudioClip myCurrent;

    bool hasSwitched = false;
    void Start() {
        myMusicPlayers = new AudioSource[GetComponents<AudioSource>().Length];
        myMusicPlayers = GetComponents<AudioSource>();
        MP1 = myMusicPlayers[0];
        MP2 = myMusicPlayers[1];
        MP1.volume = 0f;
        MP2.volume = 0f;
        mainClip = myClips[0];
        MP1.clip = mainClip;
        myCurrent = myClips[0];
        MP2.clip = myClips[1];
    }

    void Update() {
        //probably wierd stuff, unnecessary currently
    }

    public void ChangeMusicToString(string myClip) {
        for (int i = 0; i < myClips.Length; i++) {
            if (myClips[i].name == myClip) {
                ChangeMusicFinal(myClips[i]);
                return;
            }
        }
    }

    void ChangeMusicFinal(AudioClip clip) {
        //use a coroutine to repeat the following in a synthetic update loop
        //fade out the first clip
        //fade in second clip
        //once second clip is at 100, stop the first


        //do a nice fade effect or something.
        //for now just instantly switch em
        if (hasSwitched) {
            MP2.Stop();
            MP1.clip = clip;
            MP1.Play();
        }
        else {
            MP1.Stop();
            MP2.clip = clip;
            MP2.Play();
        }
        hasSwitched = !hasSwitched;
    }*/
}
