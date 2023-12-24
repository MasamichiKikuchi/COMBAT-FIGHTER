using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimeCountdown : MonoBehaviour
{
    public float totalTime = 90f; // �Q�[���̑����ԁi�b�j
    private float currentTime; // ���݂̌o�ߎ���

    public TextMeshProUGUI countdownText; // UI�ɕ\�����邽�߂̃e�L�X�g
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
        // UI�Ɏc�莞�Ԃ�\��
        if (countdownText != null)
        {
            int seconds = Mathf.CeilToInt(currentTime);
            countdownText.text = "Time: " + seconds.ToString() + "s";
        }
    }
}
