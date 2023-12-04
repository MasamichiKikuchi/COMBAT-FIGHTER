using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    public float flankSpeed = 5f;       // ��荞�ݎ��̑��x
   
    private Transform player; // �v���C���[��Transform

    public float flankDistance = 5f; // ��荞�ދ���
    public float moveSpeed = 5f; // �ړ����x

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 0.05f);
        ContinueFlanking();
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

    void ContinueFlanking()
    {
        // �v���C���[�̌��ɉ�荞�ޖڕW�n�_���v�Z
        Vector3 flankDirection =  player.forward - transform.position;
        Vector3 flankPosition = player.position + flankDirection.normalized * flankDistance;

      
        // �ڕW�n�_�̕���������
        Vector3 directionToTarget = flankPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
       
    }
}


