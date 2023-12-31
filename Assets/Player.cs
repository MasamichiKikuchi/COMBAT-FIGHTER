using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//プレイヤーのステータスや演出に関するクラス
public class Player : MobStatus,IDamageable
{
    //ライフゲージのゲームオブジェクト
    public GameObject lifeGauge;
    //ロックオン警告演出のゲームオブジェクト
    public GameObject lookOnArert;
    //撃墜演出のゲームオブジェクト
    public GameObject shootingDownDirection;
    //UIを表示するパネル
    public GameObject uiPanel;
    //ダメージ演出用のパネル
    public GameObject damagePanel;
    //撃墜演出コルーチン中のフラグ
    bool shootingDownDirectionCoroutineRunning = false;
    //ダメージを受けた時用のパーティクルプレハブ
    public GameObject particlePrefab;
   　//ダメージを受けたときのSE
    public AudioSource damageAudioSource;
    //ロックオンを受けている時のSE
    public AudioSource waningAudioSource;
    //サウンドを鳴らしている時のフラグ
    bool playingSound = false;
    //プレイヤーのインスタンス
    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("プレイヤーのインスタンスが存在しません");
            }
            return instance;
        }
    }

    private Player()
    {
        //プレイヤーのインスタンスが無い場合、インスタンスを設定する
        instance = this;
    }


    void Start()
    {
        //ライフを設定
        maxLife = 10;
        life = maxLife;
        damagePanel.SetActive(false);
        shootingDownDirection.SetActive(false);

    }


    void Update()
    {
        lookOnArert.SetActive(false);
        
        //プレイヤーへのロックオンフラグを持っている敵がいたら、ロックオンアラートを出す
        var enemys = FindObjectsByType<EnemyAttackArea>(FindObjectsSortMode.None);
        foreach (var enemy in enemys)
        {
            if (enemy.lockPlayer == true)
            {
                lookOnArert.SetActive(true);              
                break;
            }     
            
        }
        //ロックオンアラートがONのとき、SEを流す
       if (lookOnArert.activeSelf) 
       {
       
            if (!playingSound)       
            {
                playingSound = true;           
                waningAudioSource.loop = true;            
                waningAudioSource.Play();       
            }
       }
        else
        {
         waningAudioSource.Stop();
         playingSound = false;
        }
    }
    public override void Damage(int damage) 
    {
        base.Damage(damage);
        //ライフゲージの表示を減らす
        lifeGauge.GetComponent<Image>().fillAmount = (life * 1.0f) / maxLife;
        //UIパネルを揺らす
        uiPanel.GetComponent<UIVibration>().StartUIVibration();
        //ダメージ演出
        StartCoroutine(DamagePanelCoroutine());
        StartCoroutine(DamageEffectCoroutine());
        damageAudioSource.Play();
       
        if (life <= 0) 
        {
            //プレイヤーが撃墜されたらリザルトへ
            SceneManager.LoadScene("ResultScene");      
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EnemyAttackArea")
        {
            //敵の攻撃エリアに入ったら警告演出
            Waning();
        }
    }
   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyAttackArea")
        {
           lookOnArert.SetActive(false);          
        }
    }
    public void Waning()
    {  
       //ロックオン警告音演出をON
       lookOnArert.SetActive(true);
  
    }

    public void ShowShootingDownDirection()
    {
        //コルーチンフラグがOFFなら
        if (shootingDownDirectionCoroutineRunning != true)
        {
            //撃墜演出のコルーチンON
            StartCoroutine(ShootingDownDirectionCoroutine());
        }
    }

    private IEnumerator ShootingDownDirectionCoroutine()
    {
        //コルーチン中フラグをON
        shootingDownDirectionCoroutineRunning = true;
        //撃墜演出をON
        shootingDownDirection.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        
        //撃墜演出をOFF
        shootingDownDirection.SetActive(false);
        //コルーチンフラグをOFF
        shootingDownDirectionCoroutineRunning = false;

    }

    private IEnumerator DamagePanelCoroutine() 
    {
        //ダメージパネルを表示
        damagePanel.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //ダメージパネルを非表示
        damagePanel.SetActive(false);
    }

    private IEnumerator DamageEffectCoroutine()
    {
        //パーティクルを実行
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        particleInstance.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f); 
        Destroy(particleInstance);

    }    
       
}
