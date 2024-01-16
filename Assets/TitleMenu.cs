using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//タイトル画面を管理するクラス
public class TitleMenu : MonoBehaviour
{
    //マニュアル表示用のパネル
    public GameObject manualPanel;
    //マニュアル表示ボタン
    public Button manualButton;
    //マニュアルを閉じるボタン
    public Button closeManualButton;
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのスコアをリセット
        Score.Instance.playerScore = 0;

        //ゲームメニューからタイトルに戻った場合のために、ゲーム時間を元に戻す
        Time.timeScale = 1;

        manualPanel.SetActive(false);
        manualButton.onClick.AddListener(ShowManual);
        closeManualButton.onClick.AddListener(CloseManual);
    }

    private void ShowManual()
    {
        //マニュアル表示
        manualPanel.SetActive(true);
    }

    private void CloseManual()
    {
        //マニュアル非表示
        manualPanel.SetActive(false);
    }
}
