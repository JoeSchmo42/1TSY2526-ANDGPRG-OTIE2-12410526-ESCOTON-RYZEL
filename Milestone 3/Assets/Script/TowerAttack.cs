using UnityEngine;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour
{
    public TowerData data;
    float nextAttackTime = 0f;
    List<GameObject> enemiesInRange = new List<GameObject>();

    public void Initialize(TowerData towerData)
    {
        data = towerData;
    }

    void Update()
    {
        if (data == null) return;

        UpdateEnemies();
        if (enemiesInRange.Count == 0) return;
        if (Time.time < nextAttackTime) return;

        GameObject target = enemiesInRange[0];
        transform.LookAt(target.transform);

        if (data.isSplash)
            SplashAttack(target.transform.position);
        else
            SingleAttack(target);

        nextAttackTime = Time.time + data.attackSpeed;
    }

    void UpdateEnemies()
    {
        enemiesInRange.Clear();
        Collider[] hits = Physics.OverlapSphere(transform.position, data.range);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            MonsterHealth mh = hit.GetComponent<MonsterHealth>();
            if (mh == null) continue;
            if (mh.isFlying && !data.canTargetFlying) continue;

            enemiesInRange.Add(hit.gameObject);
        }

        // Sort by closest first
        enemiesInRange.Sort((a, b) =>
            Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
    }

    void SingleAttack(GameObject target)
    {
        MonsterHealth mh = target.GetComponent<MonsterHealth>();
        if (mh == null) return;

        mh.TakeDamage(data.damage);

        // Fixed: Match MonsterHealth.cs exactly
        if (data.slowDuration > 0f)
            mh.ApplySlow(data.slowDuration);           // only duration

        if (data.burnDPS > 0f)
            mh.ApplyBurn(data.burnDPS, data.burnDuration);
    }

    void SplashAttack(Vector3 center)
    {
        Collider[] hits = Physics.OverlapSphere(center, data.splashRadius);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            MonsterHealth mh = hit.GetComponent<MonsterHealth>();
            if (mh == null) continue;
            if (mh.isFlying && !data.canTargetFlying) continue;

            mh.TakeDamage(data.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (data != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, data.range);
        }
    }
}