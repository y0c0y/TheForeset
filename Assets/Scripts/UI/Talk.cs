using TMPro;
using UnityEngine;


public class Talk : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 0.5f;
    private int i = 0;
    private float t;
    private float timeScale = 1f;
    private float timer;

    private string[] text;

    private bool fadein = false;
    private bool textPlay = false;
    private bool fadeout = false;

    private bool isEnd;

    public bool IsAllEnd { get; private set; }


    public void Play(float time, string[] texts)
    {
        timer = time;
        text = texts;


        isEnd = true;
        fadein = false;
        textPlay = false;
        fadeout = false;
        IsAllEnd = false;
    }


    void Update()
    {
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
