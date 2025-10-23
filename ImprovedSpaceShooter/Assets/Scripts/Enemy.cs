using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float speed = 2f;
    public float playerDamage = 20f;
    private Transform player;

    public event Action<GameObject> OnEnemyDestroyed;

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
            OnEnemyDestroyed?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerDamage);
                Destroy(gameObject);
            }
        }
    }
}