using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCombat : CombatPlayer
{
    [Header("Projectile")]
    public Projectile projectilePrefab;
    public Transform spawnProjectilePosition;
    public override void Attack()
    {
        base.Attack();
        var projectile = Instantiate(projectilePrefab, spawnProjectilePosition.position, spawnProjectilePosition.rotation);
        projectile.SetTarget(target.GetObject(),target.GetTarget(), stats.Damage);
    }
    public void RangedAttack()
    {
        Attack();
    }
}
