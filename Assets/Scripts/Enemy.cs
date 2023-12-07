using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool dead;
    [SerializeField]
    private Transform projectileTarget;

    public Transform GetTarget()
    {
        return projectileTarget;
    }
    public bool isDead()
    {
        return dead;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
