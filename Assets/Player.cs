using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour,IDamageable
{
    public int hp;
    int maxHp = 10;
    public GameObject lifeGauge;
    public GameObject lookOnArert;
    public GameObject shootingDownDirection;
    public GameObject uiPanel;
    public GameObject damagePanel;
    bool coroutine = false;

    public GameObject particlePrefab;

   
    public AudioSource damageAudioSource;
    public AudioSource waningAudioSource;

    bool playingSound = false;
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
        if (_instance == null)
        {
            _instance = this;
        }

        hp = maxHp;
        damagePanel.SetActive(false);
        lookOnArert.SetActive(false);  
        shootingDownDirection.SetActive(false);

    }


    void Update()
    {    
        var enemys = FindObjectsByType<EnemyAttackArea>(FindObjectsSortMode.None);

        lookOnArert.SetActive(false);
          
        foreach (var enemy in enemys)
        {
            if (enemy.lockPlayer == true)
            {
                lookOnArert.SetActive(true);
              
                break;
            }     
            
        }
        
       if (lookOnArert.activeSelf) // lookOnArertがアクティブであるかどうかを確認
       {
        if (!playingSound) // playingSoundがfalseの場合に実行
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

    
   

    public void Damage(int damage) 
    {
        hp -= damage;
        lifeGauge.GetComponent<Image>().fillAmount = (hp * 1.0f) / maxHp;
        uiPanel.GetComponent<UIVibration>().StartUIVibration();
        StartCoroutine(DamagePanelCoroutine());
        StartCoroutine(DamageEffectCoroutine());
        damageAudioSource.Play();
        
       
        if (hp <= 0) 
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
       lookOnArert.SetActive(true);

       

    }

    public void ShootingDown()
    {
        if (coroutine != true)
        {
            StartCoroutine(ShootingDownDirectionCoroutine());
        }
    }

    private IEnumerator ShootingDownDirectionCoroutine()
    {
        coroutine = true;

        shootingDownDirection.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        shootingDownDirection.SetActive(false);

        coroutine = false;

    }

    private IEnumerator DamagePanelCoroutine() 
    {
        damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damagePanel.SetActive(false);
    }

    private IEnumerator DamageEffectCoroutine()
    {
        // プレハブをインスタンス化してゲームオブジェクトに追加
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        // 別のゲームオブジェクトにアタッチする場合は、それに合わせて操作してください
        particleInstance.transform.parent = transform;
        // パーティクル再生
        particleInstance.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        Destroy(particleInstance);

    }    
       
}
