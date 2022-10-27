using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static private float directional_speed = 5f;
    static private float rotate_speed = directional_speed * directional_speed * directional_speed;
    static private float jump_force = 6f;
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
        transform.Rotate(Vector3.up * Time.deltaTime * rotate_speed * horizontal_input);

        // Move the player on the z axis
        transform.Translate(Vector3.forward * Time.deltaTime * directional_speed * forward_input);

        // Player jumps
        if (Input.GetKeyDown(KeyCode.Space) && is_on_ground)
        {
            player_rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            is_on_ground = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_on_ground = true;

            // If the player is upside down
            //if (Vector3.Dot(transform.up, Vector3.down) > 0)
            //    gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, 1, gameObject.transform.rotation.z));
        }
    }
}
