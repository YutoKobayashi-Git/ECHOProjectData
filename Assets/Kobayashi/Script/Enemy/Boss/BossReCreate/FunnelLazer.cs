using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelLazer : ActionBase
{
    // ファンネルを格納
    private Funnel[] Funnel = new Funnel[2];

    private Vector3 PlayerPos;

    private Vector3 FunnelPositon;

    private float FunnelPosX = 8.2f;
    private readonly float FunnelPosY = 6.0f;
    private readonly float BesideFunnelPosY = 0.7f;

    public FunnelLazer(BossEnemy boss, Funnel[] Funel, Vector3 playerposition) : base(boss)
    {
        // ファンネルの初期化
        for(int i = 0; i < Funel.Length; ++i)
        {
            this.Funnel[i] = Funel[i];
        }

        PlayerPos = playerposition;
    }

    public override void Excute()
    {
        FunnelPositon = PlayerPos;
        FunnelPositon.y = FunnelPosY;

        Funnel[0].MoveFunnel(FunnelPositon);

        FunnelPosX = boss.transform.position.x - 3;

        if (boss.transform.position.x < 0) FunnelPosX = FunnelPosX + 6;

        FunnelPositon = PlayerPos;
        FunnelPositon.x = FunnelPosX;
        FunnelPositon.y = BesideFunnelPosY;

        Funnel[1].MoveFunnel(FunnelPositon);
    }
}
