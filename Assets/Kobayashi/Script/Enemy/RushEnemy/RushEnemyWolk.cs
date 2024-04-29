using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RushEnemyWolk : RushEnemyActionBase
{
    // Bossのトランスフォームを代入
    Vector3 RushEnemyAngles;

    readonly float Speed;

    Rigidbody rigidbody;

    private readonly string SoundName2 = "Bike";

    public RushEnemyWolk(RushEnemy rushEnemy, float Speed) : base(rushEnemy)
    {
        // Bossのトランスフォームを代入
        RushEnemyAngles = rushEnemy.transform.eulerAngles;

        this.Speed = Speed;

        rigidbody = rushEnemy.rigidbody;

    }

    async public override void Excute()
    {

        await Rush();

    }

    private async Task Rush()
    {
        await Task.Delay(2990);

        if (rushEnemy == null) return;

        // SoundManager.Instance.Play(SoundName2);

        // 進行方向を決める
        if (rushEnemy.PlayerTransform.position.x > rushEnemy.transform.position.x)
        {
            rigidbody.velocity = new Vector3(Speed, 0.0f, 0.0f);
            RushEnemyAngles.y = 90.0f;

        }
        else
        {
            rigidbody.velocity = new Vector3(-Speed, 0.0f, 0.0f);
            RushEnemyAngles.y = 270.0f;
        }

        rushEnemy.transform.eulerAngles = RushEnemyAngles;

        SoundManager.Instance.Play("RushWork");
    }
}