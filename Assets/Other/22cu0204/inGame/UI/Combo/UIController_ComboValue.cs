using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController_ComboValue : MonoBehaviour
{
    // 変数宣言
    [SerializeField, Header("コンボ用のテキスト")] 
    private TMPro.TMP_Text comboText;
    [SerializeField, Header("コンボの成功数")]
    private int comboNum;
    [SerializeField, Header("コンボの連続成功の最大値")]
    private int comboNumMax;
    [SerializeField, Header("コンボが続けることが可能な時間")]
    private float comboActiveTime;
    [SerializeField, Header("時間計測用")]
    private float comboCountTime;
    [Header("コンボの上限値")] 
    const int stopCombo = 10000;

    
    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントの取得・初期化
        comboText = this.GetComponent<TMP_Text>();
        comboText.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // コンボが続いているかどうかを判定し、コンボ終了時に初期化を行う
        if(comboNum !=0)
        {
            comboCountTime += Time.deltaTime;
        }
        if(comboCountTime > comboActiveTime)
        {
            comboText.alpha = 0f;
            comboCountTime = 0f;
            _Combo_failure();
            //Debug.Log("ComboTimer Reset");
        }

        // テキストとして表示
        comboText.SetText(comboNum.ToString());
    }

    // コンボを開始する処理（プレイヤー側から操作）
    public void _Combo_Success()
    {
        // コンボの上限値を設定
        if(comboNum < stopCombo)
        {
            comboText.alpha = 1f;
            // タイマーをリセットしコンボが続く限り表示する
            comboCountTime = 0f;
            ++comboNum;
        }
    }
    // コンボ連続成功の最大値の更新を行う処理
    void _Combo_failure()
    {
        if(comboNumMax <= comboNum)
        {
            comboNumMax = comboNum;
        }
        comboNum = 0;
    }
}
