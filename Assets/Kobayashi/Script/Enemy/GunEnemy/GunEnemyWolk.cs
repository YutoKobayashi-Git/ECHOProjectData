using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyWolk : GunEnemyActionBase
{
    // Bossのトランスフォームを代入
    Vector3 gunEnemyAngles;

    Rigidbody rigidbody;

    public GunEnemyWolk(GunEnemy gunEnemy) : base(gunEnemy)
    {
        // Bossのトランスフォームを代入
        gunEnemyAngles = gunEnemy.transform.eulerAngles;

        rigidbody = gunEnemy.rigidbody;

    }

    public override void Excute()
    {

        // 進行方向を決める
        if (gunEnemyAngles.y == 270)
        {
            rigidbody.velocity = new Vector3(2.6f, 0.0f, 0.0f);
            gunEnemyAngles.y = 90.0f;
            
        }
        else
        {
            rigidbody.velocity = new Vector3(-2.6f, 0.0f, 0.0f);
            gunEnemyAngles.y = 270.0f;
        }

        gunEnemy.transform.eulerAngles = gunEnemyAngles;

    }

}
