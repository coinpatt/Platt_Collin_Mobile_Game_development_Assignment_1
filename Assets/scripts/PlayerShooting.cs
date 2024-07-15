using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public string objectPoolTag = "Bullet"; // Default tag for the object pool on ObjectPoolManager
    private ObjectPool objectPool;
    public float maxShootingDistance = 10f;
    public BulletData currentBulletData;

    private GameObject targetEnemy;
    private float shootingInterval;
    private float lastSpawnRate;

    void Start()
    {
        FindObjectPoolManager();

        if (currentBulletData == null)
        {
            Debug.LogError("BulletData reference is not set in the PlayerShooting script.");
            return;
        }

        if (objectPool == null)
        {
            Debug.LogError("ObjectPool reference is not set or ObjectPoolManager not found.");
            return;
        }

        // Initialize shooting interval based on the initial spawn rate
        lastSpawnRate = currentBulletData.spawnRate;
        shootingInterval = 1f / lastSpawnRate;

        StartCoroutine(ShootAtInterval());
    }

    void Update()
    {
        targetEnemy = FindClosestEnemy();

        // Check for updates in BulletData
        if (currentBulletData.spawnRate != lastSpawnRate)
        {
            UpdateShootingInterval();
        }
    }

    void FindObjectPoolManager()
    {
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

    IEnumerator ShootAtInterval()
    {
        while (true)
        {
            if (targetEnemy != null && IsWithinMaxDistance(targetEnemy))
            {
                Shoot();
            }

            yield return new WaitForSeconds(shootingInterval);
        }
    }

    void UpdateShootingInterval()
    {
        lastSpawnRate = currentBulletData.spawnRate;
        shootingInterval = 1f / lastSpawnRate;
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    bool IsWithinMaxDistance(GameObject enemy)
    {
        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
        return distanceToEnemy <= maxShootingDistance;
    }

    void Shoot()
    {
        if (targetEnemy != null)
        {
            Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
            GameObject bullet = objectPool.SpawnFromPool(objectPoolTag, transform.position, Quaternion.identity);

            if (bullet == null)
            {
                Debug.LogError($"Failed to spawn bullet with tag {objectPoolTag} from the object pool.");
                return;
            }

            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.Initialize(direction, currentBulletData, objectPool, objectPoolTag);
            }
            else
            {
                Debug.LogError("BulletController component not found on the spawned bullet.");
            }
        }
    }
}
