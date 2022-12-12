using System.Linq;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    private static AudioManager audioManager;
    private static TextMeshProUGUI tmp;
    private static Character playerScript;
    private static GameObject[] uiCandies;
    private static Vector3 uiCandyPosDisplayed, uiCandyPosHidden;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        tmp = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        uiCandies = GameObject.FindGameObjectsWithTag("UICandy");
    }

    void Start()
    {
        uiCandies = uiCandies.OrderBy(e => e.name).ToArray();
        uiCandyPosDisplayed = uiCandies[0].transform.position;
        uiCandyPosHidden = uiCandies[1].transform.position;
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
        foreach (GameObject candy in uiCandies)
            candy.transform.position = uiCandyPosHidden;

        // Display the right one
        switch (playerScript.nbrCandies)
        {
            case 1:
                uiCandies[1].transform.position = uiCandyPosDisplayed;
                break;
            case 2:
                uiCandies[2].transform.position = uiCandyPosDisplayed;
                break;
            case 3:
                uiCandies[3].transform.position = uiCandyPosDisplayed;
                break;
            default:
                uiCandies[0].transform.position = uiCandyPosDisplayed;
                break;
        }
    }
}
