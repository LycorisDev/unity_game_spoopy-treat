using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyIdleAnimation : MonoBehaviour
{
    Vector3 position_offset;
    Vector3 temp_position;
    private float degrees_per_sec;
    private float amplitude;
    private float frequency;

    void Start()
    {
        position_offset = transform.position;
        position_offset.y += 1f;
        degrees_per_sec = 30f;
        amplitude = 0.5f;
        frequency = 0.8f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degrees_per_sec, 0f), Space.World);
        temp_position = position_offset;
        temp_position.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = temp_position;
    }
}
