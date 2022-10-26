using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // The camera follows the player in 3rd person
        gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z - 5f);
    }
}
