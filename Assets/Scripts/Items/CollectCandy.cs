using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    private Character character_script;

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

                if (other.CompareTag("Player"))
                {
                    GUIManager.PlayCandyCollectionSound();
                    GUIManager.UpdateCandyCounter();
                    GUIManager.UpdateCandyIcon();
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
