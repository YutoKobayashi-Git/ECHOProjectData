using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    // 中心点
    [SerializeField] 
    private Vector3 _center = Vector3.zero;

    // 回転軸
    [SerializeField]
    private Transform _axis;

    // 円運動周期
    [SerializeField] 
    private float _period = 2;

    private float CircumferentialAngle = 360;

    private WeaponBaseClass Lazer;

    // レーザーの発射口
    [SerializeField]
    private Transform LazerPoint;

    // ターゲットのTransform
    [SerializeField, Header("追従するオブジェクト")] 
    private Transform _target;

    // 前方の基準となるローカル空間ベクトル
    [SerializeField] 
    private Vector3 _forward = Vector3.forward;

    // レーザーを準備する時間
    [SerializeField, Header("レーザー発射までの待機時間")]
    private float ReadyTime = 3.0f;

    private bool OnReady = false;

    Animator anim;

    // レーザーの時間
    [SerializeField]
    private float LazerFireTime;

    private bool OnFire = false;

    private bool OnAcceleration;

    private float DelaySound = 1.5f;

    // ファンネルの移動先
    private Vector3 MoveFunnelPosition;

    // ファンネルの位置
    private Vector3 FunnelPosition;

    private float FunnelSpeed = 8.0f;

    private Vector3 LazerFireAngle = new Vector3(0.0f, 0.0f, 0.0f);

    private readonly Vector3 AngleReSet = new Vector3(0.0f,0.0f,0.0f);

    private RaycastHit hits;

    private enum FunnelPattern
    {
        Normal,
        Move,
        Attack,
        ReSet,
    }

    private FunnelPattern funnelpattern;

    private void Update()
    {
        switch (funnelpattern)
        {
            case FunnelPattern.Normal:
                {
                    ResetPosition();
                    // MoveNormal();
                    break;
                }
            case FunnelPattern.Move:
                {
                    Move();
                    break;
                }
            case FunnelPattern.Attack:
                {
                    Attack();
                    break;
                }
            case FunnelPattern.ReSet:
                {
                    ResetPosition();
                    break;
                }
        }
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

   　/// <summary>
    /// ファンネルの行動を移動に
    /// </summary>
    /// <param name="MoveFunnelPosition"></param>
    public void MoveFunnel(Vector3 MoveFunnelPosition)
    {
        funnelpattern = FunnelPattern.Move;

        this.MoveFunnelPosition = MoveFunnelPosition;
        LazerFireAngle = _target.position;
    }

    /// <summary>
    /// 操作されていない状態
    /// </summary>
    private void MoveNormal()
    {
        FunnelPosition = _axis.position;

        gameObject.transform.position = FunnelPosition;
    }

    private void MoveCircle()
    {
        var tr = transform;
        // 回転のクォータニオン作成
        var angleAxis = Quaternion.AngleAxis(CircumferentialAngle / _period * (Time.deltaTime / 15), _axis.position);

        // 円運動の位置計算
        var pos = tr.position;

        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        tr.position = pos;
    }

    /// <summary>
    /// ファンネルを動かす
    /// </summary>
    private void Move()
    {
        // ターゲットへの向きベクトル計算
        var dir = LazerFireAngle - gameObject.transform.position;

        // ターゲットの方向への回転
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // 回転補正
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);

        // 回転補正→ターゲット方向への回転の順に、自身の向きを操作する
        gameObject.transform.rotation = lookAtRotation * offsetRotation;

        transform.position = Vector3.MoveTowards(transform.position, MoveFunnelPosition, FunnelSpeed * Time.deltaTime);

        if (OnReady) return;

        // 加速度をつける
        StartCoroutine(Acceleration());

        // 発射を準備
        StartCoroutine((Ready()));
    }


    IEnumerator Acceleration()
    {
        FunnelSpeed = 0;
        OnAcceleration = true; ;


        for (int i = 0; i < 16; ++i)
        {
            FunnelSpeed += 1.2f;
            yield return new WaitForSeconds(0.1f);
        }

        OnAcceleration = false;
    }

    IEnumerator Ready()
    {
        OnReady = true;

        yield return new WaitForSeconds(ReadyTime);

        OnReady = false;

        // パターン変更
        funnelpattern = FunnelPattern.Attack;
    }

    /// <summary>
    /// ファンネルをボスの位置に移動
    /// </summary>
    private void ResetPosition()
    {
        FunnelPosition = _axis.position;
        transform.position = Vector3.MoveTowards(transform.position, FunnelPosition, FunnelSpeed * Time.deltaTime);
        transform.eulerAngles = AngleReSet;

        // 円を描いて戻る
        MoveCircle();

        if (OnAcceleration) return;

        OnAcceleration = true;

        // 加速度をつける
        StartCoroutine(Acceleration());

        // if(transform.position == FunnelPosition)
        // {
        //     funnelpattern = FunnelPattern.Normal;
        // }

    }

    /// <summary>
    /// レーザーで攻撃
    /// </summary>
    private void Attack()
    {
        if (OnFire) return;

        Vector3 LazerAngle = gameObject.transform.eulerAngles;

        LazerAngle.x -= 180.0f;


        anim.SetBool("Attack",true);

        // レーザーの処理
        Lazer = ParticleManager.Instance._LazerParticle(gameObject.transform.position, LazerFireTime, LazerAngle);

        var direction = LazerPoint.position - gameObject.transform.position;

        StartCoroutine(Fire(direction));

        StartCoroutine(Sound());

        // ParticleManager.Instance
        StartCoroutine(EndFire());
    }

    IEnumerator Sound()
    {
        yield return new WaitForSeconds(DelaySound);

        SoundManager.Instance.Play("Lazer");
    }

    IEnumerator Fire(Vector3 direction)
    {
        yield return new WaitForSeconds(1.5f);

        if (Physics.Raycast(gameObject.transform.position, direction, out hits))
        {
            Debug.DrawRay(gameObject.transform.position, direction * 5f, Color.red, 1f);

            Lazer.OnTriggerEnter(hits.collider);
        }
    }

    IEnumerator EndFire()
    {
        OnFire = true;

        yield return new WaitForSeconds(LazerFireTime);

        anim.SetBool("Attack", false);

        OnFire = false;

        funnelpattern = FunnelPattern.ReSet;

    }



}
