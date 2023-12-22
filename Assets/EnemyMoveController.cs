using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    //�ړ��\�͈͂̐ݒ�
    public float minX = -500f;
    public float maxX = 500f;
    public float minY = 1f;
    public float maxY = 100f;
    public float minZ = -500f;
    public float maxZ = 500f;

    public float followDistance = 5f;  // �v���C���[��ǐ����鋗��

    //�v���C���[���痣��Ă��鎞�̐ݒ�l�i���x�͑��������񐫔\���Ⴂ�j
    public float Speed = 60f; // ��{���x
    public float rotateSpeed = 0.5f; //���񑬓x
    public float tiltAmount = 60; // �X���̗�
    public float smoothDampTime = 0.6f; // ���炩�ȓ����𓾂邽�߂̐ݒ莞��

    //�v���C���[�̌��ɒ��������̐ݒ�l�i���x�͒x�������񐫔\�������j
    public float followSpeed = 40f;
    public float followSmoothDampTime = 0.4f;
    public float followRotateSpeed = 1.0f;

    //�I�u�W�F�N�g�̌��݂̑��x
    private Vector3 currentVelocity;

    //�v���C���[
    private GameObject player;
    void Start()
    {
        //�v���C���[�̃Q�[���I�u�W�F�N�g���Q�b�g
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[��ǂ�������
        FollowPlayer();

        // �ʒu�𐧌�
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }

    void FollowPlayer()
    {
        //�ڕW�n�_���v���C���[�̏������ɐݒ�
        float offsetDistance = 20f;
        Vector3 targetPosition = player.transform.position - player.transform.forward * offsetDistance;

        //�ڕW�n�_�������ꍇ
        if ((Vector3.Distance(transform.position, targetPosition) >= 10f))
        {
            //�O�i
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            // �v���C���[�̈ʒu�Ɍ������Ďw�肳�ꂽ���ԂƃX�s�[�h�Ŋ��炩�Ɉړ�
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothDampTime, Speed);

            //�v���C���[�̕������w�肳�ꂽ���x�Ō���
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

            //�v���C���[�̕����֌X����ǉ�
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }

        //�ڕW�n�_�ɋ߂Â����ꍇ�@�������͂قړ��������A�ݒ�l�͕ʂ̒l�ɂȂ��Ă���
        if ((Vector3.Distance(transform.position, targetPosition) < 10f))
        {
            // �v���C���[�̈ʒu�Ɍ������Ďw�肳�ꂽ���ԂƃX�s�[�h�Ŋ��炩�Ɉړ��@
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmoothDampTime, followSpeed);

            //�v���C���[�̕������w�肳�ꂽ���x�Ō���
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followRotateSpeed);

            //�v���C���[�̕����֌X����ǉ�
            float tiltZ = -directionToPlayer.x * tiltAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tiltZ);
        }
    }
}
