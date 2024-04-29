using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// ���[�v
/// </summary>
public class Warp : ActionBase
{
    private Vector3 effectCreatePos;

    private Transform bossTransform;

    private readonly float AddEffectCreatePosY = 3.0f;

    private readonly float DeleteEffectTime = 3.0f;

    private readonly float BossWarpPosX = 11.0f;
    private readonly Vector3 ResetPos = new Vector3(0.0f, 0.0f, 1.0f);

    private readonly float BossWorldAngleLeft = 90.0f;
    private readonly float BossWorldAngleRight = 270.0f;

    private readonly int DelayTime = 2990;

    private readonly string SoundName = "BossWarp";

    public Warp(BossEnemy boss) : base(boss)
    {

    }

    async public override void Excute()
    {
        // Boss�̃g�����X�t�H�[������
        bossTransform = boss.transform;

        // �G�t�F�N�g�̒ǉ�
        effectCreatePos = boss.gameObject.transform.position;
        effectCreatePos.y += AddEffectCreatePosY;
        ParticleManager.Instance._summonsParticle(effectCreatePos, DeleteEffectTime);

        await BossWarp();

    }

    private async Task BossWarp()
    {
        await Task.Delay(DelayTime);

        if (boss == null) return;

        // ���[�v����
        boss.transform.position = BossPositionChange(bossTransform);
        boss.transform.eulerAngles = BossAnglesChange(bossTransform);

        SoundManager.Instance.Play(SoundName);
    }

    /// <summary>
    /// �{�X�̃��[�v����
    /// </summary>
    /// <param name="bossTransform"></param>
    Vector3 BossPositionChange(Transform bossTransform)
    {
        Vector3 BossPos = bossTransform.position;

        if (bossTransform.position.x < 0)
        {
            BossPos.x = BossWarpPosX;
        }
        else
        {
            BossPos.x = -BossWarpPosX;
        }

        BossPos.y = ResetPos.y;
        BossPos.z = ResetPos.z;
 
        return BossPos;

    }

    /// <summary>
    /// ���[�v��̌�����␳
    /// </summary>
    /// <param name="bossTransform"></param>
    Vector3 BossAnglesChange(Transform bossTransform)
    {
        // Boss�̌�����ύX���鏈��
        Vector3 BossworldAngle = bossTransform.eulerAngles;

        if (bossTransform.position.x < 0)
        {
            BossworldAngle.y = BossWorldAngleLeft;
        }
        else
        {
            BossworldAngle.y = BossWorldAngleRight;
        }

        bossTransform.eulerAngles = BossworldAngle;

        return BossworldAngle;
    }
}
