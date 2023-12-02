using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private float rotateSpeedMovement = 0.05f;
    [SerializeField]
    private float rotateVelocitry;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Animation()
    {

    }
}
