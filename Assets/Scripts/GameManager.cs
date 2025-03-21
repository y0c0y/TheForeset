using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    
    public event Action<bool> OnGameOverCanvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnGameOverCanvas?.Invoke(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        OnGameOverCanvas?.Invoke(true);
    }
}
