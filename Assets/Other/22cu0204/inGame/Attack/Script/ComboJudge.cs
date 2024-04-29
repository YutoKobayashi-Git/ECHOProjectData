using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboJudge : MonoBehaviour
{
    // 変数宣言
    private RectTransform RectTrans;
    private GameObject parentObj;
    private GameObject canvasObj;
    private float TimeCount;

    [SerializeField, Header("現在のスケール")] Vector3 scale;
    [SerializeField, Header("攻撃時の判定のスケール")] public Vector3[] AttackScale;
    [SerializeField, Header("スケールの値の減少値")] float downScaleValue;
    [SerializeField, Header("成功にするスケール")] float successScaleValue;
    [SerializeField, Header("判定のスケールを小さくする時間の値")] float downScaleTime;
    [SerializeField, Header("成功時・失敗時に表示させるエフェクト")] GameObject[] judgeEffect;
    [SerializeField, Header("プレイヤーのオブジェクト")] PlayerFireSaka playerFire;

    // Start is called before the first frame update
    void Start()
    {
        // アタッチされたオブジェクトのコンポーネント等を取得する
        playerFire = GameObject.Find("Player").GetComponent<PlayerFireSaka>();
        parentObj = transform.parent.gameObject;
        canvasObj = parentObj.transform.parent.gameObject;
        RectTrans = this.GetComponent<RectTransform>();
        scale = RectTrans.localScale;
        parentObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Instance.AttackJudgeFrag)
        {
            // 時間の計測
            TimeCount += Time.deltaTime;

            if (TimeCount >= downScaleTime)
            {
                scale = new Vector3(scale.x - downScaleValue, scale.y - downScaleValue, 1f);
                RectTrans.localScale = scale;
                TimeCount = 0f;
            }

            // インゲーム内にある進行を管理するオブジェクトの変数によって処理を行うか判定
            // オブジェクトのスケールを元に戻し進行を管理するオブジェクトの変数を変化させる
            if (RectTrans.localScale.x <= 0f)
            {
                _Next_JudgeScale(AttackScale[0]);
                Instantiate(judgeEffect[1], parentObj.transform.position, Quaternion.identity, canvasObj.transform);
                playerFire._Phase_Reset();
                ScoreManager.Instance.AttackJudgeFrag = false;
                parentObj.gameObject.SetActive(false);
                TimeCount = 0f;
            }
        }
    }

    // 現在のスケールを判定し、2~N段目の攻撃を発生させるかを管理する変数を変更する処理
    public void _Attack_Judge()
    {
        if (RectTrans.localScale.x <= successScaleValue)
        {
            ScoreManager.Instance.AttackFrag = true;
            Instantiate(judgeEffect[0], parentObj.transform.position, Quaternion.identity, canvasObj.transform);
            Debug.Log("iiyo");
        }
        else
        {
            ScoreManager.Instance.AttackFrag = false;
            Instantiate(judgeEffect[1], parentObj.transform.position, Quaternion.identity, canvasObj.transform);
            playerFire.inputStopTime = 1f;
        }
        ScoreManager.Instance.AttackJudgeFrag = false;
        parentObj.gameObject.SetActive(false);
    }


    public void _Next_JudgeScale(Vector2 _NextScale)
    {
        scale = _NextScale;

        RectTrans.localScale = scale;
    }
}
