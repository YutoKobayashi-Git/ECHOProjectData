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
//    [SerializeField, Header("攻撃エフェクト用オブジェクト")] GameObject[] playerAttackEffect = new GameObject[2];
//    [SerializeField, Header("UIの位置取得用")] GameObject targetObj;
//    [SerializeField, Header("現在の攻撃段階を保存する変数")] ATTACK_PHASE previousJudgment;
//    [SerializeField, Header("攻撃可能かどうかを判定するオブジェクトのどちらのモードを起動する")] ACTIVE_JUDGE bootJudgment;
//    [SerializeField, Header("現在攻撃中かどうかを判定する変数")] bool attackflag;
//    [SerializeField, Header("立ちに戻る時間を保存する変数")] float comboEndTime;
//    [SerializeField, Header("時間計測用")] float timeCount;
//    [Header("入力停止時間")]public float inputStopTime;
//
//    private Animator Attackanim;
//    private PlayerAttackCollider attackCollider;
//    private UIController_ComboValue comboValueDisplay;
//    [SerializeField, Header("コンボのスクリプト")] ComboJudge comboJudge;
//    [SerializeField, Header("コンボのオブジェクト")] GameObject JudgeObj;
//
//    // デバッグ用
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
//    // 攻撃
//    public void OnFire(InputAction.CallbackContext context)
//    {
//        // 押された瞬間だけ
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
//                    // 攻撃の段階によって処理を変更する
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
//    // 通常攻撃のアニメーション・必殺技ゲージの増加を行う
//    public void _Player_Attack()
//    {
//        if (!ScoreManager.Instance.specialFireFrag)
//        {
//            // アタックトリガーをオンにする
//            Attackanim.SetTrigger("Attack_trg");
//
//            // 各種ゲージの増加
//            ++previousJudgment;
//            SoundManager.Instance.Play("斬撃");
//            attackflag = false;
//            timeCount = 0;
//            ScoreManager.Instance.AttackFrag = false;
//
//        }
//        Debug.Log("プレイヤーの攻撃 : " + (previousJudgment - 1));
//
//    }
//
//    // ４段目の攻撃のアニメーション・必殺技ゲージの増加を行う
//    public void _Player_EX_Attack()
//    {
//        if (!ScoreManager.Instance.specialFireFrag)
//        {
//            Attackanim.SetTrigger("EXAttack_trg");
//            SoundManager.Instance.Play("特殊攻撃");
//            attackflag = false;
//            timeCount = 0;
//            ScoreManager.Instance.AttackFrag = false;
//        }
//
//        Debug.Log("プレイヤーの派生攻撃");
//    }
//
//    // 判定を行うオブジェクトを起動させる
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
//    // 判定を行うオブジェクトを起動させる
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
