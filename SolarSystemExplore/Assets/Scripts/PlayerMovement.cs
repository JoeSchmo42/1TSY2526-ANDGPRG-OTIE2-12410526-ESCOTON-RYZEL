using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; public float moveSpeed = 10f; public float rotationSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // WASD/Arrow key movement
        float moveHorizontal = UnityEngine.Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveVertical = UnityEngine.Input.GetAxis("Vertical"); // W/S or Up/Down Arrow

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        movement = transform.TransformDirection(movement); // Relative to player's rotation
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}