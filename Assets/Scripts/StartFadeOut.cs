using System;
using UnityEngine;
using UnityEngine.UI;

public class StartImage : MonoBehaviour
{
    //연결할 시작 화면 이미지
    public Image fadeImage;
    //
    public Canvas canvas;
    //
    private CanvasGroup CanvasGroup;
    //
    private GameObject GM;
    //이미지가 사라지는데 걸리는 시간 (초)
    public float fadeDuration = 1f;
    //시간이 얼마나 흘렀는지 저장하는 변수
    float timer = 0f;
    //지금 페이드아웃중인지 확인하는 변수
    bool isfading = false;
    //이 스크립트가 한번만 실행되도록 하는 값을 담을 변수
    bool hasStarted = true;
    //
    float originalAlpha;
    
    void Start()
    {
        Time.timeScale = 0f;
        CanvasGroup = canvas.GetComponent<CanvasGroup>();
        GM = GameObject.Find("GM");
        originalAlpha = CanvasGroup.alpha;
    }

    void Update()
    {
        if (hasStarted && CheckPlayerInput())
        {
            isfading = true;
            hasStarted = false;
        }
        if (isfading)
        {
            // Debug.Log("시작됨");
            
            
            
            timer += Time.unscaledDeltaTime;
            
            float alpha = Mathf.Lerp(originalAlpha, 0f, timer / fadeDuration);
            // Debug.Log($"{timer} -> {alpha}");
            
            // Color color = fadeImage.color;
            // color.a = alpha;
            // fadeImage.color = color;
            
            CanvasGroup.alpha = alpha;

            if (alpha <= 0f)
            {
                isfading = false;
                // canvas.gameObject.SetActive(false);
                GM.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
    
    //플레이어 입력 감지 함수
    bool CheckPlayerInput()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

 
}
