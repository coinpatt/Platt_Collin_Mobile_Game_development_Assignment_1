using UnityEngine;

public class PooledEnemy : MonoBehaviour
{
    public string poolTag;  // Tag of the pool this object belongs to
    private ObjectPool objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();  // Find the ObjectPool instance
    }

    public void DestroyEnemy()
    {
        if (objectPool != null)
        {
            objectPool.ReturnToPool(gameObject, poolTag);  // Return the enemy to the pool
        }
    }
}
