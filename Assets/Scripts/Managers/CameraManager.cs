using UnityEngine;

public class CameraManager: MonoBehaviour
{
    // The camera follows the target in 3rd person

    private static Transform target;
    private static Vector3 back;
    private static Vector3 rotation;
    private static float max_distance;
    private static float distance;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        max_distance = 4f;
        distance = max_distance;
    }

    void Update()
    {
        // The camera goes forward or backward with the target
        back = -target.forward * distance;
        back.y = distance == max_distance ? distance / 2 : distance * 2;
        back.y += 0.5f;
        transform.position = target.position + back;

        // The camera turns to the left or the right with the target
        transform.LookAt(target);

        // Fix the camera x rotation axis so the target is in the center of the view and not towards the top
        rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
