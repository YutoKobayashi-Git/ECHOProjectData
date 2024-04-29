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
//    [SerializeField, Header("������ɐ�������G�t�F�N�g")]
//    GameObject effect;
//
//    [SerializeField, Header("������̈ړ�����")]
//    Vector3 avoidDirection = new Vector3(1f, 0f, 0f);
//
//    [SerializeField, Header("�I�t�Z�b�g")]
//    Vector3 offset = new Vector3(0f,1.5f,0f);
//
//    [SerializeField, Header("������ɕǂ𔻒肷�郌�C")]
//    Ray ray;
//
//    [SerializeField, Header("���C�̒���")]
//    float rayLength;
//
//    [SerializeField, Header("���C�̒���")]
//    RaycastHit hitInformation
//;
//
//    private Animator anim;
//    private Rigidbody rb;
//    private Collider playerCollider;
//
//    [Header("������̈ړ����x")]
//    public float AvoidPower;
//    [Header("�ړ����x�̐���")]
//    float AvoidPowercontrol = 1f;
//
//
//
//
//
//    private void Awake()
//    {
//        // �p�[�e�B�N������
//        effect = Instantiate(effect, transform.position, Quaternion.identity);
//        effect.SetActive(false);
//
//        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
//
//        anim = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody>();
//    }
//
//    // �E���
//    public void OnAvoidR(InputAction.CallbackContext context)
//    {
//        // �����ꂽ�u�Ԃ���
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
//    // �����
//    public void OnAvoidL(InputAction.CallbackContext context)
//    {
//        // �����ꂽ�u�Ԃ���
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
//                            Debug.Log("����~:�Ώ�:" + hitInformation.collider.gameObject.name);
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
//        // ����A�j���[�V����
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
