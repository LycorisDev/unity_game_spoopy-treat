using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private Character _playerScript;
    private CameraManager _cameraScript;
    private MenuManager _menuScript;
    private string _currentSubMenu;

    private bool _isInGame = false;
    private Vector2 _directions = Vector2.zero;
    private float _sideStep = 0f;

    [SerializeField] private InputActionReference _horizontalDirectionValue;
    [SerializeField] private InputActionReference _verticalDirectionValue;
    [SerializeField] private InputActionReference _sideStepValue;

    [SerializeField] private InputActionReference _validateButton;

    [SerializeField] private InputActionReference _jumpButton;
    [SerializeField] private InputActionReference _escapeButton;
    [SerializeField] private InputActionReference _helpModeButton;
    [SerializeField] private InputActionReference _quickSaveButton;
    [SerializeField] private InputActionReference _povModeButton;
    [SerializeField] private InputActionReference _screenModeButton;

    private void Awake()
    {
        _playerScript = GetComponent<Character>();
        _cameraScript = Camera.main.GetComponent<CameraManager>();
        _menuScript = FindObjectOfType<MenuManager>();

        _currentSubMenu = "main";

        _horizontalDirectionValue.action.started += HorizontalDirection;
        _horizontalDirectionValue.action.canceled += HorizontalDirection;
        _verticalDirectionValue.action.started += VerticalDirection;
        _verticalDirectionValue.action.canceled += VerticalDirection;
        _sideStepValue.action.started += SideStep;
        _sideStepValue.action.canceled += SideStep;

        _validateButton.action.started += Validate;

        _jumpButton.action.started += Jump;
        _escapeButton.action.started += EscapeButton;
        _helpModeButton.action.started += HelpMode;
        _quickSaveButton.action.started += QuickSave;
        _povModeButton.action.started += PovMode;
        _screenModeButton.action.started += ScreenMode;
    }

    private void HorizontalDirection(InputAction.CallbackContext context)
    {
        // TODO: Perfect for in-game movement, but not for menu
        _directions.x = context.ReadValue<float>();
    }

    private void VerticalDirection(InputAction.CallbackContext context)
    {
        // TODO: Perfect for in-game movement, but not for menu
        _directions.y = context.ReadValue<float>();
    }

    private void SideStep(InputAction.CallbackContext context)
    {
        // TODO: Perfect for in-game movement, but not for menu
        _sideStep = context.ReadValue<float>();
    }

    private void Validate(InputAction.CallbackContext context)
    {
        // TODO: KeyCode.Return
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _playerScript.Jump();
    }

    private void EscapeButton(InputAction.CallbackContext context)
    {
        // Open menu if in game
        if (Time.timeScale == 1)
        {
            _menuScript.OpenMainMenu();
        } 
        // Close the soft if in main menu
        else if (_currentSubMenu == "main")
        {
            _menuScript.Quit();
        }
        // Go back to main menu if in sub-menu
        else
        {
            _menuScript.CloseSubMenu(_currentSubMenu);
            _currentSubMenu = "main";
        }
    }

    private void HelpMode(InputAction.CallbackContext context)
    {
        // Toggle/Untoggle help mode
        // Tutorial/Advice and not just a display of the different keys
        Debug.Log("Help Key");
    }

    private void QuickSave(InputAction.CallbackContext context)
    {
        // Quick save only - Do not open the save sub-menu
        Debug.Log("Quick Save Key");
    }

    private void PovMode(InputAction.CallbackContext context)
    {
        // Switch between 3rd (default) and 1st person POV
        _cameraScript.SwitchCameraMode();
    }

    private void ScreenMode(InputAction.CallbackContext context)
    {
        // Switch between fullscreen and windowed mode
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            HandleMenuInput();
        else
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
            if (_sideStep < 0f)
                transform.Translate(Vector3.left * Time.deltaTime * _playerScript.DirectionalSpeed);
            if (_sideStep > 0f)
                transform.Translate(Vector3.right * Time.deltaTime * _playerScript.DirectionalSpeed);
        } 
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

        if (_directions.y > 0f ||_directions.x < 0f || _sideStep < 0f)
            _menuScript.SelectUp(_currentSubMenu);
        else if (_directions.y < 0f || _directions.x > 0f || _sideStep > 0f)
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

    private void HandleMainMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(KeyCode.Return))
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _menuScript.CloseSubMenu(_currentSubMenu);
                _currentSubMenu = "main";
            }
        }
        else if (_directions.x < 0f || _sideStep < 0f)
            _menuScript.UpdateVolume(_menuScript.IndexOption, -1);
        else if (_directions.x > 0f || _sideStep > 0f)
            _menuScript.UpdateVolume(_menuScript.IndexOption, 1);
    }

    private void HandleLicensesMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentSubMenu);
        SelectMenuOption();

        if (Input.GetKeyDown(KeyCode.Return))
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
                    _menuScript.CloseSubMenu(_currentSubMenu);
                    _currentSubMenu = "main";
                    break;
            }
        }
    }
}
