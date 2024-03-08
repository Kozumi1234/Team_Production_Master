using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �v���C���[
public class PlayerPun2 : Photon.Pun.MonoBehaviourPun
{
    public static string playerState = "";
    [Header("プレイヤーの状態")]
    [SerializeField] public static string fruitState;
    [Header("サクランボの所持数")]
    [SerializeField] public static int cherryCount;              //サクランボの取得数

    public static Vector3 lastScale;
    public static List<string> fruitsNameList = new List<string> {
        "Strawberry",
        "Grape",
        "Orange",
        "Persimmon",
        "Apple",
        "Pear",
        "Peach",
        "Pineapple",
        "Melon",
        "Watermelon" };


    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x
    [SerializeField] private float applySpeed = 0.2f;       // �U������̓K�p���x
    public PlayerFollowCameraPun2 refCamera;  // �J�����̐�����]���Q�Ƃ���p
    [SerializeField] private float maxSpeed;                //�ō����x
    [SerializeField] private float lerp = 0.1f;             //������
    [SerializeField] private float dashSpeed;               //�_�b�V�����̃X�s�[�h�{��
    [SerializeField] private float dashTimer = 1.0f;        //�N�[���^�C��
    [SerializeField] private int cherry_count;              //�T�N�����{�̎擾��
    [SerializeField] private GameObject strawberry;
    [SerializeField] private float maxChargeTime = 3.0f;
    [SerializeField] private float releaseBorderTime = 1.0f;
    [SerializeField] private float chargeTargetSize = 0.5f;
    [SerializeField] private float chargeLerp = 0.1f;
    [SerializeField] private float releaseLerp = 0.9f;
    [SerializeField] private float changeSizeSpeed = 0.01f;


    [SerializeField] private int dropCost;                  //落ちた際に引かれるサクランボの個数
    [SerializeField] private float enabledExplodeTime = 1.0f;//爆発時のクールタイム

    public bool enabledExplode;                             //爆発可能かどうか
    public bool is_exploded;                                //爆発を終えたかどうか

    Vector3 defaultScale;
    bool dash;                                              //�E�N���b�N���ꂽ���ǂ���
    bool canDash;                                           //�N�[���^�C�����I���ă_�b�V���\���ǂ���
    bool isCharge = false;
    Rigidbody rb;
    bool isCharging = false;
    int fruitsNum;

    float x;
    float z;
    float timer = 0f;
    int fruitsCount = 0;
    float chargeTime = 0f;

    private void Start()
    {
        defaultScale = this.transform.localScale;
        transform.localScale = lastScale;
        fruitsNum = ChangeFruitScriptPun2.fruitsNum;
        fruitState = fruitsNameList[fruitsNum];
        
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) return;

        transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, changeSizeSpeed);
        playerState = "idle";

        if (!isCharge)
        {

            velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                velocity.z += 1;          
            if (Input.GetKey(KeyCode.A))
                velocity.x -= 1;
            if (Input.GetKey(KeyCode.S))
                velocity.z -= 1;
            if (Input.GetKey(KeyCode.D))
                velocity.x += 1;
        }
        

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && canDash == true) //�E�N���b�N�Ń_�b�V��
        {
            velocity = Vector3.zero;
            isCharge = true;

        }

        if (Input.GetMouseButton(0))
        {
            if (chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime;
            }

            rb.AddTorque(refCamera.hRotation * velocity * dashSpeed, ForceMode.Acceleration);
            this.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(chargeTargetSize, chargeTargetSize, chargeTargetSize),chargeLerp);
            rb.velocity = Vector3.Lerp(this.rb.velocity, Vector3.zero, lerp);

        }

        if (Input.GetMouseButtonUp(0))
        {
            playerState = "dash";
            this.transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, releaseLerp); 
            dash = true;
            isCharge = false;
            isCharging = false;
            canDash = false;
            
        }

        if (!canDash)
        {
            timer += Time.deltaTime; //�w�肵���b���_�b�V�����ł��Ȃ�����
            if (dashTimer < timer)
            {
                canDash = true;
                timer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            is_exploded = true;
            enabledExplode = false;
            GetComponent<ExplosionControllerPun2>().Explode();
            if (cherryCount < 0)
            {
                cherryCount = 0;
            }
        }

    }

    void FixedUpdate()
    {

        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) return;   // ���ǉ�

        if (velocity.magnitude > 0)
        {


            rb.AddForce(refCamera.hRotation * velocity);

            //rb.AddTorque(refCamera.hRotation * velocity);

        }

        if (dash == true)
        {
            rb.AddForce(refCamera.transform.forward * dashSpeed * chargeTime, ForceMode.Impulse);
            dash = false;
            chargeTime = 0f;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        
        
        if (collision.gameObject.CompareTag("Player"))
        {
            //GetComponent<Animator>().Play("Hit", 0, 0f);
            Vector3 otherVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(otherVelocity + rb.velocity * 2f,ForceMode.Impulse);
            rb.GetComponent<Rigidbody>().AddForce(-(otherVelocity + rb.velocity) * 2f, ForceMode.Impulse);
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        //GetComponent<Animator>().Play("Hit",0,0f);

        if (other.gameObject.CompareTag("cherry") && is_exploded == false)
        {
            cherryCountIncrease();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Death") && is_exploded == false)
        {
            transform.position = new Vector3(0f,150f,0f);
        }
    }

    void ChargeEnd()
    {
        isCharging = true;
        GetComponent<Animator>().Play("Charging");

    }

    private void OnDestroy()
    {
        lastScale = transform.localScale;
        Debug.Log(lastScale);
    }


    private void cherryCountIncrease()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) return;
        PlayerPun2.cherryCount += 1;

    }





}
