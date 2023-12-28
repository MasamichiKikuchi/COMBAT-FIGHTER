using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//リザルト画面に関するクラス
public class Result : MonoBehaviour
{
    //各数値を表示するテキスト
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeBonusText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI rankText;

    void Start()
    {
        //スコアとランクを計算
        Score.Instance.LifeBonus();
        Score.Instance.TotalScore();
        Score.Instance.Rank();

        //結果を表示
        ShowResultScore();
        ShowLifeBonus();
        ShowTotalScore();
        ShowRank();

    }

    void ShowResultScore()
    {
        //プレイヤーの撃墜スコアを表示
        scoreText.text = ($"SCORE:{Score.Instance.playerScore}");
    }

    void ShowLifeBonus()
    {   
       lifeBonusText.text =($"LIFE BONUS:{Score.Instance.lifeBonus}");
    }

    void ShowTotalScore()
    {
        totalScoreText.text = ($"TOTAL SCORE:{Score.Instance.totalScore}");       
    }

    void ShowRank()
    {
        rankText.text = ($"RACK:{Score.Instance.rank}");
    }  
}
