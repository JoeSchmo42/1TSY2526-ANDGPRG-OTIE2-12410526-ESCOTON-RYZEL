using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player movement
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public Transform[] nozzles; // Array of nozzle transforms
    private int currentNozzle = 0;

    // Bullet prefabs for each nozzle
    public GameObject[] bulletPrefabs;
    private float[] fireRates = { 0.5f, 1f, 0.2f, 20f };
    private float nextFireTime = 0f;

    // Camera reference for screen boundaries
    private Camera mainCamera;
    private float cameraHeight;
    private float cameraWidth;

    // Player health
    public float maxHealth = 100f;
    private float health;

    // Audio
    private AudioSource audioSource;
    public AudioClip[] shootSFX;

    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        for (int i = 0; i < shootSFX.Length; i++)
        {
            Debug.Log($"Nozzle {i} SFX: {(shootSFX[i] != null ? shootSFX[i].name : "None")}");
        }
        Debug.Log($"Player initialized with health: {health}");
    }

    void Update()
    {
        HandleMovement();
        HandleNozzleSwitching();
        HandleShooting();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveX, moveY, 0).normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        float clampedX = Mathf.Clamp(transform.position.x, -cameraWidth / 2, cameraWidth / 2);
        float clampedY = Mathf.Clamp(transform.position.y, -cameraHeight / 2, cameraHeight / 2);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void HandleNozzleSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentNozzle = 0;
            Debug.Log($"Switched to Nozzle 1 (Regular), SFX: {(shootSFX[0] != null ? shootSFX[0].name : "None")}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentNozzle = 1;
            Debug.Log($"Switched to Nozzle 2 (Strong), SFX: {(shootSFX[1] != null ? shootSFX[1].name : "None")}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentNozzle = 2;
            Debug.Log($"Switched to Nozzle 3 (Quick), SFX: {(shootSFX[2] != null ? shootSFX[2].name : "None")}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentNozzle = 3;
            Debug.Log($"Switched to Nozzle 4 (Large), SFX: {(shootSFX[3] != null ? shootSFX[3].name : "None")}");
        }
    }

    void HandleShooting()
    {
        // Use GetKeyDown for single-shot trigger
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRates[currentNozzle];
            Debug.Log($"Shot fired from Nozzle {currentNozzle}, next shot in {fireRates[currentNozzle]}s");
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefabs[currentNozzle], nozzles[currentNozzle].position, transform.rotation);
        // Play shoot SFX
        if (shootSFX[currentNozzle] != null)
        {
            audioSource.PlayOneShot(shootSFX[currentNozzle], AudioManager.Instance.sfxVolume);
            Debug.Log($"Playing SFX for Nozzle {currentNozzle}: {shootSFX[currentNozzle].name}");
        }
        else
        {
            Debug.LogWarning($"No SFX assigned for Nozzle {currentNozzle}");
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage, health now {health}");
        if (health <= 0)
        {
            AudioManager.Instance.PlayDeathSFX(transform.position);
            Debug.Log("Player destroyed");
            Destroy(gameObject);
            AudioManager.Instance.TriggerGameOver();
        }
    }
}