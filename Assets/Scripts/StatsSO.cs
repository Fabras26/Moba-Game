using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InitialState")]
public class StatsSO : ScriptableObject
{

    [Header("Defence")]
    public int health;
    public float armor;
    public float magicResist;

    [Header("Attack")]
    public float attackSpeed;
    public int damage;
    public float range;
    public int crit;
    public float mana;
    [Header("Movement")]
    public float moveSpeed;

}
