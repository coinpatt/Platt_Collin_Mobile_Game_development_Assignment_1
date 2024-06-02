using System.Collections.Generic;
using UnityEngine;

public class RewardObject : MonoBehaviour
{
    public List<string> messages = new List<string>(); // List of messages to send to the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SendRandomMessageToPlayer();
            DespawnRewardObject();
        }
    }

    private void SendRandomMessageToPlayer()
    {
        if (messages.Count > 0)
        {
            // Select a random message from the list
            string randomMessage = messages[Random.Range(0, messages.Count)];
            
            // Send the message to the player (you need to replace "Player" with the actual tag of the player)
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.SendMessage("ReceiveMessage", randomMessage);
            }
            else
            {
                Debug.LogWarning("Player not found.");
            }
        }
    }

    private void DespawnRewardObject()
    {
        // Despawn the reward object
        Destroy(gameObject);
    }
}
