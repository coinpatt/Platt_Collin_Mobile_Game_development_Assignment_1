using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerachase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    public bool lockX;  // Control whether to lock the X axis
    public bool lockY;  // Control whether to lock the Y axis
    public bool lockZ;  // Control whether to lock the Z axis
    
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
        distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;

        Vector3 newPosition = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        if (lockX)
        {
            newPosition.x = transform.position.x;  // Lock X position
        }

        if (lockY)
        {
            newPosition.y = transform.position.y;  // Lock Y position
        }

        if (lockZ)
        {
            newPosition.z = transform.position.z;  // Lock Z position
        }

        transform.position = newPosition;
    }
}
