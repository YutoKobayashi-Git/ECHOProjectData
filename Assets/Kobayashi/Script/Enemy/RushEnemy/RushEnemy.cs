using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy : BaseEnemy
{
    [SerializeField, Header("移動スピード")]
    float Speed = 0;

    // 行動する情報を獲得
    private GetPatternAction actions = new GetPatternAction();

    private readonly float KnockBackPosition = 0.3f;

    private Vector3 effectPos;

    private int AttackCount;

    private readonly string SoundName = "ReadyBike";


    Animator animator;

    private void Start()
    {
        // HPを設定
        status.GetHp();
        // スタミナを設定
        ActionStamina = MaxAcitonStamina;

        ReSetHitColorDead();

        effectPos = gameObject.transform.position;
        effectPos.y += 3.0f;

        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        // プレイヤーが近くにいる場合のみ動く
        // if (!NearPlayer()) return;


        if (rigidbody.velocity.x != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        _diemove();

        AddStamina();

        RushEnemyPatternAdd();

        if (!canAttack) return;

        actions.Action();



        // Animate(RushCas.Run);


        canAttack = false;

    }

    void RushEnemyPatternAdd()
    {
        if (ActionStamina < MaxAcitonStamina) return;

        ++AttackCount;

        // スタミナを消費
        ActionStamina -= MaxAcitonStamina;

        rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (AttackCount >= 2)
        {
            AttackCount = 0;

            return;
        }

        // 行動
        canAttack = true;

        Animate(RushCas.Ready);

        actions.AddSkill(new RushEnemyWolk(this,Speed));

        // エフェクトの座標
        effectPos = gameObject.transform.position;
        effectPos.y += 5.0f;

        ParticleManager.Instance._sparkParticle(effectPos, 3.0f);

        if (!NearPlayer()) return;

        SoundManager.Instance.Play(SoundName);

        return;

    }

    // 死んだ時
    void _diemove()
    {
        if (status.GetHp() > 0) return;

        ReSetHitColorDead();

        ScoreManager.Instance.Add_Score(100);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box"))
        {
            rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            Vector3 pos = gameObject.transform.position;

            if (other.transform.position.x > gameObject.transform.position.x)
            {
                pos.x -= KnockBackPosition;
            }
            else
            {
                pos.x += KnockBackPosition;
            }

            gameObject.transform.position = pos;

        }

    }

    public override void KnockBack()
    {

        Animate(RushCas.Damage);

        // rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        // 
        // Vector3 KnockBackPower = new Vector3(0.0f, 0.0f, 0.0f);

        // if (PlayerTransform.transform.position.x > this.gameObject.transform.position.x)
        // {
        //     KnockBackPower.x = -KnockBackPower.x;
        //     rigidbody.AddForce(KnockBackPower);
        // }
        // else
        // {
        //     rigidbody.AddForce(KnockBackPower);
        // }
        // 
        // Debug.Log("aaaa");

    }

}
