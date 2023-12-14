using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeBonusText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI rankText;
    private int lifeBonus;
    private int totalScore;
    private string rank;



    // Start is called before the first frame update
    void Start()
    {
        ShowResultScore();
        ShowLifeBonus();
        ShowTotalScore(lifeBonus);
        ShowRank(totalScore);
        Debug.Log($"{Player.Instance.hp}");
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (totalScore >= 2000)
        {
            rank = "S";
        }
        else if (totalScore >= 1500)
        {
            rank = "A";
        }
        else if (totalScore >= 1000)
        {
            rank = "B";
        }
        else if (totalScore >= 500)
        { 
            rank = "C";
        }
        else
        {
            rank = "D";
        }

        rankText.text = ($"RACK:{rank}");
    
    }  
}
