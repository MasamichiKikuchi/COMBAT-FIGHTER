using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    Ranking.Ranker ranker;

    public Ranking.Ranker Ranker
    {
        get
        {
            return ranker;
        }
        set
        {
            ranker = value;
            scoreText.text = ranker.score.ToString();
        }
    }
}
