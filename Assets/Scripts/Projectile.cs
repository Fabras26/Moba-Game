using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    private GameObject targetObject;
    private Transform targetPoint;
    private bool started = false;
    private Rigidbody rb;
    private float damage;

    Vector3 direction;
    Vector3 targetPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!started) return;
        if (targetObject != null)
        {
            direction = targetPoint.position - transform.position;
            targetPosition = targetPoint.position;
        }
        else
        {
            direction = targetPosition - transform.position;

            if (Vector3.Distance(targetPosition, transform.position) < 0.5) Destroy(gameObject); 
        }

        rb.velocity = direction.normalized * speed;
        transform.LookAt(targetPosition);
    }
    public void SetTarget(GameObject enemy, Transform newTarget, float newDamage, float newSpeed = -1)
    {
        if(newSpeed != -1) speed = newSpeed;
        damage = newDamage;
        targetObject = enemy;
        targetPoint = newTarget;
        started = true;
    }
    public void SetTarget(GameObject enemy, Vector3 newTarget, float newDamage, float newSpeed = -1)
    {
        if (newSpeed != -1) speed = newSpeed;
        damage = newDamage;
        targetObject = enemy;
        targetPosition = newTarget;
        started = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (targetObject == null) return;
        if(other.transform == targetObject.transform)
        {
            var health = other.GetComponent<Stats>();
            if(health)
            {
                health.TakeDamage(damage);
            }
            else
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
