using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    //[Header("�ړ��̑���"), SerializeField]
    //private float moveSpeed = 5.0f;
    //
    //[Header("�W�����v�̋���"), SerializeField]
    //private float jumpPower = 1.0f;
    //
    //[Header("�������x"), SerializeField]
    //private float gravity = 15;
    //
    //[Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    //private float fallSpeedLimit = 10;
    //
    //[Header("�����̏���"), SerializeField]
    //private float initFallSpeed = 2;
    //
    //// ���C���[�}�X�N
    //[SerializeField]
    //private LayerMask groundLayers = 0;
    //RaycastHit hit;       // �Փ˂����Q�[���I�u�W�F�N�g���
    //Vector3 raykeisan;      // ���C�v�Z
    //
    //private Vector2 inputMove;      // �ړ���
    //private bool isGrounded;        // �ڒ����Ă邩
    //private bool isGroundedPrev;    // �ڒ��̑O���
    //private float verticalVelocity; // Y���x�A�㏸�E����
    //private float targetAngleY; // �p�x
    //private float angleY = 90.0f;       // �C�[�W���O��]�p�x[deg]
    //
    //public float turnVelocity;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �}�l�[�W���[�������Ǘ�������
        // �v���C���[�͎��񂾔��肾��
        //if (status.GetHp() <= 0 && !IsOnce) 
        //{
        //    IsOnce = true;
        //    PlayerIsDead();
        //}
        //KeyInput();
        //MoveAmount();
        //CheckGroundStatus();
        if(GetPlayerHp() <= 0)
        {
            PlayerIsDead();
        }
    }

    // �_���[�W����
    public override void ApllayDamage(Damage damage, HittingObject.WeaponOwner owner)
    {
        if (owner == HittingObject.WeaponOwner.Player) return;
        if (this.gameObject.layer == 6) return;
        Animate(Player.Damage);
        HpDown(damage._damagereceived());
    }

    // ���݂�HP�����^�[������
    public int GetPlayerHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// ���S�t���O
    /// </summary>
    public void PlayerIsDead()
    {
        Animate(Player.Dead);
        // ���G���C���[�Ɉړ�
        this.gameObject.layer = 6;
    }

    //public void KeyInput()
    //{
    //    // Keybord
    //    if (InputManager.GetKey(Key.KeybordMoveUp))
    //    {
    //        Move(new Vector2(0, moveSpeed));
    //    }
    //    if (InputManager.GetKey(Key.KeybordMoveDown))
    //    {
    //        Move(new Vector2(0, -moveSpeed));
    //    }
    //    if (InputManager.GetKey(Key.KeybordMoveLeft))
    //    {
    //        Move(new Vector2(-moveSpeed, 0));
    //    }
    //    if (InputManager.GetKey(Key.KeybordMoveRight))
    //    {
    //        Move(new Vector2(moveSpeed, 0));
    //    }
    //    if (InputManager.GetKey(Key.KeybordJump))
    //    {
    //        Jump(jumpPower);
    //    }
    //    // Gamepad
    //    if (InputManager.GetKey(Key.GamepadMoveLeft))
    //    {
    //        //Move(InputManager.GetMoveValue());
    //    }
    //    if (InputManager.GetKey(Key.GamepadMoveRight))
    //    {
    //        //Move(InputManager.GetMoveValue());
    //    }
    //    if (InputManager.GetKey(Key.GamepadJump))
    //    {
    //        Jump(jumpPower);
    //    }
    //}
    //
    ///// <summary>
    ///// �ړ���
    ///// </summary>
    //private void MoveAmount()
    //{
    //    // ���͒l�󂯎��
    //    inputMove = InputManager.GetMoveValue();
    //
    //    // ������͂Ɖ����������x����A���ݑ��x���v�Z
    //    var moveVelocity = new Vector3(
    //        inputMove.x * moveSpeed,
    //        verticalVelocity,
    //        0
    //    );
    //
    //    //if (ScoreManager.Instance.specialFireFrag) inputMove.x = 0;
    //
    //    // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
    //    var moveDelta = (moveVelocity * Time.deltaTime) * 100.0f;
    //
    //    // �ړ��ʑ��
    //    rb.velocity = moveDelta;
    //
    //    // ��]
    //    if (inputMove != Vector2.zero)
    //    {
    //        // ������͂���y������̖ڕW�p�x[deg]���v�Z
    //        targetAngleY = -Mathf.Atan2(0, inputMove.x) * Mathf.Rad2Deg + 90;
    //        //Debug.Log(targetAngleY);
    //        //// �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
    //        //if (targetAngleY >= 90.0f) 
    //        //{
    //        //    while(angleY <= 90.0f)
    //        //    {
    //        //        angleY += Time.deltaTime * turnVelocity;
    //        //    }
    //        //}
    //        //if (targetAngleY <= -90)
    //        //{
    //        //    while (angleY >= -90.0f)
    //        //    {
    //        //        angleY += Time.deltaTime * -turnVelocity;
    //        //    }
    //        //}
    //        // �I�u�W�F�N�g�̉�]���X�V  
    //        transform.rotation = Quaternion.Euler(0, targetAngleY, 0);
    //    }
    //}
    //
    ///// <summary>
    ///// �ڒn����
    ///// </summary>
    //private void CheckGroundStatus()
    //{
    //    // �n�ʔ���ƃW�����v
    //    raykeisan = base.transform.position;
    //
    //    // ���f���ɍ��킹�đ������班���グ��
    //    raykeisan.y += 0.75f;
    //
    //    // DrawRay(�����ʒu,�����ƒ���,���b�\�����邩)
    //    Debug.DrawRay(raykeisan, Vector3.up * -0.52f, Color.red, 1f);
    //
    //    // Physics.SphereCast(���ˈʒu,���̔��a,Ray�̕���,�Փ˂����Q�[���I�u�W�F�N�g���,Ray�̒���)
    //    if (Physics.SphereCast(raykeisan, 0.3f, Vector3.down, out hit, 0.6f))
    //    {
    //        //Debug.Log(zimen);
    //        // �n��
    //        isGrounded = true;
    //        //animator.SetBool("Jump", false);
    //    }
    //    else
    //    {
    //        // ��
    //        isGrounded = false;
    //    }
    //
    //    if (isGrounded && !isGroundedPrev)
    //    {
    //        // ���n����u�Ԃɗ����̏������w�肵�Ă���
    //        verticalVelocity = -initFallSpeed;
    //        //SoundManager.Instance.Play("Chakuchi");
    //    }
    //    else if (!isGrounded)
    //    {
    //        // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
    //        verticalVelocity -= gravity * Time.deltaTime;
    //
    //        // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
    //        if (verticalVelocity < -fallSpeedLimit)
    //            verticalVelocity = -fallSpeedLimit;
    //    }
    //
    //    isGroundedPrev = isGrounded;
    //}



    //// �ړ�
    //private void Move(Vector2 value)
    //{
    //    //Vector3 vector = new Vector3(value.x,0,value.y);
    //    //rb.velocity = vector;
    //    inputMove = value;
    //}
    //
    //// �W�����v
    //private void Jump(float jumpPower)
    //{
    //    //Debug.Log("jump");
    //    // ����������ɑ��x��^����
    //    if (isGrounded == true)
    //    {
    //        //SoundManager.Instance.Play("Jump");
    //        verticalVelocity = jumpPower;
    //    }
    //}
    //// SphereRay�\��
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(raykeisan + Vector3.up * -0.52f, 0.3f);
    //}
}
