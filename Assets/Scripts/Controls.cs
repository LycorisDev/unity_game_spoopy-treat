using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Controls : MonoBehaviour
{
    private float horizontal_input;
    private float forward_input;
    private Rigidbody player_rb;
    private Character character_script;
    private Behaviour menu_camera;
    private Behaviour ui_camera;
    private TextMeshProUGUI[] arr_menu_tmp;
    private int index_menu_option;
    private int min_index_menu_option;
    private bool is_first_game;
    private static bool user_asked_for_restart = false;

    void Start()
    {
        GameObject[] arr_menu_go;
        int i;

        player_rb = GetComponent<Rigidbody>();
        character_script = GetComponent<Character>();
        menu_camera = (Behaviour)GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
        ui_camera = (Behaviour)GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        // Set arr_menu_tmp
        arr_menu_go = GameObject.FindGameObjectsWithTag("MainMenuOption");
        arr_menu_go = arr_menu_go.OrderBy(e => e.name).ToArray();
        arr_menu_tmp = new TextMeshProUGUI[arr_menu_go.Length];
        for (i = 0; i < arr_menu_go.Length; ++i)
            arr_menu_tmp[i] = arr_menu_go[i].GetComponent<TextMeshProUGUI>();

        // When the soft starts, there is no ongoing game, so disable the first option ("Resume Current Game")
        DisableFirstMenuOption();
        index_menu_option = min_index_menu_option;
        is_first_game = true;

        // If user had started a game and then selects "New Game" again, the new game needs to start immediately
        if (user_asked_for_restart)
        {
            ResumeGame();
            // A game starting also implies that "Resume Current Game" needs to be enabled
            EnableFirstMenuOption();
        }
        else
            OpenMenu();
    }

    void Update()
    {
        // Open menu if in game, or close the soft if already in menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                OpenMenu();
            else
                Application.Quit();
        }

        if (Time.timeScale == 0)
            MenuControls();
        else
            GameControls();
    }

    private void OpenMenu()
    {
        // Pause the game
        Time.timeScale = 0;
        // Activate the menu camera
        menu_camera.enabled = true;
        // Deactivate the UI camera so it doesn't show in the menu
        ui_camera.enabled = false;
        index_menu_option = min_index_menu_option;
    }

    private void ResumeGame()
    {
        is_first_game = false;
        // Resume the game
        Time.timeScale = 1;
        // Deactivate the menu camera
        menu_camera.enabled = false;
        // Reactivate the UI camera for the game
        ui_camera.enabled = true;
        // Reset the menu option selector
        index_menu_option = min_index_menu_option;
    }

    private void DisableFirstMenuOption()
    {
        arr_menu_tmp[0].enabled = false;
        min_index_menu_option = 1;
    }

    private void EnableFirstMenuOption()
    {
        arr_menu_tmp[0].enabled = true;
        min_index_menu_option = 0;
    }

    private void MenuControls()
    {
        // Set all options to white
        foreach (TextMeshProUGUI tmp in arr_menu_tmp)
            tmp.color = new Color(1f, 1f, 1f, 1f);

        // Set the selected option to red
        arr_menu_tmp[index_menu_option].color = new Color(0.49f, 0.03f, 0.14f, 1f);

        // Controls to select an option
        if (Input.GetKeyDown(KeyCode.UpArrow))
            index_menu_option = index_menu_option > min_index_menu_option ? index_menu_option - 1 : arr_menu_tmp.Length - 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            index_menu_option = index_menu_option < arr_menu_tmp.Length - 1 ? index_menu_option + 1 : min_index_menu_option;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (index_menu_option)
            {
                // Resume Current Game
                case 0:
                    ResumeGame();
                    break;
                // New Game
                case 1:
                    if (!is_first_game)
                    {
                        user_asked_for_restart = true;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    ResumeGame();
                    EnableFirstMenuOption();
                    break;
                // Options
                case 2:
                    break;
                // Licenses
                case 3:
                    break;
                // Quit
                case 4:
                    Application.Quit();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("InvisibleWall"))
            character_script.is_on_ground = true;
    }
}
