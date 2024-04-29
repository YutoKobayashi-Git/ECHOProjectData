using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    // 変数宣言
    [Header("ライフの最大値"), SerializeField]
    private int life_Max;
    [Header("ゲージのサイズ"), SerializeField]
    private Vector2 lifeGauge_Size;
    [Header("ゲージのトランスフォーム（座標等）"), SerializeField]
    private RectTransform lifeGauge_RectTransform;

    [Header("ライフの現在値")]
    public int Life;
    [Header("現在のゲージのサイズ倍率")]
    public float _total;

    // Start is called before the first frame update
    void Start()
    {
        // アタッチされた体力ゲージ用オブジェクトの座標等を取得
        lifeGauge_RectTransform = this.GetComponent<RectTransform>();
        lifeGauge_RectTransform.sizeDelta = lifeGauge_Size;
        Life = life_Max;
    }

    // Update is called once per frame
    void Update()
    {
    }
    // ダメージを受けた際のゲージの変更（プレイヤー側から呼び出し） 
    public void Life_Damage(int _Damege)
    {
        // 体力が残っている場合に処理を行う
        if(Life > 0)
        {
            // 管理しているlifeをダメージ分減らす
            Life -= _Damege;
            // 現在のゲージのサイズ倍率を計算
            _total = (float)Life / (float)life_Max;
            lifeGauge_RectTransform.sizeDelta = new Vector2(lifeGauge_Size.x * _total, lifeGauge_Size.y);
            lifeGauge_RectTransform.localPosition = new Vector3(lifeGauge_RectTransform.localPosition.x - (float)_Damege / 2f, lifeGauge_RectTransform.localPosition.y, lifeGauge_RectTransform.localPosition.z);
        }
    }

    // 現在のHP
    public int Get_Hp()
    {
        return Life;
    }
}
