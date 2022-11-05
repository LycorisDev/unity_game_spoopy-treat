using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private float horizontal_input;
    private float forward_input;
    private Rigidbody player_rb;
    private Character character_script;
    private Behaviour menu_camera;
    private Behaviour ui_camera;

    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        character_script = GetComponent<Character>();
        menu_camera = (Behaviour)GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
        ui_camera = (Behaviour)GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        OpenMenu();
    }

    void Update()
    {
        // Open/Close menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                OpenMenu();
            else
                ResumeGame();
        }

        if (Time.timeScale == 1)
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
    }

    private void OpenMenu()
    {
        // Pause the game
        Time.timeScale = 0;
        // Activate the menu camera
        menu_camera.enabled = true;
        // Deactivate the UI camera so it doesn't show in the menu
        ui_camera.enabled = false;
    }

    private void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1;
        // Deactivate the menu camera
        menu_camera.enabled = false;
        // Reactivate the UI camera for the game
        ui_camera.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("InvisibleWall"))
            character_script.is_on_ground = true;
    }
}
