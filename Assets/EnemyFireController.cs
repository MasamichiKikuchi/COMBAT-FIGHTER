using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFireController : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Collider collider)
    {
        Debug.Log($"{gameObject.name}ÇÃçUåÇ");
        GameObject enemyMissile = Instantiate(enemyMissilePrefab,transform.position,transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }
}
