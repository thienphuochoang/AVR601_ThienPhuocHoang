using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;   // drag 1 or more enemy prefabs here

    [Header("Spawn Settings")]
    public float minSpawnTime = 1.5f;
    public float maxSpawnTime = 3f;
    public float spawnY = 7f;           // above camera top edge
    public float spawnXMin = -2.2f;     // match your background bounds
    public float spawnXMax =  2.2f;

    [Header("Difficulty")]
    public float difficultyInterval = 15f;  // speed up every 15 seconds
    public float spawnTimeReduction = 0.2f; // reduce interval by this amount

    void Start()
    {
        StartCoroutine(SpawnLoop());
        StartCoroutine(IncreaseDifficulty());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(wait);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Pick random X within play area
        float x = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPos = new Vector3(x, spawnY, 0);

        // Pick a random enemy type
        int index = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);
    }

    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyInterval);

            // Reduce spawn interval (faster spawns over time)
            minSpawnTime = Mathf.Max(0.5f, minSpawnTime - spawnTimeReduction);
            maxSpawnTime = Mathf.Max(1f,   maxSpawnTime - spawnTimeReduction);
        }
    }
}