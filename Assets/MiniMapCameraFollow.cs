using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float heightAbovePlayer = 100f;

    void LateUpdate()
    {
        // プレイヤーの位置に追随する
        transform.position = new Vector3(playerTransform.position.x, heightAbovePlayer, playerTransform.position.z);
    }
}