using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public BulletData bulletData;    // Reference to the BulletData scriptable object
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    private ObjectPool objectPool;   // Reference to the ObjectPool

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();

        if (objectPool == null)
        {
            Debug.LogError("ObjectPool reference is not set in BulletPoolManager.");
            return;
        }

        // Subscribe to BulletData changes
        bulletData.OnBulletDataChanged.AddListener(UpdateBulletAttributes);

        // Update the bullet pool based on current BulletData
        UpdateBulletAttributes();
    }

    private void UpdateBulletAttributes()
    {
        foreach (var pool in objectPool.pools)
        {
            if (pool.prefab == bulletPrefab)
            {
                Queue<GameObject> bulletQueue = objectPool.poolDictionary[pool.tag];

                foreach (var bullet in bulletQueue)
                {
                    if (!bullet.activeInHierarchy)
                    {
                        BulletController bulletController = bullet.GetComponent<BulletController>();

                        if (bulletController != null)
                        {
                            bulletController.UpdateAttributes(bulletData);
                        }
                    }
                }
            }
        }
    }

    // Unsubscribe from events when the script is destroyed
    void OnDestroy()
    {
        if (bulletData != null)
        {
            bulletData.OnBulletDataChanged.RemoveListener(UpdateBulletAttributes);
        }
    }
}
