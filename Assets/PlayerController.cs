using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float currentSpeed;   // ���݂̑��x
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        // �}�E�X�z�C�[���̉�]�l���擾
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // ���x��ύX
        AdjustSpeed(scrollWheelInput);

        // �v���C���[�̈ړ�����
        MovePlayer();
    }

    void AdjustSpeed(float scrollInput)
    {
        // �}�E�X�z�C�[���̉�]�����ɂ���đ��x��ύX
        currentSpeed += scrollInput * 10f; 

        // ���x�𐧌�
        currentSpeed = Mathf.Clamp(currentSpeed, 10f, 50f); 
    }

    void MovePlayer()
    {
        // �v���C���[�̏c�����͂��擾
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //��s�@��O�ɐi�߂�
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        //��s�@��X��]�i�s�b�`�j
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);

        //��s�@��Z����]�i���[���j
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * horizontalInput * -1);


        //��s�@�̂x����]�i���[�j
        // Q�{�^���������ꂽ�獶�ɉ�]
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        }

        // E�{�^���������ꂽ��E�ɉ�]
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, 1 * rotationSpeed * Time.deltaTime);
        }
    }
}



