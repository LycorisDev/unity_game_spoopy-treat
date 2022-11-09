using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CollectCandy : MonoBehaviour
{
    private Character character_script;
    private TextMeshProUGUI tmp;
    private GameObject[] ui_candies;
    private Vector3 ui_candy_pos_displayed;
    private Vector3 ui_candy_pos_hidden;

    void Start()
    {
        tmp = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
        ui_candies = GameObject.FindGameObjectsWithTag("UICandy");
        ui_candies = ui_candies.OrderBy(e => e.name).ToArray();
        /*
            ui_candy_pos_displayed = ui_candies[0].transform.position;

            This can't be used because somehow "ui_candy_pos_displayed" is rewritten before ui_candies[3] is moved, 
            placing it outside of the canvas. Even making it a static field doesn't help, so hardcoded values are used instead.
        */
        ui_candy_pos_displayed = new Vector3(-4.83f, 183.15f, 109.17f);
        ui_candy_pos_hidden = ui_candies[1].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            character_script = other.GetComponent<Character>();
            if (character_script.nbr_candies < 3)
            {
                // Collect candy
                character_script.nbr_candies++;
                Destroy(gameObject);

                // Update candy counter
                if (other.CompareTag("Player"))
                    tmp.text = character_script.nbr_candies.ToString() + "/3";

                // Update UI candy
                foreach (GameObject candy in ui_candies)
                    candy.transform.position = ui_candy_pos_hidden;
                switch (character_script.nbr_candies)
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

                // Increase character mass
                other.GetComponent<Rigidbody>().mass += 2;
                
                // Increase character moving speeds
                character_script.directional_speed += 1f;
                character_script.rotate_speed = character_script.directional_speed / 2 * character_script.directional_speed * character_script.directional_speed;
                character_script.jump_force *= 1.5f;
            }
        }
    }
}
