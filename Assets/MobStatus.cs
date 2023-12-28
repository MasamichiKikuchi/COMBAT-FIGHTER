using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターのステータスに関する継承元クラス
public class MobStatus : MonoBehaviour
{
    //オブジェクトのlife
    public int life;
    //lifeの最大値
    public int maxLife;

    public virtual void Damage(int damage)
    {
        //lifeにダメージを受ける
        life -= damage;
    }
}
