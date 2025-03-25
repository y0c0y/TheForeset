using UnityEngine;

public interface IEnemyCollisionHandler
{
    void OnPlayerHit();
    // void OnPlayerHide(Collider other);
    
    void OnPlayerDetected(Collider other);
    
    void OnPlayerLost();
    
    // void OnAnimatorIsWalking(bool value);
    
}

