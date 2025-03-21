using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHuntCollisionHandler : MonoBehaviour
{
    private IEnemyCollisionHandler _enemy;
    

    private void Awake()
    {
        _enemy = GetComponentInParent<IEnemyCollisionHandler>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _enemy?.OnPlayerHit(other.collider);
        }
    }
    
    
  
    
    
    

    



}
