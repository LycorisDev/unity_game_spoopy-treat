using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private static AudioManager audio_manager;
    private static Behaviour menu_camera;
    private static Behaviour ui_camera;
    private static GameObject screen_main, screen_options, screen_licenses;
    private static TextMeshProUGUI[] arr_tmp_main, arr_tmp_options, arr_tmp_licenses;
    public static int index_option;
    private static int min_index_option;
    private static bool is_first_game;
    private static bool user_asked_for_restart = false;

    void Start()
    {
        GameObject[] arr_go;
        int i;

        audio_manager = FindObjectOfType<AudioManager>();
        menu_camera = (Behaviour)GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
        ui_camera = (Behaviour)GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        // Set the screen variables
        screen_main = GameObject.FindGameObjectWithTag("MainMenuScreen");
        screen_options = GameObject.FindGameObjectWithTag("MenuOptionsScreen");
        screen_licenses = GameObject.FindGameObjectWithTag("MenuLicensesScreen");

        // Set arr_tmp_main
        screen_options.SetActive(false);
        screen_licenses.SetActive(false);
        arr_go = GameObject.FindGameObjectsWithTag("MenuOption");
        arr_go = arr_go.OrderBy(e => e.name).ToArray();
        arr_tmp_main = new TextMeshProUGUI[arr_go.Length];
        for (i = 0; i < arr_go.Length; ++i)
            arr_tmp_main[i] = arr_go[i].GetComponent<TextMeshProUGUI>();

        // Set arr_tmp_options
        screen_main.SetActive(false);
        screen_options.SetActive(true);
        arr_go = GameObject.FindGameObjectsWithTag("MenuOption");
        arr_go = arr_go.OrderBy(e => e.name).ToArray();
        arr_tmp_options = new TextMeshProUGUI[arr_go.Length];
        for (i = 0; i < arr_go.Length; ++i)
            arr_tmp_options[i] = arr_go[i].GetComponent<TextMeshProUGUI>();

        // Set arr_tmp_licenses
        screen_options.SetActive(false);
        screen_licenses.SetActive(true);
        arr_go = GameObject.FindGameObjectsWithTag("MenuOption");
        arr_go = arr_go.OrderBy(e => e.name).ToArray();
        arr_tmp_licenses = new TextMeshProUGUI[arr_go.Length];
        for (i = 0; i < arr_go.Length; ++i)
            arr_tmp_licenses[i] = arr_go[i].GetComponent<TextMeshProUGUI>();

        // Only activate the main screen
        screen_licenses.SetActive(false);
        screen_main.SetActive(true);

        // When the soft starts, there is no ongoing game, so disable the first option ("Resume Current Game")
        DisableFirstMainMenuOption();
        index_option = min_index_option;
        is_first_game = true;

        // If user had started a game and then selects "New Game" again, the new game needs to start immediately
        if (user_asked_for_restart)
        {
            audio_manager.Play("MenuValidate");
            ResumeGame();
            // A game starting also implies that "Resume Current Game" needs to be enabled
            EnableFirstMainMenuOption();
        }
        else
            OpenMainMenu();
    }

    public static void OpenMainMenu()
    {
        if (!is_first_game)
            audio_manager.Play("MenuBack");
        // Pause the game
        Time.timeScale = 0;
        // Activate the menu camera
        menu_camera.enabled = true;
        // Deactivate the UI camera so it doesn't show in the menu
        ui_camera.enabled = false;
        index_option = min_index_option;
        StopGameAmbience();
        audio_manager.Play("MenuTheme");
    }

    private static void ResumeGame()
    {
        is_first_game = false;
        // Resume the game
        Time.timeScale = 1;
        // Deactivate the menu camera
        menu_camera.enabled = false;
        // Reactivate the UI camera for the game
        ui_camera.enabled = true;
        // Reset the menu option selector
        index_option = min_index_option;
        audio_manager.Stop("MenuTheme");
        PlayGameAmbience();
    }

    private static void PlayGameAmbience()
    {
        audio_manager.Play("GameAmbiencePulse");
        audio_manager.Play("GameAmbienceForest");
        audio_manager.Play("GameAmbienceCreeper");
    }

    private static void StopGameAmbience()
    {
        audio_manager.Stop("GameAmbiencePulse");
        audio_manager.Stop("GameAmbienceForest");
        audio_manager.Stop("GameAmbienceCreeper");
    }

    public static void DisableFirstMainMenuOption()
    {
        arr_tmp_main[0].enabled = false;
        min_index_option = 1;
    }

    private static void EnableFirstMainMenuOption()
    {
        arr_tmp_main[0].enabled = true;
        min_index_option = 0;
    }

    public static void SetGraphicsForSelectedOption(string menu)
    {
        TextMeshProUGUI[] arr_tmp;
        if (menu == "options")
            arr_tmp = arr_tmp_options;
        else if (menu == "licenses")
            arr_tmp = arr_tmp_licenses;
        else
            arr_tmp = arr_tmp_main;

        // Set all options to white
        foreach (TextMeshProUGUI tmp in arr_tmp)
            tmp.color = new Color(1f, 1f, 1f, 1f);

        // Set the selected option to orange
        arr_tmp[index_option].color = new Color(0.65f, 0.19f, 0.08f, 1f);
    }

    public static void SelectUp(string menu)
    {
        int length, min;
        if (menu == "options")
        {
            length = arr_tmp_options.Length;
            min = 0;
        }
        else if (menu == "licenses")
        {
            length = arr_tmp_licenses.Length;
            min = 0;
        }
        else
        {
            length = arr_tmp_main.Length;
            min = min_index_option;
        }

        audio_manager.Play("MenuSelect");
        index_option = index_option > min ? index_option - 1 : length - 1;
    }

    public static void SelectDown(string menu)
    {
        int length, min;
        if (menu == "options")
        {
            length = arr_tmp_options.Length;
            min = 0;
        }
        else if (menu == "licenses")
        {
            length = arr_tmp_licenses.Length;
            min = 0;
        }
        else
        {
            length = arr_tmp_main.Length;
            min = min_index_option;
        }

        audio_manager.Play("MenuSelect");
        index_option = index_option < length - 1 ? index_option + 1 : min;
    }

    public static void ResumeCurrentGame()
    {
        audio_manager.Play("MenuForward");
        ResumeGame();
    }

    public static void NewGame()
    {
        audio_manager.Play("MenuValidate");
        if (!is_first_game)
        {
            user_asked_for_restart = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        ResumeGame();
        EnableFirstMainMenuOption();
    }

    public static void Quit()
    {
        audio_manager.Play("MenuBack");
        Application.Quit();
    }

    public static void OpenSubMenu(string menu)
    {
        audio_manager.Play("MenuValidate");
        screen_main.SetActive(false);

        if (menu == "options")
            screen_options.SetActive(true);
        else if (menu == "licenses")
            screen_licenses.SetActive(true);
        // Error, so just quit
        else
            Quit();

        index_option = 0;
    }

    public static void CloseSubMenu(string menu)
    {
        audio_manager.Play("MenuBack");

        if (menu == "options")
            screen_options.SetActive(false);
        else if (menu == "licenses")
            screen_licenses.SetActive(false);

        screen_main.SetActive(true);
        index_option = min_index_option;
    }

    public static void OpenLink(string link)
    {
        audio_manager.Play("MenuValidate");
        Application.OpenURL(link);
    }
}
