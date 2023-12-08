using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;//ミニマップ用のカメラ
    public RawImage miniMapImage;//ミニマップを表示するイメージ
    RenderTexture renderTexture;
    public GameObject playerIconPrefab;  // プレイヤーアイコンのPrefab
    public GameObject enemyIconPrefab;   // 敵アイコンのPrefab
    public Transform playerTransform;    // プレイヤーのTransform
    public static List<GameObject> enemies;
    public Transform enemyIconsParent;   // 敵アイコンを配置する親オブジェクト

    private Image playerIcon;            // プレイヤーアイコン
    private Dictionary<Transform, Image> enemyIcons; // 各敵に対応するアイコン

    public RectTransform miniMapRect;  // ミニマップのRectTransform（Inspectorで設定）

    private void Awake()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        enemyIcons = new Dictionary<Transform, Image>();
        enemies = new List<GameObject>();
    }
    void Start()
    {   
        miniMapCamera.targetTexture = renderTexture;
        playerIcon = Instantiate(playerIconPrefab, transform).GetComponent<Image>();   
    }

    void Update()
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        miniMapImage.texture = texture2D;

        UpdatePlayerIcon();
        UpdateEnemyIcons();
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
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.transform.position);
            enemyPos.z = 0f;  // ミニマップ上での深度を0に設定

            // 敵アイコンの表示位置を更新
            Image enemyIcon = GetEnemyIcon(enemy.transform);
            enemyIcon.rectTransform.anchoredPosition = new Vector2(enemyPos.x * miniMapImage.rectTransform.rect.width, enemyPos.y * miniMapImage.rectTransform.rect.height);

            if (IsInMiniMapBounds(enemyIcon.rectTransform.anchoredPosition))
            {
                // ミニマップの範囲内にいる場合、アイコンを表示するか再表示する
                SetIconVisibility(enemy.transform, true);
            }

            else 
            {
                // ミニマップの範囲外にいる場合、アイコンを非表示にする
                SetIconVisibility(enemy.transform, false);
                //continue; // 次の敵に進む
            }        
        }    
    }

    bool IsInMiniMapBounds(Vector2 mapPosition)
    {
        // ミニマップの範囲を判定する処理
        return mapPosition.x >= 0 && mapPosition.x <= miniMapRect.rect.width
            && mapPosition.y >= 0 && mapPosition.y <= miniMapRect.rect.height;
    }

    void SetIconVisibility(Transform enemyTransform, bool isVisible)
    {
        // アイコンの表示・非表示を切り替える処理
        if (enemyIcons.TryGetValue(enemyTransform, out Image enemyIcon))
        {
            enemyIcon.gameObject.SetActive(isVisible);
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

    public void RemoveEnemyIcon(GameObject enemy)
    {
        if (enemyIcons.TryGetValue(enemy.transform, out Image enemyIcon))
        {
            Destroy(enemyIcon.gameObject);
            enemyIcons.Remove(enemy.transform);           
        }
    }

}