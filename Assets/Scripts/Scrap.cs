using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scrap : MonoBehaviour
{
    [SerializeField] private int scrapAmount;
    [SerializeField] private int randomSpread;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            FindObjectOfType<MouseControls>().resources += scrapAmount + Random.Range(-randomSpread, randomSpread);
            Destroy(gameObject);
        }
    }
}
