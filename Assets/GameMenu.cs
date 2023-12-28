using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ゲーム中メニュー（ポーズ画面）に関するクラス
public class GameMenu : MonoBehaviour
{
    //ポーズ画面表示用のパネル
    public GameObject pausePanel;
    //ゲーム再開ボタン
    public Button returnGameButton;
    //マニュアル表示ボタン
    public Button manualButton;
    //マニュアル表示用のパネル
    public GameObject manualPanel;
    //マニュアルを閉じるボタン
    public Button closeManualButton;
    // プレイヤーへの入力を制御するフラグ
    public bool isInputEnabled = true;
    
    void Start()
    {
        pausePanel.SetActive(false);
        manualPanel.SetActive(false);
        returnGameButton.onClick.AddListener(ReturnGame);
        manualButton.onClick.AddListener(ShowManual);
        closeManualButton.onClick.AddListener(CloseManual);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //プレイヤーの入力を無効にしてポーズ画面を表示
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isInputEnabled = false;
        }
    }

    private void ReturnGame()
    {
        //ポーズ画面を閉じてゲーム再開
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isInputEnabled = true;
    }
    private void ShowManual()
    { 
      //マニュアル表示
      manualPanel.SetActive(true);
    }

    private void CloseManual()
    { 
      //マニュアルを閉じる
      manualPanel.SetActive(false);  
    }
}
