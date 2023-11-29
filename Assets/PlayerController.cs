using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;

    public GameObject missilePrefab;

    private GameObject instantiatedMissile;

    public Collider tagetObject;

    public Transform playerTransform; // �v���C���[��Transform�R���|�[�l���g
    public BoxCollider playerCollider; // �v���C���[�̃{�b�N�X�R���C�_�[�R���|�[�l���g
    public float lockOnRange = 10f; // ���b�N�I���͈̔�
    private GameObject[] enemiesInLockOnRange; // ���b�N�I���͈͓��̓G�̔z��
    private GameObject lockedEnemy; // ���b�N�I���Ώۂ̓G

    System.Collections.Generic.List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
      enemies = new System.Collections.Generic.List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // move the plane forward at a constant rate
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * horizontalInput * -1);
        //transform.Rotate()

        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 playerPosition = transform.position;

            FireMissile();
            //if (tagetObject != null)
            // {
            // Instantiate(missilePrefab).GetComponent<MissileController>().target = tagetObject.transform;
            // }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeLockOnEnemy();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        GetEnemiesInLockOnRange(other);
        LockOnEnemies();
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveEnemiesInLockOnRange(other);
    }
    void FireMissile()
    {
        if (lockedEnemy != null)
        {
            // �~�T�C���̔��˂ƃ��b�N�I���Ώۂ̐ݒ�
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            MissileController missileController = missile.GetComponent<MissileController>();
            missileController.SetTarget(lockedEnemy);
        }
    }

    void LockOnEnemies()
    {
        // ���b�N�I���͈͓��̓G���擾
        enemiesInLockOnRange = enemies.ToArray();
        
        if (enemiesInLockOnRange.Length > 0)
        {
            // ���݂̃��b�N�I���Ώۂ��Ȃ��ꍇ�A�ŏ��̓G�����b�N�I���Ώۂɐݒ�
            lockedEnemy = enemiesInLockOnRange[0];      
            
        }
        Debug.Log($"{lockedEnemy}");        
    }

    void ChangeLockOnEnemy()

    {
        // ���݂̃��b�N�I���Ώۂ� enemiesInLockOnRange �z����̂ǂ̈ʒu�ɂ��邩���擾
        int currentIndex = System.Array.IndexOf(enemiesInLockOnRange, lockedEnemy);

        // ���݂̃��b�N�I���Ώۂ�����ꍇ
        if (lockedEnemy != null)
        {
            // ���̓G�̈ʒu���v�Z���A�z������
            int nextIndex = (currentIndex + 1) % enemiesInLockOnRange.Length;

            // ���̓G��V�������b�N�I���Ώۂɐݒ�
            lockedEnemy = enemiesInLockOnRange[nextIndex];
        }
        Debug.Log($"{lockedEnemy}");
    }

    void GetEnemiesInLockOnRange(Collider collider)
    {
           if (collider.CompareTag("Enemy"))
            {
                // ���b�N�I���͈͓��̓G�����X�g�ɒǉ�
                enemies.Add(collider.gameObject);
           }     
    }

    void RemoveEnemiesInLockOnRange(Collider collider)
    {
        enemies.Remove(collider.gameObject);
    }
}

