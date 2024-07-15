using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab of the object to spawn
    public int numberOfItemsToSpawn = 1; // Number of items to spawn each interval
    public float spawnInterval = 3f; // Interval between spawns in seconds
    public Camera mainCamera; // Reference to the main camera

    private float timer; // Timer to track spawn interval

    void Start()
    {
        timer = spawnInterval; // Start timer at spawn interval
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero or below, spawn objects
        if (timer <= 0f)
        {
            SpawnRandomObjects();
            timer = spawnInterval; // Reset timer
        }
    }

    void SpawnRandomObjects()
    {
        // Calculate camera bounds in world space
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        float camMinX = mainCamera.transform.position.x - camWidth / 2f;
        float camMaxX = mainCamera.transform.position.x + camWidth / 2f;
        float camMinY = mainCamera.transform.position.y - camHeight / 2f;
        float camMaxY = mainCamera.transform.position.y + camHeight / 2f;

        // Spawn objects randomly within camera view
        for (int i = 0; i < numberOfItemsToSpawn; i++)
        {
            // Get a random position within camera bounds
            Vector3 spawnPosition = new Vector3(
                Random.Range(camMinX, camMaxX),
                Random.Range(camMinY, camMaxY),
                0f
            );

            // Instantiate an object from the prefab
            GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Activate the object
            obj.SetActive(true);
        }
    }
}
