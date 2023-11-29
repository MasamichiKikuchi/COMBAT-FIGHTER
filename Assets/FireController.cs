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
            ChangeLockOnEnemy();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        GetEnemiesInLockOnRange(other);
        LockOnEnemies();
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
    }

    void LockOnEnemies()
    {
        // ロックオン範囲内の敵を取得
        enemiesInLockOnRange = enemies.ToArray();

        if (enemiesInLockOnRange.Length > 0)
        {
            // 現在のロックオン対象がない場合、最初の敵をロックオン対象に設定
            lockedEnemy = enemiesInLockOnRange[0];

        }
        Debug.Log($"{lockedEnemy}");
    }

    void ChangeLockOnEnemy()

    {
        // 現在のロックオン対象が enemiesInLockOnRange 配列内のどの位置にいるかを取得
        int currentIndex = System.Array.IndexOf(enemiesInLockOnRange, lockedEnemy);

        // 現在のロックオン対象がある場合
        if (lockedEnemy != null)
        {
            // 次の敵の位置を計算し、循環させる
            int nextIndex = (currentIndex + 1) % enemiesInLockOnRange.Length;

            // 次の敵を新しいロックオン対象に設定
            lockedEnemy = enemiesInLockOnRange[nextIndex];
        }
        Debug.Log($"{lockedEnemy}");
    }

    void GetEnemiesInLockOnRange(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            // ロックオン範囲内の敵をリストに追加
            enemies.Add(collider.gameObject);
        }
    }

    void RemoveEnemiesInLockOnRange(Collider collider)
    {
        enemies.Remove(collider.gameObject);
    }
}



