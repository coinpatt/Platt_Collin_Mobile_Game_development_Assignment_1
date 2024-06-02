using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectSpawnerWithDistance : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn;  // List of objects that can be spawned
    [SerializeField] private int numberOfObjectsToSpawn;       // Number of objects to spawn
    [SerializeField] private Transform targetObject;           // The reference object to spawn around
    [SerializeField] private float minDistance;                // Minimum distance from the target object
    [SerializeField] private float maxDistance;                // Maximum distance from the target object

    public void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Get a random position around the target object
            Vector2 randomPosition = GetRandomPositionAroundTarget();
            // Instantiate a random object from the list
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionAroundTarget()
    {
        Vector2 randomPosition = Vector2.zero;
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, 2f * Mathf.PI);

        randomPosition.x = targetObject.position.x + distance * Mathf.Cos(angle);
        randomPosition.y = targetObject.position.y + distance * Mathf.Sin(angle);

        return randomPosition;
    }
}

