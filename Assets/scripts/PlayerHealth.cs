using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerLivesData playerLivesData; // Reference to the PlayerLivesData ScriptableObject

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
        playerLivesData.currentLives--;
        Debug.Log("Player Lives: " + playerLivesData.currentLives);

        if (playerLivesData.currentLives <= 0)
        {
            DespawnPlayer();
        }
    }

    void DespawnPlayer()
    {
        Debug.Log("Player has been despawned.");
        Destroy(gameObject); // Despawn player
    }
}
