using System.Collections;
using UnityEngine;

public class SpawnBulletsOnAwake : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public float bulletSpeed = 10f; // Speed of the bullet
    public int bulletCount = 8; // Number of bullets to spawn
    public float spawnRadius = 1f; // Radius from the center of the object
    public float cooldown = 1f; // Cooldown between bullet spawns

    private bool canShoot = true; // Can shoot flag

    void Awake()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned!");
            enabled = false; // Disable the script
        }
        else
        {
            StartCoroutine(ShootBullets());
        }
    }

    IEnumerator ShootBullets()
    {
        while (true)
        {
            if (canShoot)
            {
                SpawnBullets();
                canShoot = false;
                yield return new WaitForSeconds(cooldown);
                canShoot = true;
            }
            yield return null;
        }
    }

    void SpawnBullets()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            SpawnBullet(direction);
        }
    }

    void SpawnBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody2D component!");
        }
    }
}
