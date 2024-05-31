using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSourceTrigger : MonoBehaviour
{
    [SerializeField] private int damage;

    public bool isPlayer;

    public void SetIsPlayer(bool val)
    {
        isPlayer = val;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null)
        {
            if (other.GetComponent<Health>().IsPlayer() == isPlayer) return;
            other.GetComponent<Health>().TakeDamage(damage, isPlayer);
        }

        Destroy(gameObject);
    }
}
