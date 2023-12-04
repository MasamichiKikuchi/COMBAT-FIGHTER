using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int hp;
    int maxHp = 10;


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage) 
    {
        hp -= damage;
        Debug.Log($"プレイヤーのHP:{hp}");
        if (hp <= 0) 
        {
            Debug.Log("ゲームオーバー");        
        }
    }
}
