using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    // ���S�_
    [SerializeField] 
    private Vector3 _center = Vector3.zero;

    // ��]��
    [SerializeField]
    private Transform _axis;

    // �~�^������
    [SerializeField] 
    private float _period = 2;

    private float CircumferentialAngle = 360;

    private WeaponBaseClass Lazer;

    // ���[�U�[�̔��ˌ�
    [SerializeField]
    private Transform LazerPoint;

    // �^�[�Q�b�g��Transform
    [SerializeField, Header("�Ǐ]����I�u�W�F�N�g")] 
    private Transform _target;

    // �O���̊�ƂȂ郍�[�J����ԃx�N�g��
    [SerializeField] 
    private Vector3 _forward = Vector3.forward;

    // ���[�U�[���������鎞��
    [SerializeField, Header("���[�U�[���˂܂ł̑ҋ@����")]
    private float ReadyTime = 3.0f;

    private bool OnReady = false;

    Animator anim;

    // ���[�U�[�̎���
    [SerializeField]
    private float LazerFireTime;

    private bool OnFire = false;

    private bool OnAcceleration;

    private float DelaySound = 1.5f;

    // �t�@���l���̈ړ���
    private Vector3 MoveFunnelPosition;

    // �t�@���l���̈ʒu
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

   �@/// <summary>
    /// �t�@���l���̍s�����ړ���
    /// </summary>
    /// <param name="MoveFunnelPosition"></param>
    public void MoveFunnel(Vector3 MoveFunnelPosition)
    {
        funnelpattern = FunnelPattern.Move;

        this.MoveFunnelPosition = MoveFunnelPosition;
        LazerFireAngle = _target.position;
    }

    /// <summary>
    /// ���삳��Ă��Ȃ����
    /// </summary>
    private void MoveNormal()
    {
        FunnelPosition = _axis.position;

        gameObject.transform.position = FunnelPosition;
    }

    private void MoveCircle()
    {
        var tr = transform;
        // ��]�̃N�H�[�^�j�I���쐬
        var angleAxis = Quaternion.AngleAxis(CircumferentialAngle / _period * (Time.deltaTime / 15), _axis.position);

        // �~�^���̈ʒu�v�Z
        var pos = tr.position;

        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        tr.position = pos;
    }

    /// <summary>
    /// �t�@���l���𓮂���
    /// </summary>
    private void Move()
    {
        // �^�[�Q�b�g�ւ̌����x�N�g���v�Z
        var dir = LazerFireAngle - gameObject.transform.position;

        // �^�[�Q�b�g�̕����ւ̉�]
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // ��]�␳
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);

        // ��]�␳���^�[�Q�b�g�����ւ̉�]�̏��ɁA���g�̌����𑀍삷��
        gameObject.transform.rotation = lookAtRotation * offsetRotation;

        transform.position = Vector3.MoveTowards(transform.position, MoveFunnelPosition, FunnelSpeed * Time.deltaTime);

        if (OnReady) return;

        // �����x������
        StartCoroutine(Acceleration());

        // ���˂�����
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

        // �p�^�[���ύX
        funnelpattern = FunnelPattern.Attack;
    }

    /// <summary>
    /// �t�@���l�����{�X�̈ʒu�Ɉړ�
    /// </summary>
    private void ResetPosition()
    {
        FunnelPosition = _axis.position;
        transform.position = Vector3.MoveTowards(transform.position, FunnelPosition, FunnelSpeed * Time.deltaTime);
        transform.eulerAngles = AngleReSet;

        // �~��`���Ė߂�
        MoveCircle();

        if (OnAcceleration) return;

        OnAcceleration = true;

        // �����x������
        StartCoroutine(Acceleration());

        // if(transform.position == FunnelPosition)
        // {
        //     funnelpattern = FunnelPattern.Normal;
        // }

    }

    /// <summary>
    /// ���[�U�[�ōU��
    /// </summary>
    private void Attack()
    {
        if (OnFire) return;

        Vector3 LazerAngle = gameObject.transform.eulerAngles;

        LazerAngle.x -= 180.0f;


        anim.SetBool("Attack",true);

        // ���[�U�[�̏���
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
