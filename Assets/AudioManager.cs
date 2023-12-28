using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//BGM�Đ��p�̃N���X
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource bgmAudioSource;


    void Awake()
    {
        // �V���O���g���̃C���X�^���X�Ǘ�
        if (instance != null && instance != this)
        {   
            Destroy(this.gameObject);                
            return;
        }
          
        //�V�[�����܂����ł����݂���
        DontDestroyOnLoad(this.gameObject);

        // AudioManager�̃C���X�^���X��ݒ�
        instance = this;

        // AudioSource�̎擾�܂��͍쐬
        bgmAudioSource = GetComponent<AudioSource>();         
        if (bgmAudioSource == null)
        {          
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    public void PlayBGM(AudioClip bgmClip)
    {   
        //�V�[�����Ƃ�BGM���Đ�
        if (bgmAudioSource != null && bgmClip != null)
        {          
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {    
        //BGM���~
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }
    }
}


   

