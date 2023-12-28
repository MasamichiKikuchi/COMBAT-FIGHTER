using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�Q�[�������j���[�i�|�[�Y��ʁj�Ɋւ���N���X
public class GameMenu : MonoBehaviour
{
    //�|�[�Y��ʕ\���p�̃p�l��
    public GameObject pausePanel;
    //�Q�[���ĊJ�{�^��
    public Button returnGameButton;
    //�}�j���A���\���{�^��
    public Button manualButton;
    //�}�j���A���\���p�̃p�l��
    public GameObject manualPanel;
    //�}�j���A�������{�^��
    public Button closeManualButton;
    // �v���C���[�ւ̓��͂𐧌䂷��t���O
    public bool isInputEnabled = true;
    
    void Start()
    {
        pausePanel.SetActive(false);
        manualPanel.SetActive(false);
        returnGameButton.onClick.AddListener(ReturnGame);
        manualButton.onClick.AddListener(ShowManual);
        closeManualButton.onClick.AddListener(CloseManual);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�v���C���[�̓��͂𖳌��ɂ��ă|�[�Y��ʂ�\��
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isInputEnabled = false;
        }
    }

    private void ReturnGame()
    {
        //�|�[�Y��ʂ���ăQ�[���ĊJ
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isInputEnabled = true;
    }
    private void ShowManual()
    { 
      //�}�j���A���\��
      manualPanel.SetActive(true);
    }

    private void CloseManual()
    { 
      //�}�j���A�������
      manualPanel.SetActive(false);  
    }
}
