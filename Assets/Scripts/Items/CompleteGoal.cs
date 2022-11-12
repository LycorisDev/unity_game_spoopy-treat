using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteGoal : MonoBehaviour
{
    private static AudioManager audio_manager;
    private static Character player_script;
    private static Behaviour light_component;

    void Start()
    {
        audio_manager = FindObjectOfType<AudioManager>();
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        light_component = (Behaviour)gameObject.GetComponent<Light>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player_script.nbr_candies == 3)
        {
            light_component.enabled = false;
            audio_manager.Play("GameVictory");
            yield return new WaitForSecondsRealtime(1);
            MenuManager.DisableFirstMenuOption();
            MenuManager.OpenMenu();
        }
    }
}
