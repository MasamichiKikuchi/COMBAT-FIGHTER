using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//キャラクター間のダメージのやり取りに使用するインタフェース
interface IDamageable
{
    public void Damage(int damage);

}