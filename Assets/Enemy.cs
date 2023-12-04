using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    public float flankSpeed = 5f;       // 回り込み時の速度
   
    private Transform player; // プレイヤーのTransform

    public float flankDistance = 5f; // 回り込む距離
    public float moveSpeed = 5f; // 移動速度

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
   
    public BoxCollider enemyEscapeArea; //プレイヤーを感知したら回避行動をとる範囲のボックスコライダーコンポーネント
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
        transform.Translate(0, 0, 0.05f);
        ContinueFlanking();
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

    void ContinueFlanking()
    {
        // プレイヤーの後ろに回り込む目標地点を計算
        Vector3 flankDirection =  player.forward - transform.position;
        Vector3 flankPosition = player.position + flankDirection.normalized * flankDistance;

      
        // 目標地点の方向を向く
        Vector3 directionToTarget = flankPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
       
    }
}


