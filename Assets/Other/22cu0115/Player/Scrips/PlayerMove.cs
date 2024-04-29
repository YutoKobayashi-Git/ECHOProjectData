using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    [Header("移動の速さ"), SerializeField]
    private float speed = 3;

    [Header("ジャンプする瞬間の速さ"), SerializeField]
    private float jumpSpeed = 7;

    [Header("落下速度"), SerializeField]
    private float gravity = 15;

    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float fallSpeedLimit = 10;

    [Header("落下の初速"), SerializeField]
    private float initFallSpeed = 2;


    private Animator animator;
    private Rigidbody rb;
    private Vector2 inputMove;      // 入力値
    private bool isGrounded;        // 接着してるか
    private bool isGroundedPrev;    // 接着の前情報
    private float verticalVelocity; // Y速度、上昇・落下
    private float turnVelocity;
    private float angleY;       // イージング回転角度[deg]

    RaycastHit zimen;   // 衝突したゲームオブジェクト情報
    Vector3 raykeisan; // レイ計算

    Player player = null;

    // スタート時
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // InputActions
    // 移動
    public void OnMove(InputAction.CallbackContext context)
    {
        if (ScoreManager.Instance.specialFireFrag)return;
        if (player.GetPlayerHp() <= 0) return;
        // 入力値を保持しておく
        inputMove = context.ReadValue<Vector2>();

        // ランニングアニメーション
        animator.SetBool("Run", true);
    }
   
    public void OnJump(InputAction.CallbackContext context)
    {
        // チュートリアルをこなすとボタンを押せるように
        if (GameManager.Instance.Jump != true) return;
        // ボタンが押された瞬間かつ着地している時だけ処理
        if (!context.performed) return;
        if (ScoreManager.Instance.specialFireFrag) return;
        if (ScoreManager.Instance.AttackJudgeFrag) return;
        if (player.GetPlayerHp() <= 0) return;

        // 鉛直上向きに速度を与える
        if (isGrounded == true)
        {
            // ジャンプアニメーション
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
        PlayerMovement();       // 移動
        PlayerJumping();        // ジャンプ
    }
   
    private void PlayerMovement()
    {
        // 移動停止した場合
        if (inputMove.x == 0)
        {
            animator.SetBool("Run", false);
        }

        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = new Vector3(
            inputMove.x * speed,
            verticalVelocity,
            0
        );
        if (ScoreManager.Instance.specialFireFrag) inputMove.x = 0; 
        // 現在フレームの移動量を移動速度から計算
        var moveDelta = (moveVelocity * Time.deltaTime) * 100.0f;

        // 移動量代入
        rb.velocity = moveDelta;

        // 回転
        if (inputMove != Vector2.zero)
        {
            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = -Mathf.Atan2(inputMove.y, inputMove.x) * Mathf.Rad2Deg + 90;

            // オブジェクトの回転を更新  
            transform.rotation = Quaternion.Euler(0, targetAngleY, 0);
        }
        
    }
   
    private void PlayerJumping()
    {
        // 地面判定とジャンプ
        raykeisan = base.transform.position;
   
        // モデルに合わせて足元から少し上げる
        raykeisan.y += 0.75f;

        // DrawRay(発生位置,向きと長さ,何秒表示するか)
        Debug.DrawRay(raykeisan, Vector3.up * -0.52f, Color.red, 1f);

        // Physics.SphereCast(発射位置,球の半径,Rayの方向,衝突したゲームオブジェクト情報,Rayの長さ)
        if (Physics.SphereCast(raykeisan, 0.3f, Vector3.down, out zimen, 0.6f))
        {
            //Debug.Log(zimen);
            // 地上
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
        else
        {
            // 空中
            isGrounded = false;
        }

        if (isGrounded && !isGroundedPrev)
        {
            // 着地する瞬間に落下の初速を指定しておく
            verticalVelocity = -initFallSpeed;
            SoundManager.Instance.Play("Chakuchi");
        }
        else if (!isGrounded)
        {
            // 空中にいるときは、下向きに重力加速度を与えて落下させる
            verticalVelocity -= gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
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
