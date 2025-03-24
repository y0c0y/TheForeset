using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, IEnemyCollisionHandler
{
    public Enemy Instance {get; private set;}
    
    
    private NavMeshAgent _agent;

    public bool IsChasing {set; get;}
    
    private Vector3 _destination;
    private Vector3 _targetPosition;
    private List<Vector3> _startingPoint;
    private int _startingPointIdx; 

    private void Awake()
    {
        _startingPoint = new List<Vector3>();
        _startingPointIdx = 0;
        _agent = GetComponent<NavMeshAgent>();
        _startingPoint.Add(Vector3.zero);
        _agent.SetDestination(_startingPoint[_startingPointIdx]);
    }
    
    private void Start()
    {
    }
    
    void Update()
    {
        _agent.SetDestination(_targetPosition);
    }

    public void OnPlayerDetected(Collider other)
    {
        _targetPosition = other.transform.position;
    }
    
    public void OnPlayerLost(Collider other)
    {
        _targetPosition = _startingPoint[_startingPointIdx];
    }

    public void OnPlayerHit(Collider other)
    {
        _agent.isStopped = true;
        GameManager.Instance.GameOver();
    }

    // public void OnPlayerHided(Collider player)
    // {
    //     _agent.isStopped = false;
    // }
}

