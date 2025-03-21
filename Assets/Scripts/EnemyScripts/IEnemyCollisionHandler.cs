using UnityEngine;

public interface IEnemyCollisionHandler
{
    void OnPlayerDetected(Collider other);
    void OnPlayerHit(Collider other);
    
    void OnPlayerLost(Collider other);
    
}

