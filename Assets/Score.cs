using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//プレイヤーの撃墜スコアを管理するクラス
public class Score
{
    // シングルトンインスタンス
    static Score instance;

    public int playerScore = 0;

    // インスタンスにアクセスするプロパティ
    public static Score Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new Score();
            }

            return instance;
        }
    }

    private Score()
    {
    }

    public void AddScore(int score)
    {
        instance.playerScore += score;
    }

}