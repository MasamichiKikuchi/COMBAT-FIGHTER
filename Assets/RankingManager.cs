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

        for (int i = 0; i < rankingList.rankers.Count; i++)
        {
            Debug.Log(rankingList.rankers[i].score);
        }



        foreach (var ranker in rankingList.rankers)
        {

            ranking.Add(ranker.score);
        }

        ranking.Add(Result.Instance.totalScore);
        
        //保存先の確認
        //Debug.Log("保存先:" + Application.persistentDataPath);
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // データを書き込む
        writer.Write("RankingList", ranking);

        // 変更を反映
        writer.Commit();

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
       
        ranking.Add(75);
        ranking.Add(88);
        ranking.Add(20);


        ranking.Add(10);

        foreach (var ranker in ranking.Rankers)
        {
            Debug.Log($"ポイント：{ranker.score}");
        }
    }

    public void PlayerSave(QuickSaveSettings set)
    {
        Debug.Log("保存先:" + Application.persistentDataPath);
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking", set);
        // データを書き込む
        //writer.Write("RankingList",;
       
        // 変更を反映
        writer.Commit();
    }

    public void PlayerLoad(QuickSaveSettings set)
    {
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Player", set);
        // データを読み込む
        string name = reader.Read<string>("Name");
        Vector3 position = reader.Read<Vector3>("Position");
        int level = reader.Read<int>("Level");

        Debug.Log("name:" + name + ", position:" + position + ",　level:" + level);
    }
}