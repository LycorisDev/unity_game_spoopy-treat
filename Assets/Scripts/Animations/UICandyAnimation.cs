using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICandyAnimation : MonoBehaviour
{
    private static float degrees_per_sec;

    void Start()
    {
        degrees_per_sec = 30f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degrees_per_sec, 0f), Space.World);
    }
}