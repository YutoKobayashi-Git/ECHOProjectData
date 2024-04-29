using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : Character
{
    [SerializeField, Header("�X�^�~�i�̍ő�l")]�@
    public float MaxAcitonStamina;

    [SerializeField, Header("���g��Rigidbody")] 
    public Rigidbody rigidbody;

    [SerializeField, Header("�v���C���[�̃g�����X�t�H�[��")] 
    public Transform PlayerTransform;

    [SerializeField, Header("�L�����̃}�e���A��")]
    Material[] EnemyMaterial;

    [SerializeField, Header("�_���[�W���󂯂����̃q�b�g�X�g�b�v")]
    private float HitStopTime = 0.001f;

    [SerializeField, Header("�q�b�g�����ۂɐԂ����Ă�������")]
    private float HitChangeColorTime = 0.1f;

    protected float CanActionMinimumStamina = 10;   // �s���ł���Œ჉�C���̃X�^�~�i
    protected float ActionStamina;                  // �ω�����X�^�~�i
    protected float StaminaUsed;                    // �g�����X�^�~�i
    protected bool canAttack;                       // �U���ł��邩

    private static readonly string ColorName = "_Color";

    private void Start()
    {
        CanActionMinimumStamina = MaxAcitonStamina;

        for (int i = 0; i < EnemyMaterial.Length; ++i)
        {
            EnemyMaterial[i].SetColor(ColorName, Color.white);
        }
    }

    /// <summary>
    /// �U�����e���i�[�A�Ăяo������
    /// </summary>
    public class GetPatternAction : MonoBehaviour
    {
        // �U���p�^�[�����i�[���Ă����ϐ�
        private Queue<CharacterSkill> skills = new Queue<CharacterSkill>();

        /// <summary>
        /// �A�N�V������ǉ�
        /// </summary>
        /// <param name="skill"></param>
        public void AddSkill(CharacterSkill skill)
        {
            skills.Enqueue(skill);
        }

        /// <summary>
        /// �A�N�V�������s��
        /// </summary>
        public void Action()
        {
            skills.Dequeue().Excute();
        }

    }

    /// <summary>
    /// �U��������������̏���
    /// </summary>
     public override void ApllayDamage(Damage damage, HittingObject.WeaponOwner OwnerName)
     {
         // �����̕����e��
         if (OwnerName == HittingObject.WeaponOwner.Enemy) return;

        HitEffectRed();

        HitStopManager.Instance.StartHitStop(HitStopTime);

        HpDown(damage._damagereceived());

        KnockBack();
     }

    public virtual void KnockBack()
    {

    }

    /// <summary>
    /// �}�e���A����ԐF��ς���
    /// </summary>
    private void HitEffectRed()
    {
        for (int i = 0; i < EnemyMaterial.Length; ++i)
        {
            EnemyMaterial[i].SetColor(ColorName, Color.red);
        }

        StartCoroutine(ReSetHitColor());
    }

    /// <summary>
    /// �}�e���A���𔒐F��ς���
    /// </summary>
    private void HitEffectWhite()
    {
        for (int i = 0; i < EnemyMaterial.Length; ++i)
        {
            EnemyMaterial[i].SetColor(ColorName, Color.white);
        }
    }

    /// <summary>
    /// �q�b�g��Ƀ}�e���A�������̐F�ɖ߂�
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReSetHitColor()
    {
        yield return new WaitForSeconds(HitChangeColorTime);
        HitEffectWhite();
    }

    /// <summary>
    /// ���񂾂Ƃ��Ƀ}�e���A�������̐F�ɖ߂�
    /// </summary>
    protected void ReSetHitColorDead()
    {
        HitEffectWhite();
    }

    /// <summary>
    /// �X�^�~�i�̒ǉ�
    /// </summary>
    public void AddStamina()
    {
        ActionStamina += Time.deltaTime;
    }

    /// <summary>
    /// �G��HP��Ԃ�
    /// </summary>
    /// <returns></returns>
    public int GetBossHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// �߂��Ƀv���C���[�����邩
    /// </summary>
    protected bool NearPlayer()
    {
        float PlayerX = PlayerTransform.position.x;
        float Enemy = this.gameObject.transform.position.x;

        if (Mathf.Abs(PlayerX - Enemy) > 30)
        {
            return false;
        }

        return true;
    }
}
