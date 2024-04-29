using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeaponPrefab : WeaponBaseClass
{
    private Rigidbody rigidbody;

    private Vector3 WeaponSpeed = new Vector3(0.0f, 0.0f, 0.0f);

    private readonly Vector3 effectpos = new Vector3(0.0f, 0.5f, 0.0f);
    private readonly Vector3 effectpos2 = new Vector3(0.0f, 10.0f, 0.0f);

    private bool StopTremble;   // 震えを止める
    private readonly float ChangeTrembleTime = 0.03f;

    private readonly Vector3 RotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);

    private readonly float FallDelay = 4.0f;

    private readonly float VelocityY = 2.5f;
    private readonly int AddSpeedNum = 8;
    private readonly float SpeedDelay = 0.01f;

    private readonly string StopObjectName = "floor";
    private readonly string SoundtNameSpecial = "Special2";
    private readonly string SoundtNameFire = "Fire";

    private float ChangeTrembleNum;

    private Light light;

    private readonly int LightLoopCount = 100;      // ライトを小さくするために必要なCount
    private readonly float FastSmallChangeRange = 0.09f;
    private readonly float SlowSmallChangeRange = 0.25f;
    private readonly float AddIntensity = 0.1f;

    private bool OnceCameraShaking;

    CameraController_Shaking CameraShake = null;

    private void Update()
    {
        ChangeTrembleNum += Time.deltaTime;

        if (ChangeTrembleNum <= ChangeTrembleTime) return;
        
        _trembleWeapon();
        ChangeTrembleNum = 0.0f;

    }

    private void Start()
    {
        CameraShake = GameObject.Find("MainCamera").GetComponent<CameraController_Shaking>();
    }
    /// <summary>
    /// 必殺技の武器の行動開始
    /// </summary>
    /// <param name="Speed"></param>
    public override void _MoveWeapon(float Speed)
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();

        StartCoroutine(_Delay());
    }

    /// <summary>
    ///  落ちるまでDelayをかける
    /// </summary>
    /// <returns></returns>
    IEnumerator _Delay()
    {
        StartCoroutine(ChangeLightScale());
        WeaponSpeed.y = 0;

        yield return new WaitForSeconds(FallDelay);

        StopTremble = true;

        StartCoroutine(Acceleration());

        StartCoroutine(_effect());
    }


    IEnumerator Acceleration()
    {
        for (int i = 0; i < AddSpeedNum; ++i)
        {
            WeaponSpeed.y -= VelocityY;
            rigidbody.velocity = WeaponSpeed;
            yield return new WaitForSeconds(SpeedDelay);
        }
    }

    void _trembleWeapon()
    {
        if (StopTremble) return;

        rigidbody.angularVelocity = RotationSpeed;
    }

    IEnumerator _effect()
    {
        yield return new WaitForSeconds(2.0f);
        ParticleManager.Instance._summonsSpecoalParticle(effectpos, 5.0f, 2.0f);
        ParticleManager.Instance._summonsSpecoalParticle(effectpos2, 5.0f, 2.0f);

        SoundManager.Instance.Play(SoundtNameSpecial);

        CameraShake._Boot_CameraShake(3.0f, CameraController_Shaking.ShakeType.high, 0.15f, 0.2f);

        Destroy(gameObject);
    }

    /// <summary>
    /// ライトの大きさを縮める
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeLightScale()
    {
        light = GetComponent<Light>();

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < LightLoopCount; i++)
        {
            light.range -= SlowSmallChangeRange;
            light.intensity += AddIntensity;

            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// 床に当たったらストップ
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(StopObjectName) == true)
        {
            rigidbody.velocity = SpeedReSet;
            rigidbody.angularVelocity = SpeedReSet;

            StartCoroutine(DeleteLightScale());

            if (OnceCameraShaking) return;
                OnceCameraShaking = true;
            CameraShake._Boot_CameraShake(1.0f, CameraController_Shaking.ShakeType.medium, 0.02f, 0.1f);
            ParticleManager.Instance._DeadPaticle(gameObject.transform.position,0.5f);

            SoundManager.Instance.Play(SoundtNameFire);
        }
    }

    /// <summary>
    /// 爆発前にライトをさらに小さくする
    /// </summary>
    /// <returns></returns>
    IEnumerator DeleteLightScale()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < LightLoopCount; i++)
        {
            light.range -= FastSmallChangeRange;

            yield return new WaitForSeconds(0.01f);
        }
    }


}
