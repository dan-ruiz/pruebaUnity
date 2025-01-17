using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiPatrol : MonoBehaviour
{
    // Variables
    GameObject player;
    NavMeshAgent agent;
    public Transform moveSpot;
    [SerializeField] LayerMask groundLayer, playerLayer;
    // Patrol
    Vector2 destPoint;
    bool walkPointSet;
    [SerializeField] float range;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation= false;
        agent.updateUpAxis = false;
        player = GameObject.FindWithTag("Player");
    }
    public void Patrol(){
        if (!walkPointSet) SearchForDest();
        if (walkPointSet) agent.SetDestination(destPoint);
        if (Vector2.Distance(transform.position, destPoint) < 3f) walkPointSet = false;
    }
    void SearchForDest()
    {
        float x = Random.Range(-range, range);
        float y = Random.Range(-range, range);

        destPoint = new Vector2(transform.position.x + x, transform.position.y + y);

        if (Physics2D.Raycast(destPoint, Vector2.down, groundLayer))
        {
            walkPointSet = true;
        }
    }
}

