using System.Linq;
using UnityEngine;

public class CameraManager: MonoBehaviour
{
    private static float _minDistance = -0.2f;
    private static float _maxDistance = 4f;
    private static float _sliderDistance = (_maxDistance - _minDistance) / 5f;
    private static float _minHeight = 1.8f;
    private static float _maxHeight = 2.5f;
    private static float _sliderHeight = (_maxHeight - _minHeight) / 5f;

    private bool _isPovThirdPerson = true;
    private float _currDistance = _maxDistance;
    private float _currHeight = _maxHeight;
    private Collider[] _currColliders = new Collider[10];
    private Transform _target;
    private Vector3 _back;

    private void Awake()
    {
        // The camera is the target's child as to inherit its position
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(_target);
    }

    private void LateUpdate()
    {
        int i;
        bool anyColliderGotNulled = false;

        _back = -_target.forward * _currDistance;
        _back.y = _currHeight;
        transform.position = _target.position + _back;

        // Reset the values once all the collided with objects are far enough
        if (_isPovThirdPerson && _currDistance != _maxDistance)
        {
            if (_currColliders[0] == null)
            {
                _currDistance = _maxDistance;
                _currHeight = _maxHeight;
            }
            else
            {
                for (i = 0; i < _currColliders.Length; ++i)
                {
                    if (_currColliders[i] == null)
                        break;
                    else if (Vector3.Distance(_currColliders[i].transform.position, transform.position) > 13f)
                    {
                        _currColliders[i] = null;
                        anyColliderGotNulled = true;
                    }
                }

                if (anyColliderGotNulled)
                    _currColliders = _currColliders.OrderBy(e => e != null).ToArray();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPovThirdPerson && other.CompareTag("CameraCollide") && _currDistance > 0f)
        {
            // The minimum values put the camera in 1st person POV
            _currDistance -= _sliderDistance;
            _currHeight -= _sliderHeight;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int i;
        if (_isPovThirdPerson && other.CompareTag("CameraCollide"))
        {
            // Add new colliding object to array
            for (i = 0; i < _currColliders.Length; ++i)
            {
                if (_currColliders[i] != null)
                {
                    if (_currColliders[i].name == other.name)
                        break;
                }
                else
                    _currColliders[i] = other;
            }
        }
    }

    public void SwitchCameraMode()
    {
        _isPovThirdPerson = !_isPovThirdPerson;

        if (!_isPovThirdPerson)
        {
            _currDistance = _minDistance;
            _currHeight = _minHeight;
        }
    }
}
