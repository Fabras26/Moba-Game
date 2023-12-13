using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Combat
{
    [SerializeField]
    private bool dead;


    private Vector3 targetPosition;
    private float timeFindPlayer;
    private NavMeshAgent agent;

    private bool canWalk;

    public override void OnStart()
    {
        base.OnStart();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<IDamageable>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        canWalk = !anim.GetBool("Attack");
        MoveAnimation();
        timeFindPlayer += Time.deltaTime;
        if (target != null)
        {
            if (timeFindPlayer > 0.5f && canWalk)
            {
                timeFindPlayer = 0;
                targetPosition = target.GetPosition();
                agent.SetDestination(targetPosition);
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
    public override void Attack()
    {
        if (Vector3.Distance(target.GetPosition(), transform.position) < stats.Range && stats != null)
        {
            target.TakeDamage(stats.Damage);
        }
    }
    public void Stop()
    {
        agent.isStopped = true;
    }
    public void Walk()
    {
        agent.isStopped = false;
    }
    public void MoveAnimation()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }
}

