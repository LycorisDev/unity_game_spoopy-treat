using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    public Vector3 SpawnPoint;

    // If the player hits the bound, put them back to the spawn point
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.gameObject.transform.position = SpawnPoint;
    }
}
