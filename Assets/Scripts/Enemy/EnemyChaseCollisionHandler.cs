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
        if(!Enemy.Instance.IsChasing) return;
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player detected");
        _enemy?.OnPlayerDetected(other);
        // _enemy?.OnIsChasing(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _enemy?.OnPlayerLost();
            // _enemy?.OnIsChasing(false);
        }
    }
}
