using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 2;
    [SerializeField] private float spawnRange = 14;

    private float timePassed = 0;
    private float spawnTimer = 0;

    private void Update()
    {
        timePassed += Time.deltaTime / spawnTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
            SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < Mathf.CeilToInt(0.05f * timePassed * timePassed); i++)
        {
            int rand = Random.Range(0, 4);
            Vector3 randPos = Vector3.zero;
            if (rand == 0)
                randPos = new Vector3(spawnRange, 0, Random.Range(-spawnRange, spawnRange));
            if (rand == 1)
                randPos = new Vector3(-spawnRange, 0, Random.Range(-spawnRange, spawnRange));
            if (rand == 2)
                randPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, spawnRange);
            if (rand == 3)
                randPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, -spawnRange);
            Instantiate(enemyPrefab, randPos, Quaternion.identity);
        }
        spawnTimer = 0;
    }
}
