using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�v���C���[�̍U���Ɋւ���N���X
public class PlayerFireController : MonoBehaviour
{

    public GameObject missilePrefab;
    // ���b�N�I���͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
    public BoxCollider playerCollider;
    // ���b�N�I���Ώۂ̓G
    public static GameObject lockedEnemy; 
    //���b�N�I���͈͂ɂ���G�̃��X�g
    public static List<GameObject> enemies;
    //���b�N�I���J�[�\��
    public GameObject lockOnCursor;
    //�Q�[�����j���[
    public GameMenu gamemenu;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    private void Update()
    {
        //�Q�[�����j���[��ON�̊Ԃ͑���ł��Ȃ�
        if (gamemenu.isInputEnabled == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //�~�T�C������
                FireMissile();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                //���b�N�I���Ώې؂�ւ�
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
            PlayerMissileController missileController = missile.GetComponent<PlayerMissileController>();
            missileController.SetTarget(lockedEnemy);
        }
        else
        {
            //���b�N�I���ΏۂȂ��Ń~�T�C������
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




