using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの移動に関する継承クラス
public class MobMove : MonoBehaviour
{
    //移動可能範囲の設定
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;

    public void MovementRestrictions()
    {
        // 位置を制限
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}
