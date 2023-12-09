using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed;
    private float currentSpeed;   // 現在の速度
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;
    public int currentAltitude;

    //移動可能範囲の設定
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
        // マウスホイールの回転値を取得
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // 速度を変更
        AdjustSpeed(scrollWheelInput);

        // プレイヤーの移動処理
        MovePlayer();

        speedMeter.text = $"SPEED:{currentSpeed}";
        currentAltitude = (int)gameObject.transform.position.y;
        altiMeter.text = $"ALT:{currentAltitude}";
    }

    void AdjustSpeed(float scrollInput)
    {
        // マウスホイールの回転方向によって速度を変更
        currentSpeed += scrollInput * 10f; 

        // 速度を制限
        currentSpeed = Mathf.Clamp(currentSpeed, 5f, 100f); 
    }

    void MovePlayer()
    {
        // プレイヤーの縦横入力を取得
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //飛行機を前に進める
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        //飛行機のX回転（ピッチ）
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);

        //飛行機のZ軸回転（ロール）
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * horizontalInput * -1);


        //飛行機のＹ軸回転（ヨー）
        // Qボタンが押されたら左に回転
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        }

        // Eボタンが押されたら右に回転
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, 1 * rotationSpeed * Time.deltaTime);
        }

        // 位置を制限
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

    }
}




