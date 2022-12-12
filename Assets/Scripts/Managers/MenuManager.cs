using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private static AudioManager audioManager;
    private static Behaviour menuCamera;
    private static Behaviour uiCamera;
    private static GameObject screenMain, screenOptions, screenLicenses;
    private static TextMeshProUGUI[] arrTmpMain, arrTmpOptions, arrTmpLicenses;
    public static int indexOption;
    private static int minIndexOption;
    private static bool isFirstGame;
    private static bool userAskedForRestart = false;

    void Awake()
    {
        GameObject[] arrGo;
        int i;

        audioManager = FindObjectOfType<AudioManager>();
        menuCamera = (Behaviour)GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
        uiCamera = (Behaviour)GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        // Set the screen variables
        screenMain = GameObject.FindGameObjectWithTag("MainMenuScreen");
        screenOptions = GameObject.FindGameObjectWithTag("MenuOptionsScreen");
        screenLicenses = GameObject.FindGameObjectWithTag("MenuLicensesScreen");

        // Set arrTmpMain
        screenOptions.SetActive(false);
        screenLicenses.SetActive(false);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        arrTmpMain = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            arrTmpMain[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Set arrTmpOptions
        screenMain.SetActive(false);
        screenOptions.SetActive(true);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        arrTmpOptions = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            arrTmpOptions[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Set arrTmpLicenses
        screenOptions.SetActive(false);
        screenLicenses.SetActive(true);
        arrGo = GameObject.FindGameObjectsWithTag("MenuOption");
        arrGo = arrGo.OrderBy(e => e.name).ToArray();
        arrTmpLicenses = new TextMeshProUGUI[arrGo.Length];
        for (i = 0; i < arrGo.Length; ++i)
            arrTmpLicenses[i] = arrGo[i].GetComponent<TextMeshProUGUI>();

        // Only activate the main screen
        screenLicenses.SetActive(false);
        screenMain.SetActive(true);
    }

    void Start()
    {
        // When the soft starts, there is no ongoing game, so disable the first option ("Resume Current Game")
        DisableFirstMainMenuOption();
        indexOption = minIndexOption;
        isFirstGame = true;

        // If user had started a game and then selects "New Game" again, the new game needs to start immediately
        if (userAskedForRestart)
        {
            audioManager.Play("MenuValidate");
            ResumeGame();
            // A game starting also implies that "Resume Current Game" needs to be enabled
            EnableFirstMainMenuOption();
        }
        else
            OpenMainMenu();
    }

    public static void OpenMainMenu()
    {
        if (!isFirstGame)
            audioManager.Play("MenuBack");
        // Pause the game
        Time.timeScale = 0;
        // Activate the menu camera
        menuCamera.enabled = true;
        // Deactivate the UI camera so it doesn't show in the menu
        uiCamera.enabled = false;
        indexOption = minIndexOption;
        StopGameAmbience();
        audioManager.Play("MenuTheme");
    }

    private static void ResumeGame()
    {
        isFirstGame = false;
        // Resume the game
        Time.timeScale = 1;
        // Deactivate the menu camera
        menuCamera.enabled = false;
        // Reactivate the UI camera for the game
        uiCamera.enabled = true;
        // Reset the menu option selector
        indexOption = minIndexOption;
        audioManager.Stop("MenuTheme");
        PlayGameAmbience();
    }

    private static void PlayGameAmbience()
    {
        audioManager.Play("GameAmbiencePulse");
        audioManager.Play("GameAmbienceForest");
        audioManager.Play("GameAmbienceCreeper");
    }

    private static void StopGameAmbience()
    {
        audioManager.Stop("GameAmbiencePulse");
        audioManager.Stop("GameAmbienceForest");
        audioManager.Stop("GameAmbienceCreeper");
    }

    public static void DisableFirstMainMenuOption()
    {
        arrTmpMain[0].enabled = false;
        minIndexOption = 1;
    }

    private static void EnableFirstMainMenuOption()
    {
        arrTmpMain[0].enabled = true;
        minIndexOption = 0;
    }

    public static void SetGraphicsForSelectedOption(string menu)
    {
        TextMeshProUGUI[] arrTmp;
        if (menu == "options")
            arrTmp = arrTmpOptions;
        else if (menu == "licenses")
            arrTmp = arrTmpLicenses;
        else
            arrTmp = arrTmpMain;

        // Set all options to white
        foreach (TextMeshProUGUI tmp in arrTmp)
            tmp.color = new Color(1f, 1f, 1f, 1f);

        // Set the selected option to orange
        arrTmp[indexOption].color = new Color(0.65f, 0.19f, 0.08f, 1f);
    }

    public static void SelectUp(string menu)
    {
        int length, min;
        if (menu == "options")
        {
            length = arrTmpOptions.Length;
            min = 0;
        }
        else if (menu == "licenses")
        {
            length = arrTmpLicenses.Length;
            min = 0;
        }
        else
        {
            length = arrTmpMain.Length;
            min = minIndexOption;
        }

        audioManager.Play("MenuSelect");
        indexOption = indexOption > min ? indexOption - 1 : length - 1;
    }

    public static void SelectDown(string menu)
    {
        int length, min;
        if (menu == "options")
        {
            length = arrTmpOptions.Length;
            min = 0;
        }
        else if (menu == "licenses")
        {
            length = arrTmpLicenses.Length;
            min = 0;
        }
        else
        {
            length = arrTmpMain.Length;
            min = minIndexOption;
        }

        audioManager.Play("MenuSelect");
        indexOption = indexOption < length - 1 ? indexOption + 1 : min;
    }

    public static void ResumeCurrentGame()
    {
        audioManager.Play("MenuForward");
        ResumeGame();
    }

    public static void NewGame()
    {
        audioManager.Play("MenuValidate");
        if (!isFirstGame)
        {
            userAskedForRestart = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        ResumeGame();
        EnableFirstMainMenuOption();
    }

    public static void Quit()
    {
        audioManager.Play("MenuBack");
        Application.Quit();
    }

    public static void OpenSubMenu(string menu)
    {
        audioManager.Play("MenuValidate");
        screenMain.SetActive(false);

        if (menu == "options")
            screenOptions.SetActive(true);
        else if (menu == "licenses")
            screenLicenses.SetActive(true);
        // Error, so just quit
        else
            Quit();

        indexOption = 0;
    }

    public static void CloseSubMenu(string menu)
    {
        audioManager.Play("MenuBack");

        if (menu == "options")
            screenOptions.SetActive(false);
        else if (menu == "licenses")
            screenLicenses.SetActive(false);

        screenMain.SetActive(true);
        indexOption = minIndexOption;
    }

    public static void OpenLink(string link)
    {
        audioManager.Play("MenuValidate");
        Application.OpenURL(link);
    }

    public static void UpdateVolume(int indexOption, int input)
    {
        int newPercentage = 0;
        newPercentage = audioManager.SetMixerVolume(indexOption, input);
        if (newPercentage != -1)
            arrTmpOptions[indexOption].text = newPercentage.ToString() + "%";
    }
}
