using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//�����L���O��ʂ̃g�[�^���X�R�A�\���p�̃{�^��
public class TotalScoreButton : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        totalScoreText.text =($"{ Result.Instance.totalScore}");
    }

   
}
