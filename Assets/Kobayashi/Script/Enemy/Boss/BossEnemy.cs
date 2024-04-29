using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField, Header("ワープを行う距離(プレイヤーからボスの間)")]
    private float canWarpDistance;

    [SerializeField, Header("ワープを行うまでの間の行動回数")]
    private int canWarpCount = 1;

    [SerializeField, Header("武器降らし行動確率")]
    private int FallWeaponProbability = 2;
    [SerializeField]
    private float FallWeaponHpPercent = 90.0f;   // MaxHp / HpPercent

    [SerializeField, Header("ファンネル行動確率")]
    private int FunnelProbability = 5;

    [SerializeField, Header("必殺技行動確率")]
    private float SpecialHpPercent = 35.0f;      // MaxHp / HpPercen

    [SerializeField, Header("ボスの武器")]
    private BoxCollider BossWeaponColl;

    [SerializeField, Header("ファンネル")]
    private Funnel[] FunnelObject = new Funnel[2];

    [SerializeField, Header("ライトマネージャー")]
    private GameObject lightObject;

    private readonly int Percentage = 100;

    private int WarpCount;              // ワープを使用するカウント

    private GetPatternAction actions = new GetPatternAction();  // 行動する情報を獲得

    private bool specialActionOnce;     // 必殺技を行ったか
    private bool specialWarpOnce;       // 必殺技の後、ワープを強制

    private bool Dead;                  // 死んだフラグ

    readonly private int BossKillScore = 5000;    // ボスの死亡時、獲得スコア

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

    Action action;                      // アクションを管理するenum

    private void Start()
    {
        // スタミナを設定
        ActionStamina = MaxAcitonStamina;

        canAttack = false;

        // Bossの武器の当たり判定
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

    // 現在のHPをリターンする
    public int GetBossrHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// ボスが常にプレイヤーの方が向く
    /// </summary>
    void TurnToTheBoss()
    {
        // PlayerTransform.transform.position.x 
    }

    /// <summary>
    /// 百分率(％)を少数へ変換する
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
    /// ボスのアクションを変更する
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
    /// 変更条件
    /// </summary>
    private void AttackConditions()
    {
        // // 歩き
        // if (Random.Range(0,4) == 0)
        // {
        //     action = Action.Walk;
        //     return;
        // }          

        // 必殺技
        if (status.GetHp() < (status.MaxHp * SpecialHpPercent) && !specialActionOnce)
        {
            action = Action.Special;
            return;
        }

        // ワープ
        if (specialWarpOnce)
        {
            action = Action.Warp;
            return;
        }

        // ファンネル
        if (action == Action.FallWeapon)
        {
            action = Action.FunnelLazer;
            return;
        }

        // 武器降らし
        if (status.GetHp() < (status.MaxHp * FallWeaponHpPercent) && Random.Range(0, FallWeaponProbability) == 0)
        {
            action = Action.FallWeapon;
            return;
        }

        // ファンネル
        if (Random.Range(0, FunnelProbability) == 0)
        {
            action = Action.FunnelLazer;
            return;
        }

        // 距離を確認
        float playerdistance = PlayerTransform.position.x - gameObject.transform.position.x;

        // ワープ
        if (Mathf.Abs(playerdistance) < canWarpDistance && WarpCount > canWarpCount)
        {
            action = Action.Warp;
            return;
        }

        action = Action.Slash;
    }

    /// <summary>
    /// ボスの行動を追加する
    /// </summary>

    void BossPatternAdd()
    {
        // スタミナが足りない場合
        if (ActionStamina < CanActionMinimumStamina) return;

        // 行動可能に
        canAttack = true;

        // 追加するアクションを決める
        ActionChange();

        switch (action)
        {
            case Action.Walk:
                // 歩きアクションの処理
                Walk();
                break;
            case Action.Slash:
                // Slashアクションの処理
                Slash();
                break;
            case Action.Warp:
                // Warpアクションの処理
                Warp();
                break;
            case Action.FallSlash:
                // FallSlashアクションの処理
                FallWeapon();
                break;
            case Action.FunnelLazer:
                // FunnelLazerアクションの処理
                FunnelLazer();
                break;
            case Action.FallWeapon:
                // FallWeaponアクションの処理
                FallWeapon();
                break;
            case Action.Special:
                // Specialアクションの処理
                Special();
                break;
            default:
                // 斬撃
                Slash();
                break;
        }

        // スタミナを消費
        ActionStamina -= StaminaUsed;
    }

    /// <summary>
    /// アクションを起こす
    /// </summary>
    void AttackAction()
    {
        if (!canAttack) return;

        actions.Action();
        
        canAttack = false;
    }

    void Walk()
    {
        // スキルの追加
        actions.AddSkill(new Walk(this));
        Animate(Boss.Walk);
        StaminaUsed = 2.5f;
    }

    void Slash()
    {
        // スキルの追加
        actions.AddSkill(new SlashWeapon(this, WeaponBank.TryGetWeapon(WeaponBank.BossWeapon.SlashWeapon)));
        Animate(Boss.SlashStay);
        ++WarpCount;

        StaminaUsed = MaxAcitonStamina;
    }

    void Special()
    {
        // スキルの追加
        actions.AddSkill(new SpecialMove(this, WeaponBank.TryGetWeapon(WeaponBank.BossWeapon.SpecialWeapon)));
        Animate(Boss.Special);
        specialWarpOnce = true;
        specialActionOnce = true;

        StaminaUsed = MaxAcitonStamina;
    }

    void FunnelLazer()
    {
        // スキルの追加
        actions.AddSkill(new FunnelLazer(this, FunnelObject, PlayerTransform.position));
        Animate(Boss.LazerStay);

        StaminaUsed = MaxAcitonStamina;
    }

    void Warp()
    {
        // スキルの追加
        actions.AddSkill(new Warp(this));
        Animate(Boss.Warp);
        specialWarpOnce = false;
        WarpCount = 0;

        StaminaUsed = MaxAcitonStamina;
    }

    void FallWeapon()
    {
        // スキルの追加
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
    /// 画面を暗く
    /// </summary>
    public void BlackOut()
    {
        LightManager light = lightObject.GetComponent<LightManager>();
        light.StartBlinking();
    }

    /// <summary>
    /// 死んだ時の処理
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


