using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[�̌��ăX�R�A���Ǘ�����N���X
public class Score
{
    // �V���O���g���C���X�^���X
    static Score instance;

    public int playerScore = 0;

    // �C���X�^���X�ɃA�N�Z�X����v���p�e�B
    public static Score Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new Score();
            }

            return instance;
        }
    }

    private Score()
    {
    }

    public void AddScore(int score)
    {
        instance.playerScore += score;
    }

}