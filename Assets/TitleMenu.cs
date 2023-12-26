using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�^�C�g����ʂ��Ǘ�����N���X
public class TitleMenu : MonoBehaviour
{
    public GameObject manualPanel;
    public Button manualButton;
    public Button closeManualButton;
    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̃X�R�A�����Z�b�g
        Score.Instance.playerScore = 0;

        manualPanel.SetActive(false);
        manualButton.onClick.AddListener(ShowManual);
        closeManualButton.onClick.AddListener(CloseManual);
    }

    private void ShowManual()
    {
        manualPanel.SetActive(true);
    }

    private void CloseManual()
    {
        manualPanel.SetActive(false);
    }
}
