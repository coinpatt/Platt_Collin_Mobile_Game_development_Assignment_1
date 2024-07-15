using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Health healthData;  // Reference to the Health scriptable object
    public float rewardSpawnChance = 50f;  // Public field for reward spawn chance (1-100)
    public string rewardPoolTag = "Reward";  // Tag of the reward objects in the object pool

    private float currentHealth;  // Current health of the enemy
    private PooledEnemy pooledEnemy;  // Reference to the PooledEnemy component
    private ObjectPool objectPool;  // Reference to the ObjectPool instance

    void Start()
    {
        // Initialize current health with max health from Health scriptable object
        currentHealth = healthData.maxHealth;
        
        // Get the PooledEnemy component
        pooledEnemy = GetComponent<PooledEnemy>();

        if (pooledEnemy == null)
        {
            Debug.LogError("PooledEnemy component not found on the object.");
        }

        // Find the ObjectPool instance on the "ObjectPoolManager" GameObject
        objectPool = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPool>();
        if (objectPool == null)
        {
            Debug.LogError("ObjectPool instance not found on the ObjectPoolManager GameObject.");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if health is zero or below
        if (currentHealth <= 0)
        {
            Die();  // Implement what happens when enemy dies
        }
    }

    void Die()
    {
        // Example: Implement death behavior (return enemy to the pool)
        if (pooledEnemy != null)
        {
            // Attempt to spawn a reward
            TrySpawnReward();

            // Destroy the enemy object and return it to the pool
            pooledEnemy.DestroyEnemy();
        }
        else
        {
            Debug.LogError("PooledEnemy component is not assigned or missing.");
        }
    }

    void TrySpawnReward()
    {
        // Check if objectPool is valid
        if (objectPool == null)
        {
            Debug.LogError("ObjectPool reference is missing.");
            return;
        }

        float roll = Random.Range(0f, 100f);
        if (roll < rewardSpawnChance)
        {
            GameObject reward = objectPool.SpawnFromPool(rewardPoolTag, transform.position, Quaternion.identity);
            if (reward == null)
            {
                Debug.LogError($"Failed to spawn reward with tag {rewardPoolTag} from the object pool.");
            }
        }
    }
}
