using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWeaponPrefab : WeaponBaseClass
{
    private Rigidbody rigidbody;

    private Vector3 WeaponSpeed = new Vector3(0.0f, 0.0f, 0.0f);
    
    private bool Changetremble;     // k‚¦‚é•ûŒü‚ÌØ‚è‘Ö‚¦ƒtƒ‰ƒO
     
    private bool Stoptremble;       // k‚¦‚ğ~‚ß‚éƒtƒ‰ƒO
     
    private readonly float ChangeTrembleNum = 0.03f;    // k‚¦‚é•ûŒü‚ğ•Ï‚¦‚é‚Ü‚Å‚ÌŠÔ
    private readonly float ResetTrembleCount = 0.0f;    // k‚¦‚é•ûŒü‚ğ•Ï‚¦‚é‚Ü‚Å‚ÌŠÔƒJƒEƒ“ƒg

    private float ChangeAfterTrembleCount;              // k‚¦‚é•ûŒü‚ğ•Ï‚¦‚½Œã‚ÌŒo‰ßŠÔ
    private readonly float TrembleDistance = 0.1f;      // k‚¦‚é‹——£

    private readonly int ColorBit = 255; 
    private readonly byte DisappearDeleteSpeed = 7;
    private readonly int DisappearDeleteTime = 30;
    private readonly float DeleteStartDelay = 1.5f;
    private readonly float DeleteSpeedDelay = 0.01f;

    private readonly float NextWeaponInterval = 0.3f;@// Ÿ‚Ì•Ší‚ğ—‚Æ‚·‚Ü‚Å‚ÌŠÔŠu
    private readonly float MoveWeaponDelay = 4.5f;     // •Ší‚ğ—‚Æ‚µn‚ß‚é‚Ü‚Å‚ÌŠÔ

    private static readonly string StopObjectTag = "floor"; 

    private bool OnHitStop = false;
    private readonly float HitStopTime = 0.015f;

    private static readonly string MusicName = "BossSlash";

    MeshRenderer mesh;              // ©•ª©g‚ÌƒƒbƒVƒ…

    private void Update()
    {
        _trembleWeapon();
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }

    public override void _MoveWeapon(float Speed)
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        WeaponSpeed.y = -Speed;

        StartCoroutine(MoveDelay());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveDelay()
    {
        float DelayTime = AdjustmentDelay();

        yield return new WaitForSeconds(DelayTime);

        SoundManager.Instance.Play(MusicName);

        Stoptremble = true;
        rigidbody.velocity = WeaponSpeed;

        mesh = GetComponent<MeshRenderer>();
        StartCoroutine(Transparent());
    }

    /// <summary>
    /// •Ší‚ª~‚é‚Ü‚Å‚ÌDelayŠÔ‚ğ’²®‚·‚é
    /// </summary>
    /// <returns></returns>
    float AdjustmentDelay()
    {
        float DelayTime = (float)WeaponNumber;
        DelayTime = DelayTime * NextWeaponInterval;
        DelayTime += MoveWeaponDelay;

        return DelayTime;
    }

    /// <summary>
    /// •Ší‚ğ—h‚ç‚·
    /// </summary>
    void _trembleWeapon()
    {
        ChangeAfterTrembleCount += Time.deltaTime;

        if (ChangeAfterTrembleCount <= ChangeTrembleNum) return;

        ChangeAfterTrembleCount = ResetTrembleCount;

        if (Stoptremble) return;

        Vector3 objectPos = this.gameObject.transform.position;

        if (Changetremble)
        {
            objectPos.x -= TrembleDistance;
            Changetremble = false;
        }
        else
        {
            objectPos.x += TrembleDistance;
            Changetremble = true;
        }

        this.gameObject.transform.position = objectPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(StopObjectTag) == true)
        {
            rigidbody.velocity = SpeedReSet;

            if (OnHitStop) return;

            OnHitStop = true;
            HitStopManager.Instance.StartHitStop(HitStopTime);
        }
    }

    /// <summary>
    /// •Ší‚ğ“§–¾‰»
    /// </summary>
    /// <returns></returns>
    IEnumerator Transparent()
    {
        yield return new WaitForSeconds(DeleteStartDelay);

        for (int i = 0; i < (ColorBit / DisappearDeleteSpeed); i++)
        {
            mesh.material.color = mesh.material.color - new Color32(0, 0, 0, DisappearDeleteSpeed);
            yield return new WaitForSeconds(DeleteSpeedDelay);

            if(i == DisappearDeleteTime)
            {
                Delete();
            }
        }
    }
}

