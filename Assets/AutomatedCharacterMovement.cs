using System.Collections.Generic;
using UnityEngine;

public class AutomatedCharacterMovement : MonoBehaviour
{
    public float detectionRange = 10f; // Range within which to detect obstacles (enemies)
    public float avoidanceDistance = 5f; // Distance at which avoidance behavior starts
    public float safeDistance = 10f; // Safe distance from enemies where avoidance stops
    public float moveSpeed = 5f; // Maximum movement speed of the character
    public string obstacleTag = "Enemy"; // Tag for obstacles (e.g., enemies)
    public string targetTag = "Reward"; // Tag of the target object to move towards

    private Camera mainCamera; // Reference to the main camera
    private GameObject targetObject; // Current target object

    void Start()
    {
        mainCamera = Camera.main;
        FindTargetObject();
    }

    void Update()
    {
        FindTargetObject();
    }

    void FixedUpdate()
    {
        MoveTowardsTargetAndAvoidObstacles();
    }

    void FindTargetObject()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length > 0)
        {
            // Find the closest target object
            float closestDistance = Mathf.Infinity;
            foreach (GameObject target in targets)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    targetObject = target;
                }
            }
        }
        else
        {
            Debug.LogWarning("No objects with tag " + targetTag + " found.");
            targetObject = null;
        }
    }

    void MoveTowardsTargetAndAvoidObstacles()
    {
        Vector2 moveDirection = Vector2.zero;

        // Move towards the target if one exists
        if (targetObject != null)
        {
            moveDirection = (targetObject.transform.position - transform.position).normalized;
        }

        // Get all enemies within detection range
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(obstacleTag);
        GameObject closestEnemy = null;
        float closestEnemyDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestEnemyDistance)
            {
                closestEnemyDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestEnemyDistance < detectionRange)
        {
            // Avoid the closest enemy if it's within the detection range
            Vector2 avoidanceVector = (Vector2)transform.position - (Vector2)closestEnemy.transform.position;
            moveDirection += avoidanceVector.normalized;
        }

        // Normalize the final direction to ensure constant speed
        moveDirection = moveDirection.normalized;

        // Move the character with the final combined direction
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}

