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
    private static float z_limit;
    private static bool turning_left;

    void Start()
    {
        degrees = 0.2f;
        z_limit = transform.rotation.z - 0.001f;
        turning_left = true;
    }

    void Update()
    {
        if (turning_left)
        {
            transform.Rotate(new Vector3(0f, 0f, degrees), Space.World);
            if (transform.rotation.z < z_limit)
                turning_left = false;
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, -degrees), Space.World);
            if (transform.rotation.z < z_limit)
                turning_left = true;
        }
    }
}
