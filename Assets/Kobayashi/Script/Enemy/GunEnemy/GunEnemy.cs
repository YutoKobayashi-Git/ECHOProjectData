using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : BaseEnemy
{
    // �s����������l��
    GetPatternAction actions = new GetPatternAction();

    bool AttackChange;

    Vector3 effectPos;

    Animator animator;

    [SerializeField, Header("�����X�^�[�̍s�����~�߂�")]
    private bool MoveStop = false;

    private void Start()
    {

        // �X�^�~�i��ݒ�
        ActionStamina = MaxAcitonStamina;

        effectPos = gameObject.transform.position;
        effectPos.y += 3.0f;

        ReSetHitColorDead();

        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        _diemove();

        // if (MoveStop) return;


        if (rigidbody.velocity.x != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (!NearPlayer()) return;

        ActionStamina += Time.deltaTime;  

        GunEnemyPatternAdd();

        if (!canAttack) return;
        actions.Action();

        Animate(GunCas.Shot);

        canAttack = false;

        
    }

    void GunEnemyPatternAdd()
    {
        // �X�^�~�i������Ȃ��ꍇ
        if (ActionStamina < MaxAcitonStamina) return;

        canAttack = true;

        ActionStamina -= MaxAcitonStamina;

        if(AttackChange)
        {
            actions.AddSkill(new GunEnemyWolk(this));

            Animate(GunCas.Walk);

            AttackChange = false;

            return;
        }

        rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        // �G�t�F�N�g�̍��W
        effectPos = gameObject.transform.position;
        effectPos.y += 3.0f;

        ParticleManager.Instance._sparkParticle(effectPos, 2.0f);

        actions.AddSkill(new GunEnemyFire(this, WeaponBank.TryGetWeapon(WeaponBank.BossWeapon.SentryAmo)));

        AttackChange = true;
    }

    public override void KnockBack()
    {
        // Animate();
    }

    // ���񂾎�
    void _diemove()
    {
        if (status.GetHp() > 0) return;

        ReSetHitColorDead();

        Destroy(this.gameObject);

        ScoreManager.Instance.Add_Score(100);
    }
}
