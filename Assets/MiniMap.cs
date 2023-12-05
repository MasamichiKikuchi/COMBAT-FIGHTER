using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;
    public RawImage miniMapImage;
    RenderTexture renderTexture;
    public GameObject playerIconPrefab;  // プレイヤーアイコンのPrefab
    public GameObject enemyIconPrefab;   // 敵アイコンのPrefab
    public Transform playerTransform;    // プレイヤーのTransform
    public Transform enemyParent;        // 敵の親オブジェクト
    public Transform enemyIconsParent;   // 敵アイコンを配置する親オブジェクト

    private Image playerIcon;            // プレイヤーアイコン
    private Dictionary<Transform, Image> enemyIcons; // 各敵に対応するアイコン

    void Start()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        miniMapCamera.targetTexture = renderTexture;

        playerIcon = Instantiate(playerIconPrefab, transform).GetComponent<Image>();
        //enemyIcons = new Dictionary<Transform, Image>();
    }

    void LateUpdate()
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        miniMapImage.texture = texture2D;

        UpdatePlayerIcon();
        //UpdateEnemyIcons();
    }
    
    void UpdatePlayerIcon()
    {
        Vector3 playerPos = miniMapCamera.WorldToViewportPoint(playerTransform.position);
        playerPos.z = 0f;  // ミニマップ上での深度を0に設定

        // プレイヤーアイコンの表示位置を更新
        playerIcon.rectTransform.anchoredPosition = new Vector2(playerPos.x * miniMapImage.rectTransform.rect.width, playerPos.y * miniMapImage.rectTransform.rect.height);
    }

    void UpdateEnemyIcons()
    {
        foreach (Transform enemy in enemyParent.transform)
        {
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.position);
            enemyPos.z = 0f;  // ミニマップ上での深度を0に設定

            // 敵アイコンの表示位置を更新
            Image enemyIcon = GetEnemyIcon(enemy);
            enemyIcon.rectTransform.anchoredPosition = new Vector2(enemyPos.x * miniMapImage.rectTransform.rect.width, enemyPos.y * miniMapImage.rectTransform.rect.height);
        }
    }

    Image GetEnemyIcon(Transform enemy)
    {
        // 各敵に対応するアイコンが存在しない場合、新たに作成
        if (!enemyIcons.ContainsKey(enemy))
        {
            GameObject enemyIcon = Instantiate(enemyIconPrefab, enemyIconsParent);
            enemyIcons[enemy] = enemyIcon.GetComponent<Image>();
        }

        return enemyIcons[enemy];
    }
    
}