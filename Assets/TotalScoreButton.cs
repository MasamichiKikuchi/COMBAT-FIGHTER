using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//�����L���O��ʂ̃g�[�^���X�R�A�\���p�̃{�^��
public class TotalScoreButton : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;

    void Start()
    {
        totalScoreText.text =($"{ Score.Instance.totalScore}");
    }

   
}
