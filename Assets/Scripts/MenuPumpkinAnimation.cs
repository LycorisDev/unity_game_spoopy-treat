using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPumpkinAnimation : MonoBehaviour
{
    private float degrees;
    private float z_limit;
    private bool turning_left;

    void Start()
    {
        degrees = 0.2f;
        z_limit = 0.135f;
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
