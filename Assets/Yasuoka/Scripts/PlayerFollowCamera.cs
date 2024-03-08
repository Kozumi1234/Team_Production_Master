using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 10.0f;   // ��]���x
    [SerializeField] public GameObject player;          // �����Ώۃv���C���[
    [SerializeField] private float distance = 15.0f;    // �����Ώۃv���C���[����J�����𗣂�����
    [SerializeField] private Quaternion vRotation;      // �J�����̐�����](�����낵��])
    [SerializeField] public Quaternion hRotation;      // �J�����̐�����]
    public int fruitsNum;                               //���Ԗڂ̉ʕ���

    void Start()
    {
        float lastRotationY = transform.rotation.y;
        //fruitsNum = changeFruitsScript.fruitsNum;
        // ��]�̏�����
        vRotation = Quaternion.Euler(15, lastRotationY, 0);         // ������](X�������Ƃ����])�́A15�x�����낷��]
        hRotation = Quaternion.identity;                // ������](Y�������Ƃ����])�́A����]
        transform.rotation = hRotation * vRotation;     // �ŏI�I�ȃJ�����̉�]�́A������]���Ă��琅����]���鍇����]
        //player = player.transform.GetChild(fruitsNum).gameObject;
        // �ʒu�̏�����
        // player�ʒu���狗��distance������O�Ɉ������ʒu��ݒ肵�܂�
        transform.position = player.transform.position - transform.rotation * Vector3.forward * distance;
    }

    void Update()
    {
        //fruitsNum = changeFruitsScript.fruitsNum;
        //player = player.transform.GetChild(fruitsNum).gameObject;
    }

    void LateUpdate()
    {

        // ������]�̍X�V
        if (Input.GetMouseButton(0))
            hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnSpeed, 0);

        // �J�����̉�](transform.rotation)�̍X�V
        // ���@1 : ������]���Ă��琅����]���鍇����]�Ƃ��܂�
        transform.rotation = hRotation * vRotation;

        // �J�����̈ʒu(transform.position)�̍X�V
        // player�ʒu���狗��distance������O�Ɉ������ʒu��ݒ肵�܂�(�ʒu�␳��)
        transform.position = player.transform.position + new Vector3(0, 1, 0) - transform.rotation * Vector3.forward * distance;
    }
}
