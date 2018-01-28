﻿
using System;
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
    public float Reload = 0.5f;
    public float destinationReachedPatrolTreshold = 3;
    public float destinationReachedChaseTreshold = 8;
    public GameObject Shot;
    public int[] CheckpointsIndexs;

    GameObject _target;
    public AIState state = AIState.Patrol;
    private Coroutine _lookingCorutine;
    private Coroutine _attackCorutine;

    public float ForceShoot = 10;

    void Start()
    {
        if (!agent)
            return;
        ChangeState(state);
    }

    internal void KillTarger()
    {
        _target = null;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!agent)
            return;

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
                AttackUpdate();
                break;
            default:
                break;
        }
    }

    private void AttackUpdate()
    {
        if(_attackCorutine == null)
        {
            _attackCorutine= StartCoroutine(AttackCorutine());
        }
        if(!IsLookingOnTarget())
        {
            if (_attackCorutine != null)
            {
                StopCoroutine(_attackCorutine);
                _attackCorutine = null;
                ChangeState(AIState.Chase);
            }
        }
    }    

    void PatrolUpdate()
    {
        if(AIManager.Instance.WayPoints == null)
        {
            return;
        }
        if (CheckDestinationReached(destinationReachedPatrolTreshold))
        {
            lastIndexCheckPoint++;

            if (lastIndexCheckPoint >= AIManager.Instance.WayPoints.Length)
            {
                lastIndexCheckPoint = 0;
            }
            if(AIManager.Instance.WayPoints[lastIndexCheckPoint] != null)
                agent.SetDestination(AIManager.Instance.WayPoints[lastIndexCheckPoint].gameObject.transform.position);
            
        }
    }

    void ChaseUpdate()
    {
        if (_target)
        {
            agent.SetDestination(_target.transform.position);
        }
        else
        {
            ChangeState(AIState.Patrol);
        }

        if (CheckDestinationReached(destinationReachedChaseTreshold) && IsLookingOnTarget())
        {
            ChangeState(AIState.Attack);
        }
    }

    bool CheckDestinationReached(float ReachedTreshold)
    {
        if (!agent)
            return false;


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
        while (_target != null && state == AIState.Patrol)
        {
            Debug.Log("Looking");
            if (IsLookingOnTarget())
            {
                ChangeState(AIState.Chase);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        } 
    }

    private IEnumerator AttackCorutine()
    {
        while(_target)
        {
            yield return new WaitForSeconds(Reload);
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            GameObject shot = Instantiate(Shot, transform.position + direction * 2, Quaternion.LookRotation(direction));
            Rigidbody shotRig = shot.GetComponent<Rigidbody>();
            if(shotRig)
            {
                Shot compShot = shotRig.GetComponent<Shot>();
                if(compShot)
                {
                    compShot.owner = this;
                }
                shotRig.AddForce(direction * ForceShoot, ForceMode.Impulse);
            }
            else
            {
                Destroy(shot);
            }
        }
    }

    bool IsLookingOnTarget()
    {
        if(_target == null)
        {
            return false;
        }
        Ray ray = new Ray(transform.position, _target.transform.position - transform.position);
        RaycastHit hit;
        Color color = Color.red;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject == _target)
            {
                Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 1000), Color.green);
                return true;
            }
        }
          
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 1000), Color.red);
        return false;
    }
    

    void ChangeState(AIState newState)
    {
        if(state == AIState.Chase)
        {
            if(newState == AIState.Attack)
            {
                agent.isStopped = true;
            }
        }
        if (state == AIState.Attack)
        {
            if (newState != AIState.Attack)
            {
                agent.isStopped = false;
            }
        }
        if (newState == AIState.Patrol)
        {
            agent.SetDestination(AIManager.Instance.WayPoints[lastIndexCheckPoint].gameObject.transform.position);
        }
        state = newState; 
    }

    bool GetCheckPoint(out Vector3 point, int index)
    {

        if(AIManager.Instance.WayPoints.Length > 0 && CheckpointsIndexs.Length > 0)
        {
            if(CheckpointsIndexs.Length > index)
            {
               // CheckpointsIndexs[index];
            }
        }
        point = Vector3.zero;
        return false;
        
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

    public void Die()
    {
        Debug.Log("TODO: Some explosion effect for enemy kill.");
        Destroy(gameObject);
    }
}
