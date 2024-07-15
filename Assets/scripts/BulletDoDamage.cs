using UnityEngine;

public class BulletDoDamage : MonoBehaviour
{
    private float damage;

    // Reference to BulletData ScriptableObject
    public BulletData bulletData;

    void Awake()
    {
        if (bulletData == null)
        {
            Debug.LogError("BulletData reference is not set in BulletDoDamage script.");
            return;
        }

        // Initialize damage from BulletData
        damage = bulletData.damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to enemy
            HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                // Example: Deactivate enemy directly if it doesn't have health system
                other.gameObject.SetActive(false);
            }

            // Disable the bullet after dealing damage
            gameObject.SetActive(false);
        }
    }
}
