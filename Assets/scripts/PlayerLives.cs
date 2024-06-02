using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Starting lives

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
            DespawnPlayer();
        }
    }

    void DespawnPlayer()
    {
        Debug.Log("Player has been despawned.");
        Destroy(gameObject); // Despawn player
    }
}
