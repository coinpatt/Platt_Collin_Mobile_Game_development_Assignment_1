using UnityEngine;

public class HealthScaleManager : MonoBehaviour
{
    public Health health; // Reference to the Health ScriptableObject
    public ObjectPool objectPoolManager; // Reference to the ObjectPool manager
    public string poolTag = "Enemy"; // Tag used to identify the object pool
    public float updateInterval = 1f; // Interval in seconds to update health based on scale

    private void Start()
    {
        // Ensure objectPoolManager is assigned
        if (objectPoolManager == null)
        {
            Debug.LogError("ObjectPool manager reference not set.");
            return;
        }

        // Start periodic updates
        InvokeRepeating(nameof(UpdateHealthBasedOnPooledObjects), 0f, updateInterval);
    }

    private void UpdateHealthBasedOnPooledObjects()
    {
        // Get a pooled object from the ObjectPool
        GameObject pooledObject = objectPoolManager.SpawnFromPool(poolTag, transform.position, Quaternion.identity);

        if (pooledObject != null)
        {
            // Get the scale of the pooled object
            Vector3 scale = pooledObject.transform.localScale;

            // Update the maxHealth in the ScriptableObject based on the scale
            health.UpdateMaxHealthBasedOnScale(scale.x); // Assuming uniform scale (x, y, z are the same)

            Debug.Log($"Updated maxHealth to: {health.maxHealth} based on pooled object scale.");

            // Return the object to the pool
            objectPoolManager.ReturnToPool(pooledObject, poolTag);
        }
        else
        {
            Debug.LogWarning("No pooled object available to update health.");
        }
    }
}
