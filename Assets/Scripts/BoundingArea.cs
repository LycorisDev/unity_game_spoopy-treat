using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    private Vector3 SpawnPoint = new Vector3(0, 3, 0);

    // If the player hits the bound, put them back to the spawn point
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.gameObject.transform.position = SpawnPoint;
    }
}
