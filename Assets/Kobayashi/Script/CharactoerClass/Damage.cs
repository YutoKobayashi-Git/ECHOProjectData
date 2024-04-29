using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    [SerializeField]
    private readonly int amoutDamage = 10;  // 基礎ダメージ
    private int DamageRate = 25;            // ダメージにかける倍率

    /// <summary>
    /// ダメージを返す処理
    /// </summary>
    /// <param name="DamageRate"></param>
    /// <returns></returns>
    public int _damagereceived()
    {
        return amoutDamage * DamageRate;
    }

    /// <summary>
    /// ダメージレートを設定する
    /// </summary>
    /// <param name="DamegeRate"></param>
    public void AddDamageRate(int DamegeRate)
    {
        this.DamageRate = DamegeRate; 
    }

}
