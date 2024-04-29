using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GunEnemyFire : GunEnemyActionBase
{
    WeaponBaseClass weapon;

    private float FireDirection;

    private Vector3 createWeaponPos;

    private Rigidbody rigidbody;

    public GunEnemyFire(GunEnemy gunEnemy, WeaponBaseClass weapon) : base(gunEnemy)
    {
        this.weapon = weapon;

        // �e�̐����ꏊ�����߂�
        createWeaponPos = gunEnemy.transform.position;
        
        createWeaponPos.y += -0.3f;

    }

    //async 

    public override void Excute()
    {
        if (gunEnemy == null) return;

        // �v���C���[�̈ʒu�𑪒肵�A�ˌ����������߂�
        if (gunEnemy.transform.eulerAngles.y < 200)
        {
            FireDirection = 7.0f;

            createWeaponPos.x += 2.5f;
        }
        else
        {
            FireDirection = -7.0f;
            createWeaponPos.x -= 2.5f;
        }

        ParticleManager.Instance._gunFireParticle(createWeaponPos, 1.0f);

        // �e�𐶐�
        GameObject createWeapom = weapon._CreateWeapon(createWeaponPos, gunEnemy.transform.eulerAngles);

        rigidbody = createWeapom.GetComponent<Rigidbody>();

        // �e�ɑ��x������
        rigidbody.velocity = new Vector3(FireDirection, 0.0f, 0.0f);



        SoundManager.Instance.Play("Fire");
        // await Fire();
    }

    private async Task Fire()
    {
        await Task.Delay(2990);
    }
}
