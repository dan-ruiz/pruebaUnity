using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshAi : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
}
