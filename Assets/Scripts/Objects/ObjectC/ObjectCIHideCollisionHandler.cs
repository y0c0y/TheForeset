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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hide");
            _objectC?.OnPlayerHide(other);
           
        }
    }
    
    
  
    
    
    

    



}