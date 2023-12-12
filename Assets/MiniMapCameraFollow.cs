using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float heightAbovePlayer = 100f;

   

    void Update()
    {
        // �v���C���[�̈ʒu�ɒǐ�����
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + heightAbovePlayer, playerTransform.position.z);
        // �v���C���[�̌����ɉ����ă~�j�}�b�v�J��������]������
        float playerRotation = playerTransform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(90f, playerRotation, 0f);
    }

}