using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
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
        scoreText.text = ($"SCORE:{Score.Instance.playerScore}");
    }

    int ShowLifeBonus()
    {
       lifeBonus =   Player.Instance.hp * 200;
       lifeBonusText.text =($"LIFE BONUS:{lifeBonus}");
       return (lifeBonus);
    }

    int ShowTotalScore(int lifeBonus)
    { 
        totalScore = Score.Instance.playerScore + lifeBonus;
        totalScoreText.text = ($"TOTAL SCORE:{totalScore}");
        return (totalScore);
    
    }

    void ShowRank(int totalScore)
    {
        if (totalScore >= 5000)
        {
            rank = "S";
        }
        else if (totalScore >= 4000)
        {
            rank = "A";
        }
        else if (totalScore >= 3000)
        {
            rank = "B";
        }
        else if (totalScore >= 2000)
        {
            rank = "C";
        }
        else if (totalScore >= 1000)
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
