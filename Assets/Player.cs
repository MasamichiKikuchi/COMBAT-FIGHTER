using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    //コルーチン中のフラグ
    bool coroutine = false;
    //ダメージを受けた時用のパーティクルプレハブ
    public GameObject particlePrefab;
   　//ダメージを受けたときのSE
    public AudioSource damageAudioSource;
    //ロックオンを受けている時のSE
    public AudioSource waningAudioSource;
    //サウンドを鳴らしている時のフラグ
    bool playingSound = false;
    //プレイヤーのインスタンス
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }
    
        
    void Start()
    {
        //ライフを設定
        maxLife = 10;
        life = maxLife;


        if (_instance == null)
        {
            _instance = this;
        }

       
        damagePanel.SetActive(false);
        lookOnArert.SetActive(false);  
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
        //
        StartCoroutine(DamagePanelCoroutine());
        StartCoroutine(DamageEffectCoroutine());
        damageAudioSource.Play();
        
       
        if (life <= 0) 
        {
            SceneManager.LoadScene("ResultScene");      
        }
    }

    private void OnTriggerStay(Collider other)
    {        
      Waning();      
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
        if (coroutine != true)
        {
            //撃墜演出のコルーチンON
            StartCoroutine(ShootingDownDirectionCoroutine());
        }
    }

    private IEnumerator ShootingDownDirectionCoroutine()
    {
        //コルーチン中フラグをON
        coroutine = true;
        //撃墜演出をON
        shootingDownDirection.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        //撃墜演出をOFF
        shootingDownDirection.SetActive(false);
        //コルーチンフラグをOFF
        coroutine = false;

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
