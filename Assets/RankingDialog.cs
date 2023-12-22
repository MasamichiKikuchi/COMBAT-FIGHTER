using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����L���O����ʂɕ\�������邽�߂̃N���X
public class RankingDialog : MonoBehaviour
{
    private int buttonNumber = 5;

    public RankingButton rankingButton;

    private RankingButton[] rankingButtons;

    private void Awake()
    {
        //�����L���O�\���p�̃{�^�����p�l����ɍ쐬
        CreateButton();
    }

    private void CreateButton()
    {
        for (int i = 0; i < buttonNumber - 1; i++)
        {
            Instantiate(rankingButton, transform);
        }

        rankingButtons = GetComponentsInChildren<RankingButton>();
    }

    public void ShowRanking()
    {
        //�쐬���ꂽ�{�^���Ƀ����J�[�̏������A�\������
        List<Ranking.Ranker> rankers = Ranking.GetInstance.Rankers;
        if (rankers.Count <= buttonNumber)
        {
            for (int i = 0; i < rankers.Count; i++)
            {
                rankingButtons[i].Ranker = rankers[i];
            }
        }

        else
        {
            for (int i = 0; i < buttonNumber; i++)
            {
                rankingButtons[i].Ranker = rankers[i];
            }

        }
    }
}