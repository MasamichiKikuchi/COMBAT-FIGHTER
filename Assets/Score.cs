using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Score
{
    static Score instance;

    public int playerScore = 0;


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
        Debug.Log($"ÉXÉRÉA{playerScore}");
    }

}