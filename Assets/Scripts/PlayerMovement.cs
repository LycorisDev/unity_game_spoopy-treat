using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static private float directional_speed = 6f;
    private float rotate_speed = directional_speed * directional_speed * directional_speed;
    private float jump_force = 5f;
    private bool is_on_ground = false;
    private float horizontal_input;
    private float forward_input;
    private Rigidbody player_rb;

    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /*
            DIRECTION INPUT
            - Arrow keys
            - "Use physical keys" is enabled, so it's not necessarily WASD but whatever equivalent
            - Console controller
        */

        // Get player directional input
        horizontal_input = Input.GetAxis("Horizontal");
        forward_input = Input.GetAxis("Vertical");

        // Rotate the player to the left or the right
        transform.Rotate(rotate_speed * player_rb.mass / 4 * Vector3.up * horizontal_input * Time.deltaTime);

        // Move the player on the z axis
        transform.Translate(directional_speed * player_rb.mass / 4 * Vector3.forward * forward_input * Time.deltaTime);

        // Player jumps
        if (Input.GetKeyDown(KeyCode.Space) && is_on_ground)
        {
            player_rb.AddForce(jump_force * player_rb.mass * (player_rb.mass / 4) * Vector3.up, ForceMode.Impulse);
            is_on_ground = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("InvisibleWall"))
            is_on_ground = true;
    }
}
