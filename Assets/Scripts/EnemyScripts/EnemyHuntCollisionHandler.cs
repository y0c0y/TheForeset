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
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Hit");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            _enemy?.OnPlayerHit(other);
        }
    }
    
    
  
    
    
    

    



}
