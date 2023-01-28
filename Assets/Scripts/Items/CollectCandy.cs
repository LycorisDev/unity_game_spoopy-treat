using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    private Character characterScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            characterScript = other.GetComponent<Character>();
            if (characterScript.nbrCandies < 3)
            {
                // Collect candy
                characterScript.nbrCandies++;
                Destroy(gameObject);

                if (other.CompareTag("Player"))
                {
                    HUDManager.PlayCandyCollectionSound();
                    HUDManager.UpdateCandyCounter();
                    HUDManager.UpdateCandyIcon();
                }

                // Increase character mass
                other.GetComponent<Rigidbody>().mass += 2;

                // Increase character moving speeds
                characterScript.directionalSpeed += 1f;
                characterScript.rotateSpeed = characterScript.directionalSpeed / 2 * characterScript.directionalSpeed * characterScript.directionalSpeed;
                characterScript.jumpForce *= 1.5f;
            }
        }
    }
}
