using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_ComboJudgment : MonoBehaviour
{
    // 変数宣言
    [SerializeField, Header("キャンバスのRectTransform")]
    private RectTransform canvasRectTrans;
    [SerializeField, Header("追従先のオブジェクトのTransform")]
    private Transform playerTrans;
    [SerializeField, Header("オフセット（座標をずらす）")]
    private Vector3 offset = new Vector3(0f, 1.5f, 0f);

    // 自身のRectTransformコンポーネント
    private RectTransform myRectTrans;


    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントの取得
        myRectTrans = this.GetComponent<RectTransform>();
        if ((canvasRectTrans == null) || (playerTrans == null))
        {
            canvasRectTrans = GameObject.Find("UICanvas").GetComponent<RectTransform>();
            playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        // アタッチされたオブジェクトの座標をターゲットとなるオブジェクトのワールド座標から取得しUI上の座標に変更し代入を行う
        myRectTrans.position = RectTransformUtility.WorldToScreenPoint(Camera.main, playerTrans.position + offset);
    }
}
