using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target; // ロックオンした敵
    public float speed = 5f; // ミサイルの速度

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {

            if (target != null)
            {
                // 目標の方向を向く
                transform.LookAt(target);

                // ミサイルの移動
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                // ターゲットがない場合、ミサイルを破壊などの処理を追加
                Destroy(gameObject);
            }
        }
    }
}