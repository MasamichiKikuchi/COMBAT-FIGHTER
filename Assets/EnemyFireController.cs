using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFireController : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    public BoxCollider enemyAttackArea; //敵の攻撃範囲のボックスコライダーコンポーネント
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack(other);
    }

    public void Attack(Collider collider)
    {
        Debug.Log($"{gameObject.name}の攻撃");
        GameObject enemyMissile = Instantiate(enemyMissilePrefab,transform.position,transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }
}
