using UnityEngine;

public class CameraManager: MonoBehaviour
{
    private Vector3 _maxLocalPos;
    private Vector3 _minLocalPos;
    private bool _isPovThirdPerson = true;

    private void Awake()
    {
        // Pos: (9.8f, 5f, 0f)
        _maxLocalPos = transform.localPosition;
        _minLocalPos = new Vector3(4f, 5.5f, 0f);
    }

    public void SwitchCameraMode()
    {
        _isPovThirdPerson = !_isPovThirdPerson;
        transform.localPosition = _isPovThirdPerson ? _maxLocalPos : _minLocalPos;
    }
}
