using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    [SerializeField] private SoundObject _soundCandyCollectionComplete;
    [SerializeField] private SoundObject _soundCandyOneCollected;

    private void Start()
    {
        AudioManager.Instance.AddAudioSource(_soundCandyCollectionComplete, gameObject);
        AudioManager.Instance.AddAudioSource(_soundCandyOneCollected, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        HUDManager _hudScript;

        if (character != null && character.CandyAmount < 3)
        {
            character.ModifyCandyAmount(1);

            if (other.CompareTag("Player"))
            {
                PlayCandyCollectionSound(character.CandyAmount >= character.MaxCandyAmount);

                _hudScript = FindObjectOfType<HUDManager>();
                _hudScript.UpdateCandyCounter();
                _hudScript.UpdateCandyIcon();
            }

            character.IncreasePhysicalStats();
            Destroy(gameObject);
        }
    }

    private void PlayCandyCollectionSound(bool isCollectionComplete)
    {
        if (isCollectionComplete)
            _soundCandyCollectionComplete.source.Play();
        else
            _soundCandyOneCollected.source.Play();
    }
}
