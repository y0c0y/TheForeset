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
    
    private void OnTriggerStay(Collider other)
    {
        
        Debug.Log("OnTriggerStay");
        
        if (other.CompareTag("Player"))
        {
            _objectC?.OnPlayerInteraction(other);
            Debug.Log("Hiding");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        
        if (other.CompareTag("Player"))
        {
            _objectC?.OnPlayerExitInteraction(other);
            Debug.Log("HidingExit");
        }

        
    }

}