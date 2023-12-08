using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

    public class LockOnCursor : MonoBehaviour
    {
        public RectTransform parentRectTransform;
        public Transform playerTransform;
        public GameObject lockedEnemy;
        public TextMeshProUGUI meshPro;
   
    void Start ()
    {
        //meshPro = gameObject.GetComponent<TextMeshPro>();
        meshPro.text = "<color=yellow>[ ] </color>";
    }

    private void Update()
    {
      UpdateLockOnCursorPosition(); 
        
    }

    private void UpdateLockOnCursorPosition()
    {         
       if (lockedEnemy != null)
        {
            string text = " [ ]";
            meshPro.text = $"<color=red>{text} </color>";
           Vector3 screenPoint = Camera.main.WorldToScreenPoint(lockedEnemy.transform.position);
           RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);
           transform.localPosition = localPoint;
       }
       
       else
       {
           meshPro.text = "<color=green>[ ]</color><color=red>A</color>";
           Vector3 screenPoint = Camera.main.WorldToScreenPoint(playerTransform.position);
           RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);
           transform.localPosition = localPoint + new Vector2(0,100);
       }
    }
}
