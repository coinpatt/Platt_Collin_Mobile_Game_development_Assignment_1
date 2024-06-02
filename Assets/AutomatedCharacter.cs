using UnityEngine;

public class AutomatedCharacter : MonoBehaviour
{
    public float detectionRange = 10f; // Range within which to detect reward objects and enemies
    public string targetTag = "Reward"; // Tag for reward objects
    public string obstacleTag = "Enemy"; // Tag for obstacles (e.g., enemies)
    public float moveSpeed = 5f; // Movement speed of the character
    public float avoidanceForce = 10f; // Force applied to avoid obstacles

    private GameObject currentTarget; // Current target (reward object)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(FindClosestReward), 0f, 1f); // Check for target every second
    }

    void FixedUpdate()
    {
        if (currentTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    void FindClosestReward()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestTarget = null;
            foreach (GameObject target in targets)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = target;
                }
            }
            currentTarget = closestTarget;
        }
        else
        {
            currentTarget = null;
        }
    }

    void MoveTowardsTarget()
    {
        if (currentTarget != null)
        {
            Vector2 targetPosition = currentTarget.transform.position;
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            // Check for obstacles (e.g., enemies)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionRange);
            if (hit.collider != null && hit.collider.CompareTag(obstacleTag))
            {
                // If an obstacle is detected and it's tagged as an enemy, steer away from it
                Vector2 avoidanceDirection = (transform.position - hit.collider.transform.position).normalized;
                rb.AddForce(avoidanceDirection * avoidanceForce, ForceMode2D.Force); // Apply avoidance force
            }

            // Move the character towards the target
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    // Draw the detection range gizmo in the Scene view for visualization
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
