using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージを受けるオブジェクトに継承させるインターフェース
/// </summary>
public interface HittingObject
{
    public enum WeaponOwner
    {
        Player,
        Enemy,
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="Owner"></param>
    public virtual void ApllayDamage(Damage damage, WeaponOwner Owner) { }
}
