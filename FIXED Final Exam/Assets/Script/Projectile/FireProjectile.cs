using UnityEngine;
using System.Collections;

public class FireProjectile : Projectile
{
    [SerializeField] int dotDamage = 10;
    [SerializeField] float dotInterval = 0.5f;
    [SerializeField] float dotDuration = 4f;

    protected override void OnHit(EnemyScript enemy)
    {
        base.OnHit(enemy);
        enemy.ApplyDOT(dotDamage, dotInterval, dotDuration);
    }
}