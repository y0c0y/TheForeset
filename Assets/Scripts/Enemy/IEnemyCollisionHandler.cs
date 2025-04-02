using UnityEngine;

public interface IEnemyCollisionHandler
{
    void OnPlayerHit();
    
    void OnPlayerDetected(Collider other);
    
    void OnPlayerLost();
}

