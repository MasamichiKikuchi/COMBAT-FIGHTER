using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    int hp;
    int maxHp =1;
   
    public BoxCollider enemyEscapeArea; //プレイヤーを感知したら回避行動をとる範囲のボックスコライダーコンポーネント
    public float avoidanceSpeed = 200f;    // 回避行動時の速度
    private Vector3 avoidanceDirection; // 回避行動の方向
    
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 0.1f); 

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
    public void AvoidMove(Collider collider)
    {          
           // 回避行動の方向を計算
            avoidanceDirection = transform.position - collider.transform.position;
            avoidanceDirection.y = 0f; // Y軸方向の変化を無視
      
        // 回避行動の方向に向かって移動
        transform.Translate(avoidanceDirection.normalized * avoidanceSpeed * Time.deltaTime);
    }


}
