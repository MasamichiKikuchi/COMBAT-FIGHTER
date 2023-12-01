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

    private GameObject player;

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
            Destroy(gameObject); 
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
        
        }
        Destroy(gameObject);
    }

}