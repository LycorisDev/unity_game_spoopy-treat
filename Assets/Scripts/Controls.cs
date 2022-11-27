using UnityEngine;

public class Controls : MonoBehaviour
{
    private static string current_sub_menu;
    private static Rigidbody player_rb;
    private static Character player_script;
    private static KeyCode key_menu, key_help_mode, key_quick_save, key_pov_mode, key_screen_mode, 
        key_validate, key_up, key_down, key_left, key_right, key_side_left, key_side_right, key_jump;

    void Start()
    {
        current_sub_menu = "main";
        player_rb = GetComponent<Rigidbody>();
        player_script = GetComponent<Character>();

        // "Use Physical Keys" enabled (QWERTY)
        key_menu = KeyCode.Escape;
        key_help_mode = KeyCode.F1;
        key_quick_save = KeyCode.F2;
        key_pov_mode = KeyCode.F3;
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
        if (Input.GetKeyDown(key_menu))
        {
            // Open menu if in game
            if (Time.timeScale == 1)
                MenuManager.OpenMainMenu();
            // Close the soft if in main menu
            else if (current_sub_menu == "main")
                MenuManager.Quit();
            // Go back to main menu if in sub-menu
            else
                GoBackToMainMenu();
        }

        if (Time.timeScale == 0)
            HandleMenuInput();
        else
            HandleGameInput();

        // Switch between fullscreen and windowed mode
        if (Input.GetKeyDown(key_screen_mode))
            Screen.fullScreen = !Screen.fullScreen;
    }

    private static void HandleMenuInput()
    {
        if (current_sub_menu == "options")
            HandleOptionsMenuInput();
        else if (current_sub_menu == "licenses")
            HandleLicensesMenuInput();
        else
            HandleMainMenuInput();
    }

    private static void SelectMenuOption()
    {
        /*
            - Go up with UP and LEFT input
            - Go down with DOWN and RIGHT input
        
            UP/DOWN and LEFT/RIGHT are in different if-statements for optimisation reasons.
            Indeed, it is more likely that people go for the UP/DOWN input instead of the LEFT/RIGHT.
            This means that if the user didn't press an UP key, I don't want to have to check all three 
            LEFT keys before I realize that the user wanted to go down instead.
        */

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(key_up))
            MenuManager.SelectUp(current_sub_menu);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(key_down))
            MenuManager.SelectDown(current_sub_menu);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(key_left) || Input.GetKeyDown(key_side_left))
            MenuManager.SelectUp(current_sub_menu);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(key_right) || Input.GetKeyDown(key_side_right))
            MenuManager.SelectDown(current_sub_menu);
    }

    private static void SelectMenuOptionVerticalOnly()
    {
        /* Used for when the sub-menu requires the horizontal input for other specific options (e.g. volume sliders). */

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(key_up))
            MenuManager.SelectUp(current_sub_menu);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(key_down))
            MenuManager.SelectDown(current_sub_menu);
    }

    private static void GoBackToMainMenu()
    {
        MenuManager.CloseSubMenu(current_sub_menu);
        current_sub_menu = "main";
    }

    private static void HandleMainMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(current_sub_menu);
        SelectMenuOption();

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
                    current_sub_menu = "options";
                    MenuManager.OpenSubMenu(current_sub_menu);
                    break;
                case 3:
                    current_sub_menu = "licenses";
                    MenuManager.OpenSubMenu(current_sub_menu);
                    break;
                case 4:
                    MenuManager.Quit();
                    break;
            }
        }
    }

    private static void HandleOptionsMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(current_sub_menu);
        SelectMenuOptionVerticalOnly();

        if (MenuManager.index_option == 4)
        {
            if (Input.GetKeyDown(key_validate))
                GoBackToMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(key_left) || Input.GetKeyDown(key_side_left))
            MenuManager.UpdateVolume(MenuManager.index_option, -1);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(key_right) || Input.GetKeyDown(key_side_right))
            MenuManager.UpdateVolume(MenuManager.index_option, 1);
    }

    private static void HandleLicensesMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(current_sub_menu);
        SelectMenuOption();

        if (Input.GetKeyDown(key_validate))
        {
            switch (MenuManager.index_option)
            {
                case 0:
                    MenuManager.OpenLink("https://www.ghosthack.de");
                    break;
                case 1:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/props/exterior/halloween-pumpkins-50597");
                    break;
                case 2:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153");
                    break;
                case 3:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/environments/fantasy/mausoleum-128753");
                    break;
                case 4:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/props/poly-halloween-pack-236625");
                    break;
                case 5:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/environments/fantasy/halloween-cemetery-set-19125");
                    break;
                case 6:
                    GoBackToMainMenu();
                    break;
            }
        }
    }

    private void HandleGameInput()
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

        // Toggle/Untoggle help mode
        if (Input.GetKeyDown(key_help_mode))
        {
            // Tutorial/Advice and not just a display of the different keys
            Debug.Log("Help Key");
        }

        // Quick save
        if (Input.GetKeyDown(key_quick_save))
        {
            // Quick save only - Do not open the save sub-menu
            Debug.Log("Quick Save Key");
        }

        // Switch between 3rd (default) and 1st person POV
        if (Input.GetKeyDown(key_pov_mode))
            CameraManager.SwitchCameraMode();
    }
}
