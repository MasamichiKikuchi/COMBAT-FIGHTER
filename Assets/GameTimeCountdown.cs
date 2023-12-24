using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimeCountdown : MonoBehaviour
{
    public float totalTime = 90f; // ゲームの総時間（秒）
    private float currentTime; // 現在の経過時間

    public TextMeshProUGUI countdownText; // UIに表示するためのテキスト
    public AudioSource audioSource;
    public AudioClip countDownSE;


    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();

            if (currentTime <= 5f)
            {
                audioSource.PlayOneShot(countDownSE);
            }
        }
        
        else
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    void UpdateUI()
    {
        // UIに残り時間を表示
        if (countdownText != null)
        {
            int seconds = Mathf.CeilToInt(currentTime);
            countdownText.text = "Time: " + seconds.ToString() + "s";
        }
    }
}
