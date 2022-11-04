using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCauldron : MonoBehaviour
{
    public GameObject empty_cauldron_prefab;
    public GameObject candy_prefab;
    private Vector3 candy_position;

    void Update()
    {
        // If full caudron has fallen over
        if (transform.rotation.x < -0.3 || transform.rotation.x > 0.3 || transform.rotation.z < -0.3 || transform.rotation.z > 0.3)
        { 
            // Drop candy in front of it (and a bit above ground so it doesn't clip)
            candy_position = transform.position + Vector3.forward * 3;
            Instantiate(candy_prefab, candy_position, transform.rotation);

            // Replace cauldron with empty version
            Instantiate(empty_cauldron_prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
