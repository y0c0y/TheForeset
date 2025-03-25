using System;
using UnityEngine;
using UnityEngine.AI;

public class ObjectCHideHandler : MonoBehaviour
{
    private IObjectCColliderHandler _objectC;
    
    private void Awake()
    {
        _objectC = GetComponentInParent<IObjectCColliderHandler>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            _objectC?.OnPlayerHide();
        }
    }
    
    
  
    
    
    

    



}