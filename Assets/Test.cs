using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    RectTransform parentRectTransform;

    [SerializeField]
    Transform playerTransform;

    public Camera miniMapCamera;

    void Update()
    {
        Vector3 screenPoint = miniMapCamera.WorldToScreenPoint(playerTransform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, miniMapCamera, out Vector2 localPoint);
        
        transform.localPosition = localPoint;
    }
}
