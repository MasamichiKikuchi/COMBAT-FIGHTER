using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MobMove
{
    
    public float followDistance = 5f;  // プレイヤーを追随する距離

    //プレイヤーから離れている時の設定値（速度は早いが旋回性能が低い）
    public float Speed = 60f; // 基本速度
    public float rotateSpeed = 0.5f; //旋回速度
    public float tiltAmount = 60; // 傾きの量
    public float smoothDampTime = 0.6f; // 滑らかな動きを得るための設定時間

    //プレイヤーの後ろに着いた時の設定値（速度は遅いが旋回性能が高い）
    public float followSpeed = 40f;
    public float followSmoothDampTime = 0.4f;
    public float followRotateSpeed = 1.0f;

    //オブジェクトの現在の速度
    private Vector3 currentVelocity;

    //プレイヤー
    private GameObject player;
    void Start()
    {
        //プレイヤーのゲームオブジェクトをゲット
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを追いかける
        FollowPlayer();

        //移動範囲の制限
        MovementRestrictions();
    }

    void FollowPlayer()
    {
        //目標地点をプレイヤーの少し後ろに設定
        float offsetDistance = 20f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;

        //目標地点が遠い場合
        if ((Vector3.Distance(transform.position, targetPosition) >= 10f))
        {
            //前進
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            // プレイヤーの位置に向かって指定された時間とスピードで滑らかに移動
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothDampTime, Speed);

            //プレイヤーの方向を指定された速度で向く
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            //プレイヤーの方向へ傾きを追加
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }

        //目標地点に近づいた場合　※動きはほぼ同じだが、設定値は別の値になっている
        if ((Vector3.Distance(transform.position, targetPosition) < 10f))
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
}
