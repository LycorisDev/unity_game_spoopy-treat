using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCauldron : MonoBehaviour
{
    public GameObject empty_cauldron_prefab;
    public GameObject candy_prefab;
    private Vector3 candy_position = new Vector3(0, 0, 0);

    void Update()
    {
        // If full caudron has fallen over
        if (gameObject.transform.rotation.x < -0.3 || gameObject.transform.rotation.x > 0.3)
        { 
            // Drop candy in front of it (and a bit above ground so it doesn't clip)
            candy_position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
            candy_position += Vector3.forward * 3;
            Instantiate(candy_prefab, candy_position, gameObject.transform.rotation);

            // Replace cauldron with empty version
            Instantiate(empty_cauldron_prefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
