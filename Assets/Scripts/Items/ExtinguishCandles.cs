using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishCandles : MonoBehaviour
{
    [SerializeField]
    private GameObject unlitSkullPrefab;

    void Update()
    {
        // If lit skull has fallen over
        if (transform.rotation.x < -0.5 || transform.rotation.x > 0.5 || transform.rotation.z < -0.5 || transform.rotation.z > 0.5)
        {
            // Replace with unlit version
            Instantiate(unlitSkullPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
