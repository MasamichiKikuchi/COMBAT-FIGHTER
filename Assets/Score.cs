using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//プレイヤーのスコアを管理するクラス
public class Score
{
    // シングルトンインスタンス
    static Score instance;
    //プレイヤーの撃墜スコア
    public int playerScore;

    public int lifeBonus;

    public int totalScore;

    public string rank;
    // インスタンスにアクセスするプロパティ
    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }

            return instance;
        }
    }

    // コンストラクタ
    private Score()
    {    
        playerScore = 0;
        lifeBonus = 0;
    }

    public void AddScore(int score)
    {
        //撃墜スコアを加算
        instance.playerScore += score;
    }
    
    public void LifeBonus()
    {
        //プレイヤーのライフに応じてボーナスを計算し、表示する
        lifeBonus = Player.Instance.life * 100;
    }

    public void TotalScore()
    {  
        //撃墜スコアとライフボーナスを合算し、トータルスコアとする
        totalScore = Score.Instance.playerScore + lifeBonus;
    }

    public void Rank()
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
            rank = "E";
        }

    }

}