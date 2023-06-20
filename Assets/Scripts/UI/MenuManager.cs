using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public int IndexOption { get; private set; }

    private static bool _userAskedForRestart = false;
    private int _minIndexOption;
    private bool _isFirstGame;
    private Behaviour _menuCamera;
    private Behaviour _hudCamera;
    private GameObject _screenMain, _screenOptions, _screenLicenses;
    private TextMeshProUGUI[] _arrTmpMain, _arrTmpOptions, _arrTmpLicenses;

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

        Debug.Log("TODO: _userAskedForRestart shouldn't be static, it should be in a file that belongs to this player, so that every player can have their own _userAskedForRestart value.");

        _menuCamera = GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
        _hudCamera = GameObject.FindGameObjectWithTag("HUDCamera").GetComponent<Camera>();

        // Set the screen variables
        _screenMain = GameObject.FindGameObjectWithTag("MainMenuScreen");
        _screenOptions = GameObject.FindGameObjectWithTag("MenuOptionsScreen");
        _screenLicenses = GameObject.FindGameObjectWithTag("MenuLicensesScreen");

        // Set arrTmpMain
        _screenOptions.SetActive(false);
        _screenLicenses.SetActive(false);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        _arrTmpMain = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            _arrTmpMain[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Set arrTmpOptions
        _screenMain.SetActive(false);
        _screenOptions.SetActive(true);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        _arrTmpOptions = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            _arrTmpOptions[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Set arrTmpLicenses
        _screenOptions.SetActive(false);
        _screenLicenses.SetActive(true);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        _arrTmpLicenses = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            _arrTmpLicenses[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

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
        StopGameAmbience();
        _soundMusicTheme.Play();
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
        _arrTmpMain[0].enabled = false;
        _minIndexOption = 1;
    }

    private void EnableFirstMainMenuOption()
    {
        _arrTmpMain[0].enabled = true;
        _minIndexOption = 0;
    }

    public void SetGraphicsForSelectedOption(string menu)
    {
        TextMeshProUGUI[] arrTmp;
        if (menu == "options")
            arrTmp = _arrTmpOptions;
        else if (menu == "licenses")
            arrTmp = _arrTmpLicenses;
        else
            arrTmp = _arrTmpMain;

        // Set all options to white
        foreach (TextMeshProUGUI tmp in arrTmp)
            tmp.color = new Color(1f, 1f, 1f, 1f);

        // Set the selected option to orange
        arrTmp[IndexOption].color = new Color(0.65f, 0.19f, 0.08f, 1f);
    }

    public void SelectUp(string menu)
    {
        int length, min;
        if (menu == "options")
        {
            length = _arrTmpOptions.Length;
            min = 0;
        }
        else if (menu == "licenses")
        {
            length = _arrTmpLicenses.Length;
            min = 0;
        }
        else
        {
            length = _arrTmpMain.Length;
            min = _minIndexOption;
        }

        _menuSelect.Play();
        IndexOption = IndexOption > min ? IndexOption - 1 : length - 1;
    }

    public void SelectDown(string menu)
    {
        int length, min;
        if (menu == "options")
        {
            length = _arrTmpOptions.Length;
            min = 0;
        }
        else if (menu == "licenses")
        {
            length = _arrTmpLicenses.Length;
            min = 0;
        }
        else
        {
            length = _arrTmpMain.Length;
            min = _minIndexOption;
        }

        _menuSelect.Play();
        IndexOption = IndexOption < length - 1 ? IndexOption + 1 : min;
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

    public void OpenSubMenu(string menu)
    {
        _soundValidate.Play();
        _screenMain.SetActive(false);

        if (menu == "options")
            _screenOptions.SetActive(true);
        else if (menu == "licenses")
            _screenLicenses.SetActive(true);
        // Error, so just quit
        else
            Quit();

        IndexOption = 0;
    }

    public void CloseSubMenu(string menu)
    {
        _soundBack.Play();

        if (menu == "options")
            _screenOptions.SetActive(false);
        else if (menu == "licenses")
            _screenLicenses.SetActive(false);

        _screenMain.SetActive(true);
        IndexOption = _minIndexOption;
    }

    public void OpenLink(string link)
    {
        _soundValidate.Play();
        Application.OpenURL(link);
    }

    public void UpdateVolume(int IndexOption, int input)
    {
        int newPercentage = AudioMixerVolume.Instance.SetMixerVolume(IndexOption, input);
        if (newPercentage != -1)
            _arrTmpOptions[IndexOption].text = newPercentage.ToString() + "%";
    }
}
