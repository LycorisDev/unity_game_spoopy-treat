using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    private static Vector3 spawnPoint;

    void Awake()
    {
        // Take the player's first position
        spawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        // And add +2 to the y axis to make sure the spawn point is above ground
        spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y + 2f, spawnPoint.z);
        Debug.Log("Fix script: BoundingArea - The spawn point is correct, but the object's position isn't updated");
    }

    // If an object hits the bound, put it back to the spawn point
    private static void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.position = spawnPoint;
    }
}
