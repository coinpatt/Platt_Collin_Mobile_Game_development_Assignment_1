using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Default damage
    public int maxCollisions = 3; // Maximum number of collisions before despawning
    public float lifespan = 5f; // Lifespan in seconds before despawning

    private int collisionCount = 0;

    void Start()
    {
        // Start the coroutine to handle lifespan
        StartCoroutine(DestroyAfterTime());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            collisionCount++;
            if (collisionCount >= maxCollisions)
            {
                Destroy(gameObject); // Destroy the bullet after max collisions
            }
        }
        else
        {
            // Count non-enemy collisions as well
            collisionCount++;
            if (collisionCount >= maxCollisions)
            {
                Destroy(gameObject); // Destroy the bullet after max collisions
            }
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject); // Destroy the bullet after lifespan
    }

    // Method to upgrade bullet damage
    public void UpgradeDamage(float additionalDamage)
    {
        damage += additionalDamage;
    }

    // Method to upgrade max collisions
    public void UpgradeMaxCollisions(int additionalCollisions)
    {
        maxCollisions += additionalCollisions;
    }

    // Method to upgrade lifespan
    public void UpgradeLifespan(float additionalLifespan)
    {
        lifespan += additionalLifespan;
    }
}
