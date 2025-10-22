using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 3f;
    private int currentPoint = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned to " + gameObject.name);
            return;
        }
        Debug.Log($"Enemy {gameObject.name} initialized with {patrolPoints.Length} patrol points.");
    }

    void FixedUpdate()
    {
        if (patrolPoints.Length == 0) return;

        Vector3 target = patrolPoints[currentPoint].position;
        Vector3 direction = (target - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            Debug.Log($"Enemy reached point {currentPoint} at {patrolPoints[currentPoint].position}");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Enemy {gameObject.name} collided with Player at {transform.position}");
        }
    }
}