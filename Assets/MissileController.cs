using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target; // ロックオンした敵
    public float speed = 100f; // ミサイルの速度

    public float maxDistance = 100f; // 一定距離

    private Vector3 initialPosition;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");

        transform.position = player.transform.position;
        // ゲームオブジェクトの初期位置を保存
       initialPosition = transform.position;

    }

    void Update()
    {       
        float distance = Vector3.Distance(initialPosition, transform.position);
    
       if(maxDistance >= distance )
       {     
            // ミサイルの移動
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            // 目標の方向を向く
            transform.LookAt(target);
        }

       else 
       { 
            Destroy(gameObject); 
       }
    
    }
    public void SetTarget(Collider collider)
    {
        target = collider.transform;
    }

}