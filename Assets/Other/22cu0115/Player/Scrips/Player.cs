using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    //[Header("移動の速さ"), SerializeField]
    //private float moveSpeed = 5.0f;
    //
    //[Header("ジャンプの強さ"), SerializeField]
    //private float jumpPower = 1.0f;
    //
    //[Header("落下速度"), SerializeField]
    //private float gravity = 15;
    //
    //[Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    //private float fallSpeedLimit = 10;
    //
    //[Header("落下の初速"), SerializeField]
    //private float initFallSpeed = 2;
    //
    //// レイヤーマスク
    //[SerializeField]
    //private LayerMask groundLayers = 0;
    //RaycastHit hit;       // 衝突したゲームオブジェクト情報
    //Vector3 raykeisan;      // レイ計算
    //
    //private Vector2 inputMove;      // 移動量
    //private bool isGrounded;        // 接着してるか
    //private bool isGroundedPrev;    // 接着の前情報
    //private float verticalVelocity; // Y速度、上昇・落下
    //private float targetAngleY; // 角度
    //private float angleY = 90.0f;       // イージング回転角度[deg]
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
        // マネージャーが情報を管理したい
        // プレイヤーは死んだ判定だけ
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

    // ダメージ判定
    public override void ApllayDamage(Damage damage, HittingObject.WeaponOwner owner)
    {
        if (owner == HittingObject.WeaponOwner.Player) return;
        if (this.gameObject.layer == 6) return;
        Animate(Player.Damage);
        HpDown(damage._damagereceived());
    }

    // 現在のHPをリターンする
    public int GetPlayerHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// 死亡フラグ
    /// </summary>
    public void PlayerIsDead()
    {
        Animate(Player.Dead);
        // 無敵レイヤーに移動
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
    ///// 移動量
    ///// </summary>
    //private void MoveAmount()
    //{
    //    // 入力値受け取り
    //    inputMove = InputManager.GetMoveValue();
    //
    //    // 操作入力と鉛直方向速度から、現在速度を計算
    //    var moveVelocity = new Vector3(
    //        inputMove.x * moveSpeed,
    //        verticalVelocity,
    //        0
    //    );
    //
    //    //if (ScoreManager.Instance.specialFireFrag) inputMove.x = 0;
    //
    //    // 現在フレームの移動量を移動速度から計算
    //    var moveDelta = (moveVelocity * Time.deltaTime) * 100.0f;
    //
    //    // 移動量代入
    //    rb.velocity = moveDelta;
    //
    //    // 回転
    //    if (inputMove != Vector2.zero)
    //    {
    //        // 操作入力からy軸周りの目標角度[deg]を計算
    //        targetAngleY = -Mathf.Atan2(0, inputMove.x) * Mathf.Rad2Deg + 90;
    //        //Debug.Log(targetAngleY);
    //        //// イージングしながら次の回転角度[deg]を計算
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
    //        // オブジェクトの回転を更新  
    //        transform.rotation = Quaternion.Euler(0, targetAngleY, 0);
    //    }
    //}
    //
    ///// <summary>
    ///// 接地判定
    ///// </summary>
    //private void CheckGroundStatus()
    //{
    //    // 地面判定とジャンプ
    //    raykeisan = base.transform.position;
    //
    //    // モデルに合わせて足元から少し上げる
    //    raykeisan.y += 0.75f;
    //
    //    // DrawRay(発生位置,向きと長さ,何秒表示するか)
    //    Debug.DrawRay(raykeisan, Vector3.up * -0.52f, Color.red, 1f);
    //
    //    // Physics.SphereCast(発射位置,球の半径,Rayの方向,衝突したゲームオブジェクト情報,Rayの長さ)
    //    if (Physics.SphereCast(raykeisan, 0.3f, Vector3.down, out hit, 0.6f))
    //    {
    //        //Debug.Log(zimen);
    //        // 地上
    //        isGrounded = true;
    //        //animator.SetBool("Jump", false);
    //    }
    //    else
    //    {
    //        // 空中
    //        isGrounded = false;
    //    }
    //
    //    if (isGrounded && !isGroundedPrev)
    //    {
    //        // 着地する瞬間に落下の初速を指定しておく
    //        verticalVelocity = -initFallSpeed;
    //        //SoundManager.Instance.Play("Chakuchi");
    //    }
    //    else if (!isGrounded)
    //    {
    //        // 空中にいるときは、下向きに重力加速度を与えて落下させる
    //        verticalVelocity -= gravity * Time.deltaTime;
    //
    //        // 落下する速さ以上にならないように補正
    //        if (verticalVelocity < -fallSpeedLimit)
    //            verticalVelocity = -fallSpeedLimit;
    //    }
    //
    //    isGroundedPrev = isGrounded;
    //}



    //// 移動
    //private void Move(Vector2 value)
    //{
    //    //Vector3 vector = new Vector3(value.x,0,value.y);
    //    //rb.velocity = vector;
    //    inputMove = value;
    //}
    //
    //// ジャンプ
    //private void Jump(float jumpPower)
    //{
    //    //Debug.Log("jump");
    //    // 鉛直上向きに速度を与える
    //    if (isGrounded == true)
    //    {
    //        //SoundManager.Instance.Play("Jump");
    //        verticalVelocity = jumpPower;
    //    }
    //}
    //// SphereRay表示
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(raykeisan + Vector3.up * -0.52f, 0.3f);
    //}
}
