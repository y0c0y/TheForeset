using UnityEngine;

public interface IEnemyCollisionHandler
{
    void OnPlayerHit();
    void OnIsChasing(bool tmp);
    
    void OnPlayerDetected(Collider other);
    
    void OnPlayerLost();
    
    // void OnAnimatorIsWalking(bool value);
    
}

