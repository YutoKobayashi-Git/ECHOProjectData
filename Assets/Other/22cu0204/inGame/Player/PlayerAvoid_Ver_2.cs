using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//public class PlayerAvoid_Ver_2 : MonoBehaviour
//{
//    private enum Direction
//    {
//        right =0,
//        reft
//    };
//
//    [SerializeField, Header("回避時に生成するエフェクト")]
//    GameObject effect;
//
//    [SerializeField, Header("回避時の移動方向")]
//    Vector3 avoidDirection = new Vector3(1f, 0f, 0f);
//
//    [SerializeField, Header("オフセット")]
//    Vector3 offset = new Vector3(0f,1.5f,0f);
//
//    [SerializeField, Header("回避時に壁を判定するレイ")]
//    Ray ray;
//
//    [SerializeField, Header("レイの長さ")]
//    float rayLength;
//
//    [SerializeField, Header("レイの長さ")]
//    RaycastHit hitInformation
//;
//
//    private Animator anim;
//    private Rigidbody rb;
//    private Collider playerCollider;
//
//    [Header("回避時の移動速度")]
//    public float AvoidPower;
//    [Header("移動速度の制御")]
//    float AvoidPowercontrol = 1f;
//
//
//
//
//
//    private void Awake()
//    {
//        // パーティクル生成
//        effect = Instantiate(effect, transform.position, Quaternion.identity);
//        effect.SetActive(false);
//
//        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
//
//        anim = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody>();
//    }
//
//    // 右回避
//    public void OnAvoidR(InputAction.CallbackContext context)
//    {
//        // 押された瞬間だけ
//        if (!context.performed) return;
//        switch (GameManager.Instance.currentState.GetType().Name)
//        {
//            case "Idle":
//            case "Move":
//                keyName(context.control.name);
//                effect.SetActive(true);
//                _player_Avoid(Direction.right);
//                break;
//        }
//    }
//    // 左回避
//    public void OnAvoidL(InputAction.CallbackContext context)
//    {
//        // 押された瞬間だけ
//        if (!context.performed) return;
//        switch (GameManager.Instance.currentState.GetType().Name)
//        {
//            case "Idle":
//            case "Move":
//                keyName(context.control.name);
//                AvoidPowercontrol = 1f;
//                effect.SetActive(true);
//                playerCollider.enabled = false;
//                _player_Avoid(Direction.reft);
//                break;
//        }
//    }
//
//    private void Update()
//    {
//        effect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
//
//        if(GameManager.Instance.currentState != null)
//        {
//            switch (GameManager.Instance.currentState.GetType().Name)
//            {
//                case "Idle":
//                case "Move":
//                    effect.SetActive(false);
//                    playerCollider.enabled = true;
//                    break;
//                case "Avoid":
//                    avoidDirection.x = transform.forward.x;
//                    ray = new Ray(this.gameObject.transform.position + offset, avoidDirection);
//
//                    Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 2.0f);
//                    if (Physics.Raycast(ray,out hitInformation, rayLength))
//                    {
//                        if(hitInformation.collider.gameObject.layer == LayerMask.NameToLayer("Stage"))
//                        {
//                            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue, 2.0f);
//                            Debug.Log("回避停止:対象:" + hitInformation.collider.gameObject.name);
//                            AvoidPowercontrol = 0f;
//                        }
//                    }
//                    else
//                    {
//                        AvoidPowercontrol = 1f;
//                        effect.SetActive(true);
//                        playerCollider.enabled = false;
//                    }
//
//                    rb.AddForce(transform.forward * AvoidPower * AvoidPowercontrol, ForceMode.VelocityChange);
//                    break;
//            }
//        }
//        
//    }
//    private void _player_Avoid(Direction _avoidDirection)
//    {
//        // 回避アニメーション
//        switch(_avoidDirection)
//        {
//            case Direction.right:
//            {
//                    anim.SetTrigger("AvoidR_trg");
//                    break;
//            }
//            case Direction.reft:
//            {
//                    anim.SetTrigger("AvoidL_trg");
//                    break;
//            }
//        }
//    }
//}
