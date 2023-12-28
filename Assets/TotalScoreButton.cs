using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//ランキング画面のトータルスコア表示用のボタン
public class TotalScoreButton : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;

    void Start()
    {
        totalScoreText.text =($"{ Score.Instance.totalScore}");
    }

   
}
