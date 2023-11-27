using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target; // ���b�N�I�������G
    public float speed = 5f; // �~�T�C���̑��x

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {

            if (target != null)
            {
                // �ڕW�̕���������
                transform.LookAt(target);

                // �~�T�C���̈ړ�
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                // �^�[�Q�b�g���Ȃ��ꍇ�A�~�T�C����j��Ȃǂ̏�����ǉ�
                Destroy(gameObject);
            }
        }
    }
}