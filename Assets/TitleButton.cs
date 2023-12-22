using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//タイトルシーン移動用のボタン
public class TitleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }

}
