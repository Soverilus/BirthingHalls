using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour {

    public AudioClip[] myClips { get; private set; }
    AudioSource myMusicPlayer;
    void Start() {
        myMusicPlayer = GetComponent<AudioSource>();
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
        //do a nice fade effect or something.
        //for now just instantly switch em
        myMusicPlayer.Stop();
        myMusicPlayer.clip = clip;
        myMusicPlayer.Play();
    }
}
