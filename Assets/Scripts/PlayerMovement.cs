using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal_input;
    private float forward_input;
    private Rigidbody player_rb;
    private Character character_script;

    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        character_script = GetComponent<Character>();
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
        transform.Rotate(Vector3.up * horizontal_input * Time.deltaTime * character_script.rotate_speed);

        // Move the player on the z axis
        transform.Translate(Vector3.forward * forward_input * Time.deltaTime * character_script.directional_speed);

        // Player jumps
        if (Input.GetKeyDown(KeyCode.Space) && character_script.is_on_ground)
        {
            player_rb.AddForce(Vector3.up * character_script.jump_force, ForceMode.Impulse);
            character_script.is_on_ground = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("InvisibleWall"))
            character_script.is_on_ground = true;
    }
}
