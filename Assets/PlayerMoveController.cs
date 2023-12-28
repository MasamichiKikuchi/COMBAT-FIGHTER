using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
//�v���C���[�̈ړ��Ɋւ���N���X
public class PlayerMoveController : MobMove
{
    public float speed;
    private float currentSpeed;   // ���݂̑��x
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;
    public int currentAltitude;

   
    public TextMeshProUGUI speedMeter;
    public TextMeshProUGUI altiMeter;

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

        speedMeter.text = $"SPEED:{currentSpeed}";
        currentAltitude = (int)gameObject.transform.position.y;
        altiMeter.text = $"ALT:{currentAltitude}";

        //�ړ��͈͂̐���
        MovementRestrictions();
    }

    void AdjustSpeed(float scrollInput)
    {
        // �}�E�X�z�C�[���̉�]�����ɂ���đ��x��ύX
        currentSpeed += scrollInput * 10f; 

        // ���x�𐧌�
        currentSpeed = Mathf.Clamp(currentSpeed, 20f, 100f); 
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




