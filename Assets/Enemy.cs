using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
//敵のステータスや攻撃に関するクラス
public class Enemy : MobStatus,IDamageable
{
    //プレイヤー
    private GameObject player; 
    //ミサイルのプレハブ
    public GameObject enemyMissilePrefab;
  　 //ミニマップ
    private MiniMap miniMap;
　　//撃墜時のパーティクルプレハブ　
    public GameObject particlePrefab; 
    //被弾時のSE
    public AudioSource damageAudioSource;


    private void Start()
    {
        //ライフを設定
        maxLife = 1;
        life = maxLife;

        //プレイヤーのゲームオブジェクトをゲット
        player = GameObject.FindGameObjectWithTag("Player");
        //ミニマップのリストに自分を加える
        MiniMap.enemies.Add(gameObject);
        //ミニマップのクラスをゲット
        miniMap = GameObject.Find("MiniMap").GetComponent<MiniMap>();   
    }

    public void Attack(Collider collider)
    {
        //ミサイルをつくる
        GameObject enemyMissile = Instantiate(enemyMissilePrefab, transform.position, transform.rotation);
        //ミサイルの標的にプレイヤーをセット
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);

        //自分が倒された時
        if (life <= 0)
        {           
            //効果音ON
            damageAudioSource.Play();
            //プレイヤーの敵リストに自分が入っていたら消去する
             player.GetComponent<PlayerFireController>().RemoveEnemiesInLockOnRange(gameObject);         
            //プレイヤーにロックオンされないようにタグ変更
            gameObject.tag = "Untagged";
            //ミニマップのリストから除去
            MiniMap.enemies.Remove(gameObject);
            //ミニマップのアイコンを除去
            miniMap.RemoveEnemyIcon(gameObject);
            //スコア加算
            Score.Instance.AddScore(100);
            //撃墜時のプレイヤー側の演出ON
            player.GetComponent<Player>().ShowShootingDownDirection();
            //破壊時のコルーチンON
            StartCoroutine(DestroyCoroutine());
        }  
    }


   
    //破壊時のコルーチン
    IEnumerator DestroyCoroutine()
    {
        // パーティクルプレハブを作り、再生
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        particleInstance.GetComponent<ParticleSystem>().Play();
          
      　yield return new WaitForSeconds(0.5f);
       
        //自分を破壊
        Destroy(gameObject);
    }
}



