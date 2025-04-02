using UnityEngine;

public class GGAMBBACK : MonoBehaviour
{
    

    CanvasGroup canvasGroup;

    public float blinkSpeed = 65f;
    private float blinkSum = 0f;
    
    private float radian = 0;
    private float sin = 0;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        canvasGroup.alpha = 1f;
    }

    void Update()
    {
        blinkSum += blinkSpeed * Time.deltaTime;
        if (blinkSum >= 360f)
        {
            blinkSum -= 360f;
        }
        radian += Mathf.Deg2Rad * blinkSum;
        sin = (Mathf.Sin(radian) + 1) / 2;
        
        canvasGroup.alpha = sin;

        
    }
}
