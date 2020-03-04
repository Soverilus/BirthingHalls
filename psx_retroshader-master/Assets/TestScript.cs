using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class TestScript : MonoBehaviour
{
    private void Start() {
        Debug.Log("The Following Should ALL Be 0.75");
        Debug.Log(Regex.Match("travel PUZ 0.75 TRA 0.634", "[\\+\\-]?\\d+\\.?\\d+").Value);
        Debug.Log("This is a Float " + float.Parse(Regex.Match("travel 0.75 TRA 0.634", "[\\+\\-]?\\d+\\.?\\d+").Value));
    }
}
