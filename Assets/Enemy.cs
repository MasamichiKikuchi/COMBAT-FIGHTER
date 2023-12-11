using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    //移動可能範囲の設定
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;


    public float chaseSpeed = 5f; // 基本の追跡速度
    public float flankSpeed = 5f;       // 回り込み時の速度

    private GameObject player; // プレイヤーのTransform

    public float flankDistance = 5f; // 回り込む距離
    public float moveSpeed = 5f; // 移動速度
    public float followDistance = 5f;  // プレイヤーを追随する距離
    public float followSpeed = 5f;     // 追随速度


    private bool isFlanking = false; // 回り込み中かどうかのフラグ
    public bool attacking = false;
    private bool isFollowing = false;
    public GameObject enemyMissilePrefab;

    public BoxCollider enemyAvoidArea; //プレイヤーを感知したら回避行動をとる範囲のボックスコライダーコンポーネント
    public float avoidanceSpeed = 200f;    // 回避行動時の速度
    private Vector3 avoidanceDirection; // 回避行動の方向
    public float rotateSpeed = 0.01f;
    private MiniMap miniMap;

    public float tiltAmount = 20f;      // 傾きの量
    public float smoothDampTime = 0.1f; // 滑らかな動きを得るための時間
    private Vector3 currentVelocity;


    protected enum StateEnum
    {
        Normal,
        Attacking,
        Avoid,
    }

    bool isAttacking => StateEnum.Attacking == state;
    bool Avoid => StateEnum.Avoid == state;

    protected StateEnum state = StateEnum.Normal;

    int hp;
    int maxHp = 1;

   

    void Start()
    {
        hp = maxHp;
        player = GameObject.FindGameObjectWithTag("Player");
        MiniMap.enemies.Add(gameObject);
        miniMap = GameObject.Find("MiniMap").GetComponent<MiniMap>();
       
    }


    // Update is called once per frame
    void Update()
    {


         //前方に向かって移動
        // transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

        //プレイヤーとの距離が一定以下の場合、回り込みを開始
        //if ((Vector3.Distance(transform.position, player.transform.position) > flankDistance) && !isFlanking)
       // {
         //回り込みを開始
       // StartFlanking();
        //}

        // 回り込み中の場合、目標地点に向かって移動と回転
       // if (isFlanking)
       // {
       // ContinueFlanking();
       // }
       // if (isFollowing) 
       // {
         FollowPlayer();
        //}

        //if (isAttacking)
        //{
            AttackMove();
        //}

        // 位置を制限
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }

    public void Attack(Collider collider)
    {
        Debug.Log($"{gameObject.name}の攻撃");
        GameObject enemyMissile = Instantiate(enemyMissilePrefab, transform.position, transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }


    public void AttackMove()
    {
        /// プレイヤーの方向を向く
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followSpeed);
        /*
        // プレイヤーとの距離に基づいて敵の速度を設定
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        float speedMultiplier = Mathf.Clamp01(playerDistance / 1f); // 距離に応じて速度を変化させる

        // 敵の速度を設定
        float currentSpeed = chaseSpeed * speedMultiplier;
        // プレイヤーに向かって移動
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
        */
        // プレイヤーの位置にオフセットを加えて滑らかに移動
        float offsetDistance = 10f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

    }
    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            FireController.enemies.Remove(gameObject);
            MiniMap.enemies.Remove(gameObject);
            miniMap.RemoveEnemyIcon(gameObject);
            Score.Instance.AddScore(100);
            player.GetComponent<Player>().ShootingDown();
            Destroy(gameObject);
        }
    }

    public Vector3 AvoidDirection()
    {
        // ランダムな方向に回避行動
        Vector3 randomDirection = Random.insideUnitSphere;

        return randomDirection;

    }

    public void AvoidMove(Collider collider, Vector3 vector3)
    {

        vector3.z = 0f; // Z軸方向の変化を無視

        Quaternion targetRotation = Quaternion.LookRotation(vector3);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * avoidanceSpeed * Time.deltaTime);
    }
    void StartFlanking()
    {
        isFlanking = true;
    }


    void ContinueFlanking()
    {

        // プレイヤーの後ろに回り込む目標地点を計算
        Vector3 flankDirection = player.transform.forward - transform.position;
        Vector3 flankPosition = player.transform.position + flankDirection.normalized * flankDistance;


        // 目標地点の方向を向く
        Vector3 directionToTarget = flankPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * flankSpeed * Time.deltaTime);
        // 目標地点に到達したら回り込み終了
        if (Vector3.Distance(transform.position, flankPosition) < 30f)
        {
            isFlanking = false;
            isFollowing= true;

        }
    }
    void FollowPlayer()
    {
        /*
        // プレイヤーの位置にオフセットを加えて滑らかに移動
        float offsetDistance = 100f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;
        Quaternion rotation = Quaternion.LookRotation(targetPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        */
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
        // プレイヤーの位置に向かって滑らかに移動
        float offsetDistance = 10f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance; ;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothDampTime, followSpeed);

        // プレイヤーの方向を向く
        Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        // 傾きを追加
        float tiltZ = -directionToPlayer.x * tiltAmount;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);

        /*if (Vector3.Distance(transform.position, targetPosition) > 50f)
        {
            isFollowing = false;
            isFlanking = true;

        }
        */
    }
}



