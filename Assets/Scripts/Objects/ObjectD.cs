using System;
using UnityEngine;

public class ObjectD : MonoBehaviour
{
    public GameObject player;
    
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    private bool _hasPlayed;
    
    private void Awake()
    {
        _hasPlayed = true;
    }
    private void Update()
    {
        if (_hasPlayed) return;
        audioSource.PlayOneShot(audioClip);
        _hasPlayed = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (player == null) return;
        
        player.GetComponent<PlayerController>().Instance.isStunning = true;
        _hasPlayed = false;
        Destroy(gameObject);
    }
}
