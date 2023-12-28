using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ミニマップ用のカメラのクラス
public class MiniMapCameraFollow : MonoBehaviour
{
    //プレイヤーの位置
    public Transform playerTransform;
    //カメラの高さ
    public float heightAbovePlayer = 100f;

    void Update()
    {
        // プレイヤーの位置に追随する
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + heightAbovePlayer, playerTransform.position.z);
        // プレイヤーの向きに応じてミニマップカメラを回転させる
        float playerRotation = playerTransform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(90f, playerRotation, 0f);
    }

}