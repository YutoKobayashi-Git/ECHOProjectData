using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの切りつける
/// </summary>
public class SlashFallWeapon : ActionBase
{
    WeaponBaseClass weapon;

    public SlashFallWeapon(BossEnemy boss, WeaponBaseClass weapon) : base(boss)
    {
        this.weapon = weapon;
    }

    public override void Excute()
    {
        GameObject SlashObj;

        Vector3 createWeaponPos = boss.transform.position;
        createWeaponPos.y += 13.0f;
        Vector3 createWeaponEuler = new Vector3(0.0f,0.0f,180.0f);

        // bossの位置によって武器の生成位置を変える
        if (boss.transform.position.x < 0)
        {
            createWeaponPos.x += 4.0f;
            SlashObj = weapon._CreateWeapon(createWeaponPos, createWeaponEuler);
        }
        else
        {
            createWeaponPos.x -= 4.0f;
            SlashObj = weapon._CreateWeapon(createWeaponPos, createWeaponEuler);
        }

        SoundManager.Instance.Play("ボス　召喚");

        weapon = SlashObj.GetComponent<WeaponBaseClass>();

        weapon._MoveWeapon(-15.0f);
    }




}
