using UnityEngine;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour
{
    public enum Type { Arrow, Ice, Fire, Cannon }
    public Type towerType;

    public float range = 8f;
    public float attackSpeed = 1f;
    public int damage = 30;

    // Special values
    public float slowDuration = 3f;
    public float burnDPS = 8f;
    public float splashRadius = 4f;

    private float nextAttackTime = 0f;
    private List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        UpdateEnemiesInRange();

        if (enemies.Count == 0) return;
        if (Time.time < nextAttackTime) return;

        GameObject target = enemies[0];
        transform.LookAt(target.transform);

        switch (towerType)
        {
            case Type.Arrow: ArrowAttack(target); break;
            case Type.Ice: IceAttack(target); break;
            case Type.Fire: FireAttack(target); break;
            case Type.Cannon: CannonAttack(target); break;
        }

        nextAttackTime = Time.time + attackSpeed;
    }

    void UpdateEnemiesInRange()
    {
        enemies.Clear();
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hits)
            if (hit.CompareTag("Enemy"))
                enemies.Add(hit.gameObject);
    }

    void ArrowAttack(GameObject t) => t.GetComponent<MonsterHealth>()?.TakeDamage(damage);
    void IceAttack(GameObject t) => t.GetComponent<MonsterHealth>()?.ApplySlow(slowDuration);
    void FireAttack(GameObject t) => t.GetComponent<MonsterHealth>()?.ApplyBurn(burnDPS);
    void CannonAttack(GameObject t)
    {
        Collider[] splash = Physics.OverlapSphere(t.transform.position, splashRadius);
        foreach (var s in splash)
            if (s.CompareTag("Enemy"))
                s.GetComponent<MonsterHealth>()?.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
