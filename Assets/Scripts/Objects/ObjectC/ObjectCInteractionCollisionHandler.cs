using UnityEngine;

public class ObjectCInteractionHandler : MonoBehaviour
{
    private IObjectCColliderHandler _objectC;
    private Collider _barricadeCollider;
    
    private void Awake()
    {
        _objectC = GetComponentInParent<IObjectCColliderHandler>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _objectC?.OnPlayerInteraction(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _objectC?.OnPlayerExitInteraction(other);
    }

}