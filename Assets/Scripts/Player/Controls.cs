using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private Character _playerScript;
    private CameraManager _cameraScript;
    private MenuManager _menuScript;
    private string _currentSubMenu;
    private KeyCode _keyMenu, _keyHelpMode, _keyQuickSave, _keyPovMode, _keyScreenMode, 
        _keyValidate, _keySideLeft, _keySideRight, _keyJump;

    private Vector2 _directions;

    public void InputDirections(InputAction.CallbackContext context)
    {
        _directions = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        _playerScript = GetComponent<Character>();
        _cameraScript = Camera.main.GetComponent<CameraManager>();
        _menuScript = FindObjectOfType<MenuManager>();

        _currentSubMenu = "main";

        // "Use Physical Keys" enabled (QWERTY)
        _keyMenu = KeyCode.Escape;
        _keyHelpMode = KeyCode.F1;
        _keyQuickSave = KeyCode.F2;
        _keyPovMode = KeyCode.F3;
        _keyScreenMode = KeyCode.F11;
        _keyValidate = KeyCode.Return;
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
                _menuScript.OpenMainMenu();
            // Close the soft if in main menu
            else if (_currentSubMenu == "main")
                _menuScript.Quit();
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

        if (_directions.y > 0f ||_directions.x < 0f || Input.GetKeyDown(_keySideLeft))
            _menuScript.SelectUp(_currentSubMenu);
        else if (_directions.y < 0f || _directions.x > 0f || Input.GetKeyDown(_keySideRight))
            _menuScript.SelectDown(_currentSubMenu);
    }

    private void SelectMenuOptionVerticalOnly()
    {
        /* Used for when the sub-menu requires the horizontal input for other specific options (e.g. volume sliders). */

        if (_directions.y > 0f)
            _menuScript.SelectUp(_currentSubMenu);
        else if (_directions.y < 0f)
            _menuScript.SelectDown(_currentSubMenu);
    }

    private void GoBackToMainMenu()
    {
        _menuScript.CloseSubMenu(_currentSubMenu);
        _currentSubMenu = "main";
    }

    private void HandleMainMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(_keyValidate))
        {
            switch (_menuScript.IndexOption)
            {
                case 0:
                    _menuScript.ResumeCurrentGame();
                    break;
                case 1:
                    _menuScript.NewGame();
                    break;
                case 2:
                    _currentSubMenu = "options";
                    _menuScript.OpenSubMenu(_currentSubMenu);
                    break;
                case 3:
                    _currentSubMenu = "licenses";
                    _menuScript.OpenSubMenu(_currentSubMenu);
                    break;
                case 4:
                    _menuScript.Quit();
                    break;
            }
        }
    }

    private void HandleOptionsMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOptionVerticalOnly();

        if (_menuScript.IndexOption == 4)
        {
            if (Input.GetKeyDown(_keyValidate))
                GoBackToMainMenu();
        }
        else if (_directions.x < 0f || Input.GetKeyDown(_keySideLeft))
            _menuScript.UpdateVolume(_menuScript.IndexOption, -1);
        else if (_directions.x > 0f || Input.GetKeyDown(_keySideRight))
            _menuScript.UpdateVolume(_menuScript.IndexOption, 1);
    }

    private void HandleLicensesMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(_keyValidate))
        {
            switch (_menuScript.IndexOption)
            {
                case 0:
                    _menuScript.OpenLink("https://opengameart.org/content/a-tricky-puzzle-loop");
                    break;
                case 1:
                    _menuScript.OpenLink("https://www.ghosthack.de");
                    break;
                case 2:
                    _menuScript.OpenLink("https://assetstore.unity.com/packages/3d/props/exterior/halloween-pumpkins-50597");
                    break;
                case 3:
                    _menuScript.OpenLink("https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153");
                    break;
                case 4:
                    _menuScript.OpenLink("https://assetstore.unity.com/packages/3d/environments/fantasy/mausoleum-128753");
                    break;
                case 5:
                    _menuScript.OpenLink("https://assetstore.unity.com/packages/3d/props/poly-halloween-pack-236625");
                    break;
                case 6:
                    _menuScript.OpenLink("https://assetstore.unity.com/packages/3d/environments/fantasy/halloween-cemetery-set-19125");
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
        if (_directions.y > 0f)
            transform.Translate(Vector3.forward * Time.deltaTime * _playerScript.DirectionalSpeed);
        if (_directions.y < 0f)
            transform.Translate(Vector3.back * Time.deltaTime * _playerScript.DirectionalSpeed);

        // Rotate the player to the left or the right
        if (_directions.x < 0f)
            transform.Rotate(Vector3.down * Time.deltaTime * _playerScript.RotationalSpeed);
        if (_directions.x > 0f)
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
