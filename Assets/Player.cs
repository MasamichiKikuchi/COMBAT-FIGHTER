using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int hp;
    int maxHp = 10;
    public GameObject lifeGauge;

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
        lifeGauge.GetComponent<Image>().fillAmount = (hp * 1.0f) / maxHp;
        Debug.Log($"プレイヤーのHP:{hp}");
        if (hp <= 0) 
        {
            Debug.Log("ゲームオーバー");        
        }
    }
}
