using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private static AudioManager audio_manager;
    private static Behaviour menu_camera;
    private static Behaviour ui_camera;
    private static TextMeshProUGUI[] arr_tmp;
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

        // Set arr_tmp
        arr_go = GameObject.FindGameObjectsWithTag("MainMenuOption");
        arr_go = arr_go.OrderBy(e => e.name).ToArray();
        arr_tmp = new TextMeshProUGUI[arr_go.Length];
        for (i = 0; i < arr_go.Length; ++i)
            arr_tmp[i] = arr_go[i].GetComponent<TextMeshProUGUI>();

        // When the soft starts, there is no ongoing game, so disable the first option ("Resume Current Game")
        DisableFirstMenuOption();
        index_option = min_index_option;
        is_first_game = true;

        // If user had started a game and then selects "New Game" again, the new game needs to start immediately
        if (user_asked_for_restart)
        {
            ResumeGame();
            // A game starting also implies that "Resume Current Game" needs to be enabled
            EnableFirstMenuOption();
        }
        else
            OpenMenu();
    }

    public static void OpenMenu()
    {
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

    public static void DisableFirstMenuOption()
    {
        arr_tmp[0].enabled = false;
        min_index_option = 1;
    }

    private static void EnableFirstMenuOption()
    {
        arr_tmp[0].enabled = true;
        min_index_option = 0;
    }

    public static void SetGraphicsForSelectedOption()
    {
        // Set all options to white
        foreach (TextMeshProUGUI tmp in arr_tmp)
            tmp.color = new Color(1f, 1f, 1f, 1f);

        // Set the selected option to orange
        arr_tmp[index_option].color = new Color(0.65f, 0.19f, 0.08f, 1f);
    }

    public static void SelectUp()
    {
        audio_manager.Play("MenuSelect");
        index_option = index_option > min_index_option ? index_option - 1 : arr_tmp.Length - 1;
    }

    public static void SelectDown()
    {
        audio_manager.Play("MenuSelect");
        index_option = index_option < arr_tmp.Length - 1 ? index_option + 1 : min_index_option;
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
        EnableFirstMenuOption();
    }

    public static void Options()
    {
        audio_manager.Play("MenuValidate");
    }

    public static void Licenses()
    {
        audio_manager.Play("MenuValidate");
    }

    public static void Quit()
    {
        audio_manager.Play("MenuBack");
        Application.Quit();
    }
}
