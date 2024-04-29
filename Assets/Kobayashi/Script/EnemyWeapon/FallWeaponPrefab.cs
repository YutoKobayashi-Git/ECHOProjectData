using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWeaponPrefab : WeaponBaseClass
{
    private Rigidbody rigidbody;

    private Vector3 WeaponSpeed = new Vector3(0.0f, 0.0f, 0.0f);
    
    private bool Changetremble;     // �k��������̐؂�ւ��t���O
     
    private bool Stoptremble;       // �k�����~�߂�t���O
     
    private readonly float ChangeTrembleNum = 0.03f;    // �k���������ς���܂ł̎���
    private readonly float ResetTrembleCount = 0.0f;    // �k���������ς���܂ł̎��ԃJ�E���g

    private float ChangeAfterTrembleCount;              // �k���������ς�����̌o�ߎ���
    private readonly float TrembleDistance = 0.1f;      // �k���鋗��

    private readonly int ColorBit = 255; 
    private readonly byte DisappearDeleteSpeed = 7;
    private readonly int DisappearDeleteTime = 30;
    private readonly float DeleteStartDelay = 1.5f;
    private readonly float DeleteSpeedDelay = 0.01f;

    private readonly float NextWeaponInterval = 0.3f;�@// ���̕���𗎂Ƃ��܂ł̊Ԋu
    private readonly float MoveWeaponDelay = 4.5f;     // ����𗎂Ƃ��n�߂�܂ł̎���

    private static readonly string StopObjectTag = "floor"; 

    private bool OnHitStop = false;
    private readonly float HitStopTime = 0.015f;

    private static readonly string MusicName = "BossSlash";

    MeshRenderer mesh;              // �������g�̃��b�V��

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
    /// ���킪�~��܂ł�Delay���Ԃ𒲐�����
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
    /// �����h�炷
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
    /// ����𓧖���
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

