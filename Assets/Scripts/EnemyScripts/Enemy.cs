using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, IEnemyCollisionHandler
{
    public Enemy Instance {get; private set;}
    
    private NavMeshAgent _agent;

    public bool IsWalking { get; private set; }
    public bool IsChasing {set; get;}
    
    private Vector3 _destination;
    private Vector3 _targetPosition;
    private List<Vector3> _startingPoint;
    public int _startingPointIdx;
    

    private void Awake()
    {
        Instance = this;
        
        
        IsWalking = true;
        _startingPoint = new List<Vector3>();
        _startingPointIdx = 0;
        _agent = GetComponent<NavMeshAgent>();
        _startingPoint.Add(Vector3.zero);
        
        IsChasing = true;

       
        Debug.Log(_startingPoint.Count);
        
        _agent.SetDestination(_startingPoint[_startingPointIdx]);
        
        Debug.Log(_startingPoint.Count);
    }
    
    
    
    void Update()
    {
        _agent.SetDestination(_targetPosition);
        
        Vector3 targetDirection = (_agent.steeringTarget - transform.position).normalized;
    
        if (targetDirection.sqrMagnitude > 0.01f) // 너무 가까운 경우 회전 방지
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        
        
    }

    public void OnPlayerDetected(Collider other)
    {
     
        _targetPosition = IsChasing ? other.transform.position :  _startingPoint[_startingPointIdx];
       
    }
    
    public void OnPlayerLost()
    {
        _targetPosition = _startingPoint[_startingPointIdx];
    }

    public void OnPlayerHit()
    {
        IsWalking = false;
        GameManager.Instance.GameOver();
    }


}

