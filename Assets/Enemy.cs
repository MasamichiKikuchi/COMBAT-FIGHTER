using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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

    private GameObject player; // �v���C���[��Transform

    public float flankDistance = 5f; // ��荞�ދ���
    public float moveSpeed = 5f; // �ړ����x
    public float followDistance = 5f;  // �v���C���[��ǐ����鋗��
    public float followSpeed = 5f;     // �ǐ����x


    private bool isFlanking = false; // ��荞�ݒ����ǂ����̃t���O
    public bool attacking = false;
    private bool isFollowing = false;
    public GameObject enemyMissilePrefab;

    public BoxCollider enemyAvoidArea; //�v���C���[�����m���������s�����Ƃ�͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
    public float avoidanceSpeed = 200f;    // ����s�����̑��x
    private Vector3 avoidanceDirection; // ����s���̕���
    public float rotateSpeed = 0.01f;
    private MiniMap miniMap;

    public float tiltAmount = 20f;      // �X���̗�
    public float smoothDampTime = 0.1f; // ���炩�ȓ����𓾂邽�߂̎���
    private Vector3 currentVelocity;
    public GameObject particlePrefab; // �v���n�u���A�^�b�`���邽�߂̕ϐ�
    public AudioSource damageAudioSource;
   
    protected enum StateEnum
    {
        Normal,
        Attacking,
        Avoid,
    }

    bool isAttacking => StateEnum.Attacking == state;
    bool Avoid => StateEnum.Avoid == state;

    protected StateEnum state = StateEnum.Normal;

    int hp;
    int maxHp = 1;

   

    void Start()
    {
        hp = maxHp;
        player = GameObject.FindGameObjectWithTag("Player");
        MiniMap.enemies.Add(gameObject);
        miniMap = GameObject.Find("MiniMap").GetComponent<MiniMap>();
     
    }


    // Update is called once per frame
    void Update()
    {
         FollowPlayer();    
        
        AttackMove();
       
        // �ʒu�𐧌�
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }

    public void Attack(Collider collider)
    {
        Debug.Log($"{gameObject.name}�̍U��");
        GameObject enemyMissile = Instantiate(enemyMissilePrefab, transform.position, transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }


    public void AttackMove()
    {
        /// �v���C���[�̕���������
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followSpeed);
      
        // �v���C���[�̈ʒu�ɃI�t�Z�b�g�������Ċ��炩�Ɉړ�
        float offsetDistance = 10f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

    }
    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            damageAudioSource.Play();
            FireController.enemies.Remove(gameObject);
            MiniMap.enemies.Remove(gameObject);
            miniMap.RemoveEnemyIcon(gameObject);
            Score.Instance.AddScore(100);
            player.GetComponent<Player>().ShootingDown();
            StartCoroutine(DestroyCoroutine());
        }  
    }

    public Vector3 AvoidDirection()
    {
        // �����_���ȕ����ɉ���s��
        Vector3 randomDirection = Random.insideUnitSphere;

        return randomDirection;

    }

    public void AvoidMove(Collider collider, Vector3 vector3)
    {

        vector3.z = 0f; // Z�������̕ω��𖳎�

        Quaternion targetRotation = Quaternion.LookRotation(vector3);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * avoidanceSpeed * Time.deltaTime);
    }

    void FollowPlayer()
    {
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
        // �v���C���[�̈ʒu�Ɍ������Ċ��炩�Ɉړ�
        float offsetDistance = 10f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance; ;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothDampTime, followSpeed);

        // �v���C���[�̕���������
        Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        // �X����ǉ�
        float tiltZ = -directionToPlayer.x * tiltAmount;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);

    }

    IEnumerator DestroyCoroutine()
    {
            // �v���n�u���C���X�^���X�����ăQ�[���I�u�W�F�N�g�ɒǉ�
            GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            // �ʂ̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����ꍇ�́A����ɍ��킹�đ��삵�Ă�������
            particleInstance.transform.parent = transform;
            // �p�[�e�B�N���Đ�
            particleInstance.GetComponent<ParticleSystem>().Play();
          
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        

    }
}



