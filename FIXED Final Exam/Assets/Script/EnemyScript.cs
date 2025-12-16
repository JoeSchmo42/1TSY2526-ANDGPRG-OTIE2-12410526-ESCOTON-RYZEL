using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Stats")]
    [SerializeField] int maxHealth = 100;
    public int currentHealth;
    [SerializeField] float baseSpeed = 3.5f;

    private float originalSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        originalSpeed = agent.speed;
        currentHealth = maxHealth;

        if (GameManager.Instance != null && GameManager.Instance.Core != null)
        {
            agent.SetDestination(GameManager.Instance.Core.transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SpawnerScript.EnemyDied();  // Decrement global counter
        Destroy(gameObject);
    }

    public void ApplySlow(float multiplier, float duration)
    {
        StartCoroutine(DoSlow(multiplier, duration));
    }

    IEnumerator DoSlow(float multiplier, float duration)
    {
        agent.speed = originalSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        agent.speed = originalSpeed;
    }

    public void ApplyDOT(int damagePerTick, float interval, float duration)
    {
        StartCoroutine(DoDOT(damagePerTick, interval, duration));
    }

    IEnumerator DoDOT(int damagePerTick, float interval, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage(damagePerTick);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }
}
