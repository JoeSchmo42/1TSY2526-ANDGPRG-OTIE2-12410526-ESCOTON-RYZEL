using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;

    void Start()
    {
        health = maxHealth;
        Debug.Log($"Player initialized with health: {health}");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage, health now {health}");
        if (health <= 0)
        {
            AudioManager.Instance.PlayDeathSFX(transform.position);
            Debug.Log("Player destroyed");
            AudioManager.Instance.TriggerGameOver();

        }
    }
}
