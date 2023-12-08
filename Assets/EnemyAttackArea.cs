using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    public GameObject enemy;
    EnemyFireController enemyFireController;

    bool coroutine = false;
    // Start is called before the first frame update
    void Start()
    {
       enemyFireController = enemy.GetComponent<EnemyFireController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player>().lookedON = true;
        enemyFireController.attacking = true;
    }

    private void OnTriggerStay(Collider other)
    {
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
        other.GetComponent<Player>().lookedON = false;
        enemyFireController.attacking = false;
    }
}
