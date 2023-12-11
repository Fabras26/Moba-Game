using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Stats))]
public class Combat : MonoBehaviour
{
    protected Stats stats;
    protected Animator anim;
    protected Stats target;

    protected bool canAttack;
    void Start()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        OnStart();
    }
    public virtual void OnStart()
    {
    } 
    public virtual void OnUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= stats.Range && !target.isDead())
        {

            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
   
    public virtual void Attack()
    {
    }
}
