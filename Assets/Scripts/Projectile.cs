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
    private bool destroyOnCollision = true;
    Vector3 direction;
    Vector3 targetPosition;

    private bool isTarget = true;
    public Transform particleSpawn;
    public ParticleSystem particles;
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

            if (Vector3.Distance(targetPosition, transform.position) < 0.5 * transform.localScale.x) Destroy(gameObject); 
        }

        rb.velocity = direction.normalized * speed;
        rb.AddRelativeTorque(direction);
        transform.LookAt(targetPosition);
    }
    public void SetTarget(GameObject enemy, Transform newTarget, float newDamage, float newSpeed = -1)
    {
        if(enemy == null) Destroy(gameObject);
        if(newSpeed != -1) speed = newSpeed;
        damage = newDamage;
        targetObject = enemy;
        targetPoint = newTarget;
        started = true;
    }
    public void SetTarget(Vector3 newTarget, float newDamage,bool newIsTarget = true,bool newDestroyOnCollision = true, float newSpeed = -1)
    {
        if (newSpeed != -1) speed = newSpeed;
        destroyOnCollision = newDestroyOnCollision;
        isTarget = newIsTarget;
        damage = newDamage;
        targetPosition = newTarget;
        started = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (targetObject == null && isTarget) return;

        if(other.CompareTag("Enemy"))
        {

            var health = other.GetComponent<Stats>();
            if (isTarget)
            {
                if (other.transform != targetObject.transform)
                {
                    return;
                }
            }
            if (health)
            {
                var p = Instantiate(particles, particleSpawn.position, particleSpawn.rotation);
                if(transform.localScale.x < 6)
                p.transform.localScale = p.transform.localScale * transform.localScale.x;
                else
                {
                    p.transform.localScale = Vector3.one * 6;
                }
                health.TakeDamage(damage);
                if(destroyOnCollision) Destroy(gameObject);
            }

        }
    }

}
