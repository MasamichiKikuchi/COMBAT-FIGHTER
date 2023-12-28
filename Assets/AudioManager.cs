using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//BGM再生用のクラス
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource bgmAudioSource;


    void Awake()
    {
        // シングルトンのインスタンス管理
        if (instance != null && instance != this)
        {   
            Destroy(this.gameObject);                
            return;
        }
          
        //シーンをまたいでも存在する
        DontDestroyOnLoad(this.gameObject);

        // AudioManagerのインスタンスを設定
        instance = this;

        // AudioSourceの取得または作成
        bgmAudioSource = GetComponent<AudioSource>();         
        if (bgmAudioSource == null)
        {          
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    public void PlayBGM(AudioClip bgmClip)
    {   
        //シーンごとのBGMを再生
        if (bgmAudioSource != null && bgmClip != null)
        {          
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {    
        //BGMを停止
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }
    }
}


   

