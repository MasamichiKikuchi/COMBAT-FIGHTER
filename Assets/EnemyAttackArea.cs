using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//�U���͈͂ɓ������v���C���[�����m�E�U������N���X
public class EnemyAttackArea : MonoBehaviour
{
    public GameObject enemy;
    Enemy _enemy;

    //�R���[�`�����̃t���O
    bool coroutine = false;
    //�U����Ԃ̃t���O
    bool attacking = false;

    //�v���C���[�����b�N�I�����Ă���t���O�i�v���C���[���̃��b�N�I���x���Ɏg�p�j
    public bool lockPlayer = false;
   
    void Start()
    {
     _enemy= enemy.GetComponent<Enemy>();
    }

    private void OnTriggerStay(Collider other)
    {    
        //�v���C���[���G���A���ɂ�����U���֘A�̃t���OON
        lockPlayer = true;
        attacking = true;

        if (coroutine == false && attacking == true)
        {
            //�U���̃R���[�`��ON
            StartCoroutine(EnemyAttackLoop(other));
        }
       
    }

    private IEnumerator EnemyAttackLoop(Collider other)
    {     
        coroutine = true;

        yield return new WaitForSeconds(3.0f);
        
        //�U����Ԃ���������Ă��Ȃ���΍U��
        if (attacking == true)
        {
            _enemy.Attack(other);
        }
     
        coroutine = false;
    }

    private void OnTriggerExit(Collider other)
    {    
        //�v���C���[���G���A����o����U���֘A�̃t���O��ON
        lockPlayer = false;
        attacking = false;
    }
}
