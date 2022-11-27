using System.Linq;
using UnityEngine;

public class CameraManager: MonoBehaviour
{
    private static bool is_pov_3rd_person;
    private static Transform target;
    private static Vector3 back;
    private static float min_distance, max_distance, curr_distance, slider_distance;
    private static float min_height, max_height, curr_height, slider_height;
    private static Collider[] curr_colliders;

    void Start()
    {
        is_pov_3rd_person = true;

        // The camera is the target's child as to inherit its position
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(target);

        min_distance = -0.2f;
        max_distance = 4f;
        curr_distance = max_distance;
        slider_distance = (max_distance - min_distance) / 5;

        min_height = 1.8f;
        max_height = 2.5f;
        curr_height = max_height;
        slider_height = (max_height - min_height) / 5;

        curr_colliders = new Collider[10];
    }

    void Update()
    {
        int i;
        bool any_collider_got_nulled = false;

        back = -target.forward * curr_distance;
        back.y = curr_height;
        transform.position = target.position + back;

        // Reset the values once all the collided with objects are far enough
        if (is_pov_3rd_person && curr_distance != max_distance)
        {
            if (curr_colliders[0] == null)
            {
                curr_distance = max_distance;
                curr_height = max_height;
            }
            else
            {
                for (i = 0; i < curr_colliders.Length; ++i)
                {
                    if (curr_colliders[i] == null)
                        break;
                    else if (Vector3.Distance(curr_colliders[i].transform.position, transform.position) > 13f)
                    {
                        curr_colliders[i] = null;
                        any_collider_got_nulled = true;
                    }
                }

                if (any_collider_got_nulled)
                    curr_colliders = curr_colliders.OrderBy(e => e != null).ToArray();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (is_pov_3rd_person && other.CompareTag("CameraCollide") && curr_distance > 0f)
        {
            // The minimum values put the camera in 1st person POV
            curr_distance -= slider_distance;
            curr_height -= slider_height;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int i;
        if (is_pov_3rd_person && other.CompareTag("CameraCollide"))
        {
            // Add new colliding object to array
            for (i = 0; i < curr_colliders.Length; ++i)
            {
                if (curr_colliders[i] != null)
                {
                    if (curr_colliders[i].name == other.name)
                        break;
                }
                else
                    curr_colliders[i] = other;
            }
        }
    }

    public static void SwitchCameraMode()
    {
        is_pov_3rd_person = !is_pov_3rd_person;

        if (!is_pov_3rd_person)
        {
            curr_distance = min_distance;
            curr_height = min_height;
        }
    }
}
