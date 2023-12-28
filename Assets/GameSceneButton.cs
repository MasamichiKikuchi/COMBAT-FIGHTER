using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//�Q�[���V�[���Ɉړ�����{�^��
public class GameSceneButton : MonoBehaviour
{
    void Start()
    {
       var button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

}
