using UnityEngine;

public class Controls : MonoBehaviour
{
    private static Rigidbody playerRb;
    private static Character playerScript;
    private static string currentSubMenu;
    private static KeyCode keyMenu, keyHelpMode, keyQuickSave, keyPovMode, keyScreenMode, 
        keyValidate, keyUp, keyDown, keyLeft, keyRight, keySideLeft, keySideRight, keyJump;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerScript = GetComponent<Character>();
    }

    void Start()
    {
        currentSubMenu = "main";

        // "Use Physical Keys" enabled (QWERTY)
        keyMenu = KeyCode.Escape;
        keyHelpMode = KeyCode.F1;
        keyQuickSave = KeyCode.F2;
        keyPovMode = KeyCode.F3;
        keyScreenMode = KeyCode.F11;
        keyValidate = KeyCode.Return;
        keyUp = KeyCode.W;
        keyDown = KeyCode.S;
        keyLeft = KeyCode.A;
        keyRight = KeyCode.D;
        keySideLeft = KeyCode.Q;
        keySideRight = KeyCode.E;
        keyJump = KeyCode.Space;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyMenu))
        {
            // Open menu if in game
            if (Time.timeScale == 1)
                MenuManager.OpenMainMenu();
            // Close the soft if in main menu
            else if (currentSubMenu == "main")
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
        if (Input.GetKeyDown(keyScreenMode))
            Screen.fullScreen = !Screen.fullScreen;
    }

    private static void HandleMenuInput()
    {
        if (currentSubMenu == "options")
            HandleOptionsMenuInput();
        else if (currentSubMenu == "licenses")
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

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(keyUp))
            MenuManager.SelectUp(currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(keyDown))
            MenuManager.SelectDown(currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(keyLeft) || Input.GetKeyDown(keySideLeft))
            MenuManager.SelectUp(currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(keyRight) || Input.GetKeyDown(keySideRight))
            MenuManager.SelectDown(currentSubMenu);
    }

    private static void SelectMenuOptionVerticalOnly()
    {
        /* Used for when the sub-menu requires the horizontal input for other specific options (e.g. volume sliders). */

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(keyUp))
            MenuManager.SelectUp(currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(keyDown))
            MenuManager.SelectDown(currentSubMenu);
    }

    private static void GoBackToMainMenu()
    {
        MenuManager.CloseSubMenu(currentSubMenu);
        currentSubMenu = "main";
    }

    private static void HandleMainMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(keyValidate))
        {
            switch (MenuManager.indexOption)
            {
                case 0:
                    MenuManager.ResumeCurrentGame();
                    break;
                case 1:
                    MenuManager.NewGame();
                    break;
                case 2:
                    currentSubMenu = "options";
                    MenuManager.OpenSubMenu(currentSubMenu);
                    break;
                case 3:
                    currentSubMenu = "licenses";
                    MenuManager.OpenSubMenu(currentSubMenu);
                    break;
                case 4:
                    MenuManager.Quit();
                    break;
            }
        }
    }

    private static void HandleOptionsMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(currentSubMenu);
        SelectMenuOptionVerticalOnly();

        if (MenuManager.indexOption == 4)
        {
            if (Input.GetKeyDown(keyValidate))
                GoBackToMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(keyLeft) || Input.GetKeyDown(keySideLeft))
            MenuManager.UpdateVolume(MenuManager.indexOption, -1);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(keyRight) || Input.GetKeyDown(keySideRight))
            MenuManager.UpdateVolume(MenuManager.indexOption, 1);
    }

    private static void HandleLicensesMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(keyValidate))
        {
            switch (MenuManager.indexOption)
            {
                case 0:
                    MenuManager.OpenLink("https://opengameart.org/content/a-tricky-puzzle-loop");
                    break;
                case 1:
                    MenuManager.OpenLink("https://www.ghosthack.de");
                    break;
                case 2:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/props/exterior/halloween-pumpkins-50597");
                    break;
                case 3:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153");
                    break;
                case 4:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/environments/fantasy/mausoleum-128753");
                    break;
                case 5:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/props/poly-halloween-pack-236625");
                    break;
                case 6:
                    MenuManager.OpenLink("https://assetstore.unity.com/packages/3d/environments/fantasy/halloween-cemetery-set-19125");
                    break;
                case 7:
                    GoBackToMainMenu();
                    break;
            }
        }
    }

    private void HandleGameInput()
    {
        // Move the player forward or backward
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(keyUp))
            transform.Translate(Vector3.forward * Time.deltaTime * playerScript.directionalSpeed);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(keyDown))
            transform.Translate(Vector3.back * Time.deltaTime * playerScript.directionalSpeed);

        // Rotate the player to the left or the right
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(keyLeft))
            transform.Rotate(Vector3.down * Time.deltaTime * playerScript.rotateSpeed);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(keyRight))
            transform.Rotate(Vector3.up * Time.deltaTime * playerScript.rotateSpeed);

        // Move the player to the side
        if (Input.GetKey(keySideLeft))
            transform.Translate(Vector3.left * Time.deltaTime * playerScript.directionalSpeed);
        if (Input.GetKey(keySideRight))
            transform.Translate(Vector3.right * Time.deltaTime * playerScript.directionalSpeed);

        // Make the player jump
        if (Input.GetKeyDown(keyJump) && playerScript.isOnGround)
        {
            playerRb.AddForce(Vector3.up * playerScript.jumpForce, ForceMode.Impulse);
            playerScript.isOnGround = false;
        }

        // Toggle/Untoggle help mode
        if (Input.GetKeyDown(keyHelpMode))
        {
            // Tutorial/Advice and not just a display of the different keys
            Debug.Log("Help Key");
        }

        // Quick save
        if (Input.GetKeyDown(keyQuickSave))
        {
            // Quick save only - Do not open the save sub-menu
            Debug.Log("Quick Save Key");
        }

        // Switch between 3rd (default) and 1st person POV
        if (Input.GetKeyDown(keyPovMode))
            CameraManager.SwitchCameraMode();
    }
}
