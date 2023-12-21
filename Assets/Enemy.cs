using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour,IDamageable
{
    //�ړ��\�͈͂̐ݒ�
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;

    public float chaseSpeed = 5f; // ��{���x
    

    private GameObject player; // �v���C���[��Transform

    public float followDistance = 5f;  // �v���C���[��ǐ����鋗��
   


    public bool attacking = false;
    
    public GameObject enemyMissilePrefab;

    public BoxCollider enemyAvoidArea; //�v���C���[�����m���������s�����Ƃ�͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
    public float avoidanceSpeed = 200f;    // ����s�����̑��x
    private Vector3 avoidanceDirection; // ����s���̕���
    public float rotateSpeed = 0.01f;
    private MiniMap miniMap;

    public float followSpeed = 5f;     // �ǐ����x
    public float tiltAmount = 20f;      // �X���̗�
    public float smoothDampTime = 0.1f; // ���炩�ȓ����𓾂邽�߂̎���
    private Vector3 currentVelocity;

    
    public float followSmoothDampTime = 0.1f; // ���炩�ȓ����𓾂邽�߂̎���
    public float followRotateSpeed = 0.01f;
    public GameObject particlePrefab; // �v���n�u���A�^�b�`���邽�߂̕ϐ�
    public AudioSource damageAudioSource;
   
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
        
       
        // �ʒu�𐧌�
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }

    public void Attack(Collider collider)
    {
        GameObject enemyMissile = Instantiate(enemyMissilePrefab, transform.position, transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }

    public void Damage(int damage)
    {
        hp -= damage;

        //�������|���ꂽ��
        if (hp <= 0)
        {
            
            //���ʉ�ON
            damageAudioSource.Play();
           
             player.GetComponent<FireController>().RemoveEnemiesInLockOnRange(gameObject);
           
            //�v���C���[�Ƀ��b�N�I������Ȃ��悤�Ƀ^�O�ύX
            gameObject.tag = "Untagged";
            //�~�j�}�b�v�̃��X�g���珜��
            MiniMap.enemies.Remove(gameObject);
            //�~�j�}�b�v�̃A�C�R��������
            miniMap.RemoveEnemyIcon(gameObject);
            //�X�R�A���Z
            Score.Instance.AddScore(100);
            //���Ẳ��oON
            player.GetComponent<Player>().ShootingDown();
            //�j�󎞂̃R���[�`��ON
            StartCoroutine(DestroyCoroutine());
        }  
    }


    void FollowPlayer()
    {
        
        float offsetDistance = 20f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;
        if ((Vector3.Distance(transform.position, targetPosition) >= 10f))
        {

            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
            // �v���C���[�̈ʒu�Ɍ������Ċ��炩�Ɉړ�
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothDampTime, chaseSpeed);

            // �v���C���[�̕���������
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            // �X����ǉ�
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }
        if ((Vector3.Distance(transform.position, targetPosition) < 10f) )
        {
           
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmoothDampTime, followSpeed);

            // �v���C���[�̕���������
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followRotateSpeed);

            // �X����ǉ�
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }
    }
    //�j�󎞂̃R���[�`��
    IEnumerator DestroyCoroutine()
    {
        // �p�[�e�B�N���v���n�u�����A�Q�[���I�u�W�F�N�g�ɒǉ�
        GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.transform.parent = transform;
        // �p�[�e�B�N���Đ�
        particleInstance.GetComponent<ParticleSystem>().Play();
          
      �@yield return new WaitForSeconds(0.5f);
        //������j��
        Destroy(gameObject);
    }
}



