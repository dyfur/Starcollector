using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject blueStarPrefab;
    public GameObject purpleStarPrefab;

    public GameObject marsPrefab;
    public GameObject venusPrefab;
    public GameObject neptunePrefab;

    public float spawnRate = 1f;
    public float starSpeed = 1f;

    private float timer;

    public int currentLevel = 1;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnStar();
            timer = 0f;
        }
    }

    void SpawnStar()
    {
        // Portrait scherm -> kleiner speelgebied
        float randomX = Random.Range(-1.8f, 1.8f);

        Vector3 spawnPosition = new Vector3(randomX, 6f, 0);

        GameObject objectToSpawn;

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

        GameObject spawnedObject =
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        spawnedObject.GetComponent<StarFall>().fallSpeed = starSpeed;
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
}