using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー
public class Player : MonoBehaviour
{
    [Header("プレイヤーの状態")]
    [SerializeField] public static string playerState;
    [Header("サクランボの所持数")]
    [SerializeField] public static int cherryCount;              //サクランボの取得数

    public List<string> fruitsNameList = new List<string> { 
        "strawberry",
        "grape",
        "dekopon",
        "persimmon",
        "apple",
        "pear",
        "peach",
        "pineapple",
        "melon",
        "watermelon" };

    //[SerializeField] SphereCollider sphCol_1;               //フルーツのサイズ通りのコライダー
    //[SerializeField] public SphereCollider sphCol_2;        //フルーツ当たり判定用の一回りサイズの大きいコライダー

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度
    [SerializeField] public PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用
    [SerializeField] private float maxSpeed;                //最高速度
    //[SerializeField] private float lerp = 0.1f;             //減速率
    [SerializeField] private float dashSpeed;               //ダッシュ時のスピード倍率
    [SerializeField] private float enabledDashTime = 1.0f;  //ダッシュのクールタイム
    
    [SerializeField] private int dropCost;                  //落ちた際に引かれるサクランボの個数
    [SerializeField] private float enabledExplodeTime = 1.0f;//爆発時のクールタイム
    bool dash;                                              //右クリックされたかどうか
    bool canDash;                                           //クールタイムを終えてダッシュ可能かどうか
    public bool enabledExplode;                             //爆発可能かどうか
    public bool is_exploded;                                //爆発を終えたかどうか
    Rigidbody rb;
    int fruitsNum;

    float x;
    float z;
    float timer = 0f;

    [Header("ステージ円の設定")]
    // 円の半径
    [SerializeField] float _radius = 1.0f;
    // 円の中心点
    [SerializeField] Vector3 _center;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fruitsNum = changeFruitsScript.fruitsNum;
        playerState = fruitsNameList[fruitsNum];
        Debug.Log("現在のplayerのフルーツは" + playerState + "です");
    }

    public void ChangePlayerState()
    {
        fruitsNum = changeFruitsScript.fruitsNum;
        playerState = fruitsNameList[fruitsNum];
        Debug.Log("現在のplayerのフルーツは" + playerState + "です");
    }

    void PlayerTransformReset()
    {
        // 指定された半径の円内のランダム位置を取得
        var circlePos = _radius * Random.insideUnitCircle;

        // XZ平面で指定された半径、中心点の円内のランダム位置を計算
        var spawnPos = new Vector3(circlePos.x, 10, circlePos.y) + _center;

        // Playerの速度を0にし、移動させる
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = spawnPos;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_center, _radius);
    }

    void Update()
    {
        if(UIDirector.timerIsRunning)
        {
            // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
            velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                velocity.z += 1;
            if (Input.GetKey(KeyCode.A))
                velocity.x -= 1;
            if (Input.GetKey(KeyCode.S))
                velocity.z -= 1;
            if (Input.GetKey(KeyCode.D))
                velocity.x += 1;

            // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
            velocity = velocity.normalized * moveSpeed * Time.deltaTime;

            // いずれかの方向に移動している場合
            if (velocity.magnitude > 0)
            {
                // プレイヤーの回転(transform.rotation)の更新
                // 無回転状態のプレイヤーのZ+方向(後頭部)を、
                // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(refCamera.hRotation * -velocity),
                                                      applySpeed);


                if (Input.GetMouseButtonDown(1) && canDash == true) //右クリックでダッシュ
                {
                    dash = true;
                    canDash = false;

                }

                if (!canDash)
                {
                    timer += Time.deltaTime; //指定した秒数ダッシュをできなくする
                    if (enabledDashTime < timer)
                    {
                        canDash = true;
                        timer = 0f;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                is_exploded = true;
                enabledExplode = false;
                GetComponent<ExplosionScript>().Explode();
                //cherryCount -= explodeCost;
                if (cherryCount < 0)
                {
                    cherryCount = 0;
                }
                //is_exploded = false;
            }

            if (!enabledExplode)
            {
                timer += Time.deltaTime; //指定した秒数ダッシュをできなくする
                if (enabledExplodeTime < timer)
                {
                    enabledExplode = true;
                    timer = 0f;
                }
            }
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
    }
    

    void FixedUpdate()
    {
        if (velocity.magnitude > 0)
        {

            // プレイヤーの位置(transform.position)の更新
            // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
            rb.AddForce(refCamera.hRotation * velocity);

        }

        if(dash == true)
        {
            rb.velocity *= dashSpeed;
            dash = false;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("cherry")  && is_exploded == false)
        {
           
            cherryCount++;
            string cherryCountView = cherryCount.ToString("00");
            Debug.Log("さくらんぼを" + cherryCount + "個取得！");
            Destroy(collider.gameObject);            
        }
        else if (collider.gameObject.CompareTag("Death") && is_exploded == false)
        {
            cherryCount -= dropCost;
            if (cherryCount < 0)
            {
                cherryCount = 0;
            }
            this.PlayerTransformReset();

        }
    }

}
