
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [Header("Star Prefabs")]
    public GameObject starPrefab;
    public GameObject blueStarPrefab;
    public GameObject purpleStarPrefab;

    [Header("Planet Prefabs")]
    public GameObject marsPrefab;
    public GameObject venusPrefab;
    public GameObject neptunePrefab;

    [Header("Spawn Settings")]
    public float spawnRate = 1f;
    public float starSpeed = 1f;

    private float timer;

    public int currentLevel = 1;

    // Stops the game from spawning more objects
    public bool gameEnded = false;

    void Update()
    {
        // Stop spawning when the game ends
        if (gameEnded)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnStar();
            timer = 0f;
        }
    }

    void SpawnStar()
    {
        float randomX = Random.Range(-1.8f, 1.8f);

        Vector3 spawnPosition =
            new Vector3(randomX, 6f, 0);

        GameObject objectToSpawn = null;

        // LEVEL 1
        if (currentLevel == 1)
        {
            objectToSpawn = starPrefab;
        }

        // LEVEL 2
        else if (currentLevel == 2)
        {
            if (Random.value > 0.5f)
            {
                objectToSpawn = blueStarPrefab;
            }
            else
            {
                objectToSpawn = purpleStarPrefab;
            }
        }

        // LEVEL 3
        else
        {
            int randomPlanet = Random.Range(0, 3);

            if (randomPlanet == 0)
            {
                objectToSpawn = marsPrefab;
            }
            else if (randomPlanet == 1)
            {
                objectToSpawn = venusPrefab;
            }
            else
            {
                objectToSpawn = neptunePrefab;
            }
        }

        if (objectToSpawn == null)
            return;

        GameObject spawnedObject =
            Instantiate(
                objectToSpawn,
                spawnPosition,
                Quaternion.identity
            );

        StarFall fallingObject =
            spawnedObject.GetComponent<StarFall>();

        if (fallingObject != null)
        {
            fallingObject.fallSpeed = starSpeed;
        }
    }

    public void SetLevel2()
    {
        currentLevel = 2;
        starSpeed = 4f;
    }

    public void SetLevel3()
    {
        currentLevel = 3;
        starSpeed = 6f;
    }

    // Call this when the player wins or loses
    public void StopSpawning()
    {
        gameEnded = true;

        // Find and remove all falling stars and planets
        StarFall[] fallingObjects =
            FindObjectsByType<StarFall>(
                FindObjectsSortMode.None
            );

        foreach (StarFall fallingObject in fallingObjects)
        {
            Destroy(fallingObject.gameObject);
        }
    }
}
