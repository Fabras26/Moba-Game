using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Movement)), RequireComponent(typeof(Stats))]
public class Combat : MonoBehaviour
{
    private Movement moveScript;
    protected Stats stats;
    private Animator anim;
    public Image rangeImage;
    protected Enemy target;
    public LayerMask enemyLayer;

    private bool canAttack;
    void Start()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        moveScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        target = moveScript.GetTarget();
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= stats.Range && !target.isDead())
        {

            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rangeImage.enabled = true;
            canAttack = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!canAttack) return;
            if (Input.GetMouseButtonDown(0))
            {
                rangeImage.enabled = false;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    moveScript.GoTo(FindNearestEnemy(hit.point));
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            canAttack = false;
            rangeImage.enabled = false;
        }
    }
   
    public virtual void Attack()
    {
    }
    Enemy FindNearestEnemy(Vector3 clickPoint)
    {
        Collider[] hitColliders = Physics.OverlapSphere(clickPoint, stats.Range * 2, enemyLayer);

        Enemy nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(clickPoint, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = collider.transform.GetComponent<Enemy>();
            }
        }

        return nearestEnemy;
    }
}
