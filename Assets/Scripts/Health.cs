using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isPlayer;
    [SerializeField] private int scrapDropAmount = 3;
    [SerializeField] private GameObject scrapPrefab;

    public int health;
    bool dead = false;
    
    private void Awake()
    {
        health = maxHealth;
        if (isPlayer)
        {
            FindObjectOfType<MouseControls>().DisplayCurrentHealth(health);
        }
    }

    public void TakeDamage(int damage, bool isPlayer)
    {
        //Enemy bullet on enemy or player bullet on player
        if (this.isPlayer == isPlayer) return;

        health -= damage;
         if (isPlayer)
        {
            FindObjectOfType<MouseControls>().DisplayCurrentHealth(health);
        }

        if (health <= 0 && !dead)
            Die();
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }

    

    public void Die()
    {
        dead = true;

        if (!isPlayer)
        {
            float dropDist = 2;

            for (int i = 0; i < scrapDropAmount; i++)
            {
                Instantiate(scrapPrefab, transform.position + new Vector3(Random.Range(-dropDist, dropDist), 0, Random.Range(-dropDist, dropDist)), Quaternion.Euler(0, Random.Range(0, 360), 0));
            }
        }

        Destroy(gameObject);
   

        if (isPlayer == dead)
        {
            SceneManager.LoadScene("Credits Proper");
        }
    }
    
}