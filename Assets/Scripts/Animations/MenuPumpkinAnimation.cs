using UnityEngine;

public class MenuPumpkinAnimation : MonoBehaviour
{
    /*
        The time is stopped in the menu, so the delta time cannot be used.
        This is alright, however, because the pumpkin doesn't need to have a fixed speed, 
        it can become slower or faster without any issue. It just needs not to be too fast.
        Also note that the pumpkin is slower in the build, which is just right.
    */

    private static float _zLimit;
    private static float _degrees = 0.2f;
    private bool _turningLeft = true;

    private void Awake()
    {
        _zLimit = transform.rotation.z - 0.001f;
    }

    private void LateUpdate()
    {
        if (_turningLeft)
        {
            transform.Rotate(new Vector3(0f, 0f, _degrees), Space.World);
            if (transform.rotation.z < _zLimit)
                _turningLeft = false;
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, -_degrees), Space.World);
            if (transform.rotation.z < _zLimit)
                _turningLeft = true;
        }
    }
}
