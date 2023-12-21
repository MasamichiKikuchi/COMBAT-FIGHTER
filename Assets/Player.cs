using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//�v���C���[�̃X�e�[�^�X�≉�o�Ɋւ���N���X
public class Player : MobStatus,IDamageable
{
    //���C�t�Q�[�W�̃Q�[���I�u�W�F�N�g
    public GameObject lifeGauge;
    //���b�N�I���x�����o�̃Q�[���I�u�W�F�N�g
    public GameObject lookOnArert;
    //���ĉ��o�̃Q�[���I�u�W�F�N�g
    public GameObject shootingDownDirection;
    //UI��\������p�l��
    public GameObject uiPanel;
    //�_���[�W���o�p�̃p�l��
    public GameObject damagePanel;
    //�R���[�`�����̃t���O
    bool coroutine = false;
    //�_���[�W���󂯂����p�̃p�[�e�B�N���v���n�u
    public GameObject particlePrefab;
   �@//�_���[�W���󂯂��Ƃ���SE
    public AudioSource damageAudioSource;
    //���b�N�I�����󂯂Ă��鎞��SE
    public AudioSource waningAudioSource;
    //�T�E���h��炵�Ă��鎞�̃t���O
    bool playingSound = false;
    //�v���C���[�̃C���X�^���X
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
        //���C�t��ݒ�
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
        
        //�v���C���[�ւ̃��b�N�I���t���O�������Ă���G��������A���b�N�I���A���[�g���o��
        var enemys = FindObjectsByType<EnemyAttackArea>(FindObjectsSortMode.None);
        foreach (var enemy in enemys)
        {
            if (enemy.lockPlayer == true)
            {
                lookOnArert.SetActive(true);
              
                break;
            }     
            
        }
        //���b�N�I���A���[�g��ON�̂Ƃ��ASE�𗬂�
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
        //���C�t�Q�[�W�̕\�������炷
        lifeGauge.GetComponent<Image>().fillAmount = (life * 1.0f) / maxLife;
        //UI�p�l����h�炷
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
       //���b�N�I���x�������o��ON
       lookOnArert.SetActive(true);
  
    }

    public void ShowShootingDownDirection()
    {
        //�R���[�`���t���O��OFF�Ȃ�
        if (coroutine != true)
        {
            //���ĉ��o�̃R���[�`��ON
            StartCoroutine(ShootingDownDirectionCoroutine());
        }
    }

    private IEnumerator ShootingDownDirectionCoroutine()
    {
        //�R���[�`�����t���O��ON
        coroutine = true;
        //���ĉ��o��ON
        shootingDownDirection.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        //���ĉ��o��OFF
        shootingDownDirection.SetActive(false);
        //�R���[�`���t���O��OFF
        coroutine = false;

    }

    private IEnumerator DamagePanelCoroutine() 
    {
        //�_���[�W�p�l����\��
        damagePanel.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //�_���[�W�p�l�����\��
        damagePanel.SetActive(false);
    }

    private IEnumerator DamageEffectCoroutine()
    {
        //�p�[�e�B�N�������s
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        particleInstance.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f); 
        Destroy(particleInstance);

    }    
       
}
