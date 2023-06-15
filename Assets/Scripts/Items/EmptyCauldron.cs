using UnityEngine;

public class EmptyCauldron : MonoBehaviour
{
    [SerializeField] private GameObject _emptyCauldronPrefab;
    [SerializeField] private GameObject _candyPrefab;
    private Vector3 _candyPosition;

    private void Update()
    {
        GameObject emptyCauldron;

        // If full caudron has fallen over
        if (transform.rotation.x < -0.4 || transform.rotation.x > 0.4 || transform.rotation.z < -0.4 || transform.rotation.z > 0.4)
        {
            // Replace cauldron with empty version
            emptyCauldron = Instantiate(_emptyCauldronPrefab, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);

            // Drop candy in front of it (and a bit above ground so it doesn't clip)
            _candyPosition = transform.position + Vector3.forward * 3;
            Instantiate(_candyPrefab, _candyPosition, transform.rotation, emptyCauldron.transform.parent);
        }
    }
}
