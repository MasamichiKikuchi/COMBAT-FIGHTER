using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//���U���g��ʂ̕\����f�[�^�Ɋւ���N���X
public class Result : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeBonusText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI rankText;
    private int lifeBonus;
    public int totalScore;
    public string rank;

    // �V���O���g���C���X�^���X
    private static Result _instance;

    // �C���X�^���X�ɃA�N�Z�X����v���p�e�B
    public static Result Instance
    {
        get
        {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowResultScore();
        ShowLifeBonus();
        ShowTotalScore(lifeBonus);
        ShowRank(totalScore);

        if (_instance == null)
        {
            _instance = this;
        }
    }

    void ShowResultScore()
    {
        //�v���C���[�̌��ăX�R�A��\��
        scoreText.text = ($"SCORE:{Score.Instance.playerScore}");
    }

    int ShowLifeBonus()
    {
       //�v���C���[�̃��C�t�ɉ����ă{�[�i�X���v�Z���A�\������
       lifeBonus =   Player.Instance.life * 100;
       lifeBonusText.text =($"LIFE BONUS:{lifeBonus}");
       return (lifeBonus);
    }

    int ShowTotalScore(int lifeBonus)
    {
        //���ăX�R�A�ƃ��C�t�{�[�i�X�����Z���A�g�[�^���X�R�A�Ƃ���
        totalScore = Score.Instance.playerScore + lifeBonus;
        totalScoreText.text = ($"TOTAL SCORE:{totalScore}");
        return (totalScore);   
    }

    void ShowRank(int totalScore)
    {
        //�g�[�^���X�R�A�ɉ����ă����N�t��
        if (totalScore >= 2500)
        {
            rank = "S";
        }
        else if (totalScore >= 2000)
        {
            rank = "A";
        }
        else if (totalScore >= 1500)
        {
            rank = "B";
        }
        else if (totalScore >= 1000)
        {
            rank = "C";
        }
        else if (totalScore >= 500)
        {
            rank = "D";
        }
        else
        { 
            rank= "E";
        }

        rankText.text = ($"RACK:{rank}");
    
    }  
}
