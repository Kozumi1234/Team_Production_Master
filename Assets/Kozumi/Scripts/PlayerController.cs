using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    float moveX;
    float moveZ;
    bool canDash;
    float dashTimer;
    [Header("通常の移動速度")]
    public float speed;
    [Header("ダッシュ時の移動速度")]
    public float dashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 distance = transform.position - Camera.main.transform.position;

        //// WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        //velocity = Vector3.zero;
        //if (Input.GetKey(KeyCode.W))
        //    velocity.z += 1;
        //if (Input.GetKey(KeyCode.A))
        //    velocity.x -= 1;
        //if (Input.GetKey(KeyCode.S))
        //    velocity.z -= 1;
        //if (Input.GetKey(KeyCode.D))
        //    velocity.x += 1;


        if (Input.GetMouseButtonDown(0) && canDash == true)
        {
            canDash = false;
            dashTimer = 5.0f;
            dashSpeed = 2.0f;

            moveX = Input.GetAxis("Horizontal") * speed * dashSpeed; // 左右
            moveZ = Input.GetAxis("Vertical") * speed * dashSpeed; // 前後
        }
        else
        {
            moveX = Input.GetAxis("Horizontal") * speed; // 左右
            moveZ = Input.GetAxis("Vertical") * speed; // 前後
        }

        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            canDash = true;
        }

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 move = cameraForward * moveZ + Camera.main.transform.right * moveX;

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        //velocity = move * Time.deltaTime;

        var movement = new Vector3(moveX, 0, moveZ);
        rb.AddForce(movement * 2.0f);

        this.transform.LookAt(Camera.main.transform);
    }
}
