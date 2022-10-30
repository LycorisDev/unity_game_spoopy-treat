using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject target;
    private Vector3 back;
    private Vector3 rotation;
    private float height;
    private float distance;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        height = 0.6f;
        distance = 4f;
    }

    void Update()
    {
        // The camera follows the target in 3rd person
        back = -target.transform.forward;
        back.y = height;
        transform.position = target.transform.position + back * distance;

        // The camera turns to the left or the right with the target
        transform.forward = target.transform.position - transform.position;
        
        // Fix the camera x rotation axis so the target is horizontally in the center of the view and not towards the top
        rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
