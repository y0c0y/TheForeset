using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHuntCollisionHandler : MonoBehaviour
{
    private IEnemyCollisionHandler _enemy;
    private Enemy _parent;
    

    private void Awake()
    {
        _enemy = GetComponentInParent<IEnemyCollisionHandler>();
        _parent = GetComponentInParent<Enemy>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!_parent.Instance.IsChasing) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            _enemy?.OnPlayerHit();
        }
    }
    
    
  
    
    
    

    



}
