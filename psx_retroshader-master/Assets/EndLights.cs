using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLights : MonoBehaviour
{
    public GameObject[] myEm;
    ParticleSystem[] myPS;
    int myLocation;
    int maxLocation;
    public EndChallenge myEC;

    private void Start() {
        myLocation = 0;
        maxLocation = myEm.Length;
        myPS = new ParticleSystem[myEm.Length];
        for(int i = 0; i < myEm.Length; i++) {
            myPS[i] = myEm[i].GetComponent<ParticleSystem>();
        }
    }

    public void OnUse() {
        if (myLocation < maxLocation && _StaticGameManager.PlayerStats.keepLighter) {
            myEm[myLocation].SetActive(true);
            myLocation++;
            myEC.ScaleDown();
        }
    }
}
