using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//ランキング表示用のボタンのクラス
public class RankingButton : MonoBehaviour
{    
    //スコアを表示するテキスト
    public TextMeshProUGUI scoreText;
    //ランキングクラスのランカー
    Ranking.Ranker ranker;

    public Ranking.Ranker Ranker
    {
        get
        {
            return ranker;
        }
        set
        {
            //ランカーのスコアを表示
            ranker = value;
            scoreText.text = ranker.totalScore.ToString();
        }
    }
}
