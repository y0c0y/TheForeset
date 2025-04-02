using System;
using UnityEngine;

public class ObjectD : MonoBehaviour
{
    public AudioSource audioSource;
    
    private PlayerController _player;

    private void Start()
    {
        _player = PlayerController.Instance;
        if (_player == null)
        {
            Debug.LogError("플레이어 싱글톤이 초기화되지 않았습니다!");
        }
        
        audioSource.Stop();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_player == null) return;
        
        _player.isStunning = true;
        audioSource.Play();
        Destroy(gameObject, audioSource.clip.length);
    }
}