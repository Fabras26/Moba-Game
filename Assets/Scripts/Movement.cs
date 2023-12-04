using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Camera GameCamera;

    [SerializeField]
    private float attackRange = 5f;
    private float rotateVelocitry;

    float motionSmoothTime = 0.1f;

    void Start()
    {
        anim = GetComponent<Animator>();
        GameCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Animation();
        Attack();
    }
    void Move()
    {
        if (Input.GetMouseButton(1))
        {
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Ground")
                {
                    GoTo(hit.point);
                }
            }
        }
    }
    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    void Animation()
    {
        float speed = agent.velocity.magnitude/ agent.speed;
        anim.SetFloat("Speed", speed,motionSmoothTime, Time.deltaTime);
    }
    public virtual void GoTo(Vector3 position)
    {
        agent.SetDestination(position);
        agent.isStopped = false;
        agent.stoppingDistance = 0;
    }


}
