using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float speed = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move towards player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Enemy {gameObject.name} took {damage} damage, health now {health}");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}