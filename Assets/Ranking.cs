using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ranking
{
    Ranking()
    {
    }

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

    //ランキング表示用リスト
    public List<Ranker> Rankers => rankers.OrderByDescending(ranker => ranker.score).ToList();

    public void Add(int score)
    {
        Ranker ranker = new Ranker(score);
        rankers.Add(ranker);
    }

    public void Remove()
    {
        // scoreが大きい順にソート
        rankers = rankers.OrderByDescending(ranker => ranker.score).ToList();
        
        // 大きい順で5番目以下の要素を削除
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
