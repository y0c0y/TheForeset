using System;
using UnityEngine;

public class ObjectD : MonoBehaviour
{
    public GameObject player;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.GetComponent<PlayerController>().Instance.isStunning = true;
                Debug.Log("Destroyed");
            }
            Destroy(gameObject);
            
            
            
        }
    }
}
