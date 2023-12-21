using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour
{ 
        public Transform target; // ロックオンした敵
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
        // プレハブをインスタンス化してゲームオブジェクトに追加
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        // 別のゲームオブジェクトにアタッチする場合は、それに合わせて操作してください
        particleInstance.transform.parent = transform;
        // パーティクル再生
        particleInstance.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}
