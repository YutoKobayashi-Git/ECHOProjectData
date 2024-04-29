using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : Character
{
    [SerializeField, Header("スタミナの最大値")]　
    public float MaxAcitonStamina;

    [SerializeField, Header("自身のRigidbody")] 
    public Rigidbody rigidbody;

    [SerializeField, Header("プレイヤーのトランスフォーム")] 
    public Transform PlayerTransform;

    [SerializeField, Header("キャラのマテリアル")]
    Material[] EnemyMaterial;

    [SerializeField, Header("ダメージを受けた時のヒットストップ")]
    private float HitStopTime = 0.001f;

    [SerializeField, Header("ヒットした際に赤くしておく時間")]
    private float HitChangeColorTime = 0.1f;

    protected float CanActionMinimumStamina = 10;   // 行動できる最低ラインのスタミナ
    protected float ActionStamina;                  // 変化するスタミナ
    protected float StaminaUsed;                    // 使ったスタミナ
    protected bool canAttack;                       // 攻撃できるか

    private static readonly string ColorName = "_Color";

    private void Start()
    {
        CanActionMinimumStamina = MaxAcitonStamina;

        for (int i = 0; i < EnemyMaterial.Length; ++i)
        {
            EnemyMaterial[i].SetColor(ColorName, Color.white);
        }
    }

    /// <summary>
    /// 攻撃内容を格納、呼び出しする
    /// </summary>
    public class GetPatternAction : MonoBehaviour
    {
        // 攻撃パターンを格納しておく変数
        private Queue<CharacterSkill> skills = new Queue<CharacterSkill>();

        /// <summary>
        /// アクションを追加
        /// </summary>
        /// <param name="skill"></param>
        public void AddSkill(CharacterSkill skill)
        {
            skills.Enqueue(skill);
        }

        /// <summary>
        /// アクションを行う
        /// </summary>
        public void Action()
        {
            skills.Dequeue().Excute();
        }

    }

    /// <summary>
    /// 攻撃をくらった時の処理
    /// </summary>
     public override void ApllayDamage(Damage damage, HittingObject.WeaponOwner OwnerName)
     {
         // 自分の武器を弾く
         if (OwnerName == HittingObject.WeaponOwner.Enemy) return;

        HitEffectRed();

        HitStopManager.Instance.StartHitStop(HitStopTime);

        HpDown(damage._damagereceived());

        KnockBack();
     }

    public virtual void KnockBack()
    {

    }

    /// <summary>
    /// マテリアルを赤色を変える
    /// </summary>
    private void HitEffectRed()
    {
        for (int i = 0; i < EnemyMaterial.Length; ++i)
        {
            EnemyMaterial[i].SetColor(ColorName, Color.red);
        }

        StartCoroutine(ReSetHitColor());
    }

    /// <summary>
    /// マテリアルを白色を変える
    /// </summary>
    private void HitEffectWhite()
    {
        for (int i = 0; i < EnemyMaterial.Length; ++i)
        {
            EnemyMaterial[i].SetColor(ColorName, Color.white);
        }
    }

    /// <summary>
    /// ヒット後にマテリアルを元の色に戻す
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReSetHitColor()
    {
        yield return new WaitForSeconds(HitChangeColorTime);
        HitEffectWhite();
    }

    /// <summary>
    /// 死んだときにマテリアルを元の色に戻す
    /// </summary>
    protected void ReSetHitColorDead()
    {
        HitEffectWhite();
    }

    /// <summary>
    /// スタミナの追加
    /// </summary>
    public void AddStamina()
    {
        ActionStamina += Time.deltaTime;
    }

    /// <summary>
    /// 敵のHPを返す
    /// </summary>
    /// <returns></returns>
    public int GetBossHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// 近くにプレイヤーがいるか
    /// </summary>
    protected bool NearPlayer()
    {
        float PlayerX = PlayerTransform.position.x;
        float Enemy = this.gameObject.transform.position.x;

        if (Mathf.Abs(PlayerX - Enemy) > 30)
        {
            return false;
        }

        return true;
    }
}
