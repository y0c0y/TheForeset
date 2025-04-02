using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IEnemyCollisionHandler
{
    public static Enemy Instance { get; private set; }
    
    public bool IsWalking { get; private set; } = true;
    public bool IsHiding { get; private set; } = false; 


    [Header("Audio Clips")]
    public AudioClip voiceClip;
    public AudioClip crunchClip;
    private AudioSource _audio;

    [Header("Chasing")]
    [SerializeField] private PlayerController player;
    public float distance;
    
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
        player = PlayerController.Instance;
        SetNewPatrolPoint();
    }

    private void Update()
    {
        if (IsHiding && 
            !_agent.pathPending && 
            _agent.remainingDistance <= _agent.stoppingDistance && 
            _agent.velocity.sqrMagnitude == 0f)
        {
            SetNewPatrolPoint();
        }
        
        _agent.SetDestination(_targetPosition);
        RotateTowardsSteeringTarget();
    }
    
    private void SetNewPatrolPoint()
    {
        var randomCircle = Random.insideUnitCircle * distance;
        _staticPoint =  player.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        _targetPosition = _staticPoint;
    }
    
    private void RotateTowardsSteeringTarget()
    {
        var direction = (_agent.steeringTarget - transform.position).normalized;
        if (!(direction.sqrMagnitude > 0.01f)) return;
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void OnIsHiding(bool isHiding)
    {
        IsHiding = isHiding;
        Debug.Log(IsHiding);
    }

    public void OnPlayerDetected(Collider other)
    {
        if (IsHiding)
        {
            OnPlayerLost();
        }
        else
        {
            _targetPosition = other.transform.position;
        }
    }
    
    public void OnPlayerLost()
    {
        SetNewPatrolPoint();
    }

    public void OnPlayerHit()
    {
        IsWalking = false;
        player.isDead = true;
        
        // 플레이어와 거미의 움직임을 정지하고 캔버스를 표시
        GameManager.Instance.GameOver();
        // 캔버스가 표시된 후 오디오 재생을 위한 코루틴 실행
        StartCoroutine(PlayCrunchAudio());
    }

    private IEnumerator PlayCrunchAudio()
    {
        // Time.timeScale이 0이어도 실제 시간 기준으로 잠시 대기 (예: 0.1초)
        yield return new WaitForSecondsRealtime(0.1f);
    
        _audio.clip = crunchClip;
        _audio.volume = 1f;
        _audio.loop = false;
        _audio.Play();
    }

}
