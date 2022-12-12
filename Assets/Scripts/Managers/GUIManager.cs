using System.Linq;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    private static AudioManager audio_manager;
    private static TextMeshProUGUI tmp;
    private static Character player_script;
    private static GameObject[] ui_candies;
    private static Vector3 ui_candy_pos_displayed;
    private static Vector3 ui_candy_pos_hidden;

    void Awake()
    {
        audio_manager = FindObjectOfType<AudioManager>();
        tmp = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        ui_candies = GameObject.FindGameObjectsWithTag("UICandy");
    }

    void Start()
    {
        ui_candies = ui_candies.OrderBy(e => e.name).ToArray();
        ui_candy_pos_displayed = ui_candies[0].transform.position;
        ui_candy_pos_hidden = ui_candies[1].transform.position;
    }

    public static void PlayCandyCollectionSound()
    {
        // If candy is gained (and not lost)
        if (player_script.nbr_candies == 3)
            audio_manager.Play("GameCandyCollectionComplete");
        else
            audio_manager.Play("GameCandyOneCollected");
    }

    public static void UpdateCandyCounter()
    {
        tmp.text = player_script.nbr_candies.ToString() + "/3";
    }

    public static void UpdateCandyIcon()
    {
        // Hide all the versions
        foreach (GameObject candy in ui_candies)
            candy.transform.position = ui_candy_pos_hidden;

        // Display the right one
        switch (player_script.nbr_candies)
        {
            case 1:
                ui_candies[1].transform.position = ui_candy_pos_displayed;
                break;
            case 2:
                ui_candies[2].transform.position = ui_candy_pos_displayed;
                break;
            case 3:
                ui_candies[3].transform.position = ui_candy_pos_displayed;
                break;
            default:
                ui_candies[0].transform.position = ui_candy_pos_displayed;
                break;
        }
    }
}
