using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;  

//ランキング画面の動きやデータをを管理するクラス
public class RankingManager : MonoBehaviour
{
    //ランキング表示用のパネル
    public RankingDialog rankingDialog;

    void Start()
    {
        Ranking ranking = Ranking.GetInstance;

        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Ranking");
        // セーブされているデータを読み込む
        Ranking rankingList = reader.Read<Ranking>("RankingList");
   
        foreach (var ranker in rankingList.rankers)
        {
            ranking.Add(ranker.score);
        }

        //今回のスコア
        ranking.Add(Result.Instance.totalScore);

        rankingDialog.ShowRanking();

        ranking.Remove();

        //ランキングデータ全消去
        //ranking.rankers.Clear();
       
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // データを書き込む
        writer.Write("RankingList", ranking);
        // 変更を反映
        writer.Commit();
    }

}