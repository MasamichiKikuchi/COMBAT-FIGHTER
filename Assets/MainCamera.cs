using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //AudioSource コンポーネントを追加
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.loop = true;

        // 音の再生を開始
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
