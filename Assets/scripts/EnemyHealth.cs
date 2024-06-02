using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject gameObject; // Object to spawn
    [Range(0, 100)]
    public float spawnChance; // Chance of spawning (0 to 100)
}

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Starting health
    private float currentHealth;

    public SpawnableObject[] spawnObjects; // List of objects to spawn
    public float spawnDelay = 2f; // Delay before spawning object

    private Vector3 lastKnownPosition; // Last known position of the enemy

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            lastKnownPosition = transform.position; // Record the last known position
            StartCoroutine(DespawnAndSpawnObject());
        }
    }

    IEnumerator DespawnAndSpawnObject()
    {
        // Delay to ensure the object is spawned after the enemy is destroyed
        yield return new WaitForEndOfFrame();

        DespawnEnemy();
        yield return new WaitForSeconds(spawnDelay);

        SpawnObject();
    }

    void DespawnEnemy()
    {
        Debug.Log("Enemy has been despawned.");
        Destroy(gameObject); // Despawn enemy
    }

    void SpawnObject()
    {
        float totalSpawnChance = 0f;

        // Calculate total spawn chance
        foreach (SpawnableObject spawnObject in spawnObjects)
        {
            totalSpawnChance += spawnObject.spawnChance;
        }

        // Roll a random number
        float roll = Random.Range(0f, totalSpawnChance);
        Debug.Log("Roll: " + roll);

        // Check each object's chance to determine if it should be spawned
        foreach (SpawnableObject spawnObject in spawnObjects)
        {
            if (roll < spawnObject.spawnChance)
            {
                // Instantiate the object at the last known position of the enemy
                Debug.Log("Spawning: " + spawnObject.gameObject.name);
                Instantiate(spawnObject.gameObject, lastKnownPosition, Quaternion.identity);
                break;
            }
            else
            {
                roll -= spawnObject.spawnChance;
            }
        }
    }
}
