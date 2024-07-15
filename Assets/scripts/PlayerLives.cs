using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Starting lives
    public GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        // Ensure the player has a Rigidbody2D component
        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        lives--;
        Debug.Log("Player Lives: " + lives);

        if (lives <= 0)
        {
            NotifyGameManager(); // Notify GameManager that player has no lives left
            DespawnPlayer();
        }
    }

    void NotifyGameManager()
    {
        if (gameManager != null)
        {
            gameManager.MainCharacterDestroyed(); // Notify GameManager
        }
        else
        {
            Debug.LogError("GameManager reference is not set in PlayerLives.");
        }
    }

    void DespawnPlayer()
    {
        Debug.Log("Player has been despawned.");
        Destroy(gameObject); // Despawn player
    }
}
