using UnityEngine;

public class AutomatedCharacterMovementTowardsTarget : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the character
    public string targetTag = "Reward"; // Tag of the target object to move towards

    private GameObject targetObject; // Current target object

    void Start()
    {
        FindTargetObject();
    }

    void Update()
    {
        FindTargetObject();

        if (targetObject != null)
        {
            Vector2 targetPosition = targetObject.transform.position;
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            // Move the character towards the target
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
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
}
