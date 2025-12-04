using UnityEngine;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public int goldValue = 20;
    public bool isBoss = false;
    public bool isFlying = false;           

    private NavMeshAgent agent;
    private float slowTimer = 0f;
    private float burnDamage = 0f;
    private float burnTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Remove slow when timer ends
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
            {
                agent.speed /= 0.5f;   // restore original speed (remove the 50% slow)
            }
        }

        // Burning damage over time
        if (burnTimer > 0f)
        {
            burnTimer -= Time.deltaTime;

            // Deal burn damage every ~0.5 seconds (30 frames)
            if (Time.frameCount % 30 == 0)
            {
                TakeDamage(burnDamage);
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void ApplySlow(float duration = 3f)
    {
        agent.speed *= 0.5f;   // 50% slow
        slowTimer = duration;
    }

    public void ApplyBurn(float dmgPerSec, float duration = 4f)
    {
        burnDamage = dmgPerSec;
        burnTimer = duration;
    }

    void Die()
    {
        int bonus = isBoss ? 50 : 0;
        GameManager.Instance.AddGold(goldValue + bonus);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.Core)
        {
            int damage = isBoss ? 3 : 1;
            GameManager.Instance.DamageCore(damage);
            Destroy(gameObject);
        }
    }
}