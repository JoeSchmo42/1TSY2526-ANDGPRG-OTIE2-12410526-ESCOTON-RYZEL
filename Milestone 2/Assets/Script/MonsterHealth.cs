// MonsterHealth.cs  ← REPLACE YOURS
using UnityEngine;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public int goldValue = 20;
    public bool isBoss = false;

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
        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0) agent.speed /= 0.5f; // remove slow
        }

        if (burnTimer > 0)
        {
            burnTimer -= Time.deltaTime;
            if (Time.frameCount % 30 == 0)
                TakeDamage(burnDamage);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
    }

    public void ApplySlow(float duration = 3f)
    {
        agent.speed *= 0.5f;
        slowTimer = duration;
    }

    public void ApplyBurn(float dmgPerSec, float duration = 4f)
    {
        burnDamage = dmgPerSec;
        burnTimer = duration;
    }

    void Die()
    {
        GameManager.Instance.AddGold(goldValue + (isBoss ? 50 : 0));
        Destroy(gameObject);
    }
}