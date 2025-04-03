using TMPro;
using UnityEngine;


public class Talk : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1f;
    private int i = 0;
    private float t;
    private float timeScale = 1f;
    private float timer;

    private string[] text;

    private bool fadein = false;
    private bool textPlay = false;
    private bool fadeout = false;
    private bool textSkip = false;
    
    private bool isEnd;

    public bool IsAllEnd { get; private set; }


    public void Play(float time, string[] texts)
    {
        timer = time;
        text = texts;

        i = 0;

        isEnd = true;
        fadein = false;
        textPlay = false;
        fadeout = false;
        IsAllEnd = false;
        textSkip = false;
    }


    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     textSkip = true;
        // }
        //
        // if (textSkip)
        // { 
        //     TextSkipProcess();
        // }
        
        if (isEnd)
        {
            TextSetProcess();
        }

        if (fadein)
        {
            FadeInProcess();
        }

        if (textPlay)
        {
            TextTimeProcess();
        }

        if (fadeout)
        {
            FadeOutProcess();
        }
    }


    // private void TextSkipProcess()
    // {
    //     canvasGroup.alpha -= fadeSpeed * Time.unscaledDeltaTime;
    //
    //     if (canvasGroup.alpha <= 0f)
    //     {
    //         canvasGroup.alpha = 0f;
    //         
    //         isEnd = false;
    //         
    //
    //     }
    // }

    private void TextSetProcess()
    {
        if (i < text.Length)
        {
            displayText.text = text[i];
            fadein = true;
        }
        else
        {
            IsAllEnd = true;
        }
        isEnd = false;
    }

    private void FadeInProcess()
    {
        canvasGroup.alpha += fadeSpeed * Time.unscaledDeltaTime;
        // 페이드 인이 완료된 후
        if (canvasGroup.alpha >= 1f)
        {
            canvasGroup.alpha = 1f;
            fadein = false;
            textPlay = true;
            t = 0;
        }
    }

    private void TextTimeProcess()
    {
        t += timeScale * Time.unscaledDeltaTime;
        // 텍스트 보여주는 시간이 만료되었을 때
        if (t >= timer)
        {
            textPlay = false;
            fadeout = true;
        }
    }

    private void FadeOutProcess()
    {
        canvasGroup.alpha -= fadeSpeed * Time.unscaledDeltaTime;
        // 페이드 아웃이 완료될 때
        if (canvasGroup.alpha <= 0f)
        {
            canvasGroup.alpha = 0f;
            i++;
            isEnd = true;
            fadeout = false;
        }
    }
}
