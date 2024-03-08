using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [Header("爆発時のプレイヤーのジャンプ力")]
    [SerializeField] private float jumpForce = 100.0f;

    [Header("爆風に当たったときに吹っ飛ぶ力の強さ")]
    [SerializeField]
    private float _futtobiPower;

    [Header("爆風の判定が実際に発生するまでのディレイ")]
    [SerializeField]
    private float _startDelaySeconds = 0.1f;

    [Header("爆風の持続フレーム数")][SerializeField] private int _durationFrameCount = 1;

    [Header("エフェクト含めすべての再生が終了するまでの時間")]
    [SerializeField] private float _stopSeconds = 2f;

    [SerializeField] private int explodeCost = 10;               //爆発に必要なサクランボのコスト

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
    /// 爆破する
    /// </summary>
    public void Explode()
    {
        if(this.GetComponent<Player>().is_exploded) 
        {
            // 当たり判定管理のコルーチン
            StartCoroutine(ExplodeCoroutine());
            // エフェクトと効果音再生
            _effect.Play();
            _sfx.Play();
            Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得
            Vector3 force = new Vector3(0.0f, jumpForce, 0.0f);  // ジャンプ力を設定
            rb.AddForce(force);  // 力を加える
            // 爆発エフェクト含めてもろもろを消すコルーチン
            StartCoroutine(StopCoroutine());
        }
    }

    private IEnumerator ExplodeCoroutine()
    {
        // 指定秒数が経過するまでFixedUpdate上で待つ
        var delayCount = Mathf.Max(0, _startDelaySeconds);
        while (delayCount > 0)
        {
            yield return new WaitForFixedUpdate();
            delayCount -= Time.fixedDeltaTime;
        }

        // 時間経過したらコライダを有効化して爆発の当たり判定が出る
        _collider.enabled = true;

        // 一定フレーム数有効化
        for (var i = 0; i < _durationFrameCount; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        // 当たり判定無効化
        _collider.enabled = false;
    }

    private IEnumerator StopCoroutine()
    {
        // 時間経過後に消す
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
    /// 爆風にヒットしたときに相手をふっとばす処理
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // 衝突対象がRigidbodyの配下であるかを調べる
        var rigidBody = other.GetComponentInParent<Rigidbody>();

        // Rigidbodyがついてないなら吹っ飛ばないの終わり
        if (rigidBody == null) return;

        // 爆風によって爆発中央から吹き飛ぶ方向のベクトルを作る
        var direction = (other.transform.position - transform.position).normalized;

        // 吹っ飛ばす
        // ForceModeを変えると挙動が変わる（今回は質量無視）
        rigidBody.AddForce(direction * _futtobiPower, ForceMode.VelocityChange);
    }
}
