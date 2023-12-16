using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneBGMManager : MonoBehaviour
{
    public AudioClip sceneBGM;

    void OnEnable()
    {
        // �V�[�����A�N�e�B�u�ɂȂ����Ƃ���BGM���Đ�
        AudioManager.instance.PlayBGM(sceneBGM);
    }

    void OnDisable()
    {
        // �V�[������A�N�e�B�u�ɂȂ����Ƃ���BGM���~
        AudioManager.instance.StopBGM();
    }
}