using UnityEngine;

public interface IEnemyCollisionHandler
{
    void OnPlayerHit(Collider other);
    // void OnPlayerHide(Collider other);
    
    void OnPlayerDetected(Collider other);
    
    void OnPlayerLost(Collider other);
    
}

