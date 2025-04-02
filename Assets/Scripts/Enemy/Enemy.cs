using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IEnemyCollisionHandler
{
    public static Enemy Instance { get; private set; }
    
    public bool IsWalking { get; private set; } = true;
    public bool IsChasing { get; private set; } = true;

    public AudioClip voice;
    public AudioClip crunch;
    private AudioSource _audio;

    [SerializeField] private GameObject player;
    [SerializeField] private int distance = 10;

    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private Vector3 _staticPoint;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        _audio = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _targetPosition = GetRandomPatrolPoint();
        _agent.SetDestination(_targetPosition);
    }

    private void Update()
    {
        if (!IsChasing && 
            !_agent.pathPending && 
            _agent.remainingDistance <= _agent.stoppingDistance && 
            _agent.velocity.sqrMagnitude == 0f)
        {
            // Debug.Log("이게 얼마나 호출되는겨");
            SetNewPatrolPoint();
        }
        
        _agent.SetDestination(_targetPosition);
        RotateTowardsSteeringTarget();
    }
    
    private void SetNewPatrolPoint()
    {
        _staticPoint = GetRandomPatrolPoint();
        _targetPosition = _staticPoint;
    }
    
    private Vector3 GetRandomPatrolPoint()
    {
        var randomCircle = Random.insideUnitCircle * distance;
        return player.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
    }
    
    private void RotateTowardsSteeringTarget()
    {
        var direction = (_agent.steeringTarget - transform.position).normalized;
        if (!(direction.sqrMagnitude > 0.01f)) return;
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void OnIsChasing(bool isChasing)
    {
        IsChasing = isChasing;
        // Debug.Log(IsChasing);
    }

    public void OnPlayerDetected(Collider other)
    {
        if (IsChasing)
        {
            // 플레이어를 추적
            _targetPosition = other.transform.position;
        }
        else
        {
            // 순찰 모드로 복귀
            SetNewPatrolPoint();
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
        _audio.clip =null;
        _audio.PlayOneShot(crunch);
        GameManager.Instance.GameOver();
    }
}
