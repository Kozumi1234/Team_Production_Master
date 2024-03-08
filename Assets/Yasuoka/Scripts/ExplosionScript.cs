using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [Header("�������̃v���C���[�̃W�����v��")]
    [SerializeField] private float jumpForce = 100.0f;

    [Header("�����ɓ��������Ƃ��ɐ�����ԗ͂̋���")]
    [SerializeField]
    private float _futtobiPower;

    [Header("�����̔��肪���ۂɔ�������܂ł̃f�B���C")]
    [SerializeField]
    private float _startDelaySeconds = 0.1f;

    [Header("�����̎����t���[����")][SerializeField] private int _durationFrameCount = 1;

    [Header("�G�t�F�N�g�܂߂��ׂĂ̍Đ����I������܂ł̎���")]
    [SerializeField] private float _stopSeconds = 2f;

    [SerializeField] private int explodeCost = 10;               //�����ɕK�v�ȃT�N�����{�̃R�X�g

    [SerializeField] private ParticleSystem _effect;

    [SerializeField] private AudioSource _sfx;

    [SerializeField] private SphereCollider _collider;

    private void Awake()
    {
        _effect.Stop();
        _sfx.Stop();
        _collider.enabled = false;
        
    }

    /// <summary>
    /// ���j����
    /// </summary>
    public void Explode()
    {
        if(this.GetComponent<Player>().is_exploded) 
        {
            // �����蔻��Ǘ��̃R���[�`��
            StartCoroutine(ExplodeCoroutine());
            // �G�t�F�N�g�ƌ��ʉ��Đ�
            _effect.Play();
            _sfx.Play();
            Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbody���擾
            Vector3 force = new Vector3(0.0f, jumpForce, 0.0f);  // �W�����v�͂�ݒ�
            rb.AddForce(force);  // �͂�������
            // �����G�t�F�N�g�܂߂Ă������������R���[�`��
            StartCoroutine(StopCoroutine());
        }
    }

    private IEnumerator ExplodeCoroutine()
    {
        // �w��b�����o�߂���܂�FixedUpdate��ő҂�
        var delayCount = Mathf.Max(0, _startDelaySeconds);
        while (delayCount > 0)
        {
            yield return new WaitForFixedUpdate();
            delayCount -= Time.fixedDeltaTime;
        }

        // ���Ԍo�߂�����R���C�_��L�������Ĕ����̓����蔻�肪�o��
        _collider.enabled = true;

        // ���t���[�����L����
        for (var i = 0; i < _durationFrameCount; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        // �����蔻�薳����
        _collider.enabled = false;
    }

    private IEnumerator StopCoroutine()
    {
        // ���Ԍo�ߌ�ɏ���
        yield return new WaitForSeconds(_stopSeconds);
        _effect.Stop();
        _sfx.Stop();
        _collider.enabled = false;
        this.GetComponent<Player>().is_exploded = false;
        GetComponent<Player>().is_exploded = false;
        Player.cherryCount -= explodeCost;
        if (Player.cherryCount < 0)
        {
            Player.cherryCount = 0;
        }
    }

    /// <summary>
    /// �����Ƀq�b�g�����Ƃ��ɑ�����ӂ��Ƃ΂�����
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // �ՓˑΏۂ�Rigidbody�̔z���ł��邩�𒲂ׂ�
        var rigidBody = other.GetComponentInParent<Rigidbody>();

        // Rigidbody�����ĂȂ��Ȃ琁����΂Ȃ��̏I���
        if (rigidBody == null) return;

        // �����ɂ���Ĕ����������琁����ԕ����̃x�N�g�������
        var direction = (other.transform.position - transform.position).normalized;

        // ������΂�
        // ForceMode��ς���Ƌ������ς��i����͎��ʖ����j
        rigidBody.AddForce(direction * _futtobiPower, ForceMode.VelocityChange);
    }
}
