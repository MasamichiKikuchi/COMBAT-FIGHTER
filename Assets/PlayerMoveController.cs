using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
//プレイヤーの移動に関するクラス
public class PlayerMoveController : MobMove
{
    public float speed;
    private float currentSpeed;   // 現在の速度
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
        // マウスホイールの回転値を取得
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // 速度を変更
        AdjustSpeed(scrollWheelInput);

        // プレイヤーの移動処理
        MovePlayer();

        speedMeter.text = $"SPEED:{currentSpeed}";
        currentAltitude = (int)gameObject.transform.position.y;
        altiMeter.text = $"ALT:{currentAltitude}";

        //移動範囲の制限
        MovementRestrictions();
    }

    void AdjustSpeed(float scrollInput)
    {
        // マウスホイールの回転方向によって速度を変更
        currentSpeed += scrollInput * 10f; 

        // 速度を制限
        currentSpeed = Mathf.Clamp(currentSpeed, 20f, 100f); 
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

    }
}




