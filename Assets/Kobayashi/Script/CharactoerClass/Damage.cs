using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    [SerializeField]
    private readonly int amoutDamage = 10;  // ��b�_���[�W
    private int DamageRate = 25;            // �_���[�W�ɂ�����{��

    /// <summary>
    /// �_���[�W��Ԃ�����
    /// </summary>
    /// <param name="DamageRate"></param>
    /// <returns></returns>
    public int _damagereceived()
    {
        return amoutDamage * DamageRate;
    }

    /// <summary>
    /// �_���[�W���[�g��ݒ肷��
    /// </summary>
    /// <param name="DamegeRate"></param>
    public void AddDamageRate(int DamegeRate)
    {
        this.DamageRate = DamegeRate; 
    }

}
