using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

    public class LockOnCursor : MonoBehaviour
    {
        public RectTransform parentRectTransform;
        public Transform playerTransform;
        public GameObject lockedEnemy;
        public TextMeshProUGUI meshPro;
        public AudioSource lockOnAudioSource;
        bool playingSound = false;
    void Start ()
    {
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
            if (playingSound != true)
            {
                playingSound = true;
                lockOnAudioSource.Play();
            }
           string text = "   [  ]";
           meshPro.text = $"<color=red>{text} </color><color=red><size=50%><align=right>LOCK</color></size></align>";
           Vector3 screenPoint = Camera.main.WorldToScreenPoint(lockedEnemy.transform.position);
           RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);
           transform.localPosition = localPoint;
       }
       
       else
       {
            playingSound =false;
           meshPro.text = "<color=green>[  ]</color>";
           Vector3 screenPoint = Camera.main.WorldToScreenPoint(playerTransform.position);
           RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);
           transform.localPosition = localPoint + new Vector2(0,200);
       }
    }
}
