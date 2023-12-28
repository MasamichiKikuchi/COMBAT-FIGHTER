using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[�̃X�R�A���Ǘ�����N���X
public class Score
{
    // �V���O���g���C���X�^���X
    static Score instance;
    //�v���C���[�̌��ăX�R�A
    public int playerScore;

    public int lifeBonus;

    public int totalScore;

    public string rank;
    // �C���X�^���X�ɃA�N�Z�X����v���p�e�B
    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }

            return instance;
        }
    }

    // �R���X�g���N�^
    private Score()
    {    
        playerScore = 0;
        lifeBonus = 0;
    }

    public void AddScore(int score)
    {
        //���ăX�R�A�����Z
        instance.playerScore += score;
    }
    
    public void LifeBonus()
    {
        //�v���C���[�̃��C�t�ɉ����ă{�[�i�X���v�Z���A�\������
        lifeBonus = Player.Instance.life * 100;
    }

    public void TotalScore()
    {  
        //���ăX�R�A�ƃ��C�t�{�[�i�X�����Z���A�g�[�^���X�R�A�Ƃ���
        totalScore = Score.Instance.playerScore + lifeBonus;
    }

    public void Rank()
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
            rank = "E";
        }

    }

}