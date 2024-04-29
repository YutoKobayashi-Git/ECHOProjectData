using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy : BaseEnemy
{
    [SerializeField, Header("�ړ��X�s�[�h")]
    float Speed = 0;

    // �s����������l��
    private GetPatternAction actions = new GetPatternAction();

    private readonly float KnockBackPosition = 0.3f;

    private Vector3 effectPos;

    private int AttackCount;

    private readonly string SoundName = "ReadyBike";


    Animator animator;

    private void Start()
    {
        // HP��ݒ�
        status.GetHp();
        // �X�^�~�i��ݒ�
        ActionStamina = MaxAcitonStamina;

        ReSetHitColorDead();

        effectPos = gameObject.transform.position;
        effectPos.y += 3.0f;

        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        // �v���C���[���߂��ɂ���ꍇ�̂ݓ���
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

        // �X�^�~�i������
        ActionStamina -= MaxAcitonStamina;

        rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if (AttackCount >= 2)
        {
            AttackCount = 0;

            return;
        }

        // �s��
        canAttack = true;

        Animate(RushCas.Ready);

        actions.AddSkill(new RushEnemyWolk(this,Speed));

        // �G�t�F�N�g�̍��W
        effectPos = gameObject.transform.position;
        effectPos.y += 5.0f;

        ParticleManager.Instance._sparkParticle(effectPos, 3.0f);

        if (!NearPlayer()) return;

        SoundManager.Instance.Play(SoundName);

        return;

    }

    // ���񂾎�
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
