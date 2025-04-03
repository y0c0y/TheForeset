using System;
using System.Collections;
using UnityEngine;

public class EndCan : MonoBehaviour
{
    public Talk talk;
    public CanvasGroup canvasGroup;
    public CanvasGroup endText;
    public GameObject choice;
    
    private bool isFading;
    private bool startEndText;
    private bool _canChoice;
    
    public float fadeSpeed = 0.5f;

    private PlayerController _player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFading = true;
            _player.isWon = true;
        }
    }

    private void Awake()
    {
        
        startEndText = false;
        _canChoice = false;
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        endText.alpha = 0f;
        choice.SetActive(false);
        
        _player = PlayerController.Instance;
    }

    void Update()
    {
        if (isFading)
        {
            Time.timeScale = 0f;
            canvasGroup.alpha += fadeSpeed * Time.unscaledDeltaTime;

            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;

                talk.Play(
                    1f,
                    new[]
                    {
                        "후.. 드디어 집이다..",
                        "정말 이상하고 무서운 하루였어..",
                        "으! 생각하기 싫다 얼른 자야지!!"
                    });
                isFading = false;
                startEndText = true; 
            }
        }

        if (startEndText)
        {
            if (talk.IsAllEnd)
            {
                endText.alpha += fadeSpeed * Time.unscaledDeltaTime;
                choice.SetActive(true);

                if (endText.alpha >= 1f)
                {
                    endText.alpha = 1f;
                    startEndText = false;
                    
                    GameManager.GameWon();
                }

            }
        }

        if (_canChoice)
        {
            // choice.SetActive(_canChoice);
            // Debug.Log(choice.activeSelf);
        }

    }
}

