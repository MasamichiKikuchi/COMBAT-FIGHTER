using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;

    public GameObject missilePrefab;

    private GameObject instantiatedMissile;

    public Collider tagetObject;

    public Transform playerTransform; // プレイヤーのTransformコンポーネント
    public BoxCollider playerCollider; // プレイヤーのボックスコライダーコンポーネント
    public float lockOnRange = 10f; // ロックオンの範囲
    private GameObject[] enemiesInLockOnRange; // ロックオン範囲内の敵の配列
    private GameObject lockedEnemy; // ロックオン対象の敵

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // move the plane forward at a constant rate
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * horizontalInput * -1);
        //transform.Rotate()

        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 playerPosition = transform.position;

            FireMissile();
            //if (tagetObject != null)
            // {
            // Instantiate(missilePrefab).GetComponent<MissileController>().target = tagetObject.transform;
            // }
        }

       // if (Input.GetKeyDown(KeyCode.Space))
        //{
            // LockOnEnemies();
            //}

            
    }
    void OnTriggerEnter(Collider other)
    {
        LockOnEnemies();
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
        enemiesInLockOnRange = GetEnemiesInLockOnRange();

        if (enemiesInLockOnRange.Length > 0)
        {

            // 現在のロックオン対象が enemiesInLockOnRange 配列内のどの位置にいるかを取得
            int currentIndex = System.Array.IndexOf(enemiesInLockOnRange, lockedEnemy);
            if (Input.GetKeyDown(KeyCode.Space))
                {
                // 現在のロックオン対象がある場合
                if (lockedEnemy != null)
                {

                    // 次の敵の位置を計算し、循環させる
                    int nextIndex = (currentIndex + 1) % enemiesInLockOnRange.Length;
                    Debug.Log($"{lockedEnemy}");
                    // 次の敵を新しいロックオン対象に設定
                    lockedEnemy = enemiesInLockOnRange[nextIndex];
                }
                else
                {
                    // 現在のロックオン対象がない場合、最初の敵を新しいロックオン対象に設定
                    lockedEnemy = enemiesInLockOnRange[0];
                }
            }
        }
        
    }

    GameObject[] GetEnemiesInLockOnRange()
    {
        Collider[] colliders = Physics.OverlapBox(playerTransform.position, playerCollider.size * 0.5f, playerTransform.rotation);
        System.Collections.Generic.List<GameObject> enemies = new System.Collections.Generic.List<GameObject>();

        foreach (var collider in colliders)
        {
           // if (collider.CompareTag("Enemy"))
            //{
                // ロックオン範囲内の敵をリストに追加
                enemies.Add(collider.gameObject);
           // }

            //else
            //{
               // break;
            //}
        }

        return enemies.ToArray();
    }
}

