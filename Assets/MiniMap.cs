using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//ミニマップに関するクラス
public class MiniMap : MonoBehaviour
{
    //ミニマップ用のカメラ
    public Camera miniMapCamera;
    //ミニマップを投影するイメージ
    public RawImage miniMapImage;
    //ミニマップを表示するテクスチャ
    Texture2D texture2D;
    //ミニマップカメラからの映像を画像にするレンダーテクスチャ
    RenderTexture renderTexture;
    // プレイヤーアイコンのPrefab
    public GameObject playerIconPrefab;
    // プレイヤーアイコンのイメージ
    private Image playerIcon;            
    // プレイヤーのTransform
    public Transform playerTransform;   
    // 敵アイコンのPrefab
    public GameObject enemyIconPrefab;
    // 敵アイコンを配置する親オブジェクト
    public Transform enemyIconsParent;   
    //ゲーム上に存在する敵のリスト
    public static List<GameObject> enemies;
    // 各敵に対応するアイコンを管理するディクショナリー
    private Dictionary<Transform, Image> enemyIcons;
    // ミニマップのRectTransform
    public RectTransform miniMapRect;  
    
    private void Awake()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        enemyIcons = new Dictionary<Transform, Image>();
        enemies = new List<GameObject>();
    }
    void Start()
    {   
        //ミニマップカメラの映像を画像としてレンダーテクスチャに代入
        miniMapCamera.targetTexture = renderTexture;
       //ミニマップ表示用のテクスチャ作成
        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        //プレイヤーアイコン作成
        playerIcon = Instantiate(playerIconPrefab, transform).GetComponent<Image>();
    }

    void Update()
    {
        //ミニマップカメラの画像をミニマップに表示
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        miniMapImage.texture = texture2D;

        //プレイヤーアイコンの表示
        UpdatePlayerIcon();
        //敵アイコンの表示
        UpdateEnemyIcons();    

    }
    
    void UpdatePlayerIcon()
    {
        //ミニマップカメラ内のプレイヤー位置を取得
        Vector3 playerPos = miniMapCamera.WorldToViewportPoint(playerTransform.position);
        // ミニマップ上での深度を0に設定
        playerPos.z = 0f; 
        // プレイヤーアイコンの表示位置を更新
        playerIcon.rectTransform.anchoredPosition = new Vector2(playerPos.x * miniMapImage.rectTransform.rect.width, playerPos.y * miniMapImage.rectTransform.rect.height);
    }

    void UpdateEnemyIcons()
    {
        foreach (GameObject enemy in enemies)
        {
            //ミニマップカメラ内の敵位置を取得
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.transform.position);
            // ミニマップ上での深度を0に設定
            enemyPos.z = 0f;  
           
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
            }

            //敵の向きをアイコンに反映させる
            Quaternion worldRotation = enemy.transform.rotation;
            Matrix4x4 inverseTransformMatrix = miniMapCamera.transform.worldToLocalMatrix;
            Vector3 localForward = inverseTransformMatrix.MultiplyVector(worldRotation * Vector3.forward);
            float iconRotationAngle = Mathf.Atan2(localForward.x, localForward.z) * Mathf.Rad2Deg;
            enemyIcon.rectTransform.localEulerAngles = new Vector3(0f, 0f, -iconRotationAngle);
           
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
        //対応する敵が存在しない場合、アイコンを削除
        if (enemyIcons.TryGetValue(enemy.transform, out Image enemyIcon))
        {
            Destroy(enemyIcon.gameObject);
            enemyIcons.Remove(enemy.transform);           
        }
    }

}