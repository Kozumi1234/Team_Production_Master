using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 10.0f;   // 回転速度
    [SerializeField] public GameObject player;          // 注視対象プレイヤー
    [SerializeField] private float distance = 15.0f;    // 注視対象プレイヤーからカメラを離す距離
    [SerializeField] private Quaternion vRotation;      // カメラの垂直回転(見下ろし回転)
    [SerializeField] public Quaternion hRotation;      // カメラの水平回転
    public int fruitsNum;                               //何番目の果物か

    void Start()
    {
        float lastRotationY = transform.rotation.y;
        //fruitsNum = changeFruitsScript.fruitsNum;
        // 回転の初期化
        vRotation = Quaternion.Euler(15, lastRotationY, 0);         // 垂直回転(X軸を軸とする回転)は、15度見下ろす回転
        hRotation = Quaternion.identity;                // 水平回転(Y軸を軸とする回転)は、無回転
        transform.rotation = hRotation * vRotation;     // 最終的なカメラの回転は、垂直回転してから水平回転する合成回転
        //player = player.transform.GetChild(fruitsNum).gameObject;
        // 位置の初期化
        // player位置から距離distanceだけ手前に引いた位置を設定します
        transform.position = player.transform.position - transform.rotation * Vector3.forward * distance;
    }

    void Update()
    {
        //fruitsNum = changeFruitsScript.fruitsNum;
        //player = player.transform.GetChild(fruitsNum).gameObject;
    }

    void LateUpdate()
    {

        // 水平回転の更新
        if (Input.GetMouseButton(0))
            hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnSpeed, 0);

        // カメラの回転(transform.rotation)の更新
        // 方法1 : 垂直回転してから水平回転する合成回転とします
        transform.rotation = hRotation * vRotation;

        // カメラの位置(transform.position)の更新
        // player位置から距離distanceだけ手前に引いた位置を設定します(位置補正版)
        transform.position = player.transform.position + new Vector3(0, 1, 0) - transform.rotation * Vector3.forward * distance;
    }
}
