using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private static AudioManager audio_manager;
    private Rigidbody player_rb;
    private Character character_script;
    private float horizontal_input;
    private float forward_input;

    void Start()
    {
        audio_manager = FindObjectOfType<AudioManager>();
        player_rb = GetComponent<Rigidbody>();
        character_script = GetComponent<Character>();
    }

    void Update()
    {
        // Open menu if in game, or close the soft if already in menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                audio_manager.Play("MenuBack");
                MenuManager.OpenMenu();
            }
            else
                MenuManager.Quit();
        }

        if (Time.timeScale == 0)
            MenuControls();
        else
            GameControls();
        
        // Switch between fullscreen and windowed mode
        if (Input.GetKeyDown(KeyCode.F4))
            Screen.fullScreen = !Screen.fullScreen;
    }

    private static void MenuControls()
    {
        MenuManager.SetGraphicsForSelectedOption();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MenuManager.SelectUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            MenuManager.SelectDown();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (MenuManager.index_option)
            {
                case 0:
                    MenuManager.ResumeCurrentGame();
                    break;
                case 1:
                    MenuManager.NewGame();
                    break;
                case 2:
                    MenuManager.Options();
                    break;
                case 3:
                    MenuManager.Licenses();
                    break;
                case 4:
                    MenuManager.Quit();
                    break;
            }
        }
    }

    private void GameControls()
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
