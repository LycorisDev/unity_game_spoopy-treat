using UnityEngine;

public class MenuPumpkinAnimation : MonoBehaviour
{
    /*
        The time is stopped in the menu, so the delta time cannot be used.
        This is alright, however, because the pumpkin doesn't need to have a fixed speed, 
        it can become slower or faster without any issue. It just needs not to be too fast.
        Also note that the pumpkin is slower in the build, which is just right.
    */

    private static float degrees;
    private static float zLimit;
    private static bool turningLeft;

    void Start()
    {
        degrees = 0.2f;
        zLimit = transform.rotation.z - 0.001f;
        turningLeft = true;
    }

    void Update()
    {
        if (turningLeft)
        {
            transform.Rotate(new Vector3(0f, 0f, degrees), Space.World);
            if (transform.rotation.z < zLimit)
                turningLeft = false;
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, -degrees), Space.World);
            if (transform.rotation.z < zLimit)
                turningLeft = true;
        }
    }
}
