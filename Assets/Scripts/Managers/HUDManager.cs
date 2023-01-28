using System.Linq;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private static AudioManager audioManager;
    private static TextMeshProUGUI tmp;
    private static Character playerScript;
    private static GameObject[] hudCandies;
    private static Vector3 hudCandyPosDisplayed, hudCandyPosHidden;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        tmp = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        hudCandies = GameObject.FindGameObjectsWithTag("HUDCandy");
    }

    void Start()
    {
        hudCandies = hudCandies.OrderBy(e => e.name).ToArray();
        hudCandyPosDisplayed = hudCandies[0].transform.position;
        hudCandyPosHidden = hudCandies[1].transform.position;
    }

    public static void PlayCandyCollectionSound()
    {
        // If candy is gained (and not lost)
        if (playerScript.nbrCandies == 3)
            audioManager.Play("GameCandyCollectionComplete");
        else
            audioManager.Play("GameCandyOneCollected");
    }

    public static void UpdateCandyCounter()
    {
        tmp.text = playerScript.nbrCandies.ToString() + "/3";
    }

    public static void UpdateCandyIcon()
    {
        // Hide all the versions
        foreach (GameObject candy in hudCandies)
            candy.transform.position = hudCandyPosHidden;

        // Display the right one
        switch (playerScript.nbrCandies)
        {
            case 1:
                hudCandies[1].transform.position = hudCandyPosDisplayed;
                break;
            case 2:
                hudCandies[2].transform.position = hudCandyPosDisplayed;
                break;
            case 3:
                hudCandies[3].transform.position = hudCandyPosDisplayed;
                break;
            default:
                hudCandies[0].transform.position = hudCandyPosDisplayed;
                break;
        }
    }
}
