using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//BGMçƒê∂ópÇÃÉNÉâÉX
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource bgmAudioSource;

    
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
            instance = this;

        bgmAudioSource = GetComponent<AudioSource>();
        if (bgmAudioSource == null)
        {
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmAudioSource != null && bgmClip != null)
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }
    }
}


   

