using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject pausePanel;
    public Button returnGameButton;
    // プレイヤーへの入力を制御するフラグ
    public bool isInputEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        returnGameButton.onClick.AddListener(ReturnGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isInputEnabled = false;
        }
    }

    private void ReturnGame()
    {
      
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        // プレイヤーへの入力を有効にする
        isInputEnabled = true;
    }
}
