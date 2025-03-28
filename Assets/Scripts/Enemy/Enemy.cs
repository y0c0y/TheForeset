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

    private bool _isChasing;
    
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
        _isChasing = false;
    }

    private void Start()
    {
        _targetPosition = ChangeStaticPoint();
        _agent.SetDestination(_targetPosition);
    }

    private void Update()
    {
        Debug.Log(_isChasing);
        if (!_isChasing)
        {
            
            Debug.Log("Lost");
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                
                Vector3 playerPos = player.transform.position;
                
                CheckPosition();

            }
        }


        _agent.SetDestination(_targetPosition);
        
        Vector3 targetDirection = (_agent.steeringTarget - transform.position).normalized;
    
        if (targetDirection.sqrMagnitude > 0.01f) // 너무 가까운 경우 회전 방지
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void CheckPosition()
    {
        // Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        if (_agent.pathPending) return;
        
        if (_agent.hasPath && _agent.velocity.sqrMagnitude != 0f) return;

        Debug.Log("목적지에 도착했습니다.");

        _staticPoint = ChangeStaticPoint();
       _targetPosition = _staticPoint;
    }

    private Vector3 ChangeStaticPoint()
    {
        // if(player == null) return Vector3.zero;
        Vector3 playerPos = player.transform.position;
        
        var point = new Vector3(Mathf.Sign(playerPos.x),0,Mathf.Sign(playerPos.z));
        point *= distance;
        Debug.Log(point);
        return point;
    }
    
    public void OnIsChasing(bool tmp)
    {
        _isChasing = tmp;
        Debug.Log(_isChasing);
    }


    public void OnPlayerDetected(Collider other)
    {
        if (_isChasing)
        {
            _targetPosition = other.transform.position;
            Debug.Log("Chasing");
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

