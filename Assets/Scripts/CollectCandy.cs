using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            if (other.GetComponent<Character>().nbr_candies < 3)
            {
                other.GetComponent<Character>().nbr_candies++;
                other.GetComponent<Rigidbody>().mass++;
                Destroy(gameObject);
            }
        }
    }
}
