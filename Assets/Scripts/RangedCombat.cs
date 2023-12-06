using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCombat : Combat
{
    [Header("Projectile")]
    public Projectile projectilePrefab;
    public Transform spawnProjectilePosition;
    public override void Attack()
    {
        base.Attack();
        var projectile = Instantiate(projectilePrefab, spawnProjectilePosition.position, spawnProjectilePosition.rotation);
        projectile.SetTarget(target.gameObject,target.GetTarget(), stats.damage);
    }
    public void RangedAttack()
    {
        Attack();
    }
}
