using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//撃墜スコアを表示するUIのクラス
public class ScoreGauge : MonoBehaviour
{
    public TextMeshProUGUI scoreGauge;

    private void Start()
    {
        scoreGauge = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreGauge.text = $"SCORE:{Score.Instance.playerScore}";


    }
}
