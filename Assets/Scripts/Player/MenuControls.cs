using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControls : MonoBehaviour
{
    public enum MenuState
    {
        MainMenu,
        MenuOptions,
        MenuLicenses,
    }

    private MenuState _currentState;
    private MenuManager _menuScript;

    [SerializeField] private InputActionReference _screenModeButton;
    [SerializeField] private InputActionReference _escapeButton;
    [SerializeField] private InputActionReference _validateButton;
    [SerializeField] private InputActionReference _verticalDirection;
    [SerializeField] private InputActionReference _horizontalDirection;

    private void Awake()
    {
        _currentState = MenuState.MainMenu;
        _menuScript = FindObjectOfType<MenuManager>();
    }

    private void OnEnable()
    {
        _screenModeButton.action.started += ScreenMode;
        _escapeButton.action.started += EscapeButton;
        _validateButton.action.started += Validate;
        _verticalDirection.action.started += VerticalDirection;
        _horizontalDirection.action.started += HorizontalDirection;
    }

    private void OnDisable()
    {
        _screenModeButton.action.started -= ScreenMode;
        _escapeButton.action.started -= EscapeButton;
        _validateButton.action.started -= Validate;
        _verticalDirection.action.started -= VerticalDirection;
        _horizontalDirection.action.started -= HorizontalDirection;
    }

    private void ScreenMode(InputAction.CallbackContext context)
    {
        // Switch between fullscreen and windowed mode
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void EscapeButton(InputAction.CallbackContext context)
    {
        if (_currentState == MenuState.MainMenu)
        {
            _menuScript.Quit();
        }
        else
        {
            _menuScript.CloseSubMenu(_currentState);
            _currentState = MenuState.MainMenu;
        }
    }

    /* GameActions.EscapeButton() */
    public void OpenMenu()
    {
        _menuScript.OpenMainMenu();
        _currentState = MenuState.MainMenu;
    }

    private void Validate(InputAction.CallbackContext context)
    {
        if (_currentState == MenuState.MainMenu)
        {
            HandleMainMenuInput();
        }
        else if (_currentState == MenuState.MenuOptions)
        {
            /* Volume sliders handled in HorizontalDirection(). */

            /* Validate at "Go back": */
            if (_menuScript.IndexOption == 4)
            {
                _menuScript.CloseSubMenu(_currentState);
                _currentState = MenuState.MainMenu;
            }
        }
        else if (_currentState == MenuState.MenuLicenses)
        {
            HandleLicensesMenuInput();
        }
    }

    private void VerticalDirection(InputAction.CallbackContext context)
    {
        float value = context.action.ReadValue<float>();

        if (value > 0f)
            _menuScript.SelectUp(_currentState);
        else if (value < 0f)
            _menuScript.SelectDown(_currentState);
    }

    private void HorizontalDirection(InputAction.CallbackContext context)
    {
        float value = context.action.ReadValue<float>();

        /* Cannot be used in sub-menus which require the horizontal input for other specific options (e.g. volume sliders). */

        if (_currentState == MenuState.MenuOptions)
        {
            _menuScript.UpdateVolume(_menuScript.IndexOption, (int)value);
            return;
        }

        if (value < 0f)
            _menuScript.SelectUp(_currentState);
        else if (value > 0f)
             _menuScript.SelectDown(_currentState);
    }

    private void HandleMainMenuInput()
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
                _currentState = MenuState.MenuOptions;
                _menuScript.OpenSubMenu(_currentState);
                break;
            case 3:
                _currentState = MenuState.MenuLicenses;
                _menuScript.OpenSubMenu(_currentState);
                break;
            case 4:
                _menuScript.Quit();
                break;
        }
    }

    private void HandleLicensesMenuInput()
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
                _menuScript.CloseSubMenu(_currentState);
                _currentState = MenuState.MainMenu;
                break;
        }
    }
}
