using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform playerTransform;
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