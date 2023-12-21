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
        
       if (lookOnArert.activeSelf) // lookOnArert���A�N�e�B�u�ł��邩�ǂ������m�F
       {
        if (!playingSound) // playingSound��false�̏ꍇ�Ɏ��s
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
        // �v���n�u���C���X�^���X�����ăQ�[���I�u�W�F�N�g�ɒǉ�
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        // �ʂ̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����ꍇ�́A����ɍ��킹�đ��삵�Ă�������
        particleInstance.transform.parent = transform;
        // �p�[�e�B�N���Đ�
        particleInstance.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        Destroy(particleInstance);

    }    
       
}
