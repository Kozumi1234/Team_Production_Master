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
    [Header("�ʏ�̈ړ����x")]
    public float speed;
    [Header("�_�b�V�����̈ړ����x")]
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

        //// WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
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

            moveX = Input.GetAxis("Horizontal") * speed * dashSpeed; // ���E
            moveZ = Input.GetAxis("Vertical") * speed * dashSpeed; // �O��
        }
        else
        {
            moveX = Input.GetAxis("Horizontal") * speed; // ���E
            moveZ = Input.GetAxis("Vertical") * speed; // �O��
        }

        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            canDash = true;
        }

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 move = cameraForward * moveZ + Camera.main.transform.right * moveX;

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
        //velocity = move * Time.deltaTime;

        var movement = new Vector3(moveX, 0, moveZ);
        rb.AddForce(movement * 2.0f);

        this.transform.LookAt(Camera.main.transform);
    }
}
