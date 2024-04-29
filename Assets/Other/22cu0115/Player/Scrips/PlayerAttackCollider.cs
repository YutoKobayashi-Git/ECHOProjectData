using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    UIController_ComboValue comboDisplay;

    private void Awake()
    {
        comboDisplay = GameObject.Find("ComboValue").GetComponent<UIController_ComboValue>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (ScoreManager.Instance.specialGauge < 11)
            {
                // ゲージ上昇
                ScoreManager.Instance.specialGauge++;
                // コンボ関数呼び出し
                comboDisplay._Combo_Success();

            }
        }
    }

    //protected override void _Gauge_Rising()
    //{
    //
    //}
}
