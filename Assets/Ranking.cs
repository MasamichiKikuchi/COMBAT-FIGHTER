using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//ランキングのデータを管理するクラス
public class Ranking
{
    //ランキングのシングルトンインスタンス
    static Ranking instance;

    public static Ranking GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new Ranking();
            }
            return instance;
        }
    }

    //各ランカーのスコアをもつインナークラス
    public class Ranker
    {      
    　  public int totalScore;

        public Ranker(int score)
        {
            this.totalScore = score;
        }
    }

    //プレイヤーのリスト
    public List<Ranker> rankers = new List<Ranker>();

    //ランキングを上位から表示するリスト
    public List<Ranker> Rankers => rankers.OrderByDescending(ranker => ranker.totalScore).ToList();

    public void Add(int score)
    {
        //プレイヤーのリストに追加
        Ranker ranker = new Ranker(score);
        rankers.Add(ranker);
    }

    public void Remove()
    {
        // scoreの大きい順に並び替え
        rankers = rankers.OrderByDescending(ranker => ranker.totalScore).ToList();
        
        // scoreが5番目より下の要素を削除
        if (rankers.Count > 5)
        {
            rankers.RemoveRange(5, rankers.Count - 5);
        }

    }

    public void Clear()
    {
        //プレイヤーのリストを全削除
        rankers.Clear();
    }
}
