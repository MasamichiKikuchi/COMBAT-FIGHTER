using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour
{ 
        public Transform target; // ���b�N�I�������G
        public float speed = 100f; // �~�T�C���̑��x

        public float maxDistance = 100f; // ��苗��

        private Vector3 initialPosition;

        private GameObject enemy;

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

            else
            {
                Destroy(gameObject);
            }

        }
        public void SetTarget(Collider collider)
        {
            target = collider.transform;
        }

    
}
