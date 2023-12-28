using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//�����L���O�\���p�̃{�^���̃N���X
public class RankingButton : MonoBehaviour
{    
    //�X�R�A��\������e�L�X�g
    public TextMeshProUGUI scoreText;
    //�����L���O�N���X�̃����J�[
    Ranking.Ranker ranker;

    public Ranking.Ranker Ranker
    {
        get
        {
            return ranker;
        }
        set
        {
            //�����J�[�̃X�R�A��\��
            ranker = value;
            scoreText.text = ranker.totalScore.ToString();
        }
    }
}
