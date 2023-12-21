using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ランキングを画面に表示させるためのクラス
public class RankingDialog : MonoBehaviour
{
    private int buttonNumber = 5;

    public RankingButton rankingButton;

    private RankingButton[] rankingButtons;

    private void Awake()
    {
        //ランキング表示用のボタンをパネル上に作成
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
        //作成されたボタンにランカーの情報を入れ、表示する
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