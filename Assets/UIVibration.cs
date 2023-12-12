using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVibration : MonoBehaviour
{
    public RectTransform uiPanel; // UI��RectTransform���w��
    private Vector3 uiPanelOriginalPosition;
    public float vibrationDuration = 1.0f; // �U���̌p������
    public float vibrationIntensity = 10.0f; // �U���̋��x

   
  

    void Start()
    {
       uiPanelOriginalPosition= uiPanel.anchoredPosition;
    }

    public void StartUIVibration()
    {
        // DOShakePosition���g�p����UI��U��������
        uiPanel.DOShakePosition(vibrationDuration, vibrationIntensity).OnComplete(ResetPosition);

    }

    void ResetPosition()
    {
        // �U���I����A���̈ʒu�ɖ߂�
        uiPanel.anchoredPosition = uiPanelOriginalPosition;
    }
}