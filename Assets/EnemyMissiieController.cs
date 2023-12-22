using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�G�̃~�T�C���̓����𐧌䂷��N���X
public class EnemyMissileController : MonoBehaviour
{
    �@�@// ���b�N�I�������G
    �@�@public Transform target;
   �@�@ // �~�T�C���̑��x
   �@�@ public float speed = 100f;
   �@�@ // �ړ��ł��鋗��
    �@�@public float maxDistance = 100f; 
�@�@�@�@//�����ʒu�p�̕ϐ�
        private Vector3 initialPosition;
   �@�@�@// �p�[�e�B�N���V�X�e���̃v���n�u���A�^�b�`���邽�߂̕ϐ�
    �@�@public GameObject particlePrefab; 

        void Start()
        {

            // �Q�[���I�u�W�F�N�g�̏����ʒu��ۑ�
            initialPosition = transform.position;

        }

        void Update()
        {
            float distance = Vector3.Distance(initialPosition, transform.position);

            if (maxDistance >= distance)
            {
                // �~�T�C���̈ړ�
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                if (target != null)
                {
                    // �ڕW�̕���������
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
        // �p�[�e�B�N�����Đ����Ă���j��
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        particleInstance.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}
