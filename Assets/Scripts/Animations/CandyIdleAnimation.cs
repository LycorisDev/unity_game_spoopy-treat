using UnityEngine;

public class CandyIdleAnimation : MonoBehaviour
{
    private static float _degreesPerSec = 30f;
    private static float _amplitude = 0.5f;
    private static float _frequency = 0.8f;
    private Vector3 _positionOffset;

    private void Awake()
    {
        _positionOffset = transform.position;
        _positionOffset.y += 1f;
    }

    private void FixedUpdate()
    {
        SetVerticalPosition();
        SetRotation();
    }

    private void SetVerticalPosition()
    {
        Vector3 pos = _positionOffset;
        pos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;
        transform.position = pos;
    }

    private void SetRotation()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * _degreesPerSec, 0f), Space.World);
    }
}
