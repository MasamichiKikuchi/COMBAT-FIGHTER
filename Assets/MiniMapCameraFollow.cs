using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float heightAbovePlayer = 100f;

    void LateUpdate()
    {
        // �v���C���[�̈ʒu�ɒǐ�����
        transform.position = new Vector3(playerTransform.position.x, heightAbovePlayer, playerTransform.position.z);
    }
}