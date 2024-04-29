using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseClass : MonoBehaviour , WeaponDamege
{
    protected int WeaponNumber;

    protected int WeaponDamage = 25;

    // 武器のOwnerを保存する変数
    protected HittingObject.WeaponOwner owner;

    protected static readonly Vector3 SpeedReSet = new Vector3(0.0f, 0.0f, 0.0f);

    public void OnTriggerEnter(Collider other)
    {
        // インターフェースを取得
        HittingObject hits = other.GetComponent<HittingObject>();

        if (hits == null) return;

        // 武器の所有者を決定する
        if (gameObject.CompareTag("Enemy")) owner = HittingObject.WeaponOwner.Enemy;
        if (gameObject.CompareTag("EnemyWeapon")) owner = HittingObject.WeaponOwner.Enemy;
        if (gameObject.CompareTag("Player")) owner = HittingObject.WeaponOwner.Player;

        // ダメージを生成
        Damage damage = new Damage();

        damage.AddDamageRate(WeaponDamage);

        //ダメージ処理
        hits.ApllayDamage(damage, owner);
    }

    /// <summary>
    /// 武器に番号指定する場合に呼び出す
    /// </summary>
    /// <param name="Num"></param>
    public void WeaponNumberSet(int Num)
    {
        WeaponNumber = Num;
    }

    /// <summary>
    /// 武器の生成
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="euler"></param>
    /// <returns></returns>
    public GameObject _CreateWeapon(Vector3 pos, Vector3 euler)
    {
        return Instantiate(this.gameObject, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(euler.x, euler.y, euler.z));
    }

    /// <summary>
    /// 武器を動かす
    /// </summary>
    /// <param name="Speed"></param>
    public virtual void _MoveWeapon(float Speed) { }
}

public interface WeaponDamege
{
    public virtual int WeaponDamageSet(int WeaponDamage) { return 0; }
}







