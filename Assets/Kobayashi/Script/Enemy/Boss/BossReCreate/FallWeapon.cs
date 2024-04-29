using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWeapon : ActionBase
{
    private WeaponBaseClass weapon;
    private GameObject FallObj;

    private readonly float FallWeaponSpeed = 35.0f;

    private readonly float AddCreateWeaponPosX = 3.0f;
    private readonly float AddCreateWeaponPosY = 10.0f;
    private readonly float AddCreateEffectPosY = 7.0f;

    private Vector3 createWeaponAngle = new Vector3(0.0f, 0.0f, 270.0f);

    private Vector3 effectCreatePos;
    private readonly float DeleteEffectTime = 4.0f;

    private float weaponPosDirection = 1.0f;

    private float createWeaponAngleStartRangeX = -20.0f;    // ランダム生成の範囲
    private float createWeaponAngleEndRangeX = 20.0f;       // ランダム生成の範囲
    private float createWeaponAngleStartRangeZ = 260.0f;    // ランダム生成の範囲
    private float createWeaponAngleEndRangeZ = 280.0f;      // ランダム生成の範囲

    private readonly int CreateWeaponNum = 7;               // 生成する数

    private readonly float DitanceWeapon = 2.5f;            // 武器の距離

    private readonly string SoundNameWeaponAppear = "BossWeaponAppear";

    CameraController_Shaking CameraShake = null;            // カメラの揺れを管理する

    public FallWeapon(BossEnemy boss, WeaponBaseClass weapon) : base(boss)
    {
        this.weapon = weapon;
        effectCreatePos = boss.transform.position;
        effectCreatePos.y += AddCreateEffectPosY;
    }

    public override void Excute()
    {
        CameraShake = GameObject.Find("MainCamera").GetComponent<CameraController_Shaking>();

        // エフェクトの追加
        ParticleManager.Instance._summonsSlashParticle(effectCreatePos, DeleteEffectTime);

        Vector3 createWeaponPos = boss.transform.position;

        createWeaponPos.y += AddCreateWeaponPosY;

        // bossの位置によって武器の生成位置を変える
        if (boss.transform.position.x < 0)
        {
            createWeaponPos.x = createWeaponPos.x + AddCreateWeaponPosX;
        }
        else
        {
            createWeaponPos.x = createWeaponPos.x - AddCreateWeaponPosX;
            weaponPosDirection = -weaponPosDirection;
        }

        CameraShake._Boot_CameraShake(4.8f,CameraController_Shaking.ShakeType.Low,0.01f,0.02f);

        SoundManager.Instance.Play(SoundNameWeaponAppear);

        Vector3 firstWeaponPos;

        for (int i = 0; i < CreateWeaponNum; ++i)
        {
            // 次のWeaponの生成位置を決める
            firstWeaponPos = createWeaponPos;
            firstWeaponPos.x = createWeaponPos.x + (weaponPosDirection * (i * DitanceWeapon));

            createWeaponAngle.z = Random.Range(createWeaponAngleStartRangeZ, createWeaponAngleEndRangeZ);
            createWeaponAngle.x = Random.Range(createWeaponAngleStartRangeX, createWeaponAngleEndRangeX);

            // 武器を複製して飛ばす処理
            FallObj = weapon._CreateWeapon(firstWeaponPos, createWeaponAngle);
            weapon = FallObj.GetComponent<WeaponBaseClass>();

            // 武器のナンバーをセット
            weapon.WeaponNumberSet(i);
            // 武器を動かす
            weapon._MoveWeapon(FallWeaponSpeed);
        }
    }
}