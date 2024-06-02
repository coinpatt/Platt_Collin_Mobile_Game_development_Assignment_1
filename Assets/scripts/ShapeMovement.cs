using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float scaleSpeed = 0.1f;

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction based on WASD keys
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * moveSpeed * Time.deltaTime;

        // Apply movement only along the global horizontal and vertical axes
        transform.position += new Vector3(horizontalInput, verticalInput, 0f).normalized * moveSpeed * Time.deltaTime;

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Scaling
        float scaleInput = Input.GetKey(KeyCode.Z) ? -1f : Input.GetKey(KeyCode.C) ? 1f : 0f;
        transform.localScale += Vector3.one * scaleInput * scaleSpeed * Time.deltaTime;
    }
}
