using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField, Header("���[�v���s������(�v���C���[����{�X�̊�)")]
    private float canWarpDistance;

    [SerializeField, Header("���[�v���s���܂ł̊Ԃ̍s����")]
    private int canWarpCount = 1;

    [SerializeField, Header("����~�炵�s���m��")]
    private int FallWeaponProbability = 2;
    [SerializeField]
    private float FallWeaponHpPercent = 90.0f;   // MaxHp / HpPercent

    [SerializeField, Header("�t�@���l���s���m��")]
    private int FunnelProbability = 5;

    [SerializeField, Header("�K�E�Z�s���m��")]
    private float SpecialHpPercent = 35.0f;      // MaxHp / HpPercen

    [SerializeField, Header("�{�X�̕���")]
    private BoxCollider BossWeaponColl;

    [SerializeField, Header("�t�@���l��")]
    private Funnel[] FunnelObject = new Funnel[2];

    [SerializeField, Header("���C�g�}�l�[�W���[")]
    private GameObject lightObject;

    private readonly int Percentage = 100;

    private int WarpCount;              // ���[�v���g�p����J�E���g

    private GetPatternAction actions = new GetPatternAction();  // �s����������l��

    private bool specialActionOnce;     // �K�E�Z���s������
    private bool specialWarpOnce;       // �K�E�Z�̌�A���[�v������

    private bool Dead;                  // ���񂾃t���O

    readonly private int BossKillScore = 5000;    // �{�X�̎��S���A�l���X�R�A

    enum Action
    {
        Slash,
        Warp,
        FallSlash,
        FunnelLazer,
        FallWeapon,
        Special,
        Walk,
    }

    Action action;                      // �A�N�V�������Ǘ�����enum

    private void Start()
    {
        // �X�^�~�i��ݒ�
        ActionStamina = MaxAcitonStamina;

        canAttack = false;

        // Boss�̕���̓����蔻��
        BossWeaponColl.enabled = false;

        Probability();
    }

    private void Update()
    {
        BossPatternAdd();

        AddStamina();

        AttackAction();

        DieMove();
    }

    // ���݂�HP�����^�[������
    public int GetBossrHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// �{�X����Ƀv���C���[�̕�������
    /// </summary>
    void TurnToTheBoss()
    {
        // PlayerTransform.transform.position.x 
    }

    /// <summary>
    /// �S����(��)�������֕ϊ�����
    /// </summary>
    void Probability()
    {
        if (FallWeaponHpPercent > 100) FallWeaponHpPercent = 100;
        if (FallWeaponHpPercent < 0) FallWeaponHpPercent = 0;

        if (SpecialHpPercent > 100) SpecialHpPercent = 100;
        if (SpecialHpPercent < 0) SpecialHpPercent = 0;

        SpecialHpPercent = (SpecialHpPercent / Percentage);

        FallWeaponHpPercent = (FallWeaponHpPercent / Percentage);
    }

    /// <summary>
    /// �{�X�̃A�N�V������ύX����
    /// </summary>
    void ActionChange()
    {
        Action BeforeAction = action;

        if (BeforeAction == Action.Walk)
        {
            AttackConditions();
            return;
        }

        while (BeforeAction == action)
        {
            AttackConditions();
        }
    }

    /// <summary>
    /// �ύX����
    /// </summary>
    private void AttackConditions()
    {
        // // ����
        // if (Random.Range(0,4) == 0)
        // {
        //     action = Action.Walk;
        //     return;
        // }          

        // �K�E�Z
        if (status.GetHp() < (status.MaxHp * SpecialHpPercent) && !specialActionOnce)
        {
            action = Action.Special;
            return;
        }

        // ���[�v
        if (specialWarpOnce)
        {
            action = Action.Warp;
            return;
        }

        // �t�@���l��
        if (action == Action.FallWeapon)
        {
            action = Action.FunnelLazer;
            return;
        }

        // ����~�炵
        if (status.GetHp() < (status.MaxHp * FallWeaponHpPercent) && Random.Range(0, FallWeaponProbability) == 0)
        {
            action = Action.FallWeapon;
            return;
        }

        // �t�@���l��
        if (Random.Range(0, FunnelProbability) == 0)
        {
            action = Action.FunnelLazer;
            return;
        }

        // �������m�F
        float playerdistance = PlayerTransform.position.x - gameObject.transform.position.x;

        // ���[�v
        if (Mathf.Abs(playerdistance) < canWarpDistance && WarpCount > canWarpCount)
        {
            action = Action.Warp;
            return;
        }

        action = Action.Slash;
    }

    /// <summary>
    /// �{�X�̍s����ǉ�����
    /// </summary>

    void BossPatternAdd()
    {
        // �X�^�~�i������Ȃ��ꍇ
        if (ActionStamina < CanActionMinimumStamina) return;

        // �s���\��
        canAttack = true;

        // �ǉ�����A�N�V���������߂�
        ActionChange();

        switch (action)
        {
            case Action.Walk:
                // �����A�N�V�����̏���
                Walk();
                break;
            case Action.Slash:
                // Slash�A�N�V�����̏���
                Slash();
                break;
            case Action.Warp:
                // Warp�A�N�V�����̏���
                Warp();
                break;
            case Action.FallSlash:
                // FallSlash�A�N�V�����̏���
                FallWeapon();
                break;
            case Action.FunnelLazer:
                // FunnelLazer�A�N�V�����̏���
                FunnelLazer();
                break;
            case Action.FallWeapon:
                // FallWeapon�A�N�V�����̏���
                FallWeapon();
                break;
            case Action.Special:
                // Special�A�N�V�����̏���
                Special();
                break;
            default:
                // �a��
                Slash();
                break;
        }

        // �X�^�~�i������
        ActionStamina -= StaminaUsed;
    }

    /// <summary>
    /// �A�N�V�������N����
    /// </summary>
    void AttackAction()
    {
        if (!canAttack) return;

        actions.Action();
        
        canAttack = false;
    }

    void Walk()
    {
        // �X�L���̒ǉ�
        actions.AddSkill(new Walk(this));
        Animate(Boss.Walk);
        StaminaUsed = 2.5f;
    }

    void Slash()
    {
        // �X�L���̒ǉ�
        actions.AddSkill(new SlashWeapon(this, WeaponBank.TryGetWeapon(WeaponBank.BossWeapon.SlashWeapon)));
        Animate(Boss.SlashStay);
        ++WarpCount;

        StaminaUsed = MaxAcitonStamina;
    }

    void Special()
    {
        // �X�L���̒ǉ�
        actions.AddSkill(new SpecialMove(this, WeaponBank.TryGetWeapon(WeaponBank.BossWeapon.SpecialWeapon)));
        Animate(Boss.Special);
        specialWarpOnce = true;
        specialActionOnce = true;

        StaminaUsed = MaxAcitonStamina;
    }

    void FunnelLazer()
    {
        // �X�L���̒ǉ�
        actions.AddSkill(new FunnelLazer(this, FunnelObject, PlayerTransform.position));
        Animate(Boss.LazerStay);

        StaminaUsed = MaxAcitonStamina;
    }

    void Warp()
    {
        // �X�L���̒ǉ�
        actions.AddSkill(new Warp(this));
        Animate(Boss.Warp);
        specialWarpOnce = false;
        WarpCount = 0;

        StaminaUsed = MaxAcitonStamina;
    }

    void FallWeapon()
    {
        // �X�L���̒ǉ�
        actions.AddSkill(new FallWeapon(this, WeaponBank.TryGetWeapon(WeaponBank.BossWeapon.FallWeapon)));
        Animate(Boss.LazerStay);
        ++WarpCount;

        StaminaUsed = MaxAcitonStamina;
    }

    public void OnAttack()
    {
        Animate(Boss.Slash);
    }

    public void OnLazerAttack()
    {
        Animate(Boss.Lazer);
    }

    public void OnWeaponColl()
    {
        BossWeaponColl.enabled = true;
    }

    public void EndWeaponColl()
    {
        BossWeaponColl.enabled = false;
    }

    /// <summary>
    /// ��ʂ��Â�
    /// </summary>
    public void BlackOut()
    {
        LightManager light = lightObject.GetComponent<LightManager>();
        light.StartBlinking();
    }

    /// <summary>
    /// ���񂾎��̏���
    /// </summary>
    void DieMove()
    {
        if (status.GetHp() > 0) return;
        Dead = true;

        if (!Dead) return;

        Animator anim;

        anim = gameObject.GetComponent<Animator>();

        anim.SetBool("Die", true);

        ScoreManager.Instance.Add_Score(BossKillScore);

        this.gameObject.GetComponent<BossEnemy>().enabled = false;

        ReSetHitColorDead();
    }
}


