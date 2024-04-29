using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float speed = 3;

    [Header("�W�����v����u�Ԃ̑���"), SerializeField]
    private float jumpSpeed = 7;

    [Header("�������x"), SerializeField]
    private float gravity = 15;

    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float fallSpeedLimit = 10;

    [Header("�����̏���"), SerializeField]
    private float initFallSpeed = 2;


    private Animator animator;
    private Rigidbody rb;
    private Vector2 inputMove;      // ���͒l
    private bool isGrounded;        // �ڒ����Ă邩
    private bool isGroundedPrev;    // �ڒ��̑O���
    private float verticalVelocity; // Y���x�A�㏸�E����
    private float turnVelocity;
    private float angleY;       // �C�[�W���O��]�p�x[deg]

    RaycastHit zimen;   // �Փ˂����Q�[���I�u�W�F�N�g���
    Vector3 raykeisan; // ���C�v�Z

    Player player = null;

    // �X�^�[�g��
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // InputActions
    // �ړ�
    public void OnMove(InputAction.CallbackContext context)
    {
        if (ScoreManager.Instance.specialFireFrag)return;
        if (player.GetPlayerHp() <= 0) return;
        // ���͒l��ێ����Ă���
        inputMove = context.ReadValue<Vector2>();

        // �����j���O�A�j���[�V����
        animator.SetBool("Run", true);
    }
   
    public void OnJump(InputAction.CallbackContext context)
    {
        // �`���[�g���A�������Ȃ��ƃ{�^����������悤��
        if (GameManager.Instance.Jump != true) return;
        // �{�^���������ꂽ�u�Ԃ����n���Ă��鎞��������
        if (!context.performed) return;
        if (ScoreManager.Instance.specialFireFrag) return;
        if (ScoreManager.Instance.AttackJudgeFrag) return;
        if (player.GetPlayerHp() <= 0) return;

        // ����������ɑ��x��^����
        if (isGrounded == true)
        {
            // �W�����v�A�j���[�V����
            animator.SetBool("Jump", true);
            SoundManager.Instance.Play("Jump");
            verticalVelocity = jumpSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack04"))return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Dead"))return;
        if (ScoreManager.Instance.specialFireFrag) return;
        if (ScoreManager.Instance.AttackFrag) return;
        PlayerMovement();       // �ړ�
        PlayerJumping();        // �W�����v
    }
   
    private void PlayerMovement()
    {
        // �ړ���~�����ꍇ
        if (inputMove.x == 0)
        {
            animator.SetBool("Run", false);
        }

        // ������͂Ɖ����������x����A���ݑ��x���v�Z
        var moveVelocity = new Vector3(
            inputMove.x * speed,
            verticalVelocity,
            0
        );
        if (ScoreManager.Instance.specialFireFrag) inputMove.x = 0; 
        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = (moveVelocity * Time.deltaTime) * 100.0f;

        // �ړ��ʑ��
        rb.velocity = moveDelta;

        // ��]
        if (inputMove != Vector2.zero)
        {
            // ������͂���y������̖ڕW�p�x[deg]���v�Z
            var targetAngleY = -Mathf.Atan2(inputMove.y, inputMove.x) * Mathf.Rad2Deg + 90;

            // �I�u�W�F�N�g�̉�]���X�V  
            transform.rotation = Quaternion.Euler(0, targetAngleY, 0);
        }
        
    }
   
    private void PlayerJumping()
    {
        // �n�ʔ���ƃW�����v
        raykeisan = base.transform.position;
   
        // ���f���ɍ��킹�đ������班���グ��
        raykeisan.y += 0.75f;

        // DrawRay(�����ʒu,�����ƒ���,���b�\�����邩)
        Debug.DrawRay(raykeisan, Vector3.up * -0.52f, Color.red, 1f);

        // Physics.SphereCast(���ˈʒu,���̔��a,Ray�̕���,�Փ˂����Q�[���I�u�W�F�N�g���,Ray�̒���)
        if (Physics.SphereCast(raykeisan, 0.3f, Vector3.down, out zimen, 0.6f))
        {
            //Debug.Log(zimen);
            // �n��
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
        else
        {
            // ��
            isGrounded = false;
        }

        if (isGrounded && !isGroundedPrev)
        {
            // ���n����u�Ԃɗ����̏������w�肵�Ă���
            verticalVelocity = -initFallSpeed;
            SoundManager.Instance.Play("Chakuchi");
        }
        else if (!isGrounded)
        {
            // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
            verticalVelocity -= gravity * Time.deltaTime;

            // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
            if (verticalVelocity < -fallSpeedLimit)
                verticalVelocity = -fallSpeedLimit;
        }

        isGroundedPrev = isGrounded;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(raykeisan + Vector3.up * -0.52f, 0.3f);
    }
}
