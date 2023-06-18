using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    private Vector3 _spawnPoint;

    private void Awake()
    {
        // Take the player's first position
        _spawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        // And add +2 to the y axis to make sure the spawn point is above ground
        _spawnPoint += new Vector3(0f, 2f, 0f);
        Debug.Log("[TODO] Fix script: BoundingArea - The spawn point is correct, but the object's position isn't updated");
    }

    // If an object hits the bound, put it back to the spawn point
    private void OnTriggerExit(Collider other)
    {
        other.transform.position = _spawnPoint;
    }
}
