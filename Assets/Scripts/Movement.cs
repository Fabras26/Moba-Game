using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Camera GameCamera;

    [SerializeField]
    private float rotationSpeed = 0.05f;
    private float rotateVelocity = 1f;
    float motionSmoothTime = 0.1f;

    private Enemy targetEnemy;
    private HighlightManager highlight;

    private Stats stats;
    void Start()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        GameCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        highlight = GetComponent<HighlightManager>();
        agent.speed = stats.MoveSpeed;
    }
    public Enemy GetTarget() 
    {
        return targetEnemy;
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
                if (hit.collider.GetComponent<Enemy>())
                {
                    GoTo(hit.collider.GetComponent<Enemy>());
                }
            }
        }
        if (targetEnemy != null)
        {
            if(Vector3.Distance(transform.position, targetEnemy.transform.position) > stats.Range)
            {
                agent.SetDestination(targetEnemy.transform.position);
            }
        }
    }
    void Attack()
    {
        if(targetEnemy != null &&  Vector3.Distance(transform.position, targetEnemy.transform.position) <= stats.Range)
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
        if (targetEnemy != null) targetEnemy = null;
        highlight.DeselectHighlight();
        agent.SetDestination(position);
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        Rotation(position);
    }
    public virtual void GoTo(Enemy target)
    {
        highlight.SelectHighlight(target.transform);
        targetEnemy = target;
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        agent.stoppingDistance = stats.Range;
        Rotation(target.transform.position);
    }
    void Rotation(Vector3 target)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(target - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotationSpeed * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
    void OnDrawGizmos()
    {
        stats = GetComponent<Stats>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,1,0), stats.Range);
    }


}
