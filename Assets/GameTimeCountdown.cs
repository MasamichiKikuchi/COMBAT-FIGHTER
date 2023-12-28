using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//ゲームの制限時間を管理するクラス
public class GameTimeCountdown : MonoBehaviour
{
    // ゲームの総時間（秒）
    public float totalTime = 90f;
    // 現在の経過時間
    private float currentTime;
    // UIに表示するためのテキスト
    public TextMeshProUGUI countdownText; 
    //制限時間が迫っているときの音
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

            // UIに残り時間を表示
            UpdateUI();

            if (currentTime <= 5f)
            {
                //制限時間が少なくなったら音を鳴らす
                audioSource.PlayOneShot(countDownSE);
            }
        }
        
        else
        {
            //制限時間が終了したらリザルト画面に移る
            SceneManager.LoadScene("ResultScene");
        }
    }

    void UpdateUI()
    {      
    　int seconds = Mathf.CeilToInt(currentTime);
      countdownText.text = "Time: " + seconds.ToString() + "s";
    }
}
