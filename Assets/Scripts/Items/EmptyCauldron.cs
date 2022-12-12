using UnityEngine;

public class EmptyCauldron : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyCauldronPrefab;
    [SerializeField]
    private GameObject candyPrefab;
    private Vector3 candyPosition;

    void Update()
    {
        // If full caudron has fallen over
        if (transform.rotation.x < -0.4 || transform.rotation.x > 0.4 || transform.rotation.z < -0.4 || transform.rotation.z > 0.4)
        {
            // Drop candy in front of it (and a bit above ground so it doesn't clip)
            candyPosition = transform.position + Vector3.forward * 3;
            Instantiate(candyPrefab, candyPosition, transform.rotation);

            // Replace cauldron with empty version
            Instantiate(emptyCauldronPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
