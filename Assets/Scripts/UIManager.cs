using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject endingCanvas;
    
    private void Start()
    {
        GameManager.OnGameOverCanvas += OnGameOverCanvas;
    }

    private void OnGameOverCanvas(bool isGameOver)
    {
        Debug.Log(isGameOver);
        if (endingCanvas != null)
        {
            endingCanvas.SetActive(isGameOver); 
        }
        else
        {
            Debug.LogWarning("GameOverCanvas is null or destroyed!");
        }
       
    }
}
