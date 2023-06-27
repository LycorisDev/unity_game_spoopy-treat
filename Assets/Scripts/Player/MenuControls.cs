using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControls : MonoBehaviour
{
    public enum GameState
    {
        InGame,
        MainMenu,
        MenuOptions,
        MenuLicenses,
    }

    private GameState _currentState = GameState.MainMenu;

    private MenuManager _menuScript;

    private Vector2 _directions = Vector2.zero;
    private float _sideStep = 0f;

    [SerializeField] private InputActionReference _screenModeButton;
    [SerializeField] private InputActionReference _escapeButton;
    [SerializeField] private InputActionReference _validateButton;

    private void Awake()
    {
        _menuScript = FindObjectOfType<MenuManager>();

        _screenModeButton.action.started += ScreenMode;
        _escapeButton.action.started += EscapeButton;
        _validateButton.action.started += Validate;
    }

    private void Update()
    {
        if (_currentState != GameState.InGame)
            HandleMenuInput();
    }

    private void ScreenMode(InputAction.CallbackContext context)
    {
        // Switch between fullscreen and windowed mode
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void EscapeButton(InputAction.CallbackContext context)
    {
        if (_currentState == GameState.InGame)
        {
            _menuScript.OpenMainMenu();
            _currentState = GameState.MainMenu;
        }
        else if (_currentState == GameState.MainMenu)
        {
            _menuScript.Quit();
        }
        else
        {
            _menuScript.CloseSubMenu(_currentState);
            _currentState = GameState.MainMenu;
        }
    }

    private void Validate(InputAction.CallbackContext context)
    {
        // TODO: KeyCode.Return
    }

    private void HandleMenuInput()
    {
        if (_currentState == GameState.MenuOptions)
            HandleOptionsMenuInput();
        else if (_currentState == GameState.MenuLicenses)
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
            _menuScript.SelectUp(_currentState);
        else if (_directions.y < 0f || _directions.x > 0f || _sideStep > 0f)
            _menuScript.SelectDown(_currentState);
    }

    private void SelectMenuOptionVerticalOnly()
    {
        /* Used for when the sub-menu requires the horizontal input for other specific options (e.g. volume sliders). */

        if (_directions.y > 0f)
            _menuScript.SelectUp(_currentState);
        else if (_directions.y < 0f)
            _menuScript.SelectDown(_currentState);
    }

    private void HandleMainMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentState);
        SelectMenuOption();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (_menuScript.IndexOption)
            {
                case 0:
                    _menuScript.ResumeCurrentGame();
                    _currentState = GameState.InGame;
                    break;
                case 1:
                    _menuScript.NewGame();
                    _currentState = GameState.InGame;
                    break;
                case 2:
                    _menuScript.OpenSubMenu(_currentState);
                    _currentState = GameState.MenuOptions;
                    break;
                case 3:
                    _menuScript.OpenSubMenu(_currentState);
                    _currentState = GameState.MenuLicenses;
                    break;
                case 4:
                    _menuScript.Quit();
                    break;
            }
        }
    }

    private void HandleOptionsMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentState);
        SelectMenuOptionVerticalOnly();

        if (_menuScript.IndexOption == 4)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _menuScript.CloseSubMenu(_currentState);
                _currentState = GameState.MainMenu;
            }
        }
        else if (_directions.x < 0f || _sideStep < 0f)
            _menuScript.UpdateVolume(_menuScript.IndexOption, -1);
        else if (_directions.x > 0f || _sideStep > 0f)
            _menuScript.UpdateVolume(_menuScript.IndexOption, 1);
    }

    private void HandleLicensesMenuInput()
    {
        _menuScript.SetGraphicsForSelectedOption(_currentState);
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
                    _menuScript.CloseSubMenu(_currentState);
                    _currentState = GameState.MainMenu;
                    break;
            }
        }
    }
}
