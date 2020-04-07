using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIParse : MonoBehaviour
{
    public Slider fakeHealth;
    public Slider Health;
    public Slider Stamina;

    private void OnLevelWasLoaded(int Level) {
        fakeHealth.gameObject.SetActive(false);
        Health.gameObject.SetActive(false);
        Stamina.gameObject.SetActive(false);
    }
}
