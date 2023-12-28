using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//���U���g��ʂɊւ���N���X
public class Result : MonoBehaviour
{
    //�e���l��\������e�L�X�g
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeBonusText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI rankText;

    void Start()
    {
        //�X�R�A�ƃ����N���v�Z
        Score.Instance.LifeBonus();
        Score.Instance.TotalScore();
        Score.Instance.Rank();

        //���ʂ�\��
        ShowResultScore();
        ShowLifeBonus();
        ShowTotalScore();
        ShowRank();

    }

    void ShowResultScore()
    {
        //�v���C���[�̌��ăX�R�A��\��
        scoreText.text = ($"SCORE:{Score.Instance.playerScore}");
    }

    void ShowLifeBonus()
    {   
       lifeBonusText.text =($"LIFE BONUS:{Score.Instance.lifeBonus}");
    }

    void ShowTotalScore()
    {
        totalScoreText.text = ($"TOTAL SCORE:{Score.Instance.totalScore}");       
    }

    void ShowRank()
    {
        rankText.text = ($"RACK:{Score.Instance.rank}");
    }  
}
