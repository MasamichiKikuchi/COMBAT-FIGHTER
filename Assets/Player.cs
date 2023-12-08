using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int hp;
    int maxHp = 10;
    public GameObject lifeGauge;
    public GameObject lookOnArert;
    public bool lookedON = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;

        lookOnArert.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (lookedON == true)
        {
            Waning();
        }
        else
        {
            lookOnArert.SetActive(false);
        }
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

    public void Waning()
    {
       Debug.Log("敵に狙われている！");
       lookOnArert.SetActive(true);
    }
}
