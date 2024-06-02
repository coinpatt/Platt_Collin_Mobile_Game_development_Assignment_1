using System.Collections;
using UnityEngine;

public class SpawnBulletsOnEnemyCount : MonoBehaviour
{
    public float detectionRadius = 10f; // Radius to detect enemies
    public int enemyThreshold = 5; // Number of enemies to trigger bullet spawn
    public float spawnChancePercentage = 50f; // Percentage chance to trigger bullet spawn
    public GameObject bulletPrefab; // Bullet prefab to spawn
    public int numberOfBullets = 8; // Number of bullets to spawn in different directions
    public float bulletSpeed = 10f; // Speed of the bullets
    public string enemyTag = "Enemy"; // Tag of the enemy objects
    public float cooldownDuration = 5f; // Cooldown duration between spawns

    private bool canSpawn = true; // Flag to control spawning cooldown

    void Update()
    {
        CheckAndSpawnBullets();
    }

    void CheckAndSpawnBullets()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        int enemyCount = 0;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                enemyCount++;
            }
        }

        float randomChance = Random.Range(0f, 100f);
        if (enemyCount > enemyThreshold && randomChance <= spawnChancePercentage && canSpawn)
        {
            StartCoroutine(SpawnBulletsWithCooldown());
        }
    }

    IEnumerator SpawnBulletsWithCooldown()
    {
        canSpawn = false; // Disable spawning while on cooldown

        // Spawn bullets
        SpawnBullets();

        // Wait for cooldown duration
        yield return new WaitForSeconds(cooldownDuration);

        canSpawn = true; // Enable spawning after cooldown
    }

    void SpawnBullets()
    {
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float bulletDirX = Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            angle += angleStep;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
