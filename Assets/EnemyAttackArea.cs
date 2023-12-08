using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    public GameObject enemy;
    EnemyFireController enemyFireController;

    bool coroutine = false;

    public bool lockPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
       enemyFireController = enemy.GetComponent<EnemyFireController>();
    }

    private void OnTriggerStay(Collider other)
    {
        lockPlayer = true;
        enemyFireController.attacking = true;

        if (coroutine == false&&enemyFireController.attacking==true)
        {
            StartCoroutine(EnemyAttackLoop(other));
        }
       
    }

    private IEnumerator EnemyAttackLoop(Collider other)
    {     
        coroutine = true;

        yield return new WaitForSeconds(3.0f);
        if (enemyFireController.attacking == true)
        {
            enemyFireController.Attack(other);
        }
        coroutine = false;
    }

    private void OnTriggerExit(Collider other)
    {    
        enemyFireController.attacking = false;
        lockPlayer = false;
    }
}
