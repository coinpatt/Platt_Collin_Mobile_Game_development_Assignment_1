using System.Collections.Generic;
using UnityEngine;

public class AutomatedCharacterObstacleAvoidance : MonoBehaviour
{
    public float detectionRange = 10f; // Range within which to detect obstacles (enemies)
    public float avoidanceDistance = 5f; // Distance at which avoidance behavior starts
    public float safeDistance = 10f; // Safe distance from enemies where avoidance stops
    public float moveSpeed = 5f; // Maximum movement speed of the character
    public string obstacleTag = "Enemy"; // Tag for obstacles (e.g., enemies)

    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        AvoidObstacles();
    }

    void AvoidObstacles()
    {
        // Get all enemies within detection range
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(obstacleTag);

        if (enemies.Length > 0)
        {
            // Calculate the average position of enemies within camera view
            Vector2 averageEnemyPosition = Vector2.zero;
            int enemyCount = 0;

            foreach (GameObject enemy in enemies)
            {
                // Check if enemy is within camera view
                Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
                if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0)
                {
                    averageEnemyPosition += (Vector2)enemy.transform.position;
                    enemyCount++;
                }
            }

            if (enemyCount > 0)
            {
                averageEnemyPosition /= enemyCount;

                // Calculate the direction towards the area with the smallest concentration of enemies
                Vector2 avoidanceDirection = ((Vector2)transform.position - averageEnemyPosition).normalized;

                // Move the character in the avoidance direction with a constant speed
                transform.position = (Vector2)transform.position + avoidanceDirection * moveSpeed * Time.fixedDeltaTime;

                // Check distance to each enemy and adjust movement if too close
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < avoidanceDistance)
                    {
                        // Calculate avoidance vector
                        Vector2 directionAwayFromEnemy = ((Vector2)transform.position - (Vector2)enemy.transform.position).normalized;
                        // Move away from the enemy
                        transform.position = (Vector2)transform.position + directionAwayFromEnemy * moveSpeed * Time.fixedDeltaTime;
                    }
                }
            }
        }
    }
}
