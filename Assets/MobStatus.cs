using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus : MonoBehaviour
{
    //オブジェクトのlife
    public int life;
    //lifeの最大値
    public int maxLife;

    // Start is called before the first frame update
    void Start()
    {
        //lifeの値を最大値に設定
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Damage(int damage)
    {
        //lifeにダメージを受ける
        life -= damage;
    }
}
