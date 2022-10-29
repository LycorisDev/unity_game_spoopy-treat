using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Character"))
        {
            if (other.gameObject.GetComponent<Character>().nbr_candies < 3)
            {
                other.gameObject.GetComponent<Character>().nbr_candies++;
                other.gameObject.GetComponent<Rigidbody>().mass++;
                Destroy(gameObject);
            }
        }
    }
}
