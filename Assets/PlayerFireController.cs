using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//プレイヤーの攻撃に関するクラス
public class PlayerFireController : MonoBehaviour
{

    public GameObject missilePrefab;
    // ロックオン範囲のボックスコライダーコンポーネント
    public BoxCollider playerCollider;
    // ロックオン対象の敵
    public static GameObject lockedEnemy; 
    //ロックオン範囲にいる敵のリスト
    public static List<GameObject> enemies;
    //ロックオンカーソル
    public GameObject lockOnCursor;
    //ゲームメニュー
    public GameMenu gamemenu;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    private void Update()
    {
        //ゲームメニューがONの間は操作できない
        if (gamemenu.isInputEnabled == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //ミサイル発射
                FireMissile();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                //ロックオン対象切り替え
                SwitchLockedEnemy();
            }
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        GetEnemiesInLockOnRange(other.gameObject);  
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveEnemiesInLockOnRange(other.gameObject);
    }
   
    void FireMissile()
    {
        if (lockedEnemy != null)
        {
            // ミサイルの発射とロックオン対象の設定
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            PlayerMissileController missileController = missile.GetComponent<PlayerMissileController>();
            missileController.SetTarget(lockedEnemy);
        }
        else
        {
            //ロックオン対象なしでミサイル発射
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        }

    }

    void GetEnemiesInLockOnRange(GameObject gameObject)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            // ロックオン範囲内に入った敵をリストに追加
            enemies.Add(gameObject);

            if (lockedEnemy == null)
            {
                // 現在のロックオン対象がない場合、リストの最初の要素を新しいロックオン対象に設定
                lockedEnemy = enemies[0];
                //ロックオンカーソルの位置を設定
                lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;
            }
        }
    }

    public void RemoveEnemiesInLockOnRange(GameObject gameObject)
    {
        if (lockedEnemy == gameObject)
        {
            // ロックオンしてる敵が範囲内からでた場合、ロックオン対象をリセット
            lockedEnemy = null;
        }

        // ロックオン範囲内からでた敵をリストから排除
        enemies.Remove(gameObject);

       
        if (enemies.Count != 0)
        {
            // 現在のlockedEnemy がロックオン範囲から外れた場合、最初の要素を新しいロックオン対象に設定
            lockedEnemy = enemies[0];
        }

        if (enemies.Count == 0)
        {
            //現在のロックオン範囲に敵がいない場合、ロックオン対象をリセット
            lockedEnemy = null;
        }

        //ロックオンカーソルの位置を設定
        lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;

    }

    void SwitchLockedEnemy()
    {
        if (enemies.Count > 0)
        {
            // 現在の lockedEnemy が存在する場合
            if (lockedEnemy != null)
            {
                // 現在の lockedEnemy が enemies リスト内のどの位置にいるかを取得
                int currentIndex = enemies.IndexOf(lockedEnemy);

                // 次の要素の位置を計算し、循環させる
                int nextIndex = (currentIndex + 1) % enemies.Count;

                // 次の要素を新しい lockedEnemy に設定
                lockedEnemy = enemies[nextIndex];
            }
            // 現在のlockedEnemy が存在しない場合
            else
            {
                //ロックオン範囲内に敵がいる場合、最初の要素を新しいロックオン対象に設定
                lockedEnemy = enemies[0];

            }
         
        }
        else
        {
            // enemies リストが空の場合、lockedEnemy を null に設定
            lockedEnemy = null;
        }

        //ロックオンカーソルの位置を設定
        lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;
    }
    
}




