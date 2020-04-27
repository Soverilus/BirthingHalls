using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicActive : MonoBehaviour
{
    public GameObject musicFolder;
    void Start()
    {
        musicFolder.SetActive(true);
    }
}
