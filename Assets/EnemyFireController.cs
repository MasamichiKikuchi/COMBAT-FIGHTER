using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFireController : MonoBehaviour
{
    public bool attacking = false;
    public GameObject enemyMissilePrefab;

    public void Attack(Collider collider)
    {
        GameObject enemyMissile = Instantiate(enemyMissilePrefab,transform.position,transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }
}
