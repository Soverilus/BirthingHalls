using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TempMusicManager : MonoBehaviour
{
    AudioSource[] MP;
    //public AudioClip intro;
    //public AudioClip loop;

    AudioListener myPlayer;
    bool findplayer;

    private void Start() {
        MP = GetComponents<AudioSource>();
        for (int i = 0; i < MP.Length; i++) {

        }
        MP[0].Play();
    }

    private void Update() {
        if (!myPlayer && findplayer) {
            myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioListener>();
            //myPlayer.enabled = false;
            findplayer = false;
        }
        if (!MP[0].isPlaying && !MP[1].isPlaying) {
            MP[1].Play();
        }
        if (SceneManager.GetActiveScene().name == "end_Room") {
            MP[0].Stop();
            MP[1].volume -= Time.deltaTime * 0.1f;
            if (MP[1].volume <= 0.001f) {
                if(myPlayer) myPlayer.enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
