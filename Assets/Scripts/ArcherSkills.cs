using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkills : Abilities
{
    public Image abilityAim3;
    [Header("Abilities Properties")]
    public float maxAbilityDistance3 = 7;

    public Projectile projectileAbility;

    public override void OnStart()
    {
        abilityAim3.enabled = false;
    }

    public override void OnAbilityPressed3() 
    {
        base.OnAbilityPressed3();
        abilityAim3.enabled = true;
    }
    public override void OnAbilityReleased3() 
    { 
        base.OnAbilityReleased3();
        abilityAim3.enabled = false;
    }
    public override void OnAbilitySkillShot3()
    {
        if (!abilityAim3.enabled) return;
        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                position = hit.point;
            }
        }
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbilityDistance3);

        var newHitPos = transform.position + hitPosDir * distance;
        abilityAim3.transform.position = newHitPos;
    }
    public override void CancelSkill(int number, Image abilitySkillshot)
    {
        base.CancelSkill(number, abilitySkillshot);
        if(number == 3)
        {
            abilityAim3.enabled = false;
        }
    }
    public override void Skill1()
    {
    }
    public override void Skill2()
    {

    }
    public override void Skill3()
    {
    }
    public override void Skill4()
    {
    }

}
