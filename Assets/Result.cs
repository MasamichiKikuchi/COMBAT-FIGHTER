using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//リザルト画面の表示やデータに関するクラス
public class Result : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeBonusText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI rankText;
    private int lifeBonus;
    public int totalScore;
    public string rank;

    // シングルトンインスタンス
    private static Result _instance;

    // インスタンスにアクセスするプロパティ
    public static Result Instance
    {
        get
        {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowResultScore();
        ShowLifeBonus();
        ShowTotalScore(lifeBonus);
        ShowRank(totalScore);

        if (_instance == null)
        {
            _instance = this;
        }
    }

    void ShowResultScore()
    {
        //プレイヤーの撃墜スコアを表示
        scoreText.text = ($"SCORE:{Score.Instance.playerScore}");
    }

    int ShowLifeBonus()
    {
       //プレイヤーのライフに応じてボーナスを計算し、表示する
       lifeBonus =   Player.Instance.life * 100;
       lifeBonusText.text =($"LIFE BONUS:{lifeBonus}");
       return (lifeBonus);
    }

    int ShowTotalScore(int lifeBonus)
    {
        //撃墜スコアとライフボーナスを合算し、トータルスコアとする
        totalScore = Score.Instance.playerScore + lifeBonus;
        totalScoreText.text = ($"TOTAL SCORE:{totalScore}");
        return (totalScore);   
    }

    void ShowRank(int totalScore)
    {
        //トータルスコアに応じてランク付け
        if (totalScore >= 2500)
        {
            rank = "S";
        }
        else if (totalScore >= 2000)
        {
            rank = "A";
        }
        else if (totalScore >= 1500)
        {
            rank = "B";
        }
        else if (totalScore >= 1000)
        {
            rank = "C";
        }
        else if (totalScore >= 500)
        {
            rank = "D";
        }
        else
        { 
            rank= "E";
        }

        rankText.text = ($"RACK:{rank}");
    
    }  
}
