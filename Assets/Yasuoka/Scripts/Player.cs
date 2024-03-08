using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[
public class Player : MonoBehaviour
{
    [Header("�v���C���[�̏��")]
    [SerializeField] public static string playerState;
    [Header("�T�N�����{�̏�����")]
    [SerializeField] public static int cherryCount;              //�T�N�����{�̎擾��

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

    //[SerializeField] SphereCollider sphCol_1;               //�t���[�c�̃T�C�Y�ʂ�̃R���C�_�[
    //[SerializeField] public SphereCollider sphCol_2;        //�t���[�c�����蔻��p�̈���T�C�Y�̑傫���R���C�_�[

    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x
    [SerializeField] private float applySpeed = 0.2f;       // �U������̓K�p���x
    [SerializeField] public PlayerFollowCamera refCamera;  // �J�����̐�����]���Q�Ƃ���p
    [SerializeField] private float maxSpeed;                //�ō����x
    //[SerializeField] private float lerp = 0.1f;             //������
    [SerializeField] private float dashSpeed;               //�_�b�V�����̃X�s�[�h�{��
    [SerializeField] private float enabledDashTime = 1.0f;  //�_�b�V���̃N�[���^�C��
    
    [SerializeField] private int dropCost;                  //�������ۂɈ������T�N�����{�̌�
    [SerializeField] private float enabledExplodeTime = 1.0f;//�������̃N�[���^�C��
    bool dash;                                              //�E�N���b�N���ꂽ���ǂ���
    bool canDash;                                           //�N�[���^�C�����I���ă_�b�V���\���ǂ���
    public bool enabledExplode;                             //�����\���ǂ���
    public bool is_exploded;                                //�������I�������ǂ���
    Rigidbody rb;
    int fruitsNum;

    float x;
    float z;
    float timer = 0f;

    [Header("�X�e�[�W�~�̐ݒ�")]
    // �~�̔��a
    [SerializeField] float _radius = 1.0f;
    // �~�̒��S�_
    [SerializeField] Vector3 _center;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fruitsNum = changeFruitsScript.fruitsNum;
        playerState = fruitsNameList[fruitsNum];
        Debug.Log("���݂�player�̃t���[�c��" + playerState + "�ł�");
    }

    public void ChangePlayerState()
    {
        fruitsNum = changeFruitsScript.fruitsNum;
        playerState = fruitsNameList[fruitsNum];
        Debug.Log("���݂�player�̃t���[�c��" + playerState + "�ł�");
    }

    void PlayerTransformReset()
    {
        // �w�肳�ꂽ���a�̉~���̃����_���ʒu���擾
        var circlePos = _radius * Random.insideUnitCircle;

        // XZ���ʂŎw�肳�ꂽ���a�A���S�_�̉~���̃����_���ʒu���v�Z
        var spawnPos = new Vector3(circlePos.x, 10, circlePos.y) + _center;

        // Player�̑��x��0�ɂ��A�ړ�������
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
            // WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
            velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                velocity.z += 1;
            if (Input.GetKey(KeyCode.A))
                velocity.x -= 1;
            if (Input.GetKey(KeyCode.S))
                velocity.z -= 1;
            if (Input.GetKey(KeyCode.D))
                velocity.x += 1;

            // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
            velocity = velocity.normalized * moveSpeed * Time.deltaTime;

            // �����ꂩ�̕����Ɉړ����Ă���ꍇ
            if (velocity.magnitude > 0)
            {
                // �v���C���[�̉�](transform.rotation)�̍X�V
                // ����]��Ԃ̃v���C���[��Z+����(�㓪��)���A
                // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ��̔��Ε���(-velocity)�ɉ񂷉�]�ɒi�X�߂Â��܂�
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(refCamera.hRotation * -velocity),
                                                      applySpeed);


                if (Input.GetMouseButtonDown(1) && canDash == true) //�E�N���b�N�Ń_�b�V��
                {
                    dash = true;
                    canDash = false;

                }

                if (!canDash)
                {
                    timer += Time.deltaTime; //�w�肵���b���_�b�V�����ł��Ȃ�����
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
                timer += Time.deltaTime; //�w�肵���b���_�b�V�����ł��Ȃ�����
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

            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ�����(velocity)�𑫂����݂܂�
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
            Debug.Log("�������ڂ�" + cherryCount + "�擾�I");
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
