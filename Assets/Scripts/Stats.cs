using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum StatsType
{
    MaxHealth,
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
public interface IDamageable 
{
    public bool IsDead { get; set; }
    public void TakeDamage(float damage);
    public Vector3 GetPosition();
    public GameObject GetObject();
    public Transform GetTarget();
}

[ExecuteInEditMode]

public class Stats : MonoBehaviour, IDamageable
{
    private Animator anim;
    [SerializeField]
    private StatsSO initialState;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform projectileTarget;
    [SerializeField]
    private HealthUI healthUI;

    public UnityEvent OnDie;
    private float maxHealth;
    private float health;
    private float armor;
    private float magicResist;
    private float attackSpeed;
    private float damage;
    private float range;
    private float crit;
    private float mana; 
    private float moveSpeed;
    private float cdr;

    private bool dead;
    public float Health 
    {
        get => health; 
        set 
        { 
            health = value;
            UpdateHealthBar();

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
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsDead { get => dead; set => dead = value; }

    public void ModifyStatus(StatsType status, ModifierType modifier, float value)
    {
        switch (status)
        {
            case StatsType.MaxHealth:
                ModifyStatusReference(ref maxHealth, modifier, value);
                UpdateHealthBar();
                break;

            case StatsType.Health:
                ModifyStatusReference(ref health, modifier,value);
                UpdateHealthBar();
                break;
            case StatsType.Armor:
                ModifyStatusReference(ref armor, modifier, value);
                break;
            case StatsType.MagicResist:
                ModifyStatusReference(ref magicResist, modifier, value);

                break;
            case StatsType.AttackSpeed:
                ModifyStatusReference(ref attackSpeed, modifier, value);
                UpdateAttackSpeed();
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
    public bool isDead()
    {
        return dead;
    }
    public Transform GetTarget()
    {
        return projectileTarget;
    }
    public void UpdateAttackSpeed()
    {
        anim.SetFloat("AttackSpeed", AttackSpeed);
    }
    public void TakeDamage(float damage)
    {
        Health-=damage;
    }
    public void UpdateHealthBar()
    {
        healthUI.SetTargetHealth(maxHealth, health);
    }
    private void UpdateMoveSpeed()
    {
        agent.speed = moveSpeed;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        healthUI = GetComponent<HealthUI>();

        maxHealth = initialState.health;
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
    private void Start()
    {
        UpdateAttackSpeed();
        healthUI.SetValues(maxHealth);
    }
    void Die()
    {
        if(gameObject.tag == "Enemy") ScoreManager.instance.AddPoints();
        OnDie?.Invoke();
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}
