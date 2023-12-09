using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int hp;
    int maxHp = 10;
    public GameObject lifeGauge;
    public GameObject lookOnArert;
    public GameObject shootingDownDirection;
    bool coroutine = false;

    void Start()
    {
        hp = maxHp;

        lookOnArert.SetActive(false);  
        shootingDownDirection.SetActive(false);
    }

    void Update()
    {      
        var enemys = FindObjectsByType<EnemyAttackArea>(FindObjectsSortMode.None);

        lookOnArert.SetActive(false);
        
        foreach (var enemy in enemys)
        {
            if (enemy.lockPlayer == true)
            {
                lookOnArert.SetActive(true);
                break;
            }      
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

    private void OnTriggerStay(Collider other)
    {        
      Waning();
    }
   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyAttackArea")
        {
           
            lookOnArert.SetActive(false);
        }
    }
    public void Waning()
    {  
       Debug.Log("敵に狙われている！");
       lookOnArert.SetActive(true);
    }

    public void ShootingDown()
    {
        if (coroutine != true)
        {
            StartCoroutine(ShootingDownDirectionCoroutine());
        }
    }

    private IEnumerator ShootingDownDirectionCoroutine()
    {
        coroutine = true;

        shootingDownDirection.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        shootingDownDirection.SetActive(false);

        coroutine = false;

    }
}
