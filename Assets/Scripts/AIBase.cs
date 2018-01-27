using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
 Patrol,
 Idle,
 Chase,
 Attack
}
public class AIBase : MonoBehaviour {

    public NavMeshAgent agent;
    int lastIndexCheckPoint = 0;
    public float destinationReachedTreshold = 3;

    GameObject _target;
    public AIState state;
    private Coroutine _lookingCorutine;

    void Start()
    {
        agent.SetDestination(AIManager.Instance.WayPoints[Random.Range(0, AIManager.Instance.WayPoints.Length)].gameObject.transform.position);
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
                ChaseUpdate();
                break;
            case AIState.Attack:
                break;
            default:
                break;
        }
    }

    void PatrolUpdate()
    {
        if(CheckDestinationReached(destinationReachedTreshold))
        {
            lastIndexCheckPoint++;
            if (lastIndexCheckPoint >= AIManager.Instance.WayPoints.Length)
            {
                lastIndexCheckPoint = 0;
            }
            agent.SetDestination(AIManager.Instance.WayPoints[lastIndexCheckPoint].gameObject.transform.position);
            
        }
    }

    void ChaseUpdate()
    {

        agent.SetDestination(_target.transform.position);
    }

    bool CheckDestinationReached(float ReachedTreshold)
    {
        float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        
        if (distanceToTarget < ReachedTreshold)
        {
            print("Destination reached");
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Coll with" + coll.name);
        if( coll.tag == "Player")
        {
            _target = coll.gameObject;
            _lookingCorutine = StartCoroutine(LookingForTarget());
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (_target == coll.gameObject && state == AIState.Patrol)
        {
            _target = null;
            if(_lookingCorutine != null)
            {
                StopCoroutine(_lookingCorutine);
                _lookingCorutine = null;
            }
        }
    }

    IEnumerator LookingForTarget()
    {
        while(_target != null && state == AIState.Patrol)
        {
            Debug.Log("Looking");
            Ray ray = new Ray(transform.position, _target.transform.position - transform.position);
            RaycastHit hit;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction);
            Color color = Color.red;
            if ( Physics.Raycast(ray,out hit))
            {
                
                if (hit.transform.gameObject == _target)
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction, Color.green);
                    ChangeState(AIState.Chase);
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction, color);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction, color);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    void ChangeState(AIState newState)
    {

        state = newState; 
    }

    void OnDrawGizmosSelected()
    {
        if(_target != null && _lookingCorutine == null)
        {
            Gizmos.DrawLine(transform.position, _target.transform.position);
        }
        if (AIManager.Instance.WayPoints == null)
        {
            return;
        }
        for (int i = 0; i < AIManager.Instance.WayPoints.Length; i++)
        {
            if (AIManager.Instance.WayPoints[i] == null)
                continue;
            if (i == 0)
            {
                Gizmos.DrawLine(transform.position, AIManager.Instance.WayPoints[i].transform.position);
            }
            else
            {
                if (AIManager.Instance.WayPoints[i-1] == null)
                    continue;
                Gizmos.DrawLine(AIManager.Instance.WayPoints[i - 1].transform.position, AIManager.Instance.WayPoints[i].transform.position);
            }
        }
    }
}
