using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip collectSound;
    private static GameObject instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerMouse initialized.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Destroyed duplicate PlayerMouse.");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing on PlayerMouse!");
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit by Enemy!");
            GameManager.Instance.TakeDamage();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with Obstacle!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered with: {other.tag} at position: {other.transform.position}");
        if (other.CompareTag("Collectible"))
        {
            Debug.Log($"Cheese collected! Trigger count: {GameManager.Instance.cheeseCount + 1}");
            GameManager.Instance.CollectCheese();
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.LogWarning("CollectSound or AudioSource is not assigned!");
            }
            Destroy(other.gameObject);
        }
    }
}