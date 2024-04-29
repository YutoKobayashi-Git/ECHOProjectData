using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMove : ActionBase
{
    private WeaponBaseClass weapon;

    // 武器の移動スピード
    private readonly float WeaponSpeed = 15.0f;

    // 武器の生成位置と回転
    private readonly Vector3 createWeaponPos = new Vector3(0.0f, 8.0f, 1.0f);
    private readonly Vector3 createWeaponEuler = new Vector3(0.0f, 0.0f, 270.0f);
    // ボスの位置と回転
    private readonly Vector3 bossPos = new Vector3(0.0f, 0.0f, 10.0f);
    private readonly Vector3 bossAngles = new Vector3(0.0f, 180.0f, 0.0f);
    // 効果音
    private readonly string SoundName = "BossSlash";


    public SpecialMove(BossEnemy boss, WeaponBaseClass weapon) : base(boss)
    {
        this.weapon = weapon;
    }

    public override void Excute()
    {
        SoundManager.Instance.Play(SoundName);

        boss.transform.position = bossPos;
        boss.transform.eulerAngles = bossAngles;

        GameObject specialObject = weapon._CreateWeapon(createWeaponPos, createWeaponEuler);
        weapon = specialObject.GetComponent<WeaponBaseClass>();
        weapon._MoveWeapon(WeaponSpeed);
    }


}