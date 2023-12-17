using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneBGMManager : MonoBehaviour
{
    public AudioSource sceneBGM;
    

    void OnEnable()
    {
        // シーンがアクティブになったときにBGMを再生
        AudioManager.instance.PlayBGM(sceneBGM.clip);
    }

    void OnDisable()
    {
        // シーンが非アクティブになったときにBGMを停止
        AudioManager.instance.StopBGM();
    }
}