using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//ランキングのデータを管理するクラス
public class Ranking
{
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

    public class Ranker
    {
        
        public int score;

        public Ranker(int score)
        {
            this.score = score;
        }
    }

    public List<Ranker> rankers = new List<Ranker>();

    //ランキング表示用のリスト
    public List<Ranker> Rankers => rankers.OrderByDescending(ranker => ranker.score).ToList();

    public void Add(int score)
    {
        Ranker ranker = new Ranker(score);
        rankers.Add(ranker);
    }

    public void Remove()
    {
        // scoreの大きい順に並び替え
        rankers = rankers.OrderByDescending(ranker => ranker.score).ToList();
        
        // scoreが5番目より下の要素を削除
        if (rankers.Count > 5)
        {
            rankers.RemoveRange(5, rankers.Count - 5);
        }

    }

    public void Clear()
    {
        rankers.Clear();
    }
}
