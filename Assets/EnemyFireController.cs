using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFireController : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    public BoxCollider enemyAttackArea; //�G�̍U���͈͂̃{�b�N�X�R���C�_�[�R���|�[�l���g
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
        Debug.Log($"{gameObject.name}�̍U��");
        GameObject enemyMissile = Instantiate(enemyMissilePrefab,transform.position,transform.rotation);
        EnemyMissileController enemyMissileController = enemyMissile.GetComponent<EnemyMissileController>();
        enemyMissileController.SetTarget(collider);
    }
}