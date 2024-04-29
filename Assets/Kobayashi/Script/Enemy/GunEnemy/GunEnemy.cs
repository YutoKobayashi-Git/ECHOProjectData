using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : BaseEnemy
{
    // 行動する情報を獲得
    GetPatternAction actions = new GetPatternAction();

    bool AttackChange;

    Vector3 effectPos;

    Animator animator;

    [SerializeField, Header("モンスターの行動を止める")]
    private bool MoveStop = false;

    private void Start()
    {

        // スタミナを設定
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
        // スタミナが足りない場合
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

        // エフェクトの座標
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

    // 死んだ時
    void _diemove()
    {
        if (status.GetHp() > 0) return;

        ReSetHitColorDead();

        Destroy(this.gameObject);

        ScoreManager.Instance.Add_Score(100);
    }
}
