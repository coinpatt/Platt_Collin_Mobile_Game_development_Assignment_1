using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturnToPoolOnCollision : MonoBehaviour
{
    public string poolTag = "Default";  // Pool tag for the object
    private ObjectPool objectPool;  // Reference to the ObjectPool instance

    void Start()
    {
        // Find the ObjectPool instance on the "ObjectPoolManager" GameObject
        GameObject objectPoolManager = GameObject.Find("ObjectPoolManager");
        if (objectPoolManager != null)
        {
            objectPool = objectPoolManager.GetComponent<ObjectPool>();
            if (objectPool == null)
            {
                Debug.LogError("ObjectPool component not found on ObjectPoolManager.");
            }
        }
        else
        {
            Debug.LogError("ObjectPoolManager GameObject not found in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnToPool(gameObject, poolTag);
            Debug.Log("Object returned to pool with tag: " + poolTag);
        }
        else
        {
            Debug.LogError("ObjectPool reference is missing.");
        }
    }
}
