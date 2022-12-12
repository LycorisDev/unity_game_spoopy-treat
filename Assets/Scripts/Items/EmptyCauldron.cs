using UnityEngine;

public class EmptyCauldron : MonoBehaviour
{
    [SerializeField]
    private GameObject empty_cauldron_prefab;
    [SerializeField]
    private GameObject candy_prefab;
    private Vector3 candy_position;

    void Update()
    {
        // If full caudron has fallen over
        if (transform.rotation.x < -0.4 || transform.rotation.x > 0.4 || transform.rotation.z < -0.4 || transform.rotation.z > 0.4)
        {
            // Drop candy in front of it (and a bit above ground so it doesn't clip)
            candy_position = transform.position + Vector3.forward * 3;
            Instantiate(candy_prefab, candy_position, transform.rotation);

            // Replace cauldron with empty version
            Instantiate(empty_cauldron_prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
