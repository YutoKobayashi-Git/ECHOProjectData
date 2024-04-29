using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JudgeTest : BaseObjectPool
{
    //変数の宣言
    [SerializeField, Header("判定のプレハブ")]
    private GameObject judgeObj;

    // プレイヤーに判定を伝えるゲームオブジェクト
    [SerializeField, Header("Perfect")]
    private RectTransform messageTrans;     

    Animator animator;
    GameObject[] notesObj;      // ノーツプレハブ
    int cnt = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // オブジェクト作成
        Create_Pool(5,"パーフェクト");
        Create_Pool(5,"ミス");
        Create_Pool(5,"スラッシュ");

        // 判定オブジェクト非表示
        judgeObj.SetActive(false);
        notesObj = GameObject.FindGameObjectsWithTag("Notes");
    }

    // InputActions
    public void OnFire(InputAction.CallbackContext context)
    {
        if (notesObj == null) return;
        // 押された瞬間だけ
        if (!context.performed) return;
        // 押された瞬間に判定のオブジェクトとノーツの距離を
        // 測ってそれに応じた判定のテキストを表示する
        for (int i = 0; i < notesObj.Length; ++i)
        {
            var dis = Vector3.Distance(judgeObj.transform.position, notesObj[i].transform.position);
            if (cnt % 2 == 0) return;
            //Debug.Log(cnt);
            //Debug.Log(dis);
            if (dis <= 400f)
            {
                cnt += 2;
                // オブジェクト表示または生成
                Get_Obj(messageTrans, "パーフェクト");

                // Sound
                SoundManager.Instance.Play("Zangeki");
                SoundManager.Instance.Play("Destruction");

                animator.SetTrigger("SP_trg");

                notesObj[i].SetActive(false);
                ScoreManager.Instance.perfect++;
                ScoreManager.Instance.perfectComboCnt++;
            }
            else if (dis <= 600f && dis > 450f)
            {
                cnt += 2;
                // オブジェクト表示または生成
                Get_Obj(messageTrans, "ミス");

                // Sound
                SoundManager.Instance.Play("Zangeki");

                notesObj[i].SetActive(false);
                ScoreManager.Instance.miss++;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // 存在しているアイテムの数を代入する
        notesObj = GameObject.FindGameObjectsWithTag("Notes");

        if (ScoreManager.Instance.specialFireFrag)
        {
            judgeObj.SetActive(true);
        }
        else
        {
            cnt = 1;
            SetObjActiveFalse("スラッシュ");
            SetObjActiveFalse("パーフェクト");
            judgeObj.SetActive(false);
        }
    }
}
