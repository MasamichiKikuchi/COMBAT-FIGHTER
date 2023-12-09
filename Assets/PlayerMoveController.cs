using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed;
    private float currentSpeed;   // ���݂̑��x
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;
    public int currentAltitude;

    //�ړ��\�͈͂̐ݒ�
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;

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
    }

    void AdjustSpeed(float scrollInput)
    {
        // �}�E�X�z�C�[���̉�]�����ɂ���đ��x��ύX
        currentSpeed += scrollInput * 10f; 

        // ���x�𐧌�
        currentSpeed = Mathf.Clamp(currentSpeed, 5f, 100f); 
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

        // �ʒu�𐧌�
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }
}




