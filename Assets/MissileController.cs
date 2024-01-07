using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    // ���b�N�I�������G
    public GameObject target;
    // �~�T�C���̑��x
    public float speed = 100f;
    // ��苗��
    public float maxDistance = 100f;
    //�����ʒu�p�̕ϐ�
    private Vector3 initialPosition;
    // �p�[�e�B�N���V�X�e���̃v���n�u���A�^�b�`���邽�߂̕ϐ�
    public GameObject particlePrefab;
   
    //���˂̉���
    public AudioSource missileAudioSource;
    //�j�󎞂̉���
    public AudioSource missileDestroyAudioSource;

    private bool coroutine = false;
    void Start()
    {
        // �Q�[���I�u�W�F�N�g�̏����ʒu��ۑ�
        initialPosition = transform.position;
        missileAudioSource.Play();
       
    }

    void Update()
    {       
        float distance = Vector3.Distance(initialPosition, transform.position);
    
       if(maxDistance >= distance )
       {     
            // �~�T�C���̈ړ�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
           
            if (target != null)
            {
                // �ڕW�̕���������
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
        // �v���n�u���C���X�^���X�����ăQ�[���I�u�W�F�N�g�ɒǉ�
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        // �p�[�e�B�N���Đ�
        particleInstance.GetComponent<ParticleSystem>().Play();
       
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}