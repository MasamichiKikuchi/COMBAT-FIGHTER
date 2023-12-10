using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    public GameObject enemy;
    Enemy _enemy;

    bool coroutine = false;

    public bool lockPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
       _enemy = enemy.GetComponent<Enemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        lockPlayer = true;
        _enemy.attacking = true;

        if (coroutine == false&&_enemy.attacking==true)
        {
            StartCoroutine(EnemyAttackLoop(other));
        }
       
    }

    private IEnumerator EnemyAttackLoop(Collider other)
    {     
        coroutine = true;

        yield return new WaitForSeconds(3.0f);
        if (_enemy.attacking == true)
        {
            _enemy.Attack(other);
        }
        coroutine = false;
    }

    private void OnTriggerExit(Collider other)
    {    
        _enemy.attacking = false;
        lockPlayer = false;
    }
}
