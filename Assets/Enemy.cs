using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�ړ��\�͈͂̐ݒ�
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;


    public float chaseSpeed = 5f; // ��{�̒ǐՑ��x
    public float flankSpeed = 5f;       // ��荞�ݎ��̑��x
   
    private Transform player; // �v���C���[��Transform

    public float flankDistance = 5f; // ��荞�ދ���
    public float moveSpeed = 5f; // �ړ����x

    private bool isFlanking = false; // ��荞�ݒ����ǂ����̃t���O

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
   
    public BoxCollider enemyAvoidArea; //�v���C���[�����m���������s�����Ƃ�͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
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
       // �v���C���[�Ƃ̋����Ɋ�Â��ēG�̑��x��ݒ�
        float playerDistance = Vector3.Distance(transform.position, player.position);
        float speedMultiplier = Mathf.Clamp01(playerDistance / 10f); // �����ɉ����đ��x��ω�������

        // �G�̑��x��ݒ�
        float currentSpeed = chaseSpeed * speedMultiplier;

        // �v���C���[�̕����Ɍ������Ĉړ�
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
       
        // �v���C���[�Ƃ̋��������ȉ��̏ꍇ�A��荞�݂��J�n
        if ((Vector3.Distance(transform.position, player.position) > flankDistance) && !isFlanking)
        {
            // ��荞�݂��J�n
            StartFlanking();
        }

        // ��荞�ݒ��̏ꍇ�A�ڕW�n�_�Ɍ������Ĉړ��Ɖ�]
        if (isFlanking)
        {
            ContinueFlanking();
        }

        // �ʒu�𐧌�
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

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
    void StartFlanking()
    {
        isFlanking = true;
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

        // �ڕW�n�_�ɓ��B�������荞�ݏI��
        if (Vector3.Distance(transform.position, flankPosition) < 0.1f)
        {
            isFlanking = false;
        }
    }
}


