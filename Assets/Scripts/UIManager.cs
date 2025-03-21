using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public Canvas EndingCanvas;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnGameOverCanvas += OnGameOverCanvas;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameOverCanvas(bool isGameOver)
    {
        EndingCanvas.enabled = isGameOver;
    }
}
