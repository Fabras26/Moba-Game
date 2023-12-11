using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CombatPlayer : Combat
{
    private Movement moveScript;
    public LayerMask enemyLayer;
    public Image rangeImage;

    public override void OnStart()
    {
        base.OnStart();
        moveScript = GetComponent<Movement>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        target = moveScript.GetTarget();
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
    Stats FindNearestEnemy(Vector3 clickPoint)
    {
        Collider[] hitColliders = Physics.OverlapSphere(clickPoint, stats.Range * 2, enemyLayer);

        Stats nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(clickPoint, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = collider.transform.GetComponent<Stats>();
            }
        }

        return nearestEnemy;
    }
}
