using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敵のミサイルの動きを制御するクラス
public class EnemyMissileController : MonoBehaviour
{
    　　// ロックオンした敵
    　　public Transform target;
   　　 // ミサイルの速度
   　　 public float speed = 100f;
   　　 // 移動できる距離
    　　public float maxDistance = 100f; 
　　　　//初期位置用の変数
        private Vector3 initialPosition;
   　　　// パーティクルシステムのプレハブをアタッチするための変数
    　　public GameObject particlePrefab; 

        void Start()
        {

            // ゲームオブジェクトの初期位置を保存
            initialPosition = transform.position;

        }

        void Update()
        {
            float distance = Vector3.Distance(initialPosition, transform.position);

            if (maxDistance >= distance)
            {
                // ミサイルの移動
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                if (target != null)
                {
                    // 目標の方向を向く
                    transform.LookAt(target);
                }
            }

        }
        public void SetTarget(Collider collider)
        {
            target = collider.transform;
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().Damage(1);
            StartCoroutine(DestroyCoroutine());
        }
        ;
    }

    IEnumerator DestroyCoroutine()
    {
        // パーティクルを再生してから破壊
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        particleInstance.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}
