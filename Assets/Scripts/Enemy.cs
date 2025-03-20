using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    
    float moveSpeed = 2f;
    

    void Start()
    {
        // InvokeRepeating("FindTarget", 0f, 1f);
        // target = null;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }


    

}
