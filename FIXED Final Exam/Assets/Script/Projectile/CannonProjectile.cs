using UnityEngine;

public class CannonProjectile : Projectile
{
    [SerializeField] float aoeRadius = 3f;
    [SerializeField] int splashDamage = 20; // Damage to nearby enemies

    protected override void OnHit(EnemyScript enemy)
    {
        base.OnHit(enemy);

        // AOE splash
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (Collider col in nearbyEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyScript nearbyEnemy = col.GetComponent<EnemyScript>();
                if (nearbyEnemy != null)
                {
                    nearbyEnemy.TakeDamage(splashDamage);
                }
            }
        }
    }
}