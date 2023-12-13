using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject target; // ���b�N�I�������G
    public float speed = 100f; // �~�T�C���̑��x

    public float maxDistance = 100f; // ��苗��

    private Vector3 initialPosition;

    public GameObject particlePrefab; // �p�[�e�B�N���V�X�e���̃v���n�u���A�^�b�`���邽�߂̕ϐ�

    public AudioSource missileAudioSource;//���˂̉���
    public AudioSource missileDestroyAudioSourece;//�j�󎞂̉���
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
            StartCoroutine(DestroyCoroutine());
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
            other.GetComponent<Enemy>().Damage(1);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyCoroutine());
        }
        //�j�󎞂̉����Đ�
        missileDestroyAudioSourece.Play();
    }
    IEnumerator DestroyCoroutine()
    {
        // �v���n�u���C���X�^���X�����ăQ�[���I�u�W�F�N�g�ɒǉ�
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        // �p�[�e�B�N���Đ�
        particleInstance.GetComponent<ParticleSystem>().Play();
       
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);


    }

}