using UnityEngine;

public class Controls : MonoBehaviour
{
    private Character _playerScript;
    private CameraManager _cameraScript;
    private string _currentSubMenu;
    private KeyCode _keyMenu, _keyHelpMode, _keyQuickSave, _keyPovMode, _keyScreenMode, 
        _keyValidate, _keyUp, _keyDown, _keyLeft, _keyRight, _keySideLeft, _keySideRight, _keyJump;

    private void Awake()
    {
        _playerScript = GetComponent<Character>();
        _cameraScript = Camera.main.GetComponent<CameraManager>();

        _currentSubMenu = "main";

        // "Use Physical Keys" enabled (QWERTY)
        _keyMenu = KeyCode.Escape;
        _keyHelpMode = KeyCode.F1;
        _keyQuickSave = KeyCode.F2;
        _keyPovMode = KeyCode.F3;
        _keyScreenMode = KeyCode.F11;
        _keyValidate = KeyCode.Return;
        _keyUp = KeyCode.W;
        _keyDown = KeyCode.S;
        _keyLeft = KeyCode.A;
        _keyRight = KeyCode.D;
        _keySideLeft = KeyCode.Q;
        _keySideRight = KeyCode.E;
        _keyJump = KeyCode.Space;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyMenu))
        {
            // Open menu if in game
            if (Time.timeScale == 1)
                MenuManager.OpenMainMenu();
            // Close the soft if in main menu
            else if (_currentSubMenu == "main")
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
        if (Input.GetKeyDown(_keyScreenMode))
            Screen.fullScreen = !Screen.fullScreen;
    }

    private void HandleMenuInput()
    {
        if (_currentSubMenu == "options")
            HandleOptionsMenuInput();
        else if (_currentSubMenu == "licenses")
            HandleLicensesMenuInput();
        else
            HandleMainMenuInput();
    }

    private void SelectMenuOption()
    {
        /*
            - Go up with UP and LEFT input
            - Go down with DOWN and RIGHT input
        
            UP/DOWN and LEFT/RIGHT are in different if-statements for optimisation reasons.
            Indeed, it is more likely that people go for the UP/DOWN input instead of the LEFT/RIGHT.
            This means that if the user didn't press an UP key, I don't want to have to check all three 
            LEFT keys before I realize that the user wanted to go down instead.
        */

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(_keyUp))
            MenuManager.SelectUp(_currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(_keyDown))
            MenuManager.SelectDown(_currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(_keyLeft) || Input.GetKeyDown(_keySideLeft))
            MenuManager.SelectUp(_currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(_keyRight) || Input.GetKeyDown(_keySideRight))
            MenuManager.SelectDown(_currentSubMenu);
    }

    private void SelectMenuOptionVerticalOnly()
    {
        /* Used for when the sub-menu requires the horizontal input for other specific options (e.g. volume sliders). */

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(_keyUp))
            MenuManager.SelectUp(_currentSubMenu);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(_keyDown))
            MenuManager.SelectDown(_currentSubMenu);
    }

    private void GoBackToMainMenu()
    {
        MenuManager.CloseSubMenu(_currentSubMenu);
        _currentSubMenu = "main";
    }

    private void HandleMainMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(_keyValidate))
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
                    _currentSubMenu = "options";
                    MenuManager.OpenSubMenu(_currentSubMenu);
                    break;
                case 3:
                    _currentSubMenu = "licenses";
                    MenuManager.OpenSubMenu(_currentSubMenu);
                    break;
                case 4:
                    MenuManager.Quit();
                    break;
            }
        }
    }

    private void HandleOptionsMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOptionVerticalOnly();

        if (MenuManager.indexOption == 4)
        {
            if (Input.GetKeyDown(_keyValidate))
                GoBackToMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(_keyLeft) || Input.GetKeyDown(_keySideLeft))
            MenuManager.UpdateVolume(MenuManager.indexOption, -1);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(_keyRight) || Input.GetKeyDown(_keySideRight))
            MenuManager.UpdateVolume(MenuManager.indexOption, 1);
    }

    private void HandleLicensesMenuInput()
    {
        MenuManager.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(_keyValidate))
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
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(_keyUp))
            transform.Translate(Vector3.forward * Time.deltaTime * _playerScript.DirectionalSpeed);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(_keyDown))
            transform.Translate(Vector3.back * Time.deltaTime * _playerScript.DirectionalSpeed);

        // Rotate the player to the left or the right
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(_keyLeft))
            transform.Rotate(Vector3.down * Time.deltaTime * _playerScript.RotationalSpeed);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(_keyRight))
            transform.Rotate(Vector3.up * Time.deltaTime * _playerScript.RotationalSpeed);

        // Move the player to the side
        if (Input.GetKey(_keySideLeft))
            transform.Translate(Vector3.left * Time.deltaTime * _playerScript.DirectionalSpeed);
        if (Input.GetKey(_keySideRight))
            transform.Translate(Vector3.right * Time.deltaTime * _playerScript.DirectionalSpeed);

        // Make the player jump
        if (Input.GetKeyDown(_keyJump))
            _playerScript.Jump();

        // Toggle/Untoggle help mode
        if (Input.GetKeyDown(_keyHelpMode))
        {
            // Tutorial/Advice and not just a display of the different keys
            Debug.Log("Help Key");
        }

        // Quick save
        if (Input.GetKeyDown(_keyQuickSave))
        {
            // Quick save only - Do not open the save sub-menu
            Debug.Log("Quick Save Key");
        }

        // Switch between 3rd (default) and 1st person POV
        if (Input.GetKeyDown(_keyPovMode))
            _cameraScript.SwitchCameraMode();
    }
}
