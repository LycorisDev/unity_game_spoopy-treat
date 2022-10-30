using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCandy : MonoBehaviour
{
    private Character character_script;
    private TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            character_script = other.GetComponent<Character>();
            if (character_script.nbr_candies < 3)
            {
                character_script.nbr_candies++;
                other.GetComponent<Rigidbody>().mass++;
                Destroy(gameObject);

                if (other.CompareTag("Player"))
                    tmp.text = character_script.nbr_candies.ToString() + "/3";
            }
        }
    }
}
