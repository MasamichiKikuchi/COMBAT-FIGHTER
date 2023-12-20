using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    public GameObject missilePrefab;

    public BoxCollider playerCollider; // �v���C���[�̃{�b�N�X�R���C�_�[�R���|�[�l���g
    private GameObject[] enemiesInLockOnRange; // ���b�N�I���͈͓��̓G�̔z��
    public static GameObject lockedEnemy; // ���b�N�I���Ώۂ̓G
    public static List<GameObject> enemies;
    public GameObject lockOnCursor;//���b�N�I���J�[�\��
    public GameMenu gamemenu;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    private void Update()
    {
        if (gamemenu.isInputEnabled == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 playerPosition = transform.position;

                FireMissile();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                SwitchLockedEnemy();
            }
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        GetEnemiesInLockOnRange(other.gameObject);  
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveEnemiesInLockOnRange(other.gameObject);
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
        else
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        }

    }

    void GetEnemiesInLockOnRange(GameObject gameObject)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            // ���b�N�I���͈͓��ɓ������G�����X�g�ɒǉ�
            enemies.Add(gameObject);

            if (lockedEnemy == null)
            {
                // ���݂̃��b�N�I���Ώۂ��Ȃ��ꍇ�A���X�g�̍ŏ��̗v�f��V�������b�N�I���Ώۂɐݒ�
                lockedEnemy = enemies[0];
                //���b�N�I���J�[�\���̈ʒu��ݒ�
                lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;
            }
        }
    }

    public void RemoveEnemiesInLockOnRange(GameObject gameObject)
    {
        if (lockedEnemy == gameObject)
        {
            // ���b�N�I�����Ă�G���͈͓�����ł��ꍇ�A���b�N�I���Ώۂ����Z�b�g
            lockedEnemy = null;
        }

        // ���b�N�I���͈͓�����ł��G�����X�g����r��
        enemies.Remove(gameObject);

       
        if (enemies.Count != 0)
        {
            // ���݂�lockedEnemy �����b�N�I���͈͂���O�ꂽ�ꍇ�A�ŏ��̗v�f��V�������b�N�I���Ώۂɐݒ�
            lockedEnemy = enemies[0];
        }

        if (enemies.Count == 0)
        {
            //���݂̃��b�N�I���͈͂ɓG�����Ȃ��ꍇ�A���b�N�I���Ώۂ����Z�b�g
            lockedEnemy = null;
        }

        //���b�N�I���J�[�\���̈ʒu��ݒ�
        lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;

    }

    void SwitchLockedEnemy()
    {
        if (enemies.Count > 0)
        {
            // ���݂� lockedEnemy �����݂���ꍇ
            if (lockedEnemy != null)
            {
                // ���݂� lockedEnemy �� enemies ���X�g���̂ǂ̈ʒu�ɂ��邩���擾
                int currentIndex = enemies.IndexOf(lockedEnemy);

                // ���̗v�f�̈ʒu���v�Z���A�z������
                int nextIndex = (currentIndex + 1) % enemies.Count;

                // ���̗v�f��V���� lockedEnemy �ɐݒ�
                lockedEnemy = enemies[nextIndex];
            }
            // ���݂�lockedEnemy �����݂��Ȃ��ꍇ
            else
            {
                //���b�N�I���͈͓��ɓG������ꍇ�A�ŏ��̗v�f��V�������b�N�I���Ώۂɐݒ�
                lockedEnemy = enemies[0];

            }
         
        }
        else
        {
            // enemies ���X�g����̏ꍇ�AlockedEnemy �� null �ɐݒ�
            lockedEnemy = null;
        }

        //���b�N�I���J�[�\���̈ʒu��ݒ�
        lockOnCursor.GetComponent<LockOnCursor>().lockedEnemy = lockedEnemy;
    }
    
}




