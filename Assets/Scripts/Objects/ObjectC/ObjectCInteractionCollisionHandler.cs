using System;
using UnityEngine;
using UnityEngine.AI;

public class ObjectCInteractionHandler : MonoBehaviour
{
    private IObjectCColliderHandler _objectC;
    private Collider _barricadeCollider;
    
    private void Awake()
    {
        _objectC = GetComponentInParent<IObjectCColliderHandler>();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         _objectC?.OnPlayerHide();
    //     }
    // }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            _objectC?.OnPlayerInteraction(other);
    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _objectC?.OnPlayerExitInteraction(other);
        }
        
    }

}