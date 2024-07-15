using System.Collections;
using UnityEngine;

public class AutomaticObjectSpawner : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;      // Scriptable object reference
    private ObjectPool objectPool;    // Reference to ObjectPool instance
    public string objectPoolTag = "ObjectPoolTag"; // Tag name of the ObjectPool
    public string objectPoolManagerName = "ObjectPoolManager"; // Name of the ObjectPoolManager GameObject
    public float spawnInterval = 3f; // Interval between spawns in seconds
    private bool canSpawnEnemies = true;  // Flag to check if spawning is allowed

    private void Start()
    {
        // Find the ObjectPoolManager instance based on its name
        GameObject objectPoolManager = GameObject.Find(objectPoolManagerName);
        if (objectPoolManager != null)
        {
            // Get the ObjectPool component from the ObjectPoolManager using the tag
            objectPool = objectPoolManager.GetComponent<ObjectPool>();
            if (objectPool == null)
            {
                Debug.LogError($"ObjectPool with tag '{objectPoolTag}' not found on {objectPoolManagerName}.");
            }
            else
            {
                Debug.Log($"Automatic ObjectSpawner initialized with ObjectPool from {objectPoolManagerName} with tag '{objectPoolTag}'.");
            }
        }
        else
        {
            Debug.LogError($"ObjectPoolManager GameObject with name '{objectPoolManagerName}' not found.");
        }

        // Start spawning enemies automatically
        StartCoroutine(SpawnEnemiesPeriodically());
    }

    private IEnumerator SpawnEnemiesPeriodically()
    {
        while (canSpawnEnemies)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyData.numberOfEnemiesToSpawn; i++)
        {
            Vector2 randomPosition = GetRandomPositionAroundTarget();
            SpawnEnemy(randomPosition);
        }
    }

    private void SpawnEnemy(Vector2 position)
    {
        if (enemyData.enemyPrefab == null || objectPool == null)
        {
            Debug.LogError("Enemy prefab or ObjectPool is not set.");
            return;
        }

        GameObject enemy = objectPool.SpawnFromPool(objectPoolTag, position, Quaternion.identity);

        if (enemy == null)
        {
            Debug.LogError($"Failed to spawn enemy with tag {objectPoolTag} from the object pool.");
            return;
        }

        enemy.transform.position = position; // Set the enemy's position
    }

    private Vector2 GetRandomPositionAroundTarget()
    {
        Vector2 randomPosition = Vector2.zero;
        float distance = Random.Range(enemyData.minDistance, enemyData.maxDistance);
        float angle = Random.Range(0f, 2f * Mathf.PI);

        randomPosition.x = transform.position.x + distance * Mathf.Cos(angle);
        randomPosition.y = transform.position.y + distance * Mathf.Sin(angle);

        return randomPosition;
    }

    public void StartSpawning()
    {
        canSpawnEnemies = true;
        StartCoroutine(SpawnEnemiesPeriodically());
    }

    public void StopSpawning()
    {
        canSpawnEnemies = false;
        StopCoroutine(SpawnEnemiesPeriodically());
    }
}
