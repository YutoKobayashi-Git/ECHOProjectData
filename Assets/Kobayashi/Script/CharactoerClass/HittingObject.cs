using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_���[�W���󂯂�I�u�W�F�N�g�Ɍp��������C���^�[�t�F�[�X
/// </summary>
public interface HittingObject
{
    public enum WeaponOwner
    {
        Player,
        Enemy,
    }

    /// <summary>
    /// �_���[�W���󂯂鏈��
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="Owner"></param>
    public virtual void ApllayDamage(Damage damage, WeaponOwner Owner) { }
}
