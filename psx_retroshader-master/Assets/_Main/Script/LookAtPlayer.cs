﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = Camera.main.gameObject;
    }

    void Update()
    {
        transform.LookAt(player.transform, transform.up);
    }
}
