using System.Collections.Generic;
using UnityEngine;

public class ObjectPool1 : MonoBehaviour
{
    // Singleton instance
    public static ObjectPool1 Instance { get; private set; }

    // Dictionary to hold object pools
    private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Ensure only one instance exists
    }

    // Method to spawn from object pool
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!objectPools.ContainsKey(tag))
        {
            Debug.LogWarning($"Object pool with tag '{tag}' does not exist.");
            return null;
        }

        if (objectPools[tag].Count == 0)
        {
            Debug.LogWarning($"Object pool with tag '{tag}' is empty.");
            return null;
        }

        GameObject objectToSpawn = objectPools[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    // Method to return object to pool
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!objectPools.ContainsKey(tag))
        {
            Debug.LogWarning($"Object pool with tag '{tag}' does not exist.");
            return;
        }

        objectToReturn.SetActive(false);
        objectPools[tag].Enqueue(objectToReturn);
    }

    // Method to add objects to pool
    public void AddToPool(string tag, GameObject objectToAdd)
    {
        if (!objectPools.ContainsKey(tag))
        {
            objectPools.Add(tag, new Queue<GameObject>());
        }

        objectToAdd.SetActive(false);
        objectPools[tag].Enqueue(objectToAdd);
    }
}
