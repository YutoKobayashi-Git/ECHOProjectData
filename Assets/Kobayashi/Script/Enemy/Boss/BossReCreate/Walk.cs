using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : ActionBase
{
    Rigidbody rigidbody;


    public Walk(BossEnemy boss) : base(boss)
    {
        rigidbody = boss.GetComponent<Rigidbody>();
    }

    public override void Excute()
    {
        // Boss�̃g�����X�t�H�[������
        if(Random.Range(0,1) == 0)
        {
            rigidbody.velocity = new Vector3(-5.0f, 0.0f, 0.0f);
        }
        else
        {
            rigidbody.velocity = new Vector3(5.0f, 0.0f, 0.0f);
        }

    }
}
