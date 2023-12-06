using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StatsType
{
    Health,
    Armor,
    MagicResist,
    AttackSpeed,
    Damage,
    Range,
    Crit,
    Mana,
    MoveSpeed
}

public enum ModifierType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
[ExecuteInEditMode]

public class Stats : MonoBehaviour
{
    [SerializeField]
    private StatsSO initialState;
    [SerializeField]
    private NavMeshAgent agent;
    

    public float health;
    public float armor;
    public float magicResist;
    public float attackSpeed;
    public float damage;
    public float range;
    public float crit;
    public float mana;
    
    public float moveSpeed;
    
    public float cdr;

    public float Health 
    {
        get => health; 
        set 
        { 
            health = value;
            if (health <= 0) 
            {
                Die();
            }
        } 
    }
    public float Armor { get => armor; set => armor = value; }
    public float MagicResist { get => magicResist; set => magicResist = value;}
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float Crit { get => crit; set => crit = value; }
    public float Mana { get => mana; set => mana = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Cdr { get => cdr; set => cdr = value; }


    public void ModifyStatus(StatsType status, ModifierType modifier, float value)
    {
        switch (status)
        {
            case StatsType.Health:
                ModifyStatusReference(ref health, modifier,value);
                break;
            case StatsType.Armor:
                ModifyStatusReference(ref armor, modifier, value);
                break;
            case StatsType.MagicResist:
                ModifyStatusReference(ref magicResist, modifier, value);

                break;
            case StatsType.AttackSpeed:
                ModifyStatusReference(ref attackSpeed, modifier, value);
                break;
            case StatsType.Damage:
                ModifyStatusReference(ref damage, modifier, value);

                break;
            case StatsType.Range:
                ModifyStatusReference(ref range, modifier, value);

                break;
            case StatsType.Crit:
                ModifyStatusReference(ref crit, modifier, value);

                break;
            case StatsType.Mana:
                ModifyStatusReference(ref mana, modifier, value);

                break;
            case StatsType.MoveSpeed:
                ModifyStatusReference(ref moveSpeed, modifier, value);
                UpdateMoveSpeed();
                break;
        }

    }
    private void ModifyStatusReference(ref float status, ModifierType modifier,float value)
    {
        switch (modifier)
        {
            case ModifierType.Addition:
                status += value; 
                break;
            case ModifierType.Multiplication:
                status *= value;
                break;
            case ModifierType.Division:
                status /= value;
                break;
            case ModifierType.Subtraction:
                status -= value;
                break;
        }
    }
    public void TakeDamage(float damage)
    {
        Health-=damage;
    }
    private void UpdateMoveSpeed()
    {
        agent.speed = moveSpeed;
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = initialState.health;
        armor = initialState.armor;
        magicResist = initialState.magicResist;

        attackSpeed = initialState.attackSpeed;
        damage = initialState.damage;
        range = initialState.range;
        crit = initialState.crit;
        mana = initialState.mana;

        moveSpeed = initialState.moveSpeed;

        cdr = 0;
    }
    void Die()
    {
        Destroy(gameObject);
    }
}