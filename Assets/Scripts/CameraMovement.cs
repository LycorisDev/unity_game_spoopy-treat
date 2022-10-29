using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject target;
    private Vector3 back;
    private Vector3 rotation;
    private float height = 0.5f;
    private float distance = 4f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // The camera follows the target in 3rd person
        back = -target.transform.forward;
        back.y = height;
        gameObject.transform.position = target.transform.position + back * distance;

        // The camera turns to the left or the right with the target
        gameObject.transform.forward = target.transform.position - gameObject.transform.position;
        
        // Fix the camera x rotation axis so the target is horizontally in the center of the view and not towards the top
        rotation = gameObject.transform.rotation.eulerAngles;
        rotation.x = 0;
        gameObject.transform.rotation = Quaternion.Euler(rotation);
    }
}
