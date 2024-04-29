using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyWolk : GunEnemyActionBase
{
    // Boss�̃g�����X�t�H�[������
    Vector3 gunEnemyAngles;

    Rigidbody rigidbody;

    public GunEnemyWolk(GunEnemy gunEnemy) : base(gunEnemy)
    {
        // Boss�̃g�����X�t�H�[������
        gunEnemyAngles = gunEnemy.transform.eulerAngles;

        rigidbody = gunEnemy.rigidbody;

    }

    public override void Excute()
    {

        // �i�s���������߂�
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
