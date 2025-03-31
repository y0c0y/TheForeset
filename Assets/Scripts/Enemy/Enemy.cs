using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour, IEnemyCollisionHandler
{
    public Enemy Instance {get; private set;}
    
    private NavMeshAgent _agent;

    public bool IsWalking { get; private set; }

    public bool IsChasing {get; private set;}
    
    private Vector3 _destination;
    
    
    [SerializeField] private GameObject player;
    
    private Vector3 _targetPosition;
    
    private Vector3 _staticPoint;

    [SerializeField] private int distance;
    
    
    Action<bool> _onPlayerInteraction;
    
    
    private void Awake()
    {
        Instance = this;
        
        _agent = GetComponent<NavMeshAgent>();
        
        IsWalking = true;
        IsChasing = true;
    }

    private void Start()
    {
        _targetPosition = ChangeStaticPoint();
        _agent.SetDestination(_targetPosition);
    }

    private void Update()
    {
        // Debug.Log(IsChasing);
        if (!IsChasing)
        {
            // Debug.Log("Lost");
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                CheckPosition();
            }
        }
        
        _agent.SetDestination(_targetPosition);
        
        var targetDirection = (_agent.steeringTarget - transform.position).normalized;

        if (!(targetDirection.sqrMagnitude > 0.01f)) return; // 너무 가까운 경우 회전 방지
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void CheckPosition()
    {
        if (_agent.pathPending) return;
        if (_agent.hasPath && _agent.velocity.sqrMagnitude != 0f) return;
        
        _staticPoint = ChangeStaticPoint();
       _targetPosition = _staticPoint;
    }

    private Vector3 ChangeStaticPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * distance;
        Vector3 newTarget = player.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        return newTarget;
    }
    
    public void OnIsChasing(bool tmp)
    {
        IsChasing = tmp;
        Debug.Log(IsChasing);
    }


    public void OnPlayerDetected(Collider other)
    {
        if (IsChasing)
        {
            _targetPosition = other.transform.position;
        }
        else
        {
            CheckPosition();
            _targetPosition = _staticPoint;
        }
    }
    
    public void OnPlayerLost()
    {
        _targetPosition = _staticPoint;
    }

    public void OnPlayerHit()
    {
        IsWalking = false;
        GameManager.Instance.GameOver();
        IsWalking = true;
    }


}

