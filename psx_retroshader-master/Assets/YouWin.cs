using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    public GameObject endEyeBall;
    public bool endGamePossible = false;
    bool endGameActive = false;
    public GameObject[] toDeleteOnWin;
    public GameObject Eyeball;
    public ParticleSystem[] stopOnWin;
    CrashBall myCB;
    private void Update() {
        if (endGamePossible) {
            bool b = GetComponent<Renderer>().isVisible;
            if (b && endGameActive) {

                EndGame();
            }
            if (!b) {
                //make Eyeball visible and cake "invisible"
                StartCoroutine(myWinThing());
            }
        }
    }

    IEnumerator myWinThing() {
        yield return new WaitForSecondsRealtime(1f);
            endGameActive = true;
        for (int i = 0; i < toDeleteOnWin.Length; i++) {
            Destroy(toDeleteOnWin[i]);
        }
        for (int i = 0; i < stopOnWin.Length; i++) {
            stopOnWin[i].Stop();
        }
        Eyeball.SetActive(true);
    }


        void EndGame() {
            GameObject myEye = Instantiate(endEyeBall, transform.position, Quaternion.identity);
            myCB = myEye.GetComponent<CrashBall>();
            myCB.StartCoroutine(myCB.CrashToDesktop("Congratulations! You found the cake! We are Sated.  You opened " + _StaticGameManager.Doors.DoorsOpened, "You Win!", 1f, WindowType.Info));
            Destroy(gameObject);
        }
    }
