using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
//�G�̃X�e�[�^�X��U���Ɋւ���N���X
public class Enemy : MobStatus,IDamageable
{
    //�v���C���[
    private GameObject player; 
    //�~�T�C���̃v���n�u
    public GameObject enemyMissilePrefab;
  �@ //�~�j�}�b�v
    private MiniMap miniMap;
�@�@//���Ď��̃p�[�e�B�N���v���n�u�@
    public GameObject particlePrefab; 
    //��e����SE
    public AudioSource damageAudioSource;


    private void Start()
    {
        //���C�t��ݒ�
        maxLife = 1;
        life = maxLife;

        //�v���C���[�̃Q�[���I�u�W�F�N�g���Q�b�g
        player = GameObject.FindGameObjectWithTag("Player");
        //�~�j�}�b�v�̃��X�g�Ɏ�����������
        MiniMap.enemies.Add(gameObject);
        //�~�j�}�b�v�̃N���X���Q�b�g
        miniMap = GameObject.Find("MiniMap").GetComponent<MiniMap>();   
    }

    public void Attack(Collider collider)
    {
        //�~�T�C��������
        GameObject enemyMissile = Instantiate(enemyMissilePrefab, transform.position, transform.rotation);
        //�~�T�C���̕W�I�Ƀv���C���[���Z�b�g
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);

        //�������|���ꂽ��
        if (life <= 0)
        {           
            //���ʉ�ON
            damageAudioSource.Play();
            //�v���C���[�̓G���X�g�Ɏ����������Ă������������
             player.GetComponent<PlayerFireController>().RemoveEnemiesInLockOnRange(gameObject);         
            //�v���C���[�Ƀ��b�N�I������Ȃ��悤�Ƀ^�O�ύX
            gameObject.tag = "Untagged";
            //�~�j�}�b�v�̃��X�g���珜��
            MiniMap.enemies.Remove(gameObject);
            //�~�j�}�b�v�̃A�C�R��������
            miniMap.RemoveEnemyIcon(gameObject);
            //�X�R�A���Z
            Score.Instance.AddScore(100);
            //���Ď��̃v���C���[���̉��oON
            player.GetComponent<Player>().ShowShootingDownDirection();
            //�j�󎞂̃R���[�`��ON
            StartCoroutine(DestroyCoroutine());
        }  
    }


   
    //�j�󎞂̃R���[�`��
    IEnumerator DestroyCoroutine()
    {
        // �p�[�e�B�N���v���n�u�����A�Đ�
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        particleInstance.GetComponent<ParticleSystem>().Play();
          
      �@yield return new WaitForSeconds(0.5f);
       
        //������j��
        Destroy(gameObject);
    }
}



