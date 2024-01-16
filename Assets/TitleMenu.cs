using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�^�C�g����ʂ��Ǘ�����N���X
public class TitleMenu : MonoBehaviour
{
    //�}�j���A���\���p�̃p�l��
    public GameObject manualPanel;
    //�}�j���A���\���{�^��
    public Button manualButton;
    //�}�j���A�������{�^��
    public Button closeManualButton;
    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̃X�R�A�����Z�b�g
        Score.Instance.playerScore = 0;

        //�Q�[�����j���[����^�C�g���ɖ߂����ꍇ�̂��߂ɁA�Q�[�����Ԃ����ɖ߂�
        Time.timeScale = 1;

        manualPanel.SetActive(false);
        manualButton.onClick.AddListener(ShowManual);
        closeManualButton.onClick.AddListener(CloseManual);
    }

    private void ShowManual()
    {
        //�}�j���A���\��
        manualPanel.SetActive(true);
    }

    private void CloseManual()
    {
        //�}�j���A����\��
        manualPanel.SetActive(false);
    }
}
