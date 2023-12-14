using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sample : MonoBehaviour
{

    [SerializeField]
    RankingDialog rankingDialog;

    void Start()
    {
        Ranking ranking = Ranking.GetInstance;
        ranking.Add(70);
        ranking.Add(60);
        ranking.Add(50);

        ranking.Add(65);

        foreach (var ranker in ranking.Rankers)
        {
            Debug.Log($"ポイント：{ranker.score}");
        }

        rankingDialog.ShowRanking();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateRanking();

            rankingDialog.ShowRanking();
        }
    }

    private void UpdateRanking()
    {
        Ranking ranking = Ranking.GetInstance;
        ranking.Clear();
        ranking.Add(75);
        ranking.Add(88);
        ranking.Add(20);


        ranking.Add(10);

        foreach (var ranker in ranking.Rankers)
        {
            Debug.Log($"ポイント：{ranker.score}");
        }
    }
}