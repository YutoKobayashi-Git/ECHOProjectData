using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour, HittingObject
{
    // キャラクターのステータス
    public CharacterStatus status = new CharacterStatus();           

    [SerializeField, Header("アニメーター")]
    private Animator anim;       

    /// <summary>
    /// アニメーターをハッシュで管理する
    /// </summary>
    /// <param name="animEvent"></param>
    public void Animate(System.Enum animEvent)
    {
        string animName = animEvent.ToString();
        int hash = Animator.StringToHash(animName);
        anim.SetTrigger(hash);
    }

    public enum Player
    {
        Idle01,
        Idle02,
        Run,
        Jump,
        Attack01,
        Attack02,
        Attack03,
        Attack04,
        Damage,
        Dead
    }
    public enum Boss
    {
        Idle,
        Walk,
        Slash,
        SlashStay,
        Lazer,
        LazerStay,
        Special,
        Warp,
        Die,
    }

    public enum RushCas
    {
        Idle,
        Run,
        Notice,
        Damage,
        Ready,
        Dead,
    }

    public enum GunCas
    {
        Idle,
        Walk,
        Shot,
        Change,
    }

    /// <summary>
    /// 攻撃をくらった時の処理
    /// </summary>
    public virtual void ApllayDamage(Damage damage, HittingObject.WeaponOwner OwnerName)
    {
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="hitdamege"></param>
    public void HpDown(int hitdamege)
    {
        int CurrentHp = status.GetHp();
        //現在のHPからダメージを引く
        CurrentHp = CurrentHp - hitdamege;

        status.SetCurrentHp(CurrentHp);
    }

}


