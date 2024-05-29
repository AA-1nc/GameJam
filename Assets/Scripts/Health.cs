using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isPlayer;

    public int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage, bool isPlayer)
    {
        //Enemy bullet on enemy or player bullet on player
        if (this.isPlayer == isPlayer) return;

        health -= damage;

        if (health <= 0)
            Die();
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}