using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAvoid : MonoBehaviour
{
    /* ----------�ϐ��錾---------- */
    [SerializeField, Header("������ɐ�������G�t�F�N�g")]
    GameObject avoidEffect;

    [Header("������̈ړ����x")]
    public float avoidPower;

    [SerializeField, Header("������ɕǂ𔻒肷�郌�C")]
    Ray ray;

    [SerializeField, Header("���C�̒���")]
    float rayLength;

    [SerializeField, Header("���C�̒���")]
    RaycastHit hitInformation;

    [Header("�ړ����x�̐���")]
    private float avoidPowerControl = 1f;

    [SerializeField, Header("�I�t�Z�b�g")]
    Vector3 offset = new Vector3(0f, 1.5f, 0f);

    [SerializeField, Header("������̈ړ�����")]
    Vector3 avoidDirection = new Vector3(1f, 0f, 0f);

    [SerializeField, Header("����̌p������")]
    float avoidTime;

    [SerializeField, Header("����̃����[�h����")]
    float avoidReloadTime;

    [SerializeField, Header("����̃����[�h�t���O")]
    bool avoidReloadflag;

    private float timeElapsed;
    private float Relordtime;
    private Animator animator;
    private Rigidbody rb;
    private Collider playerCollider;

    // ���������Ƃ���static����
    public static bool currentRolling;

    Player player = null;

    private void Awake()
    {
        // �p�[�e�B�N������
        avoidEffect = Instantiate(avoidEffect, transform.position, Quaternion.identity);
        avoidEffect.SetActive(false);

        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    // ���
    public void OnAvoid(InputAction.CallbackContext context)
    {
        // �`���[�g���A�������Ȃ��ƃ{�^����������悤��
        if (GameManager.Instance.Avoid != true) return;
        // �����ꂽ�u�Ԃ���
        if (!context.performed) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return;
        if (ScoreManager.Instance.specialFireFrag) return;
        if (!avoidReloadflag) return;
        if (player.GetPlayerHp() <= 0) return;

        animator.SetTrigger("Avoid");
        // ���G���C���[�Ɉړ�
        this.gameObject.layer = 6;
        avoidReloadflag = false;
        currentRolling = true;

        avoidPowerControl = 1f;
        playerCollider.enabled = true;
        avoidEffect.SetActive(true);
        // sound
        SoundManager.Instance.Play("Kaihi");
        // ����A�j���[�V����
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

            //  ��莞�Ԃ܂ŉ�����s��
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
                        Debug.Log("����~:�Ώ�:" + hitInformation.collider.gameObject.name);
                        avoidPowerControl = 0f;
                    }
                    if (hitInformation.collider.gameObject.tag == "Enemy")
                    {
                        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow, 2.0f);
                        Debug.Log("����~:�Ώ�:" + hitInformation.collider.gameObject.name);
                        avoidPowerControl = 0f;
                    }
                    playerCollider.enabled = true;

                }

                // ���ۂ̈ړ�
                rb.AddForce(transform.forward.x * avoidPower * avoidPowerControl, 0f,0f, ForceMode.VelocityChange);
                //Debug.Log(transform.forward);
            }
            else
            {
                // �w�肵�����Ԃ𒴂����珉����
                avoidEffect.SetActive(false);
                playerCollider.enabled = true;
                currentRolling = false;
                timeElapsed = 0f;
                // �ʏ탌�C���[�Ɉړ�
                this.gameObject.layer = 0;
            }
        }
        
    }
}
