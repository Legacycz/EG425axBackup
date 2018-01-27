using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AIState
{
 Patrol,
 Idle,
 Chase,
 Attack
}
public class AIBase : MonoBehaviour {

    public NavMeshAgent agent;
    public GameObject[] WayPoints;
    int lastIndexCheckPoint = 0;
    public float destinationReachedTreshold = 3;
    AIState state;
    
    void Start()
    {
        agent.SetDestination(WayPoints[0].gameObject.transform.position);
        state =AIState.Patrol;
    }
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case AIState.Patrol:
                PatrolUpdate();
                break;
            case AIState.Idle:
                break;
            case AIState.Chase:
                break;
            case AIState.Attack:
                break;
            default:
                break;
        }
    }

    void PatrolUpdate()
    {
        if(CheckDestinationReached())
        {
            lastIndexCheckPoint++;
            if (lastIndexCheckPoint >= WayPoints.Length)
            {
                lastIndexCheckPoint = 0;
            }
            agent.SetDestination(WayPoints[lastIndexCheckPoint].gameObject.transform.position);
            
        }
    }

    bool CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        
        if (distanceToTarget < destinationReachedTreshold)
        {
            print("Destination reached");
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (WayPoints == null)
        {
            return;
        }
        for (int i = 0; i < WayPoints.Length; i++)
        {
            if (WayPoints[i] == null)
                continue;
            if (i == 0)
            {
                Gizmos.DrawLine(transform.position, WayPoints[i].transform.position);
            }
            else
            {
                if (WayPoints[i-1] == null)
                    continue;
                Gizmos.DrawLine(WayPoints[i - 1].transform.position, WayPoints[i].transform.position);
            }
        }
    }
}
