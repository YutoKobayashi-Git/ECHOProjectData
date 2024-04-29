using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの武器を投げる
/// </summary>
public class ThrowWeapon : ActionBase
{
    WeaponBaseClass weapon;

    float WeaponSpeed = 1.0f;

    Vector3 effectCreatePos;

    public ThrowWeapon(BossEnemy boss, WeaponBaseClass weapon) : base(boss)
    {
        this.weapon = weapon;
        effectCreatePos = boss.transform.position;
    }

    public override void Excute()
    {
        Vector3 createWeaponPos = boss.transform.position;
        createWeaponPos.y -= 1.5f;
        Vector3 createWeaponEuler;

        effectCreatePos.x += 3.5f;
        effectCreatePos.y += 3.0f;

        if (boss.transform.position.x > 0) effectCreatePos.x -= 7.0f;
        // エフェクトの追加
        ParticleManager.Instance._summonsThrowParticle(effectCreatePos, 5.0f);

        // bossの位置によって武器の生成位置を変える
        if (boss.transform.position.x < 0)
        {
            createWeaponPos.x += 8.0f;
            createWeaponEuler = new Vector3(0f, 0.0f, 0.0f);
        }
        else
        {
            createWeaponPos.x -= 8.0f;
            WeaponSpeed = -WeaponSpeed;

            createWeaponEuler = new Vector3(0f, 0.0f, 180.0f);
        }

        SoundManager.Instance.Play("ボス 武器飛ばし");

        // 武器を複製して飛ばす処理
        for (int i = 0; i < 2; ++i)
        {
            createWeaponPos.y += 2.0f;

            GameObject throwObj = weapon._CreateWeapon(createWeaponPos, createWeaponEuler);

            weapon = throwObj.GetComponent<WeaponBaseClass>();

            weapon._MoveWeapon(WeaponSpeed * (3.5f + i * 7));
        }
    }




}
