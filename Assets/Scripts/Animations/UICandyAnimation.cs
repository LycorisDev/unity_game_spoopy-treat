using UnityEngine;

public class UICandyAnimation : MonoBehaviour
{
    private static float degreesPerSec;

    void Start()
    {
        degreesPerSec = 30f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSec, 0f), Space.World);
    }
}