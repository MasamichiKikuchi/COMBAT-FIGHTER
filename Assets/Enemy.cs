using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyMissilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0.005f, 0, 0.1f); 
    }

    public void Attack()
    {
        Debug.Log($"{gameObject.name}ÇÃçUåÇ");
    Å@Å@GameObject enemyMissile = Instantiate(enemyMissilePrefab);
    }
}
