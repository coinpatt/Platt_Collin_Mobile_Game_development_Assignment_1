using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public GameObject bigBulletPrefab; // Prefab of the big bullet
    public float defaultShootingInterval = 1f; // Default time interval between shots
    public float bulletSpeed = 10f; // Speed of the bullet
    public float maxShootingDistance = 10f; // Maximum shooting distance

    private GameObject targetEnemy; // Closest enemy
    private float shootingInterval; // Current shooting interval
    private Coroutine shootFasterCoroutine; // Reference to the coroutine for shooting faster
    private Coroutine bigBulletCoroutine; // Reference to the coroutine for big bullet
    private GameObject currentBulletPrefab; // Current bullet prefab

    void Start()
    {
        shootingInterval = defaultShootingInterval;
        currentBulletPrefab = bulletPrefab; // Initialize with the default bullet prefab
        StartCoroutine(ShootAtInterval());
    }

    void Update()
    {
        // Find the closest enemy
        targetEnemy = FindClosestEnemy();
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
            GameObject bullet = Instantiate(currentBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }

    // Method to start shooting faster for a duration
    void StartShootingFaster(float duration)
    {
        if (shootFasterCoroutine != null)
        {
            StopCoroutine(shootFasterCoroutine);
        }
        shootFasterCoroutine = StartCoroutine(ShootingFasterCoroutine(duration));
    }

    IEnumerator ShootingFasterCoroutine(float duration)
    {
        shootingInterval = 0.05f; // Change the shooting interval
        yield return new WaitForSeconds(duration);
        shootingInterval = defaultShootingInterval; // Revert back to default interval
    }

    // Method to start shooting big bullets for a duration
    void StartShootingBigBullets(float duration)
    {
        if (bigBulletCoroutine != null)
        {
            StopCoroutine(bigBulletCoroutine);
        }
        bigBulletCoroutine = StartCoroutine(ShootingBigBulletsCoroutine(duration));
    }

    IEnumerator ShootingBigBulletsCoroutine(float duration)
    {
        currentBulletPrefab = bigBulletPrefab; // Change to big bullet prefab
        yield return new WaitForSeconds(duration);
        currentBulletPrefab = bulletPrefab; // Revert back to default bullet prefab
    }

    // Method to receive messages
    void ReceiveMessage(string message)
    {
        if (message == "ShootFaster")
        {
            StartShootingFaster(10f); // Start shooting faster for 10 seconds
        }
        else if (message == "BigBullet")
        {
            StartShootingBigBullets(10f); // Start shooting big bullets for 10 seconds
        }
    }
}