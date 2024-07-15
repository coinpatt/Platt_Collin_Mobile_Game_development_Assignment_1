using UnityEngine;

public class CoinClickTracker : MonoBehaviour
{
    public string poolTag = "Default";  // Pool tag for the object
    public int clickThreshold = 5;  // Number of clicks/taps required to return the coin to pool
    public CoinsCollected coinsCollected;  // Reference to the CoinsCollected ScriptableObject

    private ObjectPool objectPool;  // Reference to the ObjectPool instance
    private int currentClicks = 0;  // Counter for current number of clicks/taps

    void Start()
    {
        // Find the ObjectPool instance on the "ObjectPoolManager" GameObject
        GameObject objectPoolManager = GameObject.Find("ObjectPoolManager");
        if (objectPoolManager != null)
        {
            objectPool = objectPoolManager.GetComponent<ObjectPool>();
            if (objectPool == null)
            {
                Debug.LogError("ObjectPool component not found on ObjectPoolManager.");
            }
        }
        else
        {
            Debug.LogError("ObjectPoolManager GameObject not found in the scene.");
        }

        // Ensure the CoinsCollected reference is set in the Inspector
        if (coinsCollected == null)
        {
            Debug.LogError("CoinsCollected reference is not set in the Inspector.");
        }
    }

    void OnEnable()
    {
        currentClicks = 0;  // Reset click count when object is enabled
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is triggered by the main character (player)
        if (other.CompareTag("Player"))
        {
            HandleTrigger();
        }
    }

    void OnMouseDown()
    {
        // Handle click on the object
        HandleClick();
    }

    void HandleTrigger()
    {
        currentClicks++;

        if (currentClicks >= clickThreshold)
        {
            // Return the parent object to the object pool
            ReturnParentToPool();
        }
    }

    void HandleClick()
    {
        currentClicks++;

        if (currentClicks >= clickThreshold)
        {
            // Return the parent object to the object pool
            ReturnParentToPool();
        }
    }

    void ReturnParentToPool()
    {
        // Get the parent object (coin) of this child object
        GameObject parentObject = transform.parent.gameObject;

        if (objectPool != null)
        {
            objectPool.ReturnToPool(parentObject, poolTag);
            Debug.Log("Object returned to pool with tag: " + poolTag);

            // Update coins collected in ScriptableObject
            if (coinsCollected != null)
            {
                coinsCollected.coins++;  // Increment coins collected
                Debug.Log("Coins collected: " + coinsCollected.coins);
            }
            else
            {
                Debug.LogError("CoinsCollected ScriptableObject reference is missing.");
            }
        }
        else
        {
            Debug.LogError("ObjectPool reference is missing.");
        }
    }
}