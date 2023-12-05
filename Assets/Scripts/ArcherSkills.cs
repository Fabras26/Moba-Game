using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkills : Abilities
{
    public Image abilityAim3;
    [Header("Abilities Properties")]
    public float maxAbilityDistance3 = 7;

    public override void OnStart()
    {
        abilityAim3.enabled = false;
    }

    public override void OnAbilityPressed1() { 
    base.OnAbilityPressed1();
    
    }
    public override void OnAbilityPressed2() {
        base.OnAbilityPressed2();
    }
    public override void OnAbilityPressed3() 
    {
        base.OnAbilityPressed3();
        abilityAim3.enabled = true;
    }
    public override void OnAbilityPressed4()
    { 
        base.OnAbilityPressed4();
    }
    public override void OnAbilityReleased1() 
    { 
        base.OnAbilityReleased1();
    }
    public override void OnAbilityReleased2()
    { 
        base.OnAbilityReleased2();
    }
    public override void OnAbilityReleased3() 
    { 
        base.OnAbilityReleased3();
        abilityAim3.enabled = false;
    }
    public override void OnAbilityReleased4() 
    { 
        base.OnAbilityReleased4();
    }

    public override void OnAbilitySkillShot1()
    {
    }
    public override void OnAbilitySkillShot2() 
    {
       
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
    public override void OnAbilitySkillShot4() { }
    public override void CancelSkill(int number, Image abilitySkillshot)
    {
        base.CancelSkill(number, abilitySkillshot);
        if(number == 3)
        {
            abilityAim3.enabled = false;
        }
    }
}
