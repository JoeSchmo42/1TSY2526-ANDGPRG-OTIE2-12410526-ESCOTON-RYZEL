using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Material towerMaterial;

    
    [SerializeField] float range = 15f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] LayerMask enemyMask = -1;

    private bool isBuilt = false;
    private float fireTimer = 0f;
    private Transform targetEnemy;

    public void Buildable()
    {
        if (towerMaterial != null)
            towerMaterial.color = Color.green;
    }

    public void NonBuildable()
    {
        if (towerMaterial != null)
            towerMaterial.color = Color.red;
    }

    public void Build()
    {
        isBuilt = true;
        if (towerMaterial != null)
            towerMaterial.color = Color.white;
    }

    void Update()
    {
        if (!isBuilt) return;

        fireTimer -= Time.deltaTime;

        // Acquire target if needed
        if (targetEnemy == null || !InRange(targetEnemy))
        {
            targetEnemy = FindNearestEnemy();
        }

        // Shoot if possible
        if (targetEnemy != null && fireTimer <= 0f)
        {
            Shoot();
        }
    }

    bool InRange(Transform enemy)
    {
        return Vector3.Distance(transform.position, enemy.position) <= range;
    }

    Transform FindNearestEnemy()
    {
        Transform nearest = null;
        float nearestDist = Mathf.Infinity;

        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemyMask);
        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = col.transform;
                }
            }
        }
        return nearest;
    }

    void Shoot()
    {
        if (targetEnemy == null || firePoint == null || projectilePrefab == null) return;

        Vector3 direction = (targetEnemy.position - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);
        fireTimer = 1f / fireRate;
    }

    // Visualize range and target in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        if (targetEnemy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetEnemy.position);
        }
    }
}
