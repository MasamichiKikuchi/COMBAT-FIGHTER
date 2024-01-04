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
        //ランキングのシングルトンインスタンスを取得
        Ranking ranking = Ranking.GetInstance;

        // QuickSaveReader(データセーブのアセット)のインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Ranking");
        // セーブされているデータを読み込む
        Ranking rankingList = reader.Read<Ranking>("RankingList");
   
        foreach (var ranker in rankingList.rankers)
        {
       　　 //アセットにセーブされてたランカーをランキングリストに入れる
            ranking.Add(ranker.totalScore);
        }

        //今回のプレイヤースコア
        ranking.Add(Score.Instance.totalScore);

        //画面にランキング表示
        rankingDialog.ShowRanking();

        //ランキング5位以下のデータをランキングから削除
        ranking.Remove();

        //ランキングデータ全消去　※セーブされてたデータも全て消えるので注意
        //ranking.rankers.Clear();

        // QuickSaveWriter(データセーブのアセット)のインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Ranking");
        // データを書き込む
        writer.Write("RankingList", ranking);
        // 変更を反映
        writer.Commit();

        //データが重複しないように保存済みのランキングデータをリセットする
       ranking.rankers.Clear();

    }



}