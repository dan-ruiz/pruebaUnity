using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    // Capa de Navmesh se arrastra de la escena ya cuando se creo 
    [SerializeField] private NavMeshSurface navMeshSurface2D;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        target = GameObject.Find("Player")?.transform;

        navMeshSurface2D = GameObject.Find("NavMesh")?.GetComponent<NavMeshSurface>() ?? null;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        FollowPlayerPosition();
    }
    public void FollowPlayerPosition()
    {
        navMeshAgent.SetDestination(target.position);
    }
}
