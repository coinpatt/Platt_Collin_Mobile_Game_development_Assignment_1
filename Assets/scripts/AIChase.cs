using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed;

    private GameObject player;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the player object in the scene by tag
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Make sure the player object has the tag 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
