using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    // ロックオンした敵
    public GameObject target;
    // ミサイルの速度
    public float speed = 100f;
    // 一定距離
    public float maxDistance = 100f;
    //初期位置用の変数
    private Vector3 initialPosition;
    // パーティクルシステムのプレハブをアタッチするための変数
    public GameObject particlePrefab;
   
    //発射の音声
    public AudioSource missileAudioSource;
    //破壊時の音声
    public AudioSource missileDestroyAudioSource;

    private bool coroutine = false;
    void Start()
    {
        // ゲームオブジェクトの初期位置を保存
        initialPosition = transform.position;
        missileAudioSource.Play();
       
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
            if (coroutine != true)
            {
                StartCoroutine(DestroyCoroutine());
            }
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
            other.GetComponent<IDamageable>().Damage(1);
            Destroy(gameObject);
        }
        else
        {
            if (coroutine != true)
            {
                StartCoroutine(DestroyCoroutine());
            }
        }
       
       
    }
    IEnumerator DestroyCoroutine()
    {
        coroutine = true;
        missileDestroyAudioSource.Play();
        // プレハブをインスタンス化してゲームオブジェクトに追加
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        // パーティクル再生
        particleInstance.GetComponent<ParticleSystem>().Play();
       
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}