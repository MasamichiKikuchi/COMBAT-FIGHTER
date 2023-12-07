using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGauge : MonoBehaviour
{
    public Text scoreGauge;

    private void Start()
    {
        scoreGauge = GetComponent<Text>();
    }


    void Update()
    {
        scoreGauge.text = ($"{Score.Instance.playerScore}");


    }
}
