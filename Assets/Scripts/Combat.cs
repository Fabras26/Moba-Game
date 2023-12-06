using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement)), RequireComponent(typeof(Stats))]
public class Combat : MonoBehaviour
{
    private Movement moveScript;
    protected Stats stats;
    private Animator anim;

    protected Enemy target;

    void Start()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        moveScript = GetComponent<Movement>();
        UpdateAttackSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        target = moveScript.GetTarget();
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= stats.Range && target.isDead())
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
    public void UpdateAttackSpeed()
    {
        anim.SetFloat("AttackSpeed", stats.AttackSpeed);
    }
    public virtual void Attack()
    {
    }
}
