using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject target; // ロックオンした敵
    public float speed = 100f; // ミサイルの速度

    public float maxDistance = 100f; // 一定距離

    private Vector3 initialPosition;

    public GameObject particlePrefab; // パーティクルシステムのプレハブをアタッチするための変数

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
            // ミサイルの移動
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
           
            if (target != null)
            {
                // 目標の方向を向く
                transform.LookAt(target.transform.position);
            }
        }

       else 
       {
            StartCoroutine(DestroyCoroutine());
       }
    
    }
    public void SetTarget(GameObject lockedEnemy)
    {
        target = lockedEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            other.GetComponent<Enemy>().Damage(1);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyCoroutine());
        }
       
    }
    IEnumerator DestroyCoroutine()
    {
        // プレハブをインスタンス化してゲームオブジェクトに追加
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        // 別のゲームオブジェクトにアタッチする場合は、それに合わせて操作してください
        particleInstance.transform.parent = transform;
        // パーティクル再生
        particleInstance.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);


    }

}