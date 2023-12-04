using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    public GameObject missilePrefab;

    public BoxCollider playerCollider; // プレイヤーのボックスコライダーコンポーネント
    private GameObject[] enemiesInLockOnRange; // ロックオン範囲内の敵の配列
    public GameObject lockedEnemy; // ロックオン対象の敵
    public static List<GameObject> enemies;
    public GameObject lockOnCursor;//ロックオンカーソル
    


    void Start()
    {
        enemies = new List<GameObject>();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 playerPosition = transform.position;

            FireMissile();
            //if (tagetObject != null)
            // {
            // Instantiate(missilePrefab).GetComponent<MissileController>().target = tagetObject.transform;
            // }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           SwitchLockedEnemy();
        }

        //Debug.Log($"{lockedEnemy}");
    }
    void OnTriggerEnter(Collider other)
    {
        GetEnemiesInLockOnRange(other);
       
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveEnemiesInLockOnRange(other);
    }
    void FireMissile()
    {
        if (lockedEnemy != null)
        {
            // ミサイルの発射とロックオン対象の設定
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            MissileController missileController = missile.GetComponent<MissileController>();
            missileController.SetTarget(lockedEnemy);
        }
        else
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        }

    }

    void GetEnemiesInLockOnRange(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            // ロックオン範囲内に入った敵をリストに追加
            enemies.Add(collider.gameObject);

            if (lockedEnemy == null)
            {
                // 現在のロックオン対象がない場合、リストの最初の要素を新しいロックオン対象に設定
                lockedEnemy = enemies[0];
                //ロックオンカーソルの位置を設定
                lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;
            }
        }
    }

    public void RemoveEnemiesInLockOnRange(Collider collider)
    {
        if (lockedEnemy == collider)
        {
            // ロックオンしてる敵が範囲内からでた場合、ロックオン対象をリセット
            lockedEnemy = null;
        }

        // ロックオン範囲内からでた敵をリストから排除
        enemies.Remove(collider.gameObject);

       
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




