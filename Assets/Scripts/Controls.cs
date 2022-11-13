using UnityEngine;

public class Controls : MonoBehaviour
{
    private static Rigidbody player_rb;
    private static Character player_script;
    private static KeyCode key_menu, key_screen_mode, key_validate, key_up, key_down, key_left, key_right, key_side_left, key_side_right, key_jump;

    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        player_script = GetComponent<Character>();

        // "Use Physical Keys" enabled (QWERTY)
        key_menu = KeyCode.Escape;
        key_screen_mode = KeyCode.F4;
        key_validate = KeyCode.Return;
        key_up = KeyCode.W;
        key_down = KeyCode.S;
        key_left = KeyCode.A;
        key_right = KeyCode.D;
        key_side_left = KeyCode.Q;
        key_side_right = KeyCode.E;
        key_jump = KeyCode.Space;
    }

    void Update()
    {
        // Open menu if in game, or close the soft if already in menu
        if (Input.GetKeyDown(key_menu))
        {
            if (Time.timeScale == 1)
                MenuManager.OpenMenu();
            else
                MenuManager.Quit();
        }

        if (Time.timeScale == 0)
            MenuControls();
        else
            GameControls();
        
        // Switch between fullscreen and windowed mode
        if (Input.GetKeyDown(key_screen_mode))
            Screen.fullScreen = !Screen.fullScreen;
    }

    private static void MenuControls()
    {
        MenuManager.SetGraphicsForSelectedOption();

        /*
            - Go up with UP and LEFT input
            - Go down with DOWN and RIGHT input
        
            UP/DOWN and LEFT/RIGHT are in different if-statements for optimisation reasons.
            Indeed, it is more likely that people go for the UP/DOWN input instead of the LEFT/RIGHT.
            This means that if the user didn't press an UP key, I don't want to have to check all three 
            LEFT keys before I realize that the user wanted to go down instead.
        */

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(key_up))
            MenuManager.SelectUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(key_down))
            MenuManager.SelectDown();
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(key_left) || Input.GetKeyDown(key_side_left))
            MenuManager.SelectUp();
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(key_right) || Input.GetKeyDown(key_side_right))
            MenuManager.SelectDown();

        if (Input.GetKeyDown(key_validate))
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
        // Move the player forward or backward
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(key_up))
            transform.Translate(Vector3.forward * Time.deltaTime * player_script.directional_speed);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(key_down))
            transform.Translate(Vector3.back * Time.deltaTime * player_script.directional_speed);

        // Rotate the player to the left or the right
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(key_left))
            transform.Rotate(Vector3.down * Time.deltaTime * player_script.rotate_speed);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(key_right))
            transform.Rotate(Vector3.up * Time.deltaTime * player_script.rotate_speed);

        // Move the player to the side
        if (Input.GetKey(key_side_left))
            transform.Translate(Vector3.left * Time.deltaTime * player_script.directional_speed);
        if (Input.GetKey(key_side_right))
            transform.Translate(Vector3.right * Time.deltaTime * player_script.directional_speed);

        // Make the player jump
        if (Input.GetKeyDown(key_jump) && player_script.is_on_ground)
        {
            player_rb.AddForce(Vector3.up * player_script.jump_force, ForceMode.Impulse);
            player_script.is_on_ground = false;
        }
    }
}
