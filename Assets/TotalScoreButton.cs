using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalScoreButton : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        totalScoreText.text =($"{ Result.Instance.totalScore}");
    }

   
}
