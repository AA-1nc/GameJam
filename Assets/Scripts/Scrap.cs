using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] private int scrapAmount;
    [SerializeField] private int randomSpread;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            FindObjectOfType<MouseControls>().AddResources(scrapAmount + Random.Range(-randomSpread, randomSpread));
            Destroy(gameObject);
        }
    }
}
