using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//public class PlayerFire : MonoBehaviour
//{
//    public enum ATTACK_PHASE
//    {
//        first = 0,
//        second,
//        third,
//        fourth,
//    }
//
//    public enum ACTIVE_JUDGE
//    {
//        normal = 0,
//        EX,
//    }
//    [SerializeField, Header("�U���G�t�F�N�g�p�I�u�W�F�N�g")] GameObject[] playerAttackEffect = new GameObject[2];
//    [SerializeField, Header("UI�̈ʒu�擾�p")] GameObject targetObj;
//    [SerializeField, Header("���݂̍U���i�K��ۑ�����ϐ�")] ATTACK_PHASE previousJudgment;
//    [SerializeField, Header("�U���\���ǂ����𔻒肷��I�u�W�F�N�g�̂ǂ���̃��[�h���N������")] ACTIVE_JUDGE bootJudgment;
//    [SerializeField, Header("���ݍU�������ǂ����𔻒肷��ϐ�")] bool attackflag;
//    [SerializeField, Header("�����ɖ߂鎞�Ԃ�ۑ�����ϐ�")] float comboEndTime;
//    [SerializeField, Header("���Ԍv���p")] float timeCount;
//    [Header("���͒�~����")]public float inputStopTime;
//
//    private Animator Attackanim;
//    private PlayerAttackCollider attackCollider;
//    private UIController_ComboValue comboValueDisplay;
//    [SerializeField, Header("�R���{�̃X�N���v�g")] ComboJudge comboJudge;
//    [SerializeField, Header("�R���{�̃I�u�W�F�N�g")] GameObject JudgeObj;
//
//    // �f�o�b�O�p
//
//    // Start is called before the first frame update
//    void Awake()
//    {
//        Attackanim = GetComponent<Animator>();
//        comboValueDisplay = GameObject.Find("ComboValue").GetComponent<UIController_ComboValue>();
//        JudgeObj = GameObject.Find("JudgeObject");
//        // comboJudge = GameObject.Find("Judge").GetComponent<ComboJudge>();
//        targetObj = GameObject.Find("UICanvas");
//    }
//
//    // �U��
//    public void OnFire(InputAction.CallbackContext context)
//    {
//        // �����ꂽ�u�Ԃ���
//        if (!context.performed) return;
//
//        if(inputStopTime > 0f) return;
//
//        switch (GameManager.Instance.currentState.GetType().Name)
//        {
//            case "Idle":
//            case "Move":
//            case "Attack":
//
//                keyName(context.control.name);
//                if (!ScoreManager.Instance.specialFireFrag)
//                {
//                    // �U���̒i�K�ɂ���ď�����ύX����
//                    if (previousJudgment != ATTACK_PHASE.first)
//                    {
//                        comboJudge._Attack_Judge();
//                    }
//
//                    if (ScoreManager.Instance.AttackFrag || previousJudgment == ATTACK_PHASE.first)
//                    {
//                        switch (previousJudgment)
//                        {
//                            case ATTACK_PHASE.first:
//                                {
//                                    _Player_Attack();
//                                    _JudgeObj_Boot(ACTIVE_JUDGE.normal);
//                                    break;
//                                }
//                            case ATTACK_PHASE.second:
//                                {
//                                    _Player_Attack();
//                                    _JudgeObj_Boot(ACTIVE_JUDGE.normal);
//
//                                    break;
//                                }
//                            case ATTACK_PHASE.third:
//                                {
//                                    _Player_Attack();
//                                    _JudgeObj_Boot(ACTIVE_JUDGE.EX);
//                                    break;
//                                }
//                            case ATTACK_PHASE.fourth:
//                                {
//                                    _Player_EX_Attack();
//                                    previousJudgment = ATTACK_PHASE.first;
//                                    ScoreManager.Instance.AttackFrag = false;
//                                    break;
//                                }
//
//                        }
//                        timeCount = 0;
//                        attackflag = true;
//                    }
//                    else
//                    {
//                        _Phase_Reset();
//                    }
//
//                }
//                break;
//        }
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        if (inputStopTime > 0f)
//        {
//            inputStopTime -= 0.01f;
//        }
//        if (attackflag)
//        {
//            timeCount += Time.deltaTime;
//            if (timeCount > comboEndTime)
//            {
//                _Phase_Reset();
//            }
//        }
//    }
//
//    // �ʏ�U���̃A�j���[�V�����E�K�E�Z�Q�[�W�̑������s��
//    public void _Player_Attack()
//    {
//        if (!ScoreManager.Instance.specialFireFrag)
//        {
//            // �A�^�b�N�g���K�[���I���ɂ���
//            Attackanim.SetTrigger("Attack_trg");
//
//            // �e��Q�[�W�̑���
//            ++previousJudgment;
//            SoundManager.Instance.Play("�a��");
//            attackflag = false;
//            timeCount = 0;
//            ScoreManager.Instance.AttackFrag = false;
//
//        }
//        Debug.Log("�v���C���[�̍U�� : " + (previousJudgment - 1));
//
//    }
//
//    // �S�i�ڂ̍U���̃A�j���[�V�����E�K�E�Z�Q�[�W�̑������s��
//    public void _Player_EX_Attack()
//    {
//        if (!ScoreManager.Instance.specialFireFrag)
//        {
//            Attackanim.SetTrigger("EXAttack_trg");
//            SoundManager.Instance.Play("����U��");
//            attackflag = false;
//            timeCount = 0;
//            ScoreManager.Instance.AttackFrag = false;
//        }
//
//        Debug.Log("�v���C���[�̔h���U��");
//    }
//
//    // ������s���I�u�W�F�N�g���N��������
//    public void _Phase_Reset()
//    {
//        Attackanim.SetTrigger("AttackEnd_trg");
//        Attackanim.ResetTrigger("Attack_trg");
//        Attackanim.ResetTrigger("EXAttack_trg");
//        GameManager.Instance.KeyName = null;
//        previousJudgment = ATTACK_PHASE.first;
//        attackflag = false;
//        timeCount = 0;
//    }
//
//    // ������s���I�u�W�F�N�g���N��������
//    public void _JudgeObj_Boot(ACTIVE_JUDGE boot)
//    {
//        ScoreManager.Instance.AttackJudgeFrag = true;
//        switch (boot)
//        {
//            case ACTIVE_JUDGE.normal:
//                {
//                    comboJudge._Next_JudgeScale(comboJudge.AttackScale[0]); 
//                    break;
//                }
//            case ACTIVE_JUDGE.EX:
//                {
//                    comboJudge._Next_JudgeScale(comboJudge.AttackScale[1]);
//                    break;
//                }
//        }
//
//        JudgeObj.SetActive(true);
//    }
//}
