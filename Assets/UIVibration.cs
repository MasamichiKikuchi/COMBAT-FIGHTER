using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVibration : MonoBehaviour
{
    public RectTransform uiPanel; // UIのRectTransformを指定
    private Vector3 uiPanelOriginalPosition;
    public float vibrationDuration = 1.0f; // 振動の継続時間
    public float vibrationIntensity = 10.0f; // 振動の強度

   
  

    void Start()
    {
       uiPanelOriginalPosition= uiPanel.anchoredPosition;
    }

    public void StartUIVibration()
    {
        // DOShakePositionを使用してUIを振動させる
        uiPanel.DOShakePosition(vibrationDuration, vibrationIntensity).OnComplete(ResetPosition);

    }

    void ResetPosition()
    {
        // 振動終了後、元の位置に戻す
        uiPanel.anchoredPosition = uiPanelOriginalPosition;
    }
}