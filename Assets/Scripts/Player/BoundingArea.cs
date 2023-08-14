using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    private Vector3 _spawnPoint;

    private void Awake()
    {
        _spawnPoint = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BoundingArea")
            transform.position = _spawnPoint;
    }
}
