using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//攻撃範囲に入ったプレイヤーを検知・攻撃するクラス
public class EnemyAttackArea : MonoBehaviour
{
    public GameObject enemy;
    Enemy _enemy;

    //コルーチン中のフラグ
    bool coroutine = false;
    //攻撃状態のフラグ
    bool attacking = false;

    //プレイヤーをロックオンしているフラグ（プレイヤー側のロックオン警告に使用）
    public bool lockPlayer = false;
   
    void Start()
    {
     _enemy= enemy.GetComponent<Enemy>();
    }

    private void OnTriggerStay(Collider other)
    {    
        //プレイヤーがエリア内にいたら攻撃関連のフラグON
        lockPlayer = true;
        attacking = true;

        if (coroutine == false && attacking == true)
        {
            //攻撃のコルーチンON
            StartCoroutine(EnemyAttackLoop(other));
        }
       
    }

    private IEnumerator EnemyAttackLoop(Collider other)
    {     
        coroutine = true;

        yield return new WaitForSeconds(3.0f);
        
        //攻撃状態が解除されていなければ攻撃
        if (attacking == true)
        {
            _enemy.Attack(other);
        }
     
        coroutine = false;
    }

    private void OnTriggerExit(Collider other)
    {    
        //プレイヤーがエリアから出たら攻撃関連のフラグをON
        lockPlayer = false;
        attacking = false;
    }
}
