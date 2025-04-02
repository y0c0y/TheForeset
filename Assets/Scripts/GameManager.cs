using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    
    public event Action<bool> OnGameOverCanvas;
    
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //uiManager.OnGameOverCanvas();
        OnGameOverCanvas?.Invoke(false);
    }

    private void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        OnGameOverCanvas?.Invoke(true);
    }
}