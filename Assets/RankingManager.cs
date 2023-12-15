using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;  

public class RankingManager : MonoBehaviour
{

    [SerializeField]
    RankingDialog rankingDialog;

    void Start()
    {
        Ranking ranking = Ranking.GetInstance;

        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Ranking");
        // データを読み込む
        Ranking rankingList = reader.Read<Ranking>("RankingList");

        foreach (var ranker in rankingList.rankers)
        {

            ranking.Add(ranker.score);
        }

        ranking.Add(Result.Instance.totalScore);

        rankingDialog.ShowRanking();

        ranking.Remove();

        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // データを書き込む
        writer.Write("RankingList", ranking);

        // 変更を反映
        writer.Commit();
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
       
        

        foreach (var ranker in ranking.Rankers)
        {
            Debug.Log($"ポイント：{ranker.score}");
        }
    }

}