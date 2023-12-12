using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkills : Abilities
{
    [Header("Abilities Properties")]

    public Transform arrowHand;
    public Projectile projectileAbility;
    [Header("Ability 2 Properties")]
    public float numberOfArrowsCone = 5;
    public float coneAngle = 30;
    [Header("Ability 3 Properties")]
    public bool isBoostActive;
    public float timeActiveBoost = 5;
    private float currentTimerBoost;
    private Outline outline;
    private Stats stats;
    Vector3 skillDirection;
    Vector3 direction;
    public override void OnStart()
    {
        outline = GetComponent<Outline>();
        stats = GetComponent<Stats>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        BoostAttackSpeed();
    }
    public override void AbilityPressed(Skill skill) 
    {
        base.AbilityPressed(skill);
        if(skills.IndexOf(skill) == 2) AbilityReleased(skill, true);
    }
    public override void AbilityReleased(Skill skill, bool stop = true) 
    { 
        base.AbilityReleased(skill, stop);
        direction = (mousePosition - transform.position).normalized;

        skillDirection = transform.position + direction * skills[skills.IndexOf(skill)].Range;
    }
    public override void OnAbilitySkillShot(int index)
    {
        base.OnAbilitySkillShot(index);
        //RainArrowsSkillshot();
    }
    public void BoostAttackSpeed()
    {
        if (isBoostActive)
        {
            currentTimerBoost += Time.deltaTime;
            if(currentTimerBoost >= timeActiveBoost)
            {
                isBoostActive = false;
                currentTimerBoost = 0;
                outline.enabled = false;
                stats.ModifyStatus(StatsType.AttackSpeed, ModifierType.Division, skills[2].Damage);
            }
        }
    }
    public void RainArrowsSkillshot()
    {
        /*if (!abilityAim3.enabled) return;
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
        distance = Mathf.Min(distance, maxDistanceRainArrows);

        var newHitPos = transform.position + hitPosDir * distance;
        abilityAim3.transform.position = newHitPos;*/
    }
    public override void CancelSkill(Skill skill)
    {
        base.CancelSkill(skill);
    }


    public override void Skill1()
    {
        var arrow = Instantiate(projectileAbility, arrowHand.position, arrowHand.rotation);
        arrow.transform.localScale = Vector3.one * 3;
        skillDirection.y = -2f;
        arrow.SetTarget(skillDirection, skills[0].Damage, false, false);
    }
    public override void Skill2()
    {
        for (int i = 0; i < numberOfArrowsCone; i++)
        {
            // Cálculo do deslocamento angular para cada flecha dentro do cone
            float angleOffset = (i - (numberOfArrowsCone - 1) / 2f) * coneAngle / (numberOfArrowsCone - 1);
            Quaternion rotationOffset = Quaternion.Euler(0f, angleOffset, 0f);

            // Calculando a direção da flecha com base no offset angular
            Vector3 coneDirection = rotationOffset * direction;

            // Aplicando o alcance e instanciando a flecha
            Vector3 arrowDir = transform.position + coneDirection * skills[1].Range;
            var arrow = Instantiate(projectileAbility, arrowHand.position, arrowHand.rotation);

            arrowDir.y = arrow.transform.position.y;
            arrow.SetTarget(arrowDir, skills[1].Damage, false, true);
        }
    }
    public override void Skill3()
    {
        isBoostActive = true;
        currentTimerBoost = 0;
        outline.enabled = true;
        stats.ModifyStatus(StatsType.AttackSpeed, ModifierType.Multiplication, skills[2].Damage);
    }
    public override void Skill4()
    {
        var arrow = Instantiate(projectileAbility, arrowHand.position, arrowHand.rotation);
        arrow.transform.localScale = new Vector3(15,15,4);
        skillDirection.y = arrow.transform.position.y;
        arrow.SetTarget(skillDirection, skills[3].Damage, false, true);
    }
}
