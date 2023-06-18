using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character characterScript = other.GetComponent<Character>();
        HUDManager _hudScript;

        if (characterScript != null && characterScript.CandyAmount < 3)
        {
            characterScript.ModifyCandyAmount(1);
            Destroy(gameObject);

            if (other.CompareTag("Player"))
            {
                _hudScript = FindObjectOfType<HUDManager>();
                _hudScript.PlayCandyCollectionSound();
                _hudScript.UpdateCandyCounter();
                _hudScript.UpdateCandyIcon();
            }

            characterScript.IncreasePhysicalStats();
        }
    }
}
