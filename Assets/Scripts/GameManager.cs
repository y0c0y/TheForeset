using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<bool> OnGameOverCanvas;

    private static bool IsGameOver { get; set; }
    
    private void Start()
    {
        IsGameOver = false;
    }

    private void Update()
    {
        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ExitGame();
            }
        }
       
    }

    public static void GameWon()
    {
        IsGameOver = true;
    }
    
    public static void GameOver()
    {
        Time.timeScale = 0f;
        IsGameOver = true;
        OnGameOverCanvas?.Invoke(IsGameOver);
    }

    public static void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public static void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}