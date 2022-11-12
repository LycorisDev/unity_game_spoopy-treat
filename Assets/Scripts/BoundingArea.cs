using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    private static Vector3 spawn_point;

    void Start()
    {
        spawn_point = new Vector3(0, 3, 0);
    }

    // If an object hits the bound, put it back to the spawn point
    private static void OnTriggerExit(Collider other)
    {
        other.transform.position = spawn_point;
    }
}
