using UnityEngine;

public class UICandyAnimation : MonoBehaviour
{
    private static float _degreesPerSec = 30f;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * _degreesPerSec, 0f), Space.World);
    }
}