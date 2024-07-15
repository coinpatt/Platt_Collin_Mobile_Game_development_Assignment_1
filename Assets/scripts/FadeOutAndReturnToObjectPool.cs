using System.Collections;
using UnityEngine;

public class FadeOutAndReturnToObjectPool : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of the fade in seconds
    public string objectPoolTag = "ObjectPool"; // Tag used to find the ObjectPool in the scene
    public string poolTag; // Tag used to identify the pool in ObjectPool
    public float interval = 1f; // Interval in seconds to add additional time
    public float additionalTime = 1f; // Additional time to add every interval

    private SpriteRenderer spriteRenderer;
    private ObjectPool objectPool;
    private float originalFadeDuration;

    void Awake()
    {
        // Access the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if spriteRenderer is assigned
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }

        // Find the ObjectPool in the scene by tag
        GameObject objectPoolObject = GameObject.FindWithTag(objectPoolTag);
        if (objectPoolObject != null)
        {
            objectPool = objectPoolObject.GetComponent<ObjectPool>();
            if (objectPool == null)
            {
                Debug.LogError("ObjectPool component not found on the GameObject with the specified tag.");
            }
        }
        else
        {
            Debug.LogError("ObjectPool GameObject with the specified tag not found in the scene.");
        }

        originalFadeDuration = fadeDuration;
    }

    void OnEnable()
    {
        // Reset fade duration to original value
        fadeDuration = originalFadeDuration;

        // Start the fade out process
        StartCoroutine(FadeOut());

        // Start the coroutine to add time at intervals
        StartCoroutine(AddTimeAtIntervals());
    }

    IEnumerator FadeOut()
    {
        // Get the initial color of the sprite
        Color initialColor = spriteRenderer.color;
        Color finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        // Iterate over the fade duration
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            // Lerp the alpha value
            spriteRenderer.color = Color.Lerp(initialColor, finalColor, t / fadeDuration);
            yield return null;
        }

        // Ensure the final color is set after the loop
        spriteRenderer.color = finalColor;

        // Return the object to the pool
        if (objectPool != null)
        {
            objectPool.ReturnToPool(gameObject, poolTag);
        }
        else
        {
            Debug.LogError("ObjectPool reference not set.");
        }
    }

    IEnumerator AddTimeAtIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            fadeDuration += additionalTime;
        }
    }
}
