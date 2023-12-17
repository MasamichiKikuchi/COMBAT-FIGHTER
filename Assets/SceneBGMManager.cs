using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneBGMManager : MonoBehaviour
{
    public AudioSource sceneBGM;
    

    void OnEnable()
    {
        // �V�[�����A�N�e�B�u�ɂȂ����Ƃ���BGM���Đ�
        AudioManager.instance.PlayBGM(sceneBGM.clip);
    }

    void OnDisable()
    {
        // �V�[������A�N�e�B�u�ɂȂ����Ƃ���BGM���~
        AudioManager.instance.StopBGM();
    }
}