using UnityEngine;

public class CandyIdleAnimation : MonoBehaviour
{
    private static float degreesPerSec;
    private static float amplitude;
    private static float frequency;
    Vector3 positionOffset;
    Vector3 tempPosition;

    void Start()
    {
        degreesPerSec = 30f;
        amplitude = 0.5f;
        frequency = 0.8f;
        positionOffset = transform.position;
        positionOffset.y += 1f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSec, 0f), Space.World);
        tempPosition = positionOffset;
        tempPosition.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPosition;
    }
}
