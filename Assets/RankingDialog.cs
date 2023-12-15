using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingDialog : MonoBehaviour
{
    [SerializeField]
    int buttonNumber = 5;

    [SerializeField]
    RankingButton rankingButton;

    RankingButton[] rankingButtons;

    private void Awake()
    {
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
        List<Ranking.Ranker> rankers = Ranking.GetInstance.Rankers;

        for (int i = 0; i < buttonNumber-1; i++)
        {
            rankingButtons[i].Ranker = rankers[i];
        }
    }
}