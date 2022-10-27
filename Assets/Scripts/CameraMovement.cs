using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject target;
    private Vector3 back;
    private float height = 0.5f;
    private float distance = 2.4f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // The camera follows the player in 3rd person
        back = -target.transform.forward;
        back.y = height;
        gameObject.transform.position = target.transform.position + back * 2f * distance;
        gameObject.transform.forward = target.transform.position - gameObject.transform.position;
    }
}
