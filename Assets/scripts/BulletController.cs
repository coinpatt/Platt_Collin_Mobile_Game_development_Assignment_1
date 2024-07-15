using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    private float speed;
    private float size;
    private float damage;
    private int maxCollisions;  // Current remaining collisions
    private float lifespan;

    public void Initialize(Vector2 dir, BulletData data, ObjectPool pool, string tag)
    {
        UpdateAttributes(data);
        SetVelocity(dir);
        StartCoroutine(HandleLifespan());
    }

    public void UpdateAttributes(BulletData data)
    {
        speed = data.speed;
        size = data.size;
        damage = data.damage;
        maxCollisions = data.maxCollisions;
        lifespan = data.lifespan;

        // Apply size to the bullet
        transform.localScale = new Vector3(size, size, 1f);
    }

    private void SetVelocity(Vector2 dir)
    {
        Vector2 direction = dir.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy here if needed

            maxCollisions--; // Reduce the remaining collisions

            if (maxCollisions <= 0)
            {
                ReturnToPool();
            }
        }
    }

    void ReturnToPool()
    {
        gameObject.SetActive(false);
        ObjectPool.Instance.ReturnToPool(gameObject, GetComponent<PooledEnemy>().poolTag);
    }

    private IEnumerator HandleLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        ReturnToPool();
    }
}
