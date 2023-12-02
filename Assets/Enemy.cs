using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected enum StateEnum
    {
        Normal,
        Attack,
        Avoid,
    }

    bool Attack => StateEnum.Attack == state;
    bool Avoid => StateEnum.Avoid == state;

    protected StateEnum state = StateEnum.Normal;

    int hp;
    int maxHp =1;
   
    public BoxCollider enemyEscapeArea; //�v���C���[�����m���������s�����Ƃ�͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
    public float avoidanceSpeed = 200f;    // ����s�����̑��x
    private Vector3 avoidanceDirection; // ����s���̕���
    public float rotateSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 0.1f); 

    }

    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        { 
          Destroy(gameObject);
          FireController.enemies.Remove(gameObject);     
        }
    }

    public Vector3 AvoidDirection()
    {
        // �����_���ȕ����ɉ���s��
        Vector3 randomDirection = Random.insideUnitSphere;

        return randomDirection;

    }

    public void AvoidMove(Collider collider,Vector3 vector3)
    {
        
        vector3.z = 0f; // Z�������̕ω��𖳎�

        Quaternion targetRotation = Quaternion.LookRotation(vector3);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * avoidanceSpeed * Time.deltaTime);
    }


}
