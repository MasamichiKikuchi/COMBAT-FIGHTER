using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //AudioSource �R���|�[�l���g��ǉ�
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.loop = true;

        // ���̍Đ����J�n
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
