using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    public Vector2 ScaleRange;
    void Start() {
        transform.localScale = new Vector3(
            transform.localScale.x * Random.Range(ScaleRange.x, ScaleRange.y),
            transform.localScale.y * Random.Range(ScaleRange.x, ScaleRange.y),
            transform.localScale.z * Random.Range(ScaleRange.x, ScaleRange.y));
    }
}
