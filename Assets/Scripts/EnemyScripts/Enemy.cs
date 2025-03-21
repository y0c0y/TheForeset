using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, IEnemyCollisionHandler
{
    private NavMeshAgent _agent;
    private GameObject _player;
    
    private Vector3 _destination;
    private Vector3 _targetPosition;
    private Vector3 _startingPoint;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startingPoint = Vector3.zero;
        _agent.SetDestination(_startingPoint);
    }
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        _agent.SetDestination(_targetPosition);
        // Debug.DrawLine(_player.transform.position, _targetPosition, Color.red);
    }

    public void OnPlayerDetected(Collider player)
    {
        _targetPosition = player.transform.position;
    }
    
    public void OnPlayerLost(Collider player)
    {
        _targetPosition = _startingPoint;
    }

    public void OnPlayerHit(Collider player)
    {
        _agent.isStopped = true;
        GameManager.Instance.GameOver();
    }
}

