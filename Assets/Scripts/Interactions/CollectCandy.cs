using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    [SerializeField] private Sound _soundOneCollected;
    [SerializeField] private Sound _soundCollectionComplete;

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        HUDManager _hudScript;

        if (character != null && character.CandyAmount < character.MaxCandyAmount)
        {
            character.ModifyCandyAmount(1);

            if (other.CompareTag("Player"))
            {
                if (character.CandyAmount < character.MaxCandyAmount)
                    _soundOneCollected.Play();
                else
                    _soundCollectionComplete.Play();

                _hudScript = FindObjectOfType<HUDManager>();
                _hudScript.UpdateCandyCounter();
                _hudScript.UpdateCandyIcon();
            }

            character.IncreasePhysicalStats();
            Destroy(gameObject);
        }
    }
}
