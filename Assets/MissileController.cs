using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target; // ���b�N�I�������G
    public float speed = 5f; // �~�T�C���̑��x

    public float maxDistance = 10f; // ��苗��

    private Vector3 initialPosition;

    void Start()
    {
        // �Q�[���I�u�W�F�N�g�̏����ʒu��ۑ�
        initialPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(initialPosition, transform.position);
    
       if(maxDistance >= distance )
       {
            // �ڕW�̕���������
            transform.LookAt(target);

            // �~�T�C���̈ړ�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        else 
        { 
            Destroy(gameObject); 
        }

    }
}