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

    public List<Ranker> Rankers => rankers.OrderByDescending(ranker => ranker.score).ToList();

    public void Add(int score)
    {
        Ranker ranker = new Ranker(score);
        rankers.Add(ranker);
    }

    public void Remove(string name)
    {
        //rankers.Remove(rankers.Find(score => score.name == name));
    }

    public void Clear()
    {
        rankers.Clear();
    }
}
