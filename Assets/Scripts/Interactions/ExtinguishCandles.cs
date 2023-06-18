using UnityEngine;

public class ExtinguishCandles : MonoBehaviour
{
    [SerializeField] private GameObject _unlitSkullPrefab;

    private void Update()
    {
        // If lit skull has fallen over
        if (transform.rotation.x < -0.5 || transform.rotation.x > 0.5 || transform.rotation.z < -0.5 || transform.rotation.z > 0.5)
        {
            // Replace with unlit version
            Instantiate(_unlitSkullPrefab, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }
}
