using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    public GameObject endEyeBall;
    public bool endGamePossible = false;
    bool endGameActive = false;
    CrashBall myCB;
    private void Update() {
        if (endGamePossible) {
            bool b = GetComponent<Renderer>().isVisible;
            if (b && endGameActive) {

                EndGame();
            }
            if (!b) {
                //make Eyeball visible and cake "invisible"
                endGameActive = true;
            }
        }
    }

    void EndGame() {
        GameObject myEye = Instantiate(endEyeBall, transform.position, Quaternion.identity);
        myCB = myEye.GetComponent<CrashBall>();
        myCB.StartCoroutine(myCB.CrashToDesktop("Congratulations! You found the cake! We are Sated.  You opened " + _StaticGameManager.Doors.DoorsOpened, "You Win!", 1f, WindowType.Info));
        Destroy(gameObject);
    }
}
