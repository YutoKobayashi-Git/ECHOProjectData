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

    private float createWeaponAngleStartRangeX = -20.0f;    // �����_�������͈̔�
    private float createWeaponAngleEndRangeX = 20.0f;       // �����_�������͈̔�
    private float createWeaponAngleStartRangeZ = 260.0f;    // �����_�������͈̔�
    private float createWeaponAngleEndRangeZ = 280.0f;      // �����_�������͈̔�

    private readonly int CreateWeaponNum = 7;               // �������鐔

    private readonly float DitanceWeapon = 2.5f;            // ����̋���

    private readonly string SoundNameWeaponAppear = "BossWeaponAppear";

    CameraController_Shaking CameraShake = null;            // �J�����̗h����Ǘ�����

    public FallWeapon(BossEnemy boss, WeaponBaseClass weapon) : base(boss)
    {
        this.weapon = weapon;
        effectCreatePos = boss.transform.position;
        effectCreatePos.y += AddCreateEffectPosY;
    }

    public override void Excute()
    {
        CameraShake = GameObject.Find("MainCamera").GetComponent<CameraController_Shaking>();

        // �G�t�F�N�g�̒ǉ�
        ParticleManager.Instance._summonsSlashParticle(effectCreatePos, DeleteEffectTime);

        Vector3 createWeaponPos = boss.transform.position;

        createWeaponPos.y += AddCreateWeaponPosY;

        // boss�̈ʒu�ɂ���ĕ���̐����ʒu��ς���
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
            // ����Weapon�̐����ʒu�����߂�
            firstWeaponPos = createWeaponPos;
            firstWeaponPos.x = createWeaponPos.x + (weaponPosDirection * (i * DitanceWeapon));

            createWeaponAngle.z = Random.Range(createWeaponAngleStartRangeZ, createWeaponAngleEndRangeZ);
            createWeaponAngle.x = Random.Range(createWeaponAngleStartRangeX, createWeaponAngleEndRangeX);

            // ����𕡐����Ĕ�΂�����
            FallObj = weapon._CreateWeapon(firstWeaponPos, createWeaponAngle);
            weapon = FallObj.GetComponent<WeaponBaseClass>();

            // ����̃i���o�[���Z�b�g
            weapon.WeaponNumberSet(i);
            // ����𓮂���
            weapon._MoveWeapon(FallWeaponSpeed);
        }
    }
}