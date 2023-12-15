using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingSceneButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("RankingScene");
        });
    }
}
