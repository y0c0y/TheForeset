using System;
using UnityEngine;

public class ObjectD : MonoBehaviour
{
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
            
            Destroy(gameObject);

            //playerControler.isState = Enum.SpiderWeb;


            //Action?? => moveSpeed -50%?
        }
    }
}
