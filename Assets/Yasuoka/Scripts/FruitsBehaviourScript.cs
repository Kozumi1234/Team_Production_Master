using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsBehaviourScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] GameObject FreeLookCamera;
    float moveX;
    float moveZ;

    [Header("���@�̈ړ����x")]
    [SerializeField] public float speed = 1.0f;
    [SerializeField] public float dashSpeed = 2.0f;
    bool canDash;
    float dashTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForward = Vector3.Scale(FreeLookCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = cameraForward * moveZ + FreeLookCamera.transform.right * moveX;
        this.transform.LookAt(FreeLookCamera.transform);

        if (Input.GetMouseButtonDown(0) && canDash == true)
        {
            canDash = false;
            dashTimer = 5.0f;
            dashSpeed = 2.0f;

            moveX = Input.GetAxis("Horizontal"); // ���E
            moveZ = Input.GetAxis("Vertical"); // �O��

            //Vector3 Player_movedir = new Vector3(moveX, 0, moveZ).normalized; // ���K��
            Vector3 Player_movedir = cameraForward * moveZ * speed * dashSpeed + FreeLookCamera.transform.right * moveX * speed * dashSpeed;
        }
        else
        {
            moveX = Input.GetAxis("Horizontal"); // ���E
            moveZ = Input.GetAxis("Vertical"); // �O��

            Vector3 Player_movedir = cameraForward * moveZ * speed + FreeLookCamera.transform.right * moveX * speed;
        }

        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            canDash = true;
        }

        var movement = new Vector3(moveX, 0, moveZ);
        rb.AddForce(movement * 3.0f);

    }
}
