using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;  // Singleton instance

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;  // Set the singleton instance
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.AddComponent<PooledEnemy>().poolTag = pool.tag;  // Attach PooledEnemy script and set poolTag
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = null;

        if (poolDictionary[tag].Count > 0 && !poolDictionary[tag].Peek().activeInHierarchy)
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
        }
        else
        {
            Pool pool = pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                objectToSpawn = Instantiate(pool.prefab);
                objectToSpawn.SetActive(false);
                objectToSpawn.AddComponent<PooledEnemy>().poolTag = pool.tag;  // Attach PooledEnemy script and set poolTag
            }
            else
            {
                Debug.LogWarning("Pool with tag " + tag + " not found in the list.");
                return null;
            }
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
