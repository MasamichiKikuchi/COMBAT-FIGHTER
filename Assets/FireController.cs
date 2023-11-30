using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireController : MonoBehaviour
{
    public GameObject missilePrefab;

    private GameObject instantiatedMissile;

    public BoxCollider playerCollider; // プレイヤーのボックスコライダーコンポーネント
    private GameObject[] enemiesInLockOnRange; // ロックオン範囲内の敵の配列
    private GameObject lockedEnemy; // ロックオン対象の敵
    System.Collections.Generic.List<GameObject> enemies;

    void Start()
    {
        enemies = new System.Collections.Generic.List<GameObject>();
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

        Debug.Log($"{lockedEnemy}");
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
            }
        }
    }

    void RemoveEnemiesInLockOnRange(Collider collider)
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
         
        }
        else
        {
            // enemies リストが空の場合、lockedEnemy を null に設定
            lockedEnemy = null;
        }

    }
    
}




