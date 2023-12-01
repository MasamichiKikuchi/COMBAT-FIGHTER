using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int hp;
    int maxHp =1;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0.005f, 0, 0.1f); 
    }

    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        { 
          Destroy(gameObject);
        }
    }

   
}
