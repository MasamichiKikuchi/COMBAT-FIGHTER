using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MobStatus,IDamageable
{
    //移動可能範囲の設定
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;

    public float chaseSpeed = 5f; // 基本速度
    

    private GameObject player; // プレイヤーのTransform

    public float followDistance = 5f;  // プレイヤーを追随する距離
   


    public bool attacking = false;
    
    public GameObject enemyMissilePrefab;

    public BoxCollider enemyAvoidArea; //プレイヤーを感知したら回避行動をとる範囲のボックスコライダーコンポーネント
    public float avoidanceSpeed = 200f;    // 回避行動時の速度
    private Vector3 avoidanceDirection; // 回避行動の方向
    public float rotateSpeed = 0.01f;
    private MiniMap miniMap;

    public float followSpeed = 5f;     // 追随速度
    public float tiltAmount = 20f;      // 傾きの量
    public float smoothDampTime = 0.1f; // 滑らかな動きを得るための時間
    private Vector3 currentVelocity;

    
    public float followSmoothDampTime = 0.1f; // 滑らかな動きを得るための時間
    public float followRotateSpeed = 0.01f;
    public GameObject particlePrefab; // プレハブをアタッチするための変数
    public AudioSource damageAudioSource;

    private void Awake()
    {
        //life最大値を設定
        maxLife = 1;
    }

    private void Start()
    {
        //プレイヤーのゲームオブジェクトをゲット
        player = GameObject.FindGameObjectWithTag("Player");
        //ミニマップのリストに自分を加える
        MiniMap.enemies.Add(gameObject);
        //ミニマップのクラスをゲット
        miniMap = GameObject.Find("MiniMap").GetComponent<MiniMap>();   
    }


    // Update is called once per frame
    void Update()
    {
        //プレイヤーを追いかける
        FollowPlayer();    
        
       
        // 位置を制限
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }

    public void Attack(Collider collider)
    {
        //ミサイルをつくる
        GameObject enemyMissile = Instantiate(enemyMissilePrefab, transform.position, transform.rotation);
        //ミサイルの標的にプレイヤーをセット
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);

        //自分が倒された時
        if (life <= 0)
        {
            
            //効果音ON
            damageAudioSource.Play();
            //プレイヤーのリストに自分が入っていたら消去する
             player.GetComponent<FireController>().RemoveEnemiesInLockOnRange(gameObject);
           
            //プレイヤーにロックオンされないようにタグ変更
            gameObject.tag = "Untagged";
            //ミニマップのリストから除去
            MiniMap.enemies.Remove(gameObject);
            //ミニマップのアイコンを除去
            miniMap.RemoveEnemyIcon(gameObject);
            //スコア加算
            Score.Instance.AddScore(100);
            //撃墜時のプレイヤー側の演出ON
            player.GetComponent<Player>().ShowShootingDownDirection();
            //破壊時のコルーチンON
            StartCoroutine(DestroyCoroutine());
        }  
    }


    void FollowPlayer()
    {
        //目標地点をプレイヤーの少し後ろに設定
        float offsetDistance = 20f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;
        
        //目標地点が遠かったら
        if ((Vector3.Distance(transform.position, targetPosition) >= 10f))
        {
            //前進
            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
            // プレイヤーの位置に向かって指定された時間とスピードで滑らかに移動
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothDampTime, chaseSpeed);

            //プレイヤーの方向を指定された速度で向く
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            //プレイヤーの方向へ傾きを追加
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }

        //目標地点に近づいた場合　※動きはほぼ同じだが、設定値は別な値になっている
        if ((Vector3.Distance(transform.position, targetPosition) < 10f) )
        {
            // プレイヤーの位置に向かって指定された時間とスピードで滑らかに移動　
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmoothDampTime, followSpeed);

            //プレイヤーの方向を指定された速度で向く
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followRotateSpeed);

            //プレイヤーの方向へ傾きを追加
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }
    }
    //破壊時のコルーチン
    IEnumerator DestroyCoroutine()
    {
        // パーティクルプレハブを作り、ゲームオブジェクトに追加
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
       
        // パーティクル再生
        particleInstance.GetComponent<ParticleSystem>().Play();
          
      　yield return new WaitForSeconds(0.5f);
       
        //自分を破壊
        Destroy(gameObject);
    }
}



