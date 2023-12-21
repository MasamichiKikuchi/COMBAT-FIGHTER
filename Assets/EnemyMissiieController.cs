using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour
{ 
        public Transform target; // ���b�N�I�������G
        public float speed = 100f; // �~�T�C���̑��x

        public float maxDistance = 100f; // ��苗��

        private Vector3 initialPosition;

        public GameObject particlePrefab; // �p�[�e�B�N���V�X�e���̃v���n�u���A�^�b�`���邽�߂̕ϐ�


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
        // �v���n�u���C���X�^���X�����ăQ�[���I�u�W�F�N�g�ɒǉ�
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        // �ʂ̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����ꍇ�́A����ɍ��킹�đ��삵�Ă�������
        particleInstance.transform.parent = transform;
        // �p�[�e�B�N���Đ�
        particleInstance.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}
