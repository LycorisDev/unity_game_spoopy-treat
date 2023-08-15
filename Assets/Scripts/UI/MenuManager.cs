using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public int IndexOption { get; private set; }

    private static bool _userAskedForRestart = false;
    private int _minIndexOption;
    private bool _isFirstGame;
    [SerializeField] private PlayerInput _input;
    private Behaviour _menuCamera;
    private Behaviour _hudCamera;
    private GameObject _screenMain, _screenOptions, _screenLicenses;
    private TextMeshProUGUI[] _arrTmproMain, _arrTmproOptions, _arrTmproLicenses;

    [SerializeField] private Sound _menuSelect;
    [SerializeField] private Sound _soundValidate;
    [SerializeField] private Sound _menuForward;
    [SerializeField] private Sound _soundBack;

    [SerializeField] private Sound _soundMusicTheme;
    [SerializeField] private Sound _soundAmbiencePulse;
    [SerializeField] private Sound _soundAmbienceForest;
    [SerializeField] private Sound _soundAmbienceCreeper;

    private void Awake()
    {
        int i;
        GameObject[] arrGo;

        _menuCamera = GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
        _hudCamera = GameObject.FindGameObjectWithTag("HUDCamera").GetComponent<Camera>();

        // Set the screen variables
        _screenMain = GameObject.FindGameObjectWithTag("MainMenuScreen");
        _screenOptions = GameObject.FindGameObjectWithTag("MenuOptionsScreen");
        _screenLicenses = GameObject.FindGameObjectWithTag("MenuLicensesScreen");

        // Set arrTmproMain
        _screenOptions.SetActive(false);
        _screenLicenses.SetActive(false);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        _arrTmproMain = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            _arrTmproMain[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Set arrTmproOptions
        _screenMain.SetActive(false);
        _screenOptions.SetActive(true);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        _arrTmproOptions = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            _arrTmproOptions[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Set arrTmproLicenses
        _screenOptions.SetActive(false);
        _screenLicenses.SetActive(true);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        _arrTmproLicenses = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            _arrTmproLicenses[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Only activate the main screen
        _screenLicenses.SetActive(false);
        _screenMain.SetActive(true);
    }

    private void Start()
    {
        // When the soft starts, there is no ongoing game, so disable the first option ("Resume Current Game")
        DisableFirstMainMenuOption();
        IndexOption = _minIndexOption;
        _isFirstGame = true;

        // If user had started a game and then selects "New Game" again, the new game needs to start immediately
        if (_userAskedForRestart)
        {
            _soundValidate.Play();
            ResumeGame();
            // A game starting also implies that "Resume Current Game" needs to be enabled
            EnableFirstMainMenuOption();
        }
        else
            OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        if (!_isFirstGame)
            _soundBack.Play();
        // Pause the game
        Time.timeScale = 0;
        // Activate the menu camera
        _menuCamera.enabled = true;
        // Deactivate the HUD camera so it doesn't show in the menu
        _hudCamera.enabled = false;
        IndexOption = _minIndexOption;
        SetGraphicsForSelectedOption(MenuControls.MenuState.MainMenu);
        StopGameAmbience();
        _soundMusicTheme.Play();
        // Activate UI input
        _input.SwitchCurrentActionMap("UI");
    }

    private void ResumeGame()
    {
        _isFirstGame = false;
        // Resume the game
        Time.timeScale = 1;
        // Deactivate the menu camera
        _menuCamera.enabled = false;
        // Reactivate the HUD camera for the game
        _hudCamera.enabled = true;
        // Reset the menu option selector
        IndexOption = _minIndexOption;
        _soundMusicTheme.Stop();
        PlayGameAmbience();
        // Activate Player input
        _input.SwitchCurrentActionMap("Player");
    }

    private void PlayGameAmbience()
    {
        _soundAmbiencePulse.Play();
        _soundAmbienceForest.Play();
        _soundAmbienceCreeper.Play();
    }

    private void StopGameAmbience()
    {
        _soundAmbiencePulse.Stop();
        _soundAmbienceForest.Stop();
        _soundAmbienceCreeper.Stop();
    }

    public void DisableFirstMainMenuOption()
    {
        _arrTmproMain[0].enabled = false;
        _minIndexOption = 1;
    }

    private void EnableFirstMainMenuOption()
    {
        _arrTmproMain[0].enabled = true;
        _minIndexOption = 0;
    }

    private void SetGraphicsForSelectedOption(MenuControls.MenuState menu)
    {
        TextMeshProUGUI[] arrTmpro;
        if (menu == MenuControls.MenuState.MenuOptions)
            arrTmpro = _arrTmproOptions;
        else if (menu == MenuControls.MenuState.MenuLicenses)
            arrTmpro = _arrTmproLicenses;
        else
            arrTmpro = _arrTmproMain;

        // Set all options to white
        foreach (TextMeshProUGUI tmpro in arrTmpro)
            tmpro.color = new Color(1f, 1f, 1f, 1f);

        // Set the selected option to orange
        arrTmpro[IndexOption].color = new Color(0.65f, 0.19f, 0.08f, 1f);
    }

    public void SelectUp(MenuControls.MenuState menu)
    {
        int length, min;
        if (menu == MenuControls.MenuState.MenuOptions)
        {
            length = _arrTmproOptions.Length;
            min = 0;
        }
        else if (menu == MenuControls.MenuState.MenuLicenses)
        {
            length = _arrTmproLicenses.Length;
            min = 0;
        }
        else
        {
            length = _arrTmproMain.Length;
            min = _minIndexOption;
        }

        _menuSelect.Play();
        IndexOption = IndexOption > min ? IndexOption - 1 : length - 1;
        SetGraphicsForSelectedOption(menu);
    }

    public void SelectDown(MenuControls.MenuState menu)
    {
        int length, min;
        if (menu == MenuControls.MenuState.MenuOptions)
        {
            length = _arrTmproOptions.Length;
            min = 0;
        }
        else if (menu == MenuControls.MenuState.MenuLicenses)
        {
            length = _arrTmproLicenses.Length;
            min = 0;
        }
        else
        {
            length = _arrTmproMain.Length;
            min = _minIndexOption;
        }

        _menuSelect.Play();
        IndexOption = IndexOption < length - 1 ? IndexOption + 1 : min;
        SetGraphicsForSelectedOption(menu);
    }

    public void ResumeCurrentGame()
    {
        _menuForward.Play();
        ResumeGame();
    }

    public void NewGame()
    {
        _soundValidate.Play();
        if (!_isFirstGame)
        {
            _userAskedForRestart = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        ResumeGame();
        EnableFirstMainMenuOption();
    }

    public void Quit()
    {
        _soundBack.Play();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void OpenSubMenu(MenuControls.MenuState menu)
    {
        _soundValidate.Play();
        _screenMain.SetActive(false);

        if (menu == MenuControls.MenuState.MenuOptions)
            _screenOptions.SetActive(true);
        else if (menu == MenuControls.MenuState.MenuLicenses)
            _screenLicenses.SetActive(true);
        // Error, so just quit
        else
            Quit();

        IndexOption = 0;
        SetGraphicsForSelectedOption(menu);
    }

    public void CloseSubMenu(MenuControls.MenuState menu)
    {
        _soundBack.Play();

        if (menu == MenuControls.MenuState.MenuOptions)
            _screenOptions.SetActive(false);
        else if (menu == MenuControls.MenuState.MenuLicenses)
            _screenLicenses.SetActive(false);

        _screenMain.SetActive(true);
        IndexOption = _minIndexOption;
        SetGraphicsForSelectedOption(MenuControls.MenuState.MainMenu);
    }

    public void OpenLink(string link)
    {
        _soundValidate.Play();
        Application.OpenURL(link);
    }

    public void UpdateVolume(int IndexOption, int input)
    {
        int newPercentage = AudioMixerVolume.Instance.SetMixerVolume((AudioMixerVolume.VolumeGroup)IndexOption, input);
        if (newPercentage != -1)
            _arrTmproOptions[IndexOption].text = newPercentage.ToString() + "%";
    }
}
