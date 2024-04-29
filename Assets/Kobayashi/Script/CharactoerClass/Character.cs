using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour, HittingObject
{
    // �L�����N�^�[�̃X�e�[�^�X
    public CharacterStatus status = new CharacterStatus();           

    [SerializeField, Header("�A�j���[�^�[")]
    private Animator anim;       

    /// <summary>
    /// �A�j���[�^�[���n�b�V���ŊǗ�����
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
    /// �U��������������̏���
    /// </summary>
    public virtual void ApllayDamage(Damage damage, HittingObject.WeaponOwner OwnerName)
    {
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="hitdamege"></param>
    public void HpDown(int hitdamege)
    {
        int CurrentHp = status.GetHp();
        //���݂�HP����_���[�W������
        CurrentHp = CurrentHp - hitdamege;

        status.SetCurrentHp(CurrentHp);
    }

}


