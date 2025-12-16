using UnityEngine;

public class IceProjectile : Projectile
{
    [SerializeField] float slowMultiplier = 0.3f;
    [SerializeField] float slowDuration = 2f;

    protected override void OnHit(EnemyScript enemy)
    {
        base.OnHit(enemy);
        enemy.ApplySlow(slowMultiplier, slowDuration);
    }
}
