using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player movement
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public Transform[] nozzles; // Array of nozzle transforms (one for each firing point)
    private int currentNozzle = 0;

    // Bullet prefabs for each nozzle
    public GameObject[] bulletPrefabs;
    private float[] fireRates = { 0.5f, 1f, 0.2f, 20f }; // Fire rates for each nozzle
    private float nextFireTime = 0f;

    // Camera reference for screen boundaries
    private Camera mainCamera;
    private float cameraHeight;
    private float cameraWidth;

    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    void Update()
    {
        HandleMovement();
        HandleNozzleSwitching();
        HandleShooting();
    }

    void HandleMovement()
    {
        // WASD or Arrow key movement
        float moveX = UnityEngine.Input.GetAxisRaw("Horizontal");
        float moveY = UnityEngine.Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveX, moveY, 0).normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Rotate to face upward (assuming top-down view)
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Clamp position to screen boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -cameraWidth / 2, cameraWidth / 2);
        float clampedY = Mathf.Clamp(transform.position.y, -cameraHeight / 2, cameraHeight / 2);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void HandleNozzleSwitching()
    {
        // Switch nozzles with keys 1, 2, 3, 4
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentNozzle = 0;
        else if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2)) currentNozzle = 1;
        else if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3)) currentNozzle = 2;
        else if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4)) currentNozzle = 3;
    }

    void HandleShooting()
    {
        // Fire bullets with Spacebar
        if (UnityEngine.Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRates[currentNozzle];
        }
    }

    void FireBullet()
    {
        // Instantiate bullet at the current nozzle position
        GameObject bullet = Instantiate(bulletPrefabs[currentNozzle], nozzles[currentNozzle].position, transform.rotation);
        
    }
}