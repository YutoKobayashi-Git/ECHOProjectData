using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAvoid : MonoBehaviour
{
    /* ----------変数宣言---------- */
    [SerializeField, Header("回避時に生成するエフェクト")]
    GameObject avoidEffect;

    [Header("回避時の移動速度")]
    public float avoidPower;

    [SerializeField, Header("回避時に壁を判定するレイ")]
    Ray ray;

    [SerializeField, Header("レイの長さ")]
    float rayLength;

    [SerializeField, Header("レイの長さ")]
    RaycastHit hitInformation;

    [Header("移動速度の制御")]
    private float avoidPowerControl = 1f;

    [SerializeField, Header("オフセット")]
    Vector3 offset = new Vector3(0f, 1.5f, 0f);

    [SerializeField, Header("回避時の移動方向")]
    Vector3 avoidDirection = new Vector3(1f, 0f, 0f);

    [SerializeField, Header("回避の継続時間")]
    float avoidTime;

    [SerializeField, Header("回避のリロード時間")]
    float avoidReloadTime;

    [SerializeField, Header("回避のリロードフラグ")]
    bool avoidReloadflag;

    private float timeElapsed;
    private float Relordtime;
    private Animator animator;
    private Rigidbody rb;
    private Collider playerCollider;

    // 統合したときにstatic消す
    public static bool currentRolling;

    Player player = null;

    private void Awake()
    {
        // パーティクル生成
        avoidEffect = Instantiate(avoidEffect, transform.position, Quaternion.identity);
        avoidEffect.SetActive(false);

        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    // 回避
    public void OnAvoid(InputAction.CallbackContext context)
    {
        // チュートリアルをこなすとボタンを押せるように
        if (GameManager.Instance.Avoid != true) return;
        // 押された瞬間だけ
        if (!context.performed) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return;
        if (ScoreManager.Instance.specialFireFrag) return;
        if (!avoidReloadflag) return;
        if (player.GetPlayerHp() <= 0) return;

        animator.SetTrigger("Avoid");
        // 無敵レイヤーに移動
        this.gameObject.layer = 6;
        avoidReloadflag = false;
        currentRolling = true;

        avoidPowerControl = 1f;
        playerCollider.enabled = true;
        avoidEffect.SetActive(true);
        // sound
        SoundManager.Instance.Play("Kaihi");
        // 回避アニメーション
    }

    private void RollingEnter()
    {
        //currentRolling = true;
    }
    private void RollingExit()
    {
        //currentRolling = false;
    }

    private void Update()
    {
        if(!avoidReloadflag)
        {
            Relordtime += Time.deltaTime;
            if(avoidReloadTime <= Relordtime)
            {
                avoidReloadflag = true;
            }
        }
        else
        {
            Relordtime = 0;
        }
        if (currentRolling)
        {
            timeElapsed += Time.deltaTime;

            //  一定時間まで回避を行う
            if (avoidTime >= timeElapsed)
            {
                avoidEffect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                avoidDirection.x = transform.forward.x;
                ray = new Ray(this.gameObject.transform.position + offset, avoidDirection);

                Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 2.0f);
                if (Physics.Raycast(ray, out hitInformation, rayLength))
                {
                    if (hitInformation.collider.gameObject.layer == LayerMask.NameToLayer("Stage"))
                    {
                        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue, 2.0f);
                        Debug.Log("回避停止:対象:" + hitInformation.collider.gameObject.name);
                        avoidPowerControl = 0f;
                    }
                    if (hitInformation.collider.gameObject.tag == "Enemy")
                    {
                        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow, 2.0f);
                        Debug.Log("回避停止:対象:" + hitInformation.collider.gameObject.name);
                        avoidPowerControl = 0f;
                    }
                    playerCollider.enabled = true;

                }

                // 実際の移動
                rb.AddForce(transform.forward.x * avoidPower * avoidPowerControl, 0f,0f, ForceMode.VelocityChange);
                //Debug.Log(transform.forward);
            }
            else
            {
                // 指定した時間を超えたら初期化
                avoidEffect.SetActive(false);
                playerCollider.enabled = true;
                currentRolling = false;
                timeElapsed = 0f;
                // 通常レイヤーに移動
                this.gameObject.layer = 0;
            }
        }
        
    }
}
