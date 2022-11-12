using UnityEngine;

public class CandyIdleAnimation : MonoBehaviour
{
    private static float degrees_per_sec;
    private static float amplitude;
    private static float frequency;
    Vector3 position_offset;
    Vector3 temp_position;

    void Start()
    {
        degrees_per_sec = 30f;
        amplitude = 0.5f;
        frequency = 0.8f;
        position_offset = transform.position;
        position_offset.y += 1f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degrees_per_sec, 0f), Space.World);
        temp_position = position_offset;
        temp_position.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = temp_position;
    }
}
