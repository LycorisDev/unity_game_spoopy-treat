using UnityEngine;

public class CameraManager: MonoBehaviour
{
    private static Transform target;
    private static Vector3 back;

    void Start()
    {
        // The camera is the target's child as to inherits its position
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(target);
    }

    void Update()
    {
        // 3rd person POV (distance of 4f and height of 2.5f)
        back = -target.forward * 4f;
        back.y = 2.5f;
        transform.position = target.position + back;
    }
}
