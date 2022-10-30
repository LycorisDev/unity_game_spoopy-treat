using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCandy : MonoBehaviour
{
    // Idle animation
    Vector3 position_offset;
    Vector3 temp_position;
    private float degrees_per_sec;
    private float amplitude;
    private float frequency;

    private Character character_script;
    private TextMeshProUGUI tmp;

    void Start()
    {
        position_offset = transform.position;
        position_offset.y += 1f;
        degrees_per_sec = 30f;
        amplitude = 0.5f;
        frequency = 0.8f;

        tmp = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degrees_per_sec, 0f), Space.World);
        temp_position = position_offset;
        temp_position.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = temp_position;
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
