using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class LockOnCursor : MonoBehaviour
    {
        [SerializeField] private RectTransform parentRectTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] public GameObject lockedEnemy;
   

    private void Start()
    {
        
    }

    private void Update()
        {
         UpdateLifeGaugePosition();        
        }

        private void UpdateLifeGaugePosition()
        {
           
        if (lockedEnemy != null)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(lockedEnemy.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);
            transform.localPosition = localPoint;
        }
        else
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(playerTransform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);
            transform.localPosition = localPoint + new Vector2(0, 50);
        }
        }
    }
