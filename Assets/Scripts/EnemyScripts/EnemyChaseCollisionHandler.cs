using System;
using UnityEngine;


public class EnemyChaseCollisionHandler : MonoBehaviour
{
    private IEnemyCollisionHandler _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<IEnemyCollisionHandler>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy?.OnPlayerDetected(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy?.OnPlayerLost();
        }
    }
}
