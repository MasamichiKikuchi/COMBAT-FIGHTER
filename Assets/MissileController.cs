using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target; // ロックオンした敵
    public float speed = 5f; // ミサイルの速度

    public float maxDistance = 10f; // 一定距離

    private Vector3 initialPosition;

    void Start()
    {
        // ゲームオブジェクトの初期位置を保存
        initialPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(initialPosition, transform.position);
    
       if(maxDistance >= distance )
       {
            // 目標の方向を向く
            transform.LookAt(target);

            // ミサイルの移動
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        else 
        { 
            Destroy(gameObject); 
        }

    }
}