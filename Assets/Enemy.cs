using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
    private Transform player; // プレイヤーのTransform

    public float flankDistance = 5f; // 回り込む距離
    public float moveSpeed = 5f; // 移動速度

    private bool isFlanking = false; // 回り込み中かどうかのフラグ

    protected enum StateEnum
    {
        Normal,
        Attack,
        Avoid,
    }

    bool Attack => StateEnum.Attack == state;
    bool Avoid => StateEnum.Avoid == state;

    protected StateEnum state = StateEnum.Normal;

    int hp;
    int maxHp =1;
   
    public BoxCollider enemyAvoidArea; //プレイヤーを感知したら回避行動をとる範囲のボックスコライダーコンポーネント
    public float avoidanceSpeed = 200f;    // 回避行動時の速度
    private Vector3 avoidanceDirection; // 回避行動の方向
    public float rotateSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        player = GameObject.FindGameObjectWithTag("Player").transform;
         
    }


    // Update is called once per frame
    void Update()
    {
       // プレイヤーとの距離に基づいて敵の速度を設定
        float playerDistance = Vector3.Distance(transform.position, player.position);
        float speedMultiplier = Mathf.Clamp01(playerDistance / 10f); // 距離に応じて速度を変化させる

        // 敵の速度を設定
        float currentSpeed = chaseSpeed * speedMultiplier;

        // プレイヤーの方向に向かって移動
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
       
        // プレイヤーとの距離が一定以下の場合、回り込みを開始
        if ((Vector3.Distance(transform.position, player.position) > flankDistance) && !isFlanking)
        {
            // 回り込みを開始
            StartFlanking();
        }

        // 回り込み中の場合、目標地点に向かって移動と回転
        if (isFlanking)
        {
            ContinueFlanking();
        }

        // 位置を制限
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }

    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        { 
          Destroy(gameObject);
          FireController.enemies.Remove(gameObject);     
        }
    }

    public Vector3 AvoidDirection()
    {
        // ランダムな方向に回避行動
        Vector3 randomDirection = Random.insideUnitSphere;

        return randomDirection;

    }

    public void AvoidMove(Collider collider,Vector3 vector3)
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
        Vector3 flankDirection =  player.forward - transform.position;
        Vector3 flankPosition = player.position + flankDirection.normalized * flankDistance;

      
        // 目標地点の方向を向く
        Vector3 directionToTarget = flankPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        // 目標地点に到達したら回り込み終了
        if (Vector3.Distance(transform.position, flankPosition) < 0.1f)
        {
            isFlanking = false;
        }
    }
}


