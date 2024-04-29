using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseClass : MonoBehaviour , WeaponDamege
{
    protected int WeaponNumber;

    protected int WeaponDamage = 25;

    // �����Owner��ۑ�����ϐ�
    protected HittingObject.WeaponOwner owner;

    protected static readonly Vector3 SpeedReSet = new Vector3(0.0f, 0.0f, 0.0f);

    public void OnTriggerEnter(Collider other)
    {
        // �C���^�[�t�F�[�X���擾
        HittingObject hits = other.GetComponent<HittingObject>();

        if (hits == null) return;

        // ����̏��L�҂����肷��
        if (gameObject.CompareTag("Enemy")) owner = HittingObject.WeaponOwner.Enemy;
        if (gameObject.CompareTag("EnemyWeapon")) owner = HittingObject.WeaponOwner.Enemy;
        if (gameObject.CompareTag("Player")) owner = HittingObject.WeaponOwner.Player;

        // �_���[�W�𐶐�
        Damage damage = new Damage();

        damage.AddDamageRate(WeaponDamage);

        //�_���[�W����
        hits.ApllayDamage(damage, owner);
    }

    /// <summary>
    /// ����ɔԍ��w�肷��ꍇ�ɌĂяo��
    /// </summary>
    /// <param name="Num"></param>
    public void WeaponNumberSet(int Num)
    {
        WeaponNumber = Num;
    }

    /// <summary>
    /// ����̐���
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="euler"></param>
    /// <returns></returns>
    public GameObject _CreateWeapon(Vector3 pos, Vector3 euler)
    {
        return Instantiate(this.gameObject, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(euler.x, euler.y, euler.z));
    }

    /// <summary>
    /// ����𓮂���
    /// </summary>
    /// <param name="Speed"></param>
    public virtual void _MoveWeapon(float Speed) { }
}

public interface WeaponDamege
{
    public virtual int WeaponDamageSet(int WeaponDamage) { return 0; }
}







