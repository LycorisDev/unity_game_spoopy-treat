using System.Collections;
using UnityEngine;

public class CompleteGoal : MonoBehaviour
{
    private static AudioManager audio_manager;
    private static Character player_script;
    private static Light light_component;
    private Color default_color, dull_color, candy_color;
    public bool is_goal_fed;
    public int required_nbr_candies;

    void Start()
    {
        audio_manager = FindObjectOfType<AudioManager>();
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        light_component = gameObject.GetComponent<Light>();

        default_color = light_component.color;
        dull_color = Color.gray;
        switch (required_nbr_candies)
        {
            case 1:
                candy_color = Color.red;
                break;
            case 2:
                candy_color = Color.blue;
                break;
            case 3:
                candy_color = Color.yellow;
                break;
            default:
                candy_color = Color.black;
                break;
        }
    }

    void Update()
    {
        if (!is_goal_fed)
        {
            float time = Mathf.PingPong(Time.time, 1f) / 1f;
            light_component.color = Color.Lerp(dull_color, candy_color, time);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!is_goal_fed)
            {
                if (player_script.nbr_candies >= required_nbr_candies)
                {
                    player_script.nbr_candies -= required_nbr_candies;
                    is_goal_fed = true;
                }
                else
                    yield break;
            }

            audio_manager.Play("GameVictory");
            yield return new WaitForSecondsRealtime(1f);

            // Version 0.0.0: End the game for now
            MenuManager.DisableFirstMainMenuOption();
            MenuManager.OpenMainMenu();
        }
    }
}
