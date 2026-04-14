using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Meteor Prefabs")]
    public GameObject[] meteorPrefabs;

    [Header("Spawn Settings")]
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    [Header("Boundaries — match your background")]
    public SpriteRenderer backgroundSprite;

    private float minX, maxX, topY, midY;

    void Start()
    {
        // Calculate boundaries
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;

        if (backgroundSprite != null)
        {
            Bounds bg = backgroundSprite.bounds;
            minX = bg.min.x;
            maxX = bg.max.x;
        }
        else
        {
            float camWidth = camHeight * cam.aspect;
            minX = -camWidth;
            maxX =  camWidth;
        }

        topY = camHeight;        // top edge of camera
        midY = 0f;               // middle of screen

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(wait);
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        int zone = Random.Range(0, 3);
        Vector3 spawnPos;
        Vector2 direction;

        switch (zone)
        {
            case 0: // top — straight down with slight random angle
                spawnPos = new Vector3(
                    Random.Range(minX, maxX),
                    topY + 0.5f,
                    0);
                direction = new Vector2(Random.Range(-0.3f, 0.3f), -1f);
                break;

            case 1: // left side — move right and down diagonally
                spawnPos = new Vector3(
                    minX - 0.5f,
                    Random.Range(midY, topY),
                    0);
                direction = new Vector2(Random.Range(0.3f, 0.6f), -1f);
                break;

            case 2: // right side — move left and down diagonally
                spawnPos = new Vector3(
                    maxX + 0.5f,
                    Random.Range(midY, topY),
                    0);
                direction = new Vector2(Random.Range(-0.6f, -0.3f), -1f);
                break;

            default:
                spawnPos = new Vector3(0, topY, 0);
                direction = Vector2.down;
                break;
        }

        int index = Random.Range(0, meteorPrefabs.Length);
        GameObject meteor = Instantiate(meteorPrefabs[index], spawnPos, Quaternion.identity);
    
        // Set the direction on the meteor
        meteor.GetComponent<Meteor>()?.SetDirection(direction);
    }
}