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

    private void OnTriggerStay(Collider other)
    {
        if (coroutine == false)
        {
            StartCoroutine(EnemyAttackLoop(other));
        }
        other.GetComponent<Player>().Waning();
    }

    private IEnumerator EnemyAttackLoop(Collider other)
    {     
        coroutine = true;

        yield return new WaitForSeconds(3.0f);

        enemyFireController.Attack(other);

        coroutine = false;
    }
}
