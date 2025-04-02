using System;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyChaseCollisionHandler : MonoBehaviour
{
    private SphereCollider _collider;
    private IEnemyCollisionHandler _enemy;
    private Enemy _parent;
    private PlayerController _player;

    private void Awake()
    {
        _enemy = GetComponentInParent<IEnemyCollisionHandler>();
        _collider = GetComponent<SphereCollider>();
    }
    
    private void Start()
    {
        _parent = Enemy.Instance;
        _player = PlayerController.Instance;
        _collider.radius = _parent.distance;
    }

    private void Update()
    {
        if (_player)
        {
            _collider.radius =  _parent.distance * SpeedMode.ChangeSpeed(_player.moveMode);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Enemy.Instance.IsHiding) return;
        if (other.CompareTag("Player"))
        {
            _enemy?.OnPlayerDetected(other);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _enemy?.OnPlayerLost();
        }
    }
}
