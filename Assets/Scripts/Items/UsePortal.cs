using System.Collections;
using UnityEngine;

public class UsePortal : MonoBehaviour
{
    private static AudioManager audioManager;
    private static Character playerScript;
    private static Light lightComponent;
    private Color defaultColor, dullColor, candyColor;
    public bool isPortalFed;
    public int requiredNbrCandies;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        lightComponent = gameObject.GetComponentInChildren(typeof(Light)) as Light;
    }

    void Start()
    {
        defaultColor = lightComponent.color;
        dullColor = Color.gray;
        switch (requiredNbrCandies)
        {
            case 1:
                candyColor = Color.red;
                break;
            case 2:
                candyColor = Color.blue;
                break;
            case 3:
                candyColor = Color.yellow;
                break;
            default:
                candyColor = Color.black;
                break;
        }
    }

    void Update()
    {
        if (!isPortalFed)
        {
            float time = Mathf.PingPong(Time.time, 1f) / 1f;
            lightComponent.color = Color.Lerp(dullColor, candyColor, time);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPortalFed)
            {
                if (playerScript.nbrCandies >= requiredNbrCandies)
                {
                    playerScript.nbrCandies -= requiredNbrCandies;
                    isPortalFed = true;
                }
                else
                    yield break;
            }

            audioManager.Play("GamePortalOpening");
            yield return new WaitForSecondsRealtime(1f);

            // Version 0.0.0: End the game for now
            MenuManager.DisableFirstMainMenuOption();
            MenuManager.OpenMainMenu();
        }
    }
}
