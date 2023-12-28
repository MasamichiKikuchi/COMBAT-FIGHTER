using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//�Q�[���̐������Ԃ��Ǘ�����N���X
public class GameTimeCountdown : MonoBehaviour
{
    // �Q�[���̑����ԁi�b�j
    public float totalTime = 90f;
    // ���݂̌o�ߎ���
    private float currentTime;
    // UI�ɕ\�����邽�߂̃e�L�X�g
    public TextMeshProUGUI countdownText; 
    //�������Ԃ������Ă���Ƃ��̉�
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

            // UI�Ɏc�莞�Ԃ�\��
            UpdateUI();

            if (currentTime <= 5f)
            {
                //�������Ԃ����Ȃ��Ȃ����特��炷
                audioSource.PlayOneShot(countDownSE);
            }
        }
        
        else
        {
            //�������Ԃ��I�������烊�U���g��ʂɈڂ�
            SceneManager.LoadScene("ResultScene");
        }
    }

    void UpdateUI()
    {      
    �@int seconds = Mathf.CeilToInt(currentTime);
      countdownText.text = "Time: " + seconds.ToString() + "s";
    }
}
