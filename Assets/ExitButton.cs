using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
   
    void Start()
    {
        var button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
          #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
          #elif UNITY_STANDALONE
            Application.Quit();
          #endif
        });
    }

   
}
