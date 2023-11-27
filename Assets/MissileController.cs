using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private Transform target; // ���b�N�I�������G
    public float speed = 100f; // �~�T�C���̑��x

    public float maxDistance = 100f; // ��苗��

    private Vector3 initialPosition;

    private GameObject enemy;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");

        transform.position = player.transform.position;
        // �Q�[���I�u�W�F�N�g�̏����ʒu��ۑ�
       initialPosition = transform.position;

        enemy = GameObject.Find("Enemy");

        target = enemy.transform;
    }

    void Update()
    {
        

        float distance = Vector3.Distance(initialPosition, transform.position);
    
       if(maxDistance >= distance )
       {
           
            // �~�T�C���̈ړ�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            // �ڕW�̕���������
            transform.LookAt(target);
        }

       else 
       { 
            Destroy(gameObject); 
       }
    
    }
}